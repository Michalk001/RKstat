using HtmlAgilityPack;
using RKstat.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RKstat.Models
{
    public class DataFromHTML : IDataFromHTML
    {
        private HtmlDocument docHTML { get; set; }
                
        public DataFromHTML(HtmlDocument docHTML)
        {
            this.docHTML = docHTML;
        }
        public DataFromHTML()
        {
         
        }
        public string GetTextFromNode(string querry)
        {
            try
            {


                var tmp = docHTML.DocumentNode.SelectNodes(querry)[0].InnerText;
                return System.Net.WebUtility.HtmlDecode((tmp.Remove(0, tmp.IndexOf(":") + 1)).TrimStart(' '));
            }
            catch(Exception ex)
            {
                
            }
       
            return null;
        }
    }
}
