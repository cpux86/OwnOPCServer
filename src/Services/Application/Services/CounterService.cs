using Application.Common.Exceptions;
using Application.Interfaces;
using OwenioNet.DataConverter.Converter;
using OwenioNet.Types;
using OwenioNet;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using OwenioNet.IO;

namespace Application.Services
{

    public class CounterService : ICounter
    {
        /// <summary>
        /// Возвращает текущее показания счетчика 
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetCurrentMeterReadings()
        {
            var port = "com3";
            var baudRate = 9600;
            var addr = 3;

            var serialPort = new SerialPort(port, baudRate, Parity.None, 6, StopBits.One);
            //var serialPort = new SerialPort();
            using (var owenProtocol = OwenProtocolMaster
                       .Create(serialPort))
            {

                try
                {
                    if (!serialPort.IsOpen)
                        serialPort.Open();
                }
                catch (Exception e)
                {
                    throw new OpenPortFiledException(e.Message);
                }

                owenProtocol.Transport.ReadTimeout = 1000;

                return await owenProtocol.OwenReadCEU(addr);
            }
        }
    }
}
