// See https://aka.ms/new-console-template for more information

using EhentaiSpider.Spiders;

var mangaImages = await MangaPageSpider.GetMangaImages("https://e-hentai.org/g/1742413/26779d61ba", 101, -1);
// await MangaPageSpider.GetMangaImages("https://e-hentai.org/g/2086013/36f77a3bf5/", 554, -1);

Console.WriteLine(mangaImages);