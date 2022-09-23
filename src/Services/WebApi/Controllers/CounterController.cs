using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OwenioNet.DataConverter.Converter;
using OwenioNet.DataConverter.Types;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CounterController : ControllerBase
    {
        private readonly ICounter _counter;

        public CounterController(ICounter counter)
        {
            _counter = counter;
        }

        [HttpGet]
        [Authorize]
        public async Task<double> GetCurrentMeterReadings()
        {
            FixedPoint t = new FixedPoint(25150123, 5, false);
            var slf = (float)t;
            var b = new ConverterFloat().Convert(slf);
            var r = new ConverterFloat().ConvertBack(b);
            var r1 = (double)r;
            return await _counter.GetCurrentMeterReadings();
        }
    }
}
