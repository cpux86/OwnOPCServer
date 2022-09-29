using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OwenioNet;
using OwenioNet.DataConverter.Converter;
using OwenioNet.DataConverter.Types;
using OwenioNet.Types;

namespace Application.Services
{
    public static class OwenProtocolMasterExtension
    {

        /// <summary>
        /// Возвращает текущие показания счетчика
        /// </summary>
        /// <param name="protocolMaster"></param>
        /// <param name="address"></param>
        /// <param name="addressLength"></param>
        /// <returns></returns>
        public static async Task<string> OwenReadCEU(this IOwenProtocolMaster protocolMaster, int address, AddressLengthType addressLength = AddressLengthType.Bits8)
        {
            // получаю положение десятичной точки
            var dp = protocolMaster.OwenRead(address, AddressLengthType.Bits8, "dP");

            // получаю массив байт содержащий заначение счетчика в физических еденицах
            var ceu = protocolMaster.OwenRead(address, AddressLengthType.Bits8, "CEU");

            // конвертируем результат запроса в читаемый вид, показаний счетчика
            var mantissa = new ConverterU().ConvertBack(ceu);

            // преобразуем мантиссу в число с плавающей точкой
            var result = ((float)new FixedPoint(mantissa, dp[1], false));

            return await Task.FromResult(result.ToString());
        }

    }
}
