using Microsoft.Extensions.Hosting;

namespace TempArAn.Services.Services.ConvertService
{
    public interface IRecordConverterHostedService : IHostedService
    {
        void InitConvertingAsync(object? state);
    }
}