using TempArAn.Domain.AbstractCore;

namespace TempArAn.Domain.Models.Record
{

    public abstract class SimpleRecordBase<TValue> : RecordBase
    {
        public TValue Value { get; set; }
        public SimpleRecordBase(Guid sourceId, DateTime dateTime, TValue value)
        {
            SourceId = sourceId;
            Value = value;
            DateTime = dateTime;
        }
    }
}
