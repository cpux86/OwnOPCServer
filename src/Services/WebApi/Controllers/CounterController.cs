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

        public CounterController(ICounter counter)
        {
            _counter = counter;
        }

        [HttpGet()]
        //[Route("{car}/meter-readings")]
        //[Route("raw/meter-readings/{address:int}")]
        //[Authorize]
        [Route("raw/{address:int}")]
        public async Task<string> GetCurrentMeterReadings(int address)
        {
            return await _counter.GetCurrentMeterReadings(address);
        }

        [HttpGet()]
        //[Route("{car}/meter-readings")]
        [Route("raw/{address:int}/reset")]
        //[Authorize]
        public async Task<string> ResetAndGetCurrentMeterReadings(int address)
        {

            return await _counter.ResetAndGetCurrentMeterReadings(address);
        }
    }
}
