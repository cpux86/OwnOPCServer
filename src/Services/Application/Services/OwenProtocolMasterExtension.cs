using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public static string OwenReadCEU(this IOwenProtocolMaster protocolMaster, int address, AddressLengthType addressLength = AddressLengthType.Bits8)
        {
            // получаю положение десятичной точки
            var decPoint = protocolMaster.OwenRead(address, AddressLengthType.Bits8, "dP");

            // получаю массив байт содержащий значение счетчика в физических единицах
            var ceu = protocolMaster.OwenRead(address, AddressLengthType.Bits8, "CEU");

            // lock счетчик
            protocolMaster.OwenWrite(address, AddressLengthType.Bits8, "LoCK", new byte[] { 0x0, 0x2 });

            // конвертируем результат запроса в читаемый вид, показаний счетчика
            var mantissa = new ConverterU().ConvertBack(ceu);

            // преобразуем мантиссу в число с плавающей точкой
            var result = ((float)new FixedPoint(mantissa, decPoint[1], false));

            return result.ToString();
        }


        public static async Task<string> OwenResetCEU(this IOwenProtocolMaster protocolMaster, int address, AddressLengthType addressLength = AddressLengthType.Bits8)
        {
            // получаю положение десятичной точки
            var decPoint = protocolMaster.OwenRead(address, AddressLengthType.Bits8, "dP");

            // сбрасываю счетчик
            protocolMaster.OwenWrite(address, AddressLengthType.Bits8, "rStC", new byte[] { 0x1 });

            // блокировка кнопок 
            protocolMaster.OwenWrite(address, AddressLengthType.Bits8, "LoCK", new byte[] {0x0, 0x3 });

            // получаю массив байт содержащий значение счетчика в физических единицах
            var ceu = protocolMaster.OwenRead(address, AddressLengthType.Bits8, "CEU");

            // конвертируем результат запроса в читаемый вид, показаний счетчика
            var mantissa = new ConverterU().ConvertBack(ceu);

            // преобразуем мантиссу в число с плавающей точкой
            var result = ((float)new FixedPoint(mantissa, decPoint[1], false));

            return await Task.FromResult(result.ToString());
        }

    }
}
