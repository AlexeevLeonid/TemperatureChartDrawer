using TempArAn.Domain.AbstractCore;

namespace TempArAn.Domain.Models.Record
{
    public class TemperatureDataSetRecords : RecordBase
    {
        public double Median { get; set; }
        public double Mean { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public TemperatureDataSetRecords(Guid sourceId, DateTime dateTime, TemperatureDataSet value)
        {
            SourceId = sourceId;
            DateTime = dateTime;
            Mean = value.Mean;
            Median = value.Median;
            Min = value.Min;
            Max = value.Max;
        }
        public TemperatureDataSetRecords(Guid sourceId, DateTime dateTime,
            double min, double max, double mean, double median)
        {
            SourceId = sourceId;
            DateTime = dateTime;
            Mean = mean;
            Median = median;
            Min = min;
            Max = max;
        }

    }
}
