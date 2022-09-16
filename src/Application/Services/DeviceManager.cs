using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Exceptions;

namespace Application.Services
{
    public class DeviceManager : IDevice
    {
        public string[] ComPorts { get; set; }
        public string[] GetComPortNames()
        {
            //throw new OpenPortFiledException("Не удалось открыть порт");
            string[] ports = SerialPort.GetPortNames();
            ComPorts = ports;
            return ports;
        }
    }
}
