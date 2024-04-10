using AutoMapper;
using TempAnAr.Persistence.Base;
using TempAnAr.Persistence.Context;
using TempArAn.Domain.Models.Record;

namespace TempAnAr.Persistence.Implementations
{
    public class DataRecordRepository : RecordBaseRepository<TemperatureDataSetRecords>
    {
        public DataRecordRepository(ApplicationContext context) :
            base(context, context.DataRecords)
        { }
    }
}
