using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using AngleSharp.Io;

namespace EhentaiSpider.Spiders
{
    public class CommonMethod
    {
        public static async Task<IDocument> LoadDocumentAsync(string url)
        {
#if !DEBUG
            return await GetFromUrl(url);
#else
            var path = @"D:\EhentaiSource\Ehentai-html.html";
            var str = await File.ReadAllTextAsync(path);
            var parser = new HtmlParser();
            return await parser.ParseDocumentAsync(str, CancellationToken.None);
#endif

            #region LocalFunction

            static async Task<IDocument> GetFromUrl(string url)
            {
                var headers = new List<KeyValuePair<string, string>>
                {
                    new("user-agent",
                        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.55 Safari/537.36 Edg/96.0.1054.34"),
                    new("accept",
                        "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9"),
                    new("accept-encoding", "gzip, deflate"),
                    new("content-type", "text/html; charset=UTF-8"),
                    new("cache-control", "max-age=0"),
                };

                var requester = new DefaultHttpRequester();
                foreach (var kv in headers)
                {
                    requester.Headers.Add(kv.Key, kv.Value);
                }

                var context = BrowsingContext.New(Configuration.Default.WithLocaleBasedEncoding().WithDefaultLoader()
                    .WithDefaultCookies().With(requester));
                //根据虚拟请求/响应模式创建文档
                var document = await context.OpenAsync(url);
                return document;
            }

            #endregion
        }
    }
}