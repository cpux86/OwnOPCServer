using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DeviceManager : IDevice
    {
        public string[] ComPorts { get; set; }
        public string[] GetComPortNames()
        {
            string[] ports = SerialPort.GetPortNames();
            ComPorts = ports;
            return ports;
        }
    }
}
