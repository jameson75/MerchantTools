using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace CipherPark.Alibaba.Api
{
    public class AlibabaScraper : ScraperClientBase
    {
        public const int DefaultPageSize = 45;
      
        public AlibabaScrapedResult FindItemsByKeywords(string keywords, int page = 1)
        {
            const string endPoint = "http://wholesaler.alibaba.com/wholesale/search";
            List<AlibabaScrapedItem> searchItems = new List<AlibabaScrapedItem>();
            string searchString = UrlEncode(string.Join("+", keywords.Split(',').Select(t => t.Trim())));
            string url = $"{endPoint}?SearchText={searchString}";
            if (page > 1)
                url += $"&pageId={page}";
            string html = GetHtml(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            HtmlNode mainProductListElement = doc.DocumentNode.SelectSingleNode("//div[@data-role=\"main-product-list\"]");
            if (mainProductListElement != null)
            {                
                var productElements = mainProductListElement.SelectNodes("div[@data-role=\"product-item\"]");
                foreach (var productElement in productElements)
                {
                    string productId = productElement.GetAttributeValue("data-pid", null);
                    var imageElement = productElement.SelectSingleNode("div/div[@class=\"item-img\"]/div/a/img");
                    string imageSrc = null;
                    if (imageElement.GetAttributeValue("src", null) != null)
                        imageSrc = imageElement.GetAttributeValue("src", null);
                    else
                        imageSrc = imageElement.GetAttributeValue("data-src", null);
                    string title = productElement.SelectSingleNode("div/div[@class=\"item-info\"]/h2/a").InnerText;
                    string detailPageUrl = productElement.SelectSingleNode("div/div[@class=\"item-info\"]/h2/a").GetAttributeValue("href", null);
                    var priceElement = productElement.SelectSingleNode("div/div[@class=\"item-info\"]/div[@class=\"price\"]/span[@class=\"latest\"]");
                    if (priceElement == null)
                        priceElement = productElement.SelectSingleNode("div/div[@class=\"item-info\"]/div[@class=\"price ladder-price\"]/span[@class=\"latest\"]");
                    string priceDescription = priceElement.InnerText;
                    searchItems.Add(new AlibabaScrapedItem()
                    {
                        ProductId = productId,
                        ImageUrl = "http:" + imageSrc,
                        Title = ScrubPriceText(title),
                        DetailPageUrl = "http:" + detailPageUrl,
                        PriceDescription = ScrubPriceText(priceDescription)
                    });
                }
                string pageDescription = mainProductListElement.SelectSingleNode("div[@id=\"J-m-pagination\"]/div[@data-role=\"product-pagination\"]").GetAttributeValue("data-test", null);
                int totalPages = int.Parse(pageDescription.Split('-')[1].Trim());
                return new AlibabaScrapedResult()
                {
                    IsSuccess = true,
                    Items = searchItems,
                    TotalPages = totalPages
                };
            }
            else
            {
                return new AlibabaScrapedResult()
                {
                    IsSuccess = false
                };
            }           
        }

        public AlibabaScrapedResult ParseSourceItemPageUrl(string sourceItemUrl)
        {           
            string html = GetHtml(sourceItemUrl);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var div_maRefPrice = doc.DocumentNode.SelectSingleNode("//div[@class=\"ma-ref-price\"]");
            if (div_maRefPrice != null)
            {
                var potentialPriceNodes = div_maRefPrice.SelectNodes("//span");
                var priceDescription = ScrubPriceText(div_maRefPrice.InnerText);
                var prices = potentialPriceNodes.Where(x => x.InnerText.IsNumeric()).Select(x => Convert.ToDouble(x.InnerText)).ToArray();
                return new AlibabaScrapedResult()
                {
                    IsSuccess = true,
                    TotalPages = 1,
                    Items = new[]
                    {
                    new AlibabaScrapedItem()
                    {
                        DetailPageUrl = sourceItemUrl,
                        PriceDescription = priceDescription,
                        PriceRangeMin = prices.Length > 0 ? (decimal?)Convert.ToDecimal(prices[0]) : null,
                        PriceRangeMax = prices.Length > 1 ? (decimal?)Convert.ToDecimal(prices[1]) : null
                    }
                }.ToList()
                };
            }
            else
                return null;
        }
    }

    public class AlibabaScrapedResult
    {
        public bool IsSuccess { get; set; }       
        public int TotalPages { get; set; }
        public List<AlibabaScrapedItem> Items { get; set; }
    }

    public class AlibabaScrapedItem
    {
        public string Title { get; set; }
        public string ProductId { get; set; }
        public string ImageUrl { get; set; }
        public string DetailPageUrl { get; set; }
        public string PriceDescription { get; set; }
        public decimal? PriceRangeMin { get; set; }
        public decimal? PriceRangeMax { get; set; }
    }

    public static class StringExtension
    {
        public static bool IsNumeric(this string s)
        {
            double result = 0;
            return double.TryParse(s, out result);
        }
    }
}
