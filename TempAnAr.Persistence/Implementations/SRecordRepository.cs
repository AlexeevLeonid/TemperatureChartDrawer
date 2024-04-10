using AutoMapper;
using TempAnAr.Persistence.Base;
using TempAnAr.Persistence.Context;
using TempArAn.Domain.Models.Record;

namespace TempAnAr.Persistence.Implementations
{
    public class DoubleRecordRepository : RecordBaseRepository<DoubleRecord>
    {

        public DoubleRecordRepository(ApplicationContext context) : base(context, context.DoubleRecords)
        {
        }
    }
}
