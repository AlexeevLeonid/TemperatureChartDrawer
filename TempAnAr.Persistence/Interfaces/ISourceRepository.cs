using System.Collections.Generic;
using TempArAn.Domain.AbstractCore;
using TempArAn.Domain.Models.Source;

namespace TempAnAr.Persistence.Interfaces
{
    public interface ISourceRepository
    {
        public Task PostSourceAsync(SourceBase sourse);
        public Task<SourceBase> GetSourceAsync(Guid id);
        public Task DeleteSourceAsync(Guid id);
        public Task<SourcesSet> GetSoursesAsync(IUser? user);
        public Task<IEnumerable<SourceBase>> GetSourcesForRecordAsync();
        public Task PutSoursesAsync(IEnumerable<SourceBase> sources);
        public Task SetRecordingStateAsync(Guid id, bool value);
    }
}
