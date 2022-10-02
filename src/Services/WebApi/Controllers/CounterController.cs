using Application.Interfaces;
using Application.Modeles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OwenioNet.DataConverter.Converter;
using OwenioNet.DataConverter.Types;

namespace WebApi.Controllers
{
    //[Route("api/[controller]/[action]")]
    [ApiController]
    public class CounterController : ControllerBase
    {
        private readonly ICounter _counter;
        // private  readonly IConfig _config;

        //public CounterController(ICounter counter, IConfig config)
        //{
        //    _counter = counter;
        //    _config = config;
        //}
        public CounterController(ICounter counter)
        {
            _counter = counter;
        }

        [HttpGet()]
        //[Route("{car}/meter-readings")]
        [Route("meter-readings/{address:int}")]
        //[Authorize]
        public async Task<string> GetCurrentMeterReadings(int address)
        {
            return await _counter.GetCurrentMeterReadings(address);
        }
    }
}
