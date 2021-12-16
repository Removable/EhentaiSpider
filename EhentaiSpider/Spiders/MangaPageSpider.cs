using System.Collections.Concurrent;
using ComponentUtil.Common.Data;

namespace EhentaiSpider.Spiders;

public class MangaPageSpider
{
    /// <summary>
    /// 获取漫画所有大图的src （高清原图暂不考虑）
    /// </summary>
    /// <param name="mangaUrl">漫画首页地址</param>
    /// <param name="mangaPageCount">漫画总页数</param>
    /// <param name="maxParallelCount">处理过程中最大任务并行量（不传则采用默认值）</param>
    public static async Task<string[]> GetMangaImages(string mangaUrl, int mangaPageCount,
        int maxParallelCount = -1)
    {
        var parallelOptions = new ParallelOptions
        {
            MaxDegreeOfParallelism = maxParallelCount
        };

        var document = await CommonMethod.LoadDocumentAsync(mangaUrl);

        //先获取总页数
        var pageNoTd = document.QuerySelectorAll(".ptt>tbody>tr>td").ToArray();
        var totalPages = pageNoTd.Length - 2; //去掉两个翻页按钮

        //大图地址集合
        var allImageDetailPageUrls = new List<string>(mangaPageCount);
        //用于多线程记录页数
        var urlsWithPage = new ConcurrentDictionary<int, string[]>();

        //找到有大图页面链接的a标签的条件
        const string imagePageUrlQuery = ".gdtm>div>a";
        //第一页直接获取
        var urls = document.QuerySelectorAll(imagePageUrlQuery).Select(item => item.Attributes["href"]?.Value ?? "");
        allImageDetailPageUrls.AddRange(urls);

        //后续页数采用多线程获取（可选最大并行数）
        if (totalPages > 1)
        {
            //后续页数的地址
            var pageUrls = new (int pageNo, string pageUrl)[totalPages - 1];
            for (var i = 0; i < pageUrls.Length; i++)
            {
                var p = i + 1;
                pageUrls[i] = (p, $"{mangaUrl}?p={p.ToString()}");
            }

            await Parallel.ForEachAsync(pageUrls, parallelOptions,
                async (pageUrlItem, _) =>
                {
                    var doc = await CommonMethod.LoadDocumentAsync(pageUrlItem.pageUrl);
                    var urlArray = doc.QuerySelectorAll(imagePageUrlQuery)
                        .Select(item => item.Attributes["href"]?.Value ?? "").ToArray();
                    urlsWithPage.TryAdd(pageUrlItem.pageNo, urlArray);
                });
        }

        if (urlsWithPage.Any())
        {
            for (var i = 1; i <= urlsWithPage.Count; i++)
            {
                allImageDetailPageUrls.AddRange(urlsWithPage[i]);
            }
        }

        //从每个大图页面获取大图src
        var imageUrlArray = new string[mangaPageCount];
        await Parallel.ForEachAsync(allImageDetailPageUrls, parallelOptions,
            async (pageUrl, _) =>
            {
                if (string.IsNullOrWhiteSpace(pageUrl))
                {
                    return;
                }

                var doc = await CommonMethod.LoadDocumentAsync(pageUrl);
                var src = doc.QuerySelector("#img")?.Attributes["src"]?.Value ?? "";
                imageUrlArray[pageUrl[(pageUrl.LastIndexOf('-') + 1)..].ToInt(1) - 1] = src;
            });

        return imageUrlArray;
    }
}