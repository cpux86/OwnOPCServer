using Application.Interfaces;
using Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Application.Modeles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using static Application.Services.CounterService;

namespace Application
{
    public static class ServiceExtensions
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration config)
        {
            
            services.AddSingleton<IDevice, DeviceManager>();
            services.AddSingleton<ICounter, CounterService>();
            RegisterOptions(services, config);
        }

        private static void RegisterOptions(IServiceCollection services, IConfiguration config)
        {
            IConfigurationSection webAppInfoSection = config.GetSection("SerialPortSetting");
            services.Configure<Config>(webAppInfoSection);
        }

    }
}
