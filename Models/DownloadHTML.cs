using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using System.Net;
using System.IO;
using RKstat.Interfaces;

namespace RKstat.Models
{
    public class DownloadHTML : IDownloadHTML
    {
        private HtmlWeb htmlWeb = new HtmlWeb();
        private string url;
        private HttpWebRequest request = null;
        private CookieContainer cookieContainer = new CookieContainer();
        public DownloadHTML(string url)
        {
            SetDomain(url);
        }
        public DownloadHTML()
        {

        }
        public void SetDomain(string url)
        {
            this.url = url;
            request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
        }
        public void SetCookie(string name, string value, string domain)
        {
            Cookie cookie = new Cookie(name, value)
            {
                Domain = new Uri(domain).Host
            };
            cookieContainer.Add(cookie);
        }
        public HtmlDocument GetHtml()
        {
            request.CookieContainer = cookieContainer;
            WebResponse webRespon = request.GetResponse();
            HttpWebResponse response = (HttpWebResponse)webRespon;
            string html;
            if (response != null)
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream stream = response.GetResponseStream();
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        html = reader.ReadToEnd();
                    }
                    HtmlDocument htmlDocument = new HtmlDocument();
                    htmlDocument.LoadHtml(html);
                    return htmlDocument;
                }
            }
            return null;

        }
    }
}
