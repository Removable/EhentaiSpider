using System.Text.RegularExpressions;
using AngleSharp.Dom;
using ComponentUtil.Common.Data;
using EhentaiSpider.Entities;
using EhentaiSpider.Enums;

namespace EhentaiSpider.Spiders
{
    /// <summary>
    /// 
    /// </summary>
    public class MangaListSpider
    {
        public static async Task<MangaBasicInfo[]> GetPopularGallery()
        {
            var url = "https://e-hentai.org/popular";
            var document = await CommonMethod.LoadDocumentAsync(url);
            var popularGallery = ResolveDefaultGallery(document);
            return popularGallery;
        }

        /// <summary>
        /// 处理漫画列表，提取信息，不包含页码
        /// </summary>
        /// <param name="galleryPage"></param>
        /// <exception cref="Exception"></exception>
        private static MangaBasicInfo[] ResolveDefaultGallery(IDocument galleryPage)
        {
            //找到所需table下的每一行
            var trs = galleryPage.QuerySelectorAll(".itg.gltc>tbody>tr").ToArray();
            if (!trs.Any())
            {
                throw new Exception("画廊为空");
            }

            var mangaList = new List<MangaBasicInfo>(trs.Length);
            foreach (var tr in trs)
            {
                var mangaInfo = new MangaBasicInfo();
                var tds = tr.QuerySelectorAll("td");
                //目前每行有4列
                //第一列class=gl1c是漫画分类
                var gl1c = tds.FirstOrDefault(t => t.ClassList.Contains("gl1c"));
                //第二列class=gl2c含有ID、封面图、评分、上传日期、是否有种子等信息
                var gl2c = tds.FirstOrDefault(t => t.ClassList.Contains("gl2c"));
                //第三列class=gl3c含有标题和标签信息
                var gl3c = tds.FirstOrDefault(t => t.ClassList.Contains("gl3c"));
                //第四列class=gl4c含有上传者名称和漫画页数
                var gl4c = tds.FirstOrDefault(t => t.ClassList.Contains("gl4c"));
                //含有class=gl1c的td的行才是有漫画数据的行（排除标题行和广告行）
                if (gl1c == null || gl2c == null || gl3c == null || gl4c == null)
                    continue;
                //分类
                mangaInfo.Category =
                    EnumHelper.GetEnumValueByDescription<MangaCategory>(gl1c.TextContent.Trim());
                //ID
                mangaInfo.Id = gl2c.QuerySelectorAll("div")[0].Attributes["id"]?.Value[2..];
                //封面图
                var imgNode = gl2c.QuerySelectorAll(".glthumb>div")[0].QuerySelector("img");
                if (imgNode != null)
                {
                    mangaInfo.CoverImgSrc = imgNode.Attributes["data-src"]?.Value ?? imgNode.Attributes["src"]?.Value;
                }

                //上传时间
                mangaInfo.UploadTime = DateTime.Parse(gl2c.Children[2].QuerySelectorAll("div")[0].TextContent.Trim());
                //评分（通过style中的position计算）

                #region 计算评分

                var rateStarStyle = gl2c.Children[2].QuerySelectorAll("div")[1].Attributes["style"]?.Value;
                if (string.IsNullOrWhiteSpace(rateStarStyle))
                {
                    mangaInfo.Rate = 0;
                }
                else
                {
                    var pattern = "(?<=background-position:).+(?=;opacity)";
                    var regex = new Regex(pattern);
                    //background-position的值
                    var positionValue = regex.Match(rateStarStyle).Value.Split(' ');
                    //水平位移的量
                    var horizontalPosition =
                        positionValue[0].StartsWith("0") ? 0 : positionValue[0].Replace("px", "").ToInt(0);
                    //垂直位移-1px时，为整星数；-21px时，含半星
                    if (positionValue[1] == "-1px")
                    {
                        mangaInfo.Rate = 5 + horizontalPosition / 16d;
                    }
                    else
                    {
                        mangaInfo.Rate = 4.5 + horizontalPosition / 16d;
                    }
                }

                #endregion

                //是否有种子
                mangaInfo.HasTorrent = gl2c.QuerySelector(".gldown")?.Children[0].TagName
                    .Equals("a", StringComparison.OrdinalIgnoreCase) ?? false;
                if (mangaInfo.HasTorrent)
                {
                    //种子下载地址
                    mangaInfo.TorrentDownloadAddress =
                        gl2c.QuerySelector(".gldown")?.Children[0].Attributes["href"]?.Value;
                }

                //漫画标题
                mangaInfo.Title = gl3c.QuerySelectorAll("a>div")[0].TextContent.Trim() ?? "";
                //标签组
                var tagGroupDivList = gl3c.QuerySelectorAll("a>div")[1].Children
                    .Where(c => c.TagName.Equals("div", StringComparison.OrdinalIgnoreCase)).ToArray();
                if (tagGroupDivList.Any())
                {
                    var tags = new MangaTag[tagGroupDivList.Length];
                    for (var j = 0; j < tagGroupDivList.Length; j++)
                    {
                        var divTitle = tagGroupDivList[j].Attributes["title"]?.Value;
                        tags[j] = new MangaTag
                        {
                            IsFavorite = tagGroupDivList[j].OuterHtml.Contains("style=\"color:"),
                            TagMainCategory = divTitle?.Split(':')[0] ?? "",
                            TagCategory = divTitle?.Split(':')[1] ?? "",
                            TagHtmlShowName = tagGroupDivList[j].TextContent.Trim(),
                        };
                    }

                    mangaInfo.Tags = tags;
                }

                //上传者
                mangaInfo.Uploader = new Uploader
                {
                    Name = gl4c.Children[0].Children[0].TextContent.Trim(),
                    PersonalCenterUrl = gl4c.Children[0].Children[0].Attributes["href"]?.Value ?? string.Empty
                };
                //漫画页数
                var pageStr = gl4c.Children[1].TextContent.Trim();
                mangaInfo.PageCount = pageStr[..pageStr.IndexOf(" ", StringComparison.Ordinal)].ToInt(0);

                mangaList.Add(mangaInfo);
            }

            return mangaList.ToArray();
        }
    }
}