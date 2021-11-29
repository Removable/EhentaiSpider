using EhentaiSpider.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 封面图地址
        /// </summary>
        public string CoverImgSrc { get; set; }

        /// <summary>
        /// 上传者
        /// </summary>
        public Uploader Uploader { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime UploadTime { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public MangaTag[] Tags { get; set; }

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
        /// 是否有种子
        /// </summary>
        public bool HasTorrent { get; set; }
    }
}
