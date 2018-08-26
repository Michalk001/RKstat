using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RKstat
{
    public class ParseHtml
    {
        HtmlWeb htmlWeb = new HtmlWeb();
        string url;
        HttpWebRequest request = null;
        CookieContainer cookieContainer = new CookieContainer();
        public ParseHtml(string url){
            SetDomain(url);
        }
        public ParseHtml()
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
        public HtmlDocument getHtml()
        {
            request.CookieContainer = cookieContainer;
            WebResponse webRespon =  request.GetResponse();
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
