using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EhentaiSpider.Enums
{
    /// <summary>
    /// 漫画分类
    /// </summary>
    public enum MangaCategory
    {
        /// <summary>
        /// 同人志
        /// </summary>
        [Description("Doujinshi")]
        Doujinshi,
        /// <summary>
        /// 漫画
        /// </summary>
        [Description("Manga")]
        Manga,
        /// <summary>
        /// 画师CG
        /// </summary>
        [Description("Artist CG")]
        ArtistCg,
        /// <summary>
        /// 游戏CG
        /// </summary>
        [Description("Game CG")]
        GameCg,
        /// <summary>
        /// 西方
        /// </summary>
        [Description("Western")]
        Western,
        /// <summary>
        /// 无H
        /// </summary>
        [Description("Non-H")]
        HonH,
        /// <summary>
        /// 画集
        /// </summary>
        [Description("Image Set")]
        ImageSet,
        /// <summary>
        /// Cosplay
        /// </summary>
        [Description("Cosplay")]
        Cosplay,
        /// <summary>
        /// 亚洲色情
        /// </summary>
        [Description("Asian Porn")]
        AsianPorn,
        /// <summary>
        /// 杂项
        /// </summary>
        [Description("Misc")]
        Misc,
    }
}
