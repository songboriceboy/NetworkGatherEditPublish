using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
/*
豆约翰博客备份专家是完全免费，功能强大的博客备份工具，
博客电子书（PDF，CHM和TXT）生成工具，博文离线浏览工具
软件界面美观大方，支持多个主流博客网站（QQ空间，新浪博客
宝宝树博客，豆瓣日记，天涯博客，简书，博客园，和讯博客
CSDN博客，Iteye博客，搜狐博客，51CTO，itpub，chinaunix博客,微信公众号文章）。
任意博文远程一键发布到新浪，网易，博客园，WordPress功能
博客导出成静态站点功能。
并带有Android版博客阅读器
*/
namespace NetworkGatherEditPublish
{
    public partial class Frm_Main : Form
    {
        protected WebDownloader m_wd = new WebDownloader();
        protected List<string> m_lstUrls = new List<string>();
        protected string m_strCnblogsUrlFilterRule = "";

        private TaskDelegate deles;
        public Frm_Main()
        {
            InitializeComponent();
        }

        protected void GatherInitCnblogsFirstUrls()
        {
            string strPagePre = "http://www.cnblogs.com/";
            string strPagePost = "/default.html?page={0}&OnlyTitle=1";
            string strPage = strPagePre + this.txtBoxCnblogsBlogID.Text + strPagePost;


            for (int i = 500; i > 0; i--)
            {
                string strTemp = string.Format(strPage, i);
                m_wd.AddUrlQueue(strTemp);

            }
        }

     
        private void Form1_Load(object sender, EventArgs e)
        {
            deles = new TaskDelegate(new ccTaskDelegate(RefreshTask));
            this.txtBoxCnblogsBlogID.Text = "xchsp";
        }

        private void ParseWebPage(string strVisitUrl, string strPageContent)
        {
            bool bNoArticle = SaveUrlToDB(strVisitUrl, strPageContent);
            if (!bNoArticle)
            {
                PageResult pr = m_wd.ProcessQueue(Encoding.UTF8);
                ParseWebPage(pr.strVisitUrl, pr.strPageContent);
            }

        }

        protected bool SaveUrlToDB(string strVisitUrl, string strReturnPage)
        {


            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument
            {
                OptionAddDebuggingAttributes = false,
                OptionAutoCloseOnEnd = true,
                OptionFixNestedTags = true,
                OptionReadEncoding = true
            };
            htmlDoc.LoadHtml(strReturnPage);


            string baseUrl = new Uri(strVisitUrl).GetLeftPart(UriPartial.Authority);
            DocumentWithLinks links = htmlDoc.GetLinks();
           
            List<string> lstRevomeSame = new List<string>();
          

            List<string> lstThisTimesUrls = new List<string>();
            foreach (string link in links.Links.Union(links.References))
            {

                if (string.IsNullOrEmpty(link))
                {
                    continue;
                }

                string decodedLink = link;
        
                string normalizedLink = decodedLink;
                if (string.IsNullOrEmpty(normalizedLink))
                {
                    continue;
                }

                MatchCollection matchs = Regex.Matches(normalizedLink, m_strCnblogsUrlFilterRule, RegexOptions.Singleline);
                if (matchs.Count > 0)
                {
                    string strLinkText = "";

                    if (links.m_dicLink2Text.Keys.Contains(normalizedLink))
                        strLinkText = links.m_dicLink2Text[normalizedLink];

                    if (strLinkText == "")
                    {
                        if (links.m_dicLink2Text.Keys.Contains(link))
                            strLinkText = links.m_dicLink2Text[link].TrimEnd().TrimStart();
                    }

                    PrintLog(strLinkText + "\n");
                    PrintLog(normalizedLink + "\n");
                    

                    lstThisTimesUrls.Add(normalizedLink);
                }
                
            }

            bool bNoArticle = CheckArticles(lstThisTimesUrls);

            return bNoArticle;

        }

        private bool CheckArticles(List<string> lstThisTimesUrls)
        {
            bool bRet = true;
            foreach (string strTemp in lstThisTimesUrls)
            {
                if (!m_lstUrls.Contains(strTemp))
                {
                    bRet = false;
                    break;
                }
            }
            foreach (string strTemp in lstThisTimesUrls)
            {
                if (!m_lstUrls.Contains(strTemp))
                    m_lstUrls.Add(strTemp);
            }
         
            return bRet;
        }

        private void buttonGetUrls_Click(object sender, EventArgs e)
        {
            m_lstUrls.Clear();
            this.richTextBoxLog.Text = "";
            this.backgroundWorker1.RunWorkerAsync();
        }
        public void RefreshTask(DelegatePara dp)
        {
            //如果需要在安全的线程上下文中执行
            if (this.InvokeRequired)
            {
                this.Invoke(new ccTaskDelegate(RefreshTask), dp);
                return;
            }
          
            //转换参数
            string strLog = (string)dp.strLog;
            WriteLog(strLog);

        }
        protected void PrintLog(string strLog)
        {
            DelegatePara dp = new DelegatePara();

            dp.strLog = strLog;
            deles.Refresh(dp);
        }
        public void WriteLog(string strLog)
        {
            try
            {
                strLog = System.DateTime.Now.ToLongTimeString() + " : " + strLog;
           
                this.richTextBoxLog.AppendText(strLog);
                this.richTextBoxLog.SelectionStart = int.MaxValue;
                this.richTextBoxLog.ScrollToCaret();
            }
            catch
            {
            }


        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            m_strCnblogsUrlFilterRule = @"www\.cnblogs\.com/" + this.txtBoxCnblogsBlogID.Text + @"/(p|archive/.*?/.*?/.*?)/.*?\.html$";

            GatherInitCnblogsFirstUrls();
            PageResult pr = m_wd.ProcessQueue(Encoding.UTF8);
            ParseWebPage(pr.strVisitUrl, pr.strPageContent);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            PrintLog("共抓取到" + m_lstUrls.Count.ToString() + "篇博文\n");
             MessageBox.Show("全部文章链接获取完成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
