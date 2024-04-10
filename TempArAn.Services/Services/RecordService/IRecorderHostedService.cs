using Microsoft.Extensions.Hosting;

namespace TempArAn.Services.Services.RecordService
{
    public interface IRecorderHostedService : IHostedService
    {
        void InitRecordingAsync(object? state);
    }
}