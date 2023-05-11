namespace TemperatureChartDrawer.src.Recorder
{
    public class Record
    {
        public Guid Id { get; set; }
        public Guid SourceId { get; set; }
        public string Value { get; set; }
        public DateTime DateTime { get; set; }

        public ShortRecord ToShortRecord() => new ShortRecord($"{DateTime.Day} {DateTime.Hour}:{DateTime.Minute}", Value);

        public Record(Guid sourceId, string value, DateTime dateTime)
        {
            Id = new Guid();
            SourceId = sourceId;
            Value = value;
            DateTime = dateTime;
        }
    }

    public struct ShortRecord 
    {
        public string Date { get; set; }
        public string Value { get; set; }

        public ShortRecord(string date, string value)
        {
            Date = date;
            Value = value;
        }
    };
}
