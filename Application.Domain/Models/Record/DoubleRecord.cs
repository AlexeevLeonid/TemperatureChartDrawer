namespace TempArAn.Domain.Models.Record
{
    public class DoubleRecord : SimpleRecordBase<double>
    {
        public DoubleRecord(Guid sourceId, DateTime dateTime, double value) : base(sourceId, dateTime, value)
        {
        }
    }
}
