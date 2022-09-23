using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.Interfaces;
using System.Text;
using Microsoft.Extensions.Configuration;
using WebApi.Wrappers;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        readonly IDevice _device;
        
        IConfiguration _configuration;
        public ConfigController(IDevice device, IConfiguration configuration)
        {
            _configuration = configuration;
            _device = device ?? throw new ArgumentNullException(nameof(device));
        }

        [HttpGet]
        public async Task<Response<IEnumerable<string>>> Get()
        {
            var portNames = await _device.GetComPortNames();
            if (!portNames.Any()) throw new Exception("port not fount");
            return new Response<IEnumerable<string>>(portNames); 

        }

        [HttpPost]
        public List<string> GetPorts()
        {
            var div = _device.ComPorts;
            var com = div.ToList<string>();
            return com;
        }
    }
}
