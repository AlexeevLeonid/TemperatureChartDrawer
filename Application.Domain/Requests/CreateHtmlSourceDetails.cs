namespace TempArAn.Domain.Requests
{
    public class CreateHtmlSourceDetails
    {
        public string Url { get; set; }
        public string Left { get; set; }
        public string Right { get; set; }
        public string Name { get; set; }
        public int Interval { get; set; }
        public CreateHtmlSourceDetails
            (string url, string left, string right, string name, int interval)
        {
            Url = url;
            Left = left;
            Right = right;
            Name = name;
            Interval = interval;
        }
    }
}
