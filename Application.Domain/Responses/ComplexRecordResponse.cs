namespace TempArAn.Domain.Responses
{
    public class ComplexRecordResponse
    {
        public Dictionary<string, string> Value { get; set; }
        public DateTime DateTime { get; set; }

        public ComplexRecordResponse(Dictionary<string, string> value, DateTime dateTime)
        {
            Value = value;
            DateTime = dateTime;
        }
    }
}
