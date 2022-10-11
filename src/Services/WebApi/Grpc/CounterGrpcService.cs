using CounterApi;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace WebApi.Grpc
{
    public class CounterGrpcService : Counter.CounterBase
    {

        // public override Task<MeterReadingsResponce> GetMeterReadingsByAddressCounter(CounterOptionsRequest request, ServerCallContext context)
        // {
        //     return Task.FromResult(new MeterReadingsResponce {
        //         Value = "Ok"
        //     });
        // }
        public override async Task GetMeterReadingsByAddressCounter(CounterOptionsRequest request, IServerStreamWriter<MeterReadingsResponce> responseStream, ServerCallContext context)
        {
            for (int i = 0; i < 10000; i++)
            {
                try
                {
                    await responseStream.WriteAsync(new MeterReadingsResponce { Value = Service.ServiceA.value });

                    await Task.Delay(10);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                }
                
            }

            


            //return base.GetMeterReadingsByAddressCounter(request, responseStream, context);
        }
    }
}
