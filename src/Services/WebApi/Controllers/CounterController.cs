using Application.Interfaces;
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
        [Route("meter-readings/{car}/{id:int?}")]
        //[Authorize]
        public async Task<string> GetCurrentMeterReadings(string car, int id = 0)
        {
            return await _counter.GetCurrentMeterReadings();
        }
    }
}
