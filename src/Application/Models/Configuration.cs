using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Modeles
{
    public class Configuration : IConfiguration
    {
        public string PortName { get; set; }
        public int BaudRate { get; set; }
    }
}
