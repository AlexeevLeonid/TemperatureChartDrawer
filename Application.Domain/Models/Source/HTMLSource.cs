using TempArAn.Domain.AbstractCore;
using TempArAn.Domain.Exceptions.SourceExeptions.HtmlSourceExceptions;
using TempArAn.Domain.Models.Record;

namespace TempArAn.Domain.Models.Source

{
    public class HTMLSource : SourceBase
    {
        public string Url { get; set; }
        public string Left { get; set; }
        public string Right { get; set; }

        public HTMLSource(string name, string url, string left, string right,
        Guid userId, int interval)
        : base(name, interval, userId)
        {
            Url = url;
            Left = left;
            Right = right;
        }

        protected override RecordBase GetValue()
        {
            return new DoubleRecord(
                Id,
                DateTime.Now,
                GetValueFromPage(GetPageAsync().Result));
        }

        public double GetValueFromPage(string page)
        {
            try
            {
                var IndexLeft = page.IndexOf(Left);
                var IndexRight = page.IndexOf(Right, IndexLeft);
                var result = page.Substring(
                        IndexLeft + Left.Length,
                        IndexRight - IndexLeft - Left.Length).
                        Replace(" ", "").Replace(".", ",");
                return double.Parse(result);
            }
            catch (Exception ex)
            {
                throw new ParseErrorHtmlSourceException(ex.Message);
            }
        }

        public async Task<string> GetPageAsync()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    using HttpResponseMessage response = await client.GetAsync(Url);
                    return await response.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    throw new NotFoundHtmlSourceException(ex.Message);
                }
            }
        }


    }
}
