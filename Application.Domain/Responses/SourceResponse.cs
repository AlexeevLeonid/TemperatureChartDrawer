namespace TempArAn.Domain.Responses
{
    public class SourceResponse
    {
        public Guid Id { get; set; }
        public int Policy { get; set; }
        public Guid UserId { get; set; }
        public bool IsRecoding { get; set; }
        public string? Name { get; set; }
        public DateTime LastRecord { get; set; }
        public int Interval { get; set; }
        public string? Type { get; set; }
        public Dictionary<string, string>? TypeServiceInfo { get; set; }
    }
}
