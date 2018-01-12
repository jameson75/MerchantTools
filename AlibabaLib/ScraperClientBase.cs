using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using HtmlAgilityPack;

namespace CipherPark.Alibaba.Api
{
    public abstract class ScraperClientBase
    {
        protected static string UrlEncode(string expr)
        {
            return WebUtility.UrlEncode(expr).Replace("+", "%20").Replace("!", "%21");
        }

        protected static string GetHtml(string url)
        {
            string responseData = null;
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.Method = System.Net.Http.HttpMethod.Get.Method;
                HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                Stream responseStream = webResponse.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream, Encoding.Default);
                responseData = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();
                responseStream.Close();
                responseStream.Dispose();
            }
            catch (WebException webEx)
            {
                Stream responseStream = webEx.Response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream, Encoding.Default);
                responseData = reader.ReadToEnd();
            }
            return responseData;
        }

        protected static string ScrubPriceText(string text)
        {
            string r = text.Replace("\t", "")
                       .Replace("\r\n", "")
                       .Replace("&quot;", "\"")
                       .Trim();
            return r;
        }
    }
}

