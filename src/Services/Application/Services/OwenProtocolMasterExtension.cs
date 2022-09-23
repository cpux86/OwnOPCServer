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
        public static async Task<double> OwenReadCEU(this IOwenProtocolMaster protocolMaster, int address, AddressLengthType addressLength = AddressLengthType.Bits8)
        {
            byte[] response = null;
            response = protocolMaster.OwenRead(address, AddressLengthType.Bits8, "CEU");

            // конвертируем результат запроса в читаемый вид, показаний счетчика
            var meterReadings = new ConverterDecDotS().ConvertBack(response).AsDouble();
            var res = Convert.ToSingle(meterReadings);
            return await Task.FromResult(Convert.ToSingle(meterReadings));
        }

    }
}
