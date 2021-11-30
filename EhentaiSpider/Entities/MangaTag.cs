namespace EhentaiSpider.Entities
{
    /// <summary>
    /// 漫画标签
    /// </summary>
    public class MangaTag
    {
        /// <summary>
        /// 标签大类
        /// </summary>
        public string TagMainCategory { get; set; } = string.Empty;

        /// <summary>
        /// 标签分类
        /// </summary>
        public string TagCategory { get; set; } = string.Empty;

        /// <summary>
        /// 从页面上直接抓取的名字
        /// </summary>
        public string TagHtmlShowName { get; set; } = string.Empty;

        /// <summary>
        /// 标签完全标题
        /// </summary>
        public string TagFullTitle => $"{TagMainCategory}:{TagCategory}";

        /// <summary>
        /// 标签简写标题
        /// </summary>
        public string TagTitle =>
            $"{(string.IsNullOrWhiteSpace(TagMainCategory) ? "" : $"{TagMainCategory[0].ToString()}:")}{TagCategory}";

        /// <summary>
        /// 是否收藏
        /// </summary>
        public bool IsFavorite { get; set; }
    }
}