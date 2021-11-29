using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EhentaiSpider.Entities
{
    public class GalleryResult
    {
        /// <summary>
        /// 漫画信息
        /// </summary>
        public MangaBasicInfo[] MangaBasicInfos { get; set; } = Array.Empty<MangaBasicInfo>();

        /// <summary>
        /// 最大页数
        /// </summary>
        public int MaxPageNum { get; set; }

        /// <summary>
        /// 当前页数
        /// </summary>
        public int CurrentPage { get; set; }
    }
}
