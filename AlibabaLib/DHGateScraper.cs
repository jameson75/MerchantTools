using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace CipherPark.Alibaba.Api
{
    public class DHGateScraper : ScraperClientBase
    {
        public const int DefaultPageSize = 24;

        public DHGateScrapedResult FindItemsByKeywords(string keywords, int page = 1)
        {
            const string endPointFormat = "http://www.dhgate.com/w/{0}{1}.html";
            List<DHGateScrapedItem> searchItems = new List<DHGateScrapedItem>();
            string searchString = UrlEncode(string.Join("+", keywords.Split(',').Select(t => t.Trim())));
            string url = null;
            int index = page - 1;
            if (index <= 0)
                url = string.Format(endPointFormat, searchString, null);
            else
                url = string.Format(endPointFormat, searchString, "/" + index.ToString());
            string html = GetHtml(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            HtmlNode productListNode = doc.DocumentNode.SelectSingleNode("//div[@id=\"proList\"]");
            if( productListNode != null)
            {
                var listItemNodes = productListNode.SelectNodes("div");
                foreach(var listItem in listItemNodes)
                {
                    string imageUrl = listItem.SelectSingleNode("div[@class=\"photo\"]/a/img")?.GetAttributeValue("src", null);
                    if(imageUrl == null)
                        imageUrl = listItem.SelectSingleNode("div[@class=\"photo\"]/a").GetAttributeValue("lazyload-src", null);
                    string title = listItem.SelectSingleNode("h3[@class=\"pro-title\"]").InnerText;
                    string detailPageUrl = listItem.SelectSingleNode("h3[@class=\"pro-title\"]/a").GetAttributeValue("href", null);
                    string priceDescription = listItem.SelectSingleNode(".//li[@class='price']").InnerText;
                    string productId = listItem.SelectSingleNode("div[@class=\"photo\"]/a").GetAttributeValue("itemcode", null);
                    searchItems.Add(new DHGateScrapedItem()
                    {
                       Title = ScrubPriceText(title),
                       ProductId = productId,
                       DetailPageUrl = detailPageUrl,
                       ImageUrl = imageUrl,
                       PriceDescription = ScrubPriceText(priceDescription)
                    });
                }
                int maxItemsPerPage = int.Parse(doc.DocumentNode.SelectSingleNode("//div[@class=\"perpage\"]/strong").InnerText.Trim());
                int totalResults = int.Parse(doc.DocumentNode.SelectSingleNode("//div[@class=\"searchresult-note\"]/h2/span[@class=\"num\"]").InnerText.Trim().Replace(",", ""));
                int totalPages = totalResults / maxItemsPerPage;
                if (totalResults % maxItemsPerPage > 0)
                    totalPages++;
                return new DHGateScrapedResult()
                {
                    IsSuccess = true,
                    Items = searchItems,
                    TotalPages = totalPages
                };
            }   
            else
            {
                return new DHGateScrapedResult()
                {
                    IsSuccess = false
                };
            }                             
        }
    }

    public class DHGateScrapedResult
    {
        public bool IsSuccess { get; set; }
        public int TotalPages { get; set; }
        public List<DHGateScrapedItem> Items { get; set; }
    }

    public class DHGateScrapedItem
    {
        public string Title { get; set; }
        public string ProductId { get; set; }
        public string ImageUrl { get; set; }
        public string DetailPageUrl { get; set; }
        public string PriceDescription { get; set; }
    }
}
