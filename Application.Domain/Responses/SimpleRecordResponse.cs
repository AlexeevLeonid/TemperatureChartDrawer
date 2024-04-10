namespace TempArAn.Domain.Responses
{
    public class SimpleRecordResponse
    {
        public string Value { get; set; }
        public DateTime DateTime { get; set; }

        public SimpleRecordResponse(string value, DateTime dateTime)
        {
            Value = value;
            DateTime = dateTime;
        }
    }
}
