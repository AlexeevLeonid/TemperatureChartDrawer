

using TemperatureChartDrawer.src.Recorder;
using TemperatureChartDrawer.src.Sourse;

namespace TemperatureChartDrawer.src.Database.Interfaces
{
    public interface IRepository<TSource>
        where TSource : SourceBase
    {
        public Task PostSourse(TSource sourse);
        public Task<TSource> GetSourse(Guid guid);
        public Task DeleteSourse(Guid guid);
        public Task<List<TSource>> GetSourses();
        public Task PutSourses(List<TSource> sources);
        public Task PostRecord(Record record);
        public Task<List<ShortRecord>> GetRecordsFromSourse(Guid guid);
    }
}
