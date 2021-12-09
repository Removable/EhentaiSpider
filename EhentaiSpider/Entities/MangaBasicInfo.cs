using ComponentUtil.Common.Data;
using EhentaiSpider.Enums;

namespace EhentaiSpider.Entities
{
    /// <summary>
    /// 漫画基本信息
    /// </summary>
    public class MangaBasicInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        public string? Id { get; set; } = string.Empty;

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 封面图地址
        /// </summary>
        public string? CoverImgSrc { get; set; }

        /// <summary>
        /// 根据封面图地址获取图片原始文件名
        /// </summary>
        public string? ImgFileName => string.IsNullOrWhiteSpace(CoverImgSrc)
            ? ""
            : CoverImgSrc[(CoverImgSrc.LastIndexOf(@"/", StringComparison.Ordinal) + 1)..];

        /// <summary>
        /// 上传者
        /// </summary>
        public Uploader? Uploader { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime UploadTime { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public MangaTag[] Tags { get; set; } = Array.Empty<MangaTag>();

        /// <summary>
        /// 评分
        /// </summary>
        public double Rate { get; set; }

        /// <summary>
        /// 页数
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public MangaCategory Category { get; set; }

        /// <summary>
        /// 分类原文
        /// </summary>
        public string CategoryText { get; set; } = string.Empty;

        /// <summary>
        /// 是否有种子
        /// </summary>
        public bool HasTorrent { get; set; }
        
        /// <summary>
        /// 种子下载地址
        /// </summary>
        public string? TorrentDownloadAddress { get; set; }
    }
}