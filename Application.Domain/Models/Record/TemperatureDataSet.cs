namespace TempArAn.Domain.Models.Record
{
    public struct TemperatureDataSet
    {
        public double Median { get; set; }
        public double Mean { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public TemperatureDataSet(double Median, double Mean, double Min, double Max)
        {
            this.Median = Median;
            this.Mean = Mean;
            this.Min = Min;
            this.Max = Max;
        }
    }
}
