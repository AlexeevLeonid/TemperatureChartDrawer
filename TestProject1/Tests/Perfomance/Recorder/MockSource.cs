using TempArAn.Domain.AbstractCore;
using TempArAn.Domain.Models.Record;

namespace TempArAn.Tests.Tests.Perfomance.Recorder
{
    public class MockSource : SourceBase
    {
        public MockSource(string name, int interval, Guid userId) : base(name, interval, userId)
        {
        }

        protected override RecordBase GetValue()
        {
            var tack = DoWork().Result;
            return new DoubleRecord(Id, DateTime.Now, 0);
        }

        public async Task<int> DoWork()
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            return 0;
        }
    }
}
