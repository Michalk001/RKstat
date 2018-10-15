using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace RKstat.Interfaces
{
    public interface IDownloadHTML
    {
        void SetDomain(string url);
        void SetCookie(string name, string value, string domain);
        HtmlDocument GetHtml();
    }
}
