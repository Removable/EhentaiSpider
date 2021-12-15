namespace EhentaiSpider.Entities
{
    /// <summary>
    /// 漫画标签
    /// </summary>
    public partial class MangaTag
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
        /// 是否收藏
        /// </summary>
        public bool IsFavorite { get; set; }
    }
}