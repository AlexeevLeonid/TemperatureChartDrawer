using TempArAn.Domain.Models.Record;

namespace TempAnAr.Persistence.Interfaces
{
    public interface IApplicationUnitOfWork
    {
        ISourceRepository Sources { get; }
        IRecordRepository<DoubleRecord> DoubleRecords { get; }
        IRecordRepository<TemperatureDataSetRecords> DataRecords { get; }
        IRecordRepository<SourceErrorRecord> ErrorRecords { get; }
        IUserRepository Users { get; }

        public void Save();
    }
}