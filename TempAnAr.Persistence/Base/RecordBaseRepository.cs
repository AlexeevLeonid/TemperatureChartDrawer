using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TempAnAr.Persistence.Context;
using TempAnAr.Persistence.Interfaces;
using TempArAn.Domain.AbstractCore;

namespace TempAnAr.Persistence.Base
{
    public abstract class RecordBaseRepository<TRecord> : IRecordRepository<TRecord> where TRecord : RecordBase
    {
        protected readonly ApplicationContext context;
        protected DbSet<TRecord> records;
        public RecordBaseRepository(ApplicationContext context, DbSet<TRecord> records)
        {
            this.context = context;
            this.records = records;
        }

        public async Task DeleteRecordsAsync(IEnumerable<TRecord> recordsToDelete)
        {
            var trecordsToDelete = recordsToDelete;
            records.RemoveRange(trecordsToDelete);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TRecord>> GetRecordsFromSourseAsync(Guid guid)
        {
            return await records.AsNoTracking()
                        .Where(x => x.SourceId == guid)
                        .ToListAsync();
        }

        public async Task<IEnumerable<TRecord>> GetRecordsFromSourceForTimeAsync(
            Guid guid, DateTime begin, DateTime end, bool isTracking = true)
        {
            IQueryable<TRecord> query = isTracking ? records : records.AsNoTracking();
            return await query.Where(x => x.SourceId == guid && x.DateTime >= begin && x.DateTime <= end).ToListAsync();
        }

        public async Task<IEnumerable<TRecord>> GetRecordsForTimeAsync(
            DateTime begin, DateTime end, bool isTracking = true)
        {
            IQueryable<TRecord> query = isTracking ? records : records.AsNoTracking();
            return await query.Where(x => x.DateTime >= begin && x.DateTime <= end).ToListAsync();
        }


        public async Task PostRecordsAsync(IEnumerable<TRecord> record)
        {
            await records.AddRangeAsync(record);
            await context.SaveChangesAsync();
        }
    }
}
