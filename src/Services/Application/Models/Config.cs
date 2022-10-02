using Application.Interfaces;
using Application.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Modeles
{
    public class Config : IConfig
    {
        public string Port { get; set; } = "COM1";
        public int BaudRate { get; set; } = 9600;

        /// <summary>
        ///  Тайм-аут на чтение/запись
        /// </summary>
        public int RWTimeout { get; set; } = 500;
        public List<OwnCounter> Counters { get; set; } = new List<OwnCounter>();
    }

}
