using Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using OwenioNet;
using OwenioNet.DataConverter.Converter;
using OwenioNet.Types;
using System.IO.Ports;
using System.Text;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ParametrsController : ControllerBase
    {

        [HttpGet]
        public async Task<int> GetPulsesValue(int addr, string port = "com6", int baudRate = 9600)
        {
            
            var serialPort = new SerialPort(port, baudRate, Parity.None, 6, StopBits.One);
            serialPort.Open();

            using (var owenProtocol = OwenProtocolMaster
                       .Create(serialPort))
            {
                try
                {
                    if (!serialPort.IsOpen) serialPort.Open();
                }
                catch (Exception e)
                {
                    throw new OpenPortFiledException(e.Message);
                }

                
                var response = owenProtocol.OwenRead(addr, AddressLengthType.Bits8, "Ctr");
                // конвертируем результат запроса в читаемый вид, показаний счетчика
                var meterReadings = new ConverterDecDotS().ConvertBack(response);
                
                //var data = Encoding.UTF8.GetBytes(r.ToString());
                serialPort.Close();

                owenProtocol.CloseSerialPort();
                return (int)meterReadings;
            }

        }


        [HttpGet]
        public int GetCounterValue()
        {
            var port = new SerialPort("COM6", 9600, Parity.None, 6, StopBits.One);
            using (var owenProtocol = OwenProtocolMaster
                       .Create(port))
            {

                port.Open();



                byte[] res;

                res = owenProtocol.OwenRead(3, AddressLengthType.Bits8, "CEU");
                var conv = new ConverterDecDotS();
                var r = conv.ConvertBack(res);
                
                var data = Encoding.UTF8.GetBytes(r.ToString());
                return (int)r;
            }
        }
        [HttpGet]
        public void ResetCounter()
        {
            var port = new SerialPort("COM6", 9600, Parity.None, 6, StopBits.One);
            using (var owenProtocol = OwenProtocolMaster
                       .Create(port))
            {
                port.Open();
                owenProtocol.OwenWrite(3, AddressLengthType.Bits8, "rStC", new byte[] { 0x1 });
            }
        }

    }
}
