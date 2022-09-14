﻿using System.IO.Ports;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OwenioNet;
using OwenioNet.DataConverter.Converter;
using OwenioNet.Types;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ParametrsController : ControllerBase
    {
        [HttpGet]
        public int GetPulsesValue()
        {
            var port = new SerialPort("COM6", 9600, Parity.None, 6, StopBits.One);
            using (var owenProtocol = OwenProtocolMaster
                       .Create(port))
            {

                port.Open();



                byte[] res;

                res = owenProtocol.OwenRead(3, AddressLengthType.Bits8, "Ctr");
                var conv = new ConverterDecDotS();
                var r = conv.ConvertBack(res);

                var data = Encoding.UTF8.GetBytes(r.ToString());
                return (int)r;
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