namespace TempArAn.Domain.AbstractCore
{
    public abstract class RecordBase
    {
        public Guid SourceId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
