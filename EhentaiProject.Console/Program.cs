// See https://aka.ms/new-console-template for more information

// var str = await EhentaiSpider.Spiders.CommonMethod.GetHtmlCode("https://e-hentai.org/popular");
var doc = await EhentaiSpider.Spiders.MangaListSpider.GetPopularGallery();
Console.WriteLine();