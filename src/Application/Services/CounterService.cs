using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{

    public class CounterService : ICounter
    {
        /// <summary>
        /// Возвращает текущее значение счетчика материала
        /// </summary>
        /// <returns></returns>
        public int GetCurrentCounterValue()
        {
            return 1;
        }
    }
}
