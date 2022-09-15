using Application.Interfaces;
using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Modeles
{
    public class Configuration : IConfiguration
    {
        public string PortName { get; set; } = string.Empty;
        public int BaudRate { get; set; }
        public List<OwnCounter> Counters { get; set; } = new List<OwnCounter>();
    }
}
