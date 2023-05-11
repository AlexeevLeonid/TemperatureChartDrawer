namespace TemperatureChartDrawer.src.Sourse
{
    public class HTMLSource : SourceBase
    {
        public string Url { get; set; }
        public string Left { get; set; }
        public string Right { get; set; }

        private HttpClient? client { get; set; }

        protected override string GetValue()
        {
            return GetValueFromPage(GetPageAsync(Url).Result);
        }

        public string GetValueFromPage(string page)
        {
            var IndexLeft = page.IndexOf(Left);
            var IndexRight = page.IndexOf(Right, IndexLeft);
            return page.Substring(
                IndexLeft + Left.Length,
                IndexRight - IndexLeft - Left.Length);
        }

        public async Task<string> GetPageAsync(string url)
        {
            using (client = new HttpClient())
            {
                using HttpResponseMessage response = await client.GetAsync(url);
                return await response.Content.ReadAsStringAsync();
            }
        }

        public HTMLSource(string name, int interval, string url, string left, string right) : base(name, interval)
        {
            Url = url;
            Left = left;
            Right = right;
        }
    }
}
