using Grpc.Core;
using MeterServer;

namespace Server.Services
{
    public class MeterGrpcService : Meter.MeterBase
    {
        public override Task<MetlerReadingResponce> Save(MetlerReadingRequest request, ServerCallContext context)
        {
            return Task.FromResult(new MetlerReadingResponce
            {
                Status = "Ok"
            });
        }
    }
}
