using AutoMapper;
using TempAnAr.Persistence.Base;
using TempAnAr.Persistence.Context;
using TempArAn.Domain.Models.Record;

namespace TempAnAr.Persistence.Implementations
{
    public class SourceErrorRecordRepository : RecordBaseRepository<SourceErrorRecord>
    {
        public SourceErrorRecordRepository(
            ApplicationContext context) :
            base(context, context.ErrorRecords)
        {
        }
    }
}
