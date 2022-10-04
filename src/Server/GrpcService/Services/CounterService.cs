using Grpc.Core;
using static GrpcService.Recorder;

namespace GrpcService.Services
{
    public class RecordService : RecorderBase
    {

        public override Task<MeterReadingsList> Save(MeterReadings request, ServerCallContext context)
        {
            IList<StatusResponce> list = new List<StatusResponce>();
            list.Add(new StatusResponce
            {
                Status = "Okfffffffffffffffffffffffffff"
            });
            list.Add(new StatusResponce
            {
                Status = "111111111111111111111111111"
            });
            var result = new MeterReadingsList();
            result.Status.AddRange(list);
            return Task.FromResult(result);
        }
    }
}
