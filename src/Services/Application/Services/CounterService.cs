using Application.Common.Exceptions;
using Application.Interfaces;
using OwenioNet.DataConverter.Converter;
using OwenioNet.Types;
using OwenioNet;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Application.Modeles;
using OwenioNet.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

//using Microsoft.Extensions.Options;

namespace Application.Services
{

    public class CounterService : ICounter
    {
        private readonly IOptionsMonitor<Config> _options;

        public CounterService(IOptionsMonitor<Config> options)
        {
            _options = options;
            _options.OnChange(evert =>
            {
                Console.WriteLine("конфигурация изменена");
            });
            
        }

        /// <summary>
        /// Возвращает текущее показания счетчика 
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetCurrentMeterReadings(int addr)
        {
            var config = _options.CurrentValue;

            var serialPort = new SerialPort(config.Port, config.BaudRate, Parity.None, 6, StopBits.One);

            using (var owenProtocol = OwenProtocolMaster
                       .Create(serialPort))
            {

                try
                {
                    serialPort.WriteTimeout = config.RWTimeout;
                    serialPort.WriteTimeout = config.RWTimeout;

                    if (!serialPort.IsOpen) serialPort.Open();
                }
                catch (Exception e)
                {
                    throw new OpenPortFiledException(e.Message);
                }
                // время ожидания ответа от счетчика
                owenProtocol.Transport.ReadTimeout = 1000;
                owenProtocol.Transport.WriteTimeout = 1000;

                return await owenProtocol.OwenReadCEU(addr);
            }
        }

        /// <summary>
        /// Возвращает текущее показания счетчика 
        /// </summary>
        /// <returns></returns>
        public async Task<string> ResetAndGetCurrentMeterReadings(int addr)
        {
            var config = _options.CurrentValue;

            var serialPort = new SerialPort(config.Port, config.BaudRate, Parity.None, 6, StopBits.One);

            using (var owenProtocol = OwenProtocolMaster
                       .Create(serialPort))
            {

                try
                {
                    serialPort.WriteTimeout = config.RWTimeout;
                    serialPort.WriteTimeout = config.RWTimeout;

                    if (!serialPort.IsOpen) serialPort.Open();
                }
                catch (Exception e)
                {
                    throw new OpenPortFiledException(e.Message);
                }
                // время ожидания ответа от счетчика
                owenProtocol.Transport.ReadTimeout = 1000;
                owenProtocol.Transport.WriteTimeout = 1000;

                return await owenProtocol.OwenResetCEU(addr);
            }
        }
    }
}
