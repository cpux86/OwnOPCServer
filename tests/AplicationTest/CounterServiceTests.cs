using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Modeles;
using Application.Services;
using Microsoft.Extensions.Options;
using Moq;
using Xunit.Abstractions;

namespace AplicationTest
{
    public class CounterServiceTests
    {
        private readonly ITestOutputHelper output;
        public CounterServiceTests(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Theory]
        [InlineData("COM8",3)]
        [InlineData("COM8", 4)]
        [InlineData("COM9", 4)]
        public async void GetCurrentMeterReadingsTest(string port, int address)
        {
            
            var mock = new Mock<IOptionsMonitor<Config>>();
            mock.Setup(config => config.CurrentValue)
                .Returns(new Config()
                {
                    Port = port
                });
            var service = new CounterService(mock.Object);
            var res =  await service.GetCurrentMeterReadings(address);
            output.WriteLine(res);
        }
    }
}
