using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TempAnAr.Persistence.Context;
using TempAnAr.Persistence.Interfaces;
using TempArAn.Domain.AbstractCore;
using TempArAn.Domain.Exceptions.ApplicationExceptions;
using TempArAn.Domain.Models.Source;

namespace TempAnAr.Persistence.Base
{
    public abstract class SourceRepository<TSource> : ISourceRepository where TSource : SourceBase
    {

        protected readonly ApplicationContext _context;
        protected readonly DbSet<TSource> _sources;
        public SourceRepository(ApplicationContext context, DbSet<TSource> sources)
        {
            _context = context;
            _sources = sources;
        }
        public async Task DeleteSourceAsync(Guid sourceId)
        {
            var sourceForDelete = await _sources.
                FirstOrDefaultAsync(x => x.Id == sourceId) ??
                throw new NotFoundException("Not found source by id");
            _sources.Remove(sourceForDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SourceBase>> GetSourcesForRecordAsync()
        {
            var now = DateTime.Now;
            return await _sources.AsNoTracking().
                Where(x => x.IsRecording && x.NextRecord <= now).
                Select(x => x as SourceBase).ToListAsync();
        }

        public async Task<SourceBase> GetSourceAsync(Guid guid)
        {
            return await _sources.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == guid) ??
                    throw new NotFoundException("Not foubd source by id"); ;
        }

        public async Task<SourcesSet> GetSoursesAsync(IUser? user)
        {
            var rawList = await _sources
                .Where(s => user != null && s.UserId == user.Id)
                .Select(x => x as SourceBase).ToListAsync();
            var userList = user != null ? rawList
                .Where(s => s.UserId == user.Id).ToList() : new List<SourceBase>();
            var publicList = rawList
                .Where(s => user != null && s.UserId != user.Id).ToList();
            return new SourcesSet(
                userList,
                publicList);
        }

        public async Task PostSourceAsync(SourceBase source)
        {
            await _sources.AddAsync(source as TSource ??
                throw new ArgumentException($"must be {nameof(TSource)} but was {nameof(source)}"));
            await _context.SaveChangesAsync();
        }

        public async Task PutSoursesAsync(IEnumerable<SourceBase> sourcesToAdd)
        {
            _sources.UpdateRange(sourcesToAdd.
                Select(x => x as TSource ??
                throw new ArgumentException($"must be {nameof(TSource)} but was {nameof(x)}")).
                ToList());
            await _context.SaveChangesAsync();
        }

        public async Task SetRecordingStateAsync(Guid id, bool value)
        {
            var source = _sources.
                FirstOrDefault(x => x.Id == id) ?? 
                throw new NotFoundException("Not found source by id");
            source.SetRecordingState(value);
            _sources.Update(source);
            await _context.SaveChangesAsync();
        }
    }
}
