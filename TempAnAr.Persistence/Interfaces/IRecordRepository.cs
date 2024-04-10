using TempArAn.Domain.AbstractCore;

namespace TempAnAr.Persistence.Interfaces
{
    public interface IRecordRepository<TRecord> where TRecord : RecordBase
    {
        public Task PostRecordsAsync(IEnumerable<TRecord> record);
        public Task<IEnumerable<TRecord>> GetRecordsFromSourseAsync(Guid guid);
        public Task<IEnumerable<TRecord>> GetRecordsFromSourceForTimeAsync(
            Guid guid, DateTime begin, DateTime end, bool isTracking = false);
        public Task<IEnumerable<TRecord>> GetRecordsForTimeAsync(
            DateTime begin, DateTime end, bool isTracking = false);
        public Task DeleteRecordsAsync(IEnumerable<TRecord> records);
    }
}
