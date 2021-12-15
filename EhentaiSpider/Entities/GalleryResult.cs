using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EhentaiSpider.Entities
{
    public partial class GalleryResult
    {
        /// <summary>
        /// 漫画信息
        /// </summary>
        public MangaBasicInfo[] MangaBasicInfos { get; set; } = Array.Empty<MangaBasicInfo>();

        /// <summary>
        /// 当前画廊一共有多少页
        /// </summary>
        public int MaxPageNum { get; set; }

        /// <summary>
        /// 当前在第几页
        /// </summary>
        public int CurrentPage { get; set; }
    }
}
