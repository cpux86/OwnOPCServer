using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.Interfaces;
using System.Text;
using Microsoft.Extensions.Configuration;

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
        public List<string> Get()
        {
            var div = _device.GetComPortNames();
            var com = div.ToList<string>();
            return com;
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
