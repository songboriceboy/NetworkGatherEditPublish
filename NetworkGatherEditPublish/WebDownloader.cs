using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;

namespace NetworkGatherEditPublish
{
    public class PageResult
    {
        public string strPageContent;
        public string strVisitUrl;
    }

    public struct CrawlerItem
    {
        public string strUrl;
        public string strRefer;
 
    }
    public class WebDownloader
    {
        private readonly Stack<CrawlerItem> m_Stack = new Stack<CrawlerItem>();

        public string GetPageByHttpWebRequest(string url, Encoding encoding, string strRefer)
        {

            string result = null;
   
            WebResponse response = null;
            StreamReader reader = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727)";
                request.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";

                if (!string.IsNullOrEmpty(strRefer))
                {
                    Uri u = new Uri(strRefer);
                    request.Referer = u.Host;
                }
                else
                {
                    request.Referer = strRefer;
                }
                request.Method = "GET";
                response = request.GetResponse();
                reader = new StreamReader(response.GetResponseStream(), encoding);
                result = reader.ReadToEnd();
                
            }
            catch (Exception ex)
            {
                result = "";
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (response != null)
                    response.Close();
                
            }
            return result;
        }
        public void AddUrlQueue(string strUrl)
        {
            CrawlerItem cI = new CrawlerItem();
            cI.strUrl = strUrl;
            cI.strRefer = strUrl;
           
            m_Stack.Push(cI);
        }
        public void ClearQueue()
        {

            m_Stack.Clear();
        }

        public PageResult ProcessQueue(Encoding encoding)
        {
            PageResult pr = new PageResult();
            if (m_Stack.Count == 0)
            {
                return null;
            }
            else 
            {
                CrawlerItem cI = m_Stack.Pop();


                string strContent = GetPageByHttpWebRequest(cI.strUrl, encoding, cI.strRefer);
               
                pr.strPageContent = strContent;
                pr.strVisitUrl = cI.strUrl;
                return pr;
            }
        }


    }
}
