using System.Text.Json.Serialization;

namespace TemperatureChartDrawer.src.Sourse
{
    public abstract class SourceBase
    {

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public DateTime LastRecord { get; private set; }
        public int Interval { get; private set; }
        [JsonIgnore]
        public TimeSpan IntervalAsTimeSpan { get; private set; }

        private string Next()
        {
            LastRecord = DateTime.Now;
            return GetValue();
        }

        /// <summary>
        /// implement value getting
        /// </summary>
        /// <returns></returns>
        protected abstract string GetValue();


        private bool IsRecordRequired()
        {
            return DateTime.Now.Subtract(LastRecord) >= IntervalAsTimeSpan;
        }

        /// <summary>
        /// returns a value if needed, otherwise returns null
        /// </summary>
        /// <returns></returns>
        public string? TryRecording()
        {
            if (IsRecordRequired())
                return Next();
            return null;
        }
        public SourceBase(string name, int interval)
        {
            Name = name;
            IntervalAsTimeSpan = TimeSpan.FromMinutes(interval);
            Interval = interval;
            Id = new Guid();
            LastRecord = DateTime.MinValue;
        }
    }
}
