using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICounter
    {
        /// <summary>
        /// Возвращает текущее значение счетчика
        /// </summary>
        /// <returns></returns>
        public Task<string> GetCurrentMeterReadings(int address);
    }
}
