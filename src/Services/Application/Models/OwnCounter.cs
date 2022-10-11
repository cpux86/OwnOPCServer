using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class OwnCounter
    {
        /// <summary>
        /// Название машины со счетчиком
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Адрес счетчика
        /// </summary>
        public int Address { get; set; }
    }
}
