using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;

namespace WebApi.Service
{
    public class ServiceA : BackgroundService
    {
        public static string value;
        public ILogger Logger { get; }
        private readonly ICounter _counter;

        public ServiceA(ILoggerFactory loggerFactory, ICounter counter)
        {
            Logger = loggerFactory.CreateLogger<ServiceA>();
            _counter = counter;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //Logger.LogInformation("ServiceA is starting.");
            while (!stoppingToken.IsCancellationRequested)
            {
                value = await _counter.GetCurrentMeterReadings(3);
                //Logger.LogInformation(await _counter.GetCurrentMeterReadings(3));
                Console.WriteLine(String.Format("{0} - {1}",value , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));

                await Task.Delay(TimeSpan.FromMilliseconds(10), stoppingToken);
            }
        }
    }
}
