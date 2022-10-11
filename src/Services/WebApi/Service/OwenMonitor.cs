using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;

namespace WebApi.Service
{
    public class OwenMonitor : BackgroundService
    {
        public static string value;
        public ILogger Logger { get; }
        private readonly ICounter _counter;

        public OwenMonitor(ILoggerFactory loggerFactory, ICounter counter)
        {
            Logger = loggerFactory.CreateLogger<OwenMonitor>();
            _counter = counter;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //Logger.LogInformation("ServiceA is starting.");
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    value = await _counter.GetCurrentMeterReadings(3);
                    Console.WriteLine(String.Format("{0} - {1}", value, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                    
                    throw new Exception("не удалось получить данные со счетчика");
                    
                }
                catch (Exception e)
                {
                    Logger.LogWarning(e.Message);
                }
                await Task.Delay(TimeSpan.FromMilliseconds(1000), stoppingToken);
            }
        }
    }
}
