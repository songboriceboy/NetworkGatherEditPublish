using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using HtmlAgilityPack;



namespace NetworkGatherEditPublish
{
	public static class HtmlToText
	{
		#region Class Methods

		public static DocumentWithLinks GetLinks(this HtmlDocument htmlDocument)
		{
			return new DocumentWithLinks(htmlDocument);
		}
        public static DocumentWithLinks GetSrcLinks(this HtmlDocument htmlDocument)
        {
            return new DocumentWithLinks(htmlDocument, true);
        }

		#endregion
	}

	public class DocumentWithLinks
	{
		#region Readonly & Static Fields

		private readonly HtmlDocument m_Doc;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates an instance of a DocumentWithLinkedFiles.
		/// </summary>
		/// <param name="doc">The input HTML document. May not be null.</param>
		public DocumentWithLinks(HtmlDocument doc)
		{


			m_Doc = doc;
			GetLinks();
			GetReferences();
            GetReferencesText();
		}
        public DocumentWithLinks(HtmlDocument doc, bool bSrc)
        {


            m_Doc = doc;
            GetSrcLinks();
            //GetReferences();
            //GetReferencesText();
        }
		#endregion

		#region Instance Properties

		/// <summary>
		/// Gets a list of links as they are declared in the HTML document.
		/// </summary>
		public IEnumerable<string> Links { get; private set; }

		/// <summary>
		/// Gets a list of reference links to other HTML documents, as they are declared in the HTML document.
		/// </summary>
		public IEnumerable<string> References { get; private set; }
        public Dictionary<string, string> m_dicLink2Text = new Dictionary<string, string>();
        //public List<string> HrefInnerTexts = new List<string>();
		#endregion

		#region Instance Methods

		private void GetLinks()
		{
			HtmlNodeCollection atts = m_Doc.DocumentNode.SelectNodes("//*[@background or @lowsrc or @src or @href or @action]");
            if (Equals(atts, null))
			{
				Links = new string[0];
				return;
			}

			Links = atts.
				SelectMany(n => new[]
					{
						ParseLink(n, "background"),
						ParseLink(n, "href"),
						ParseLink(n, "src"),
						ParseLink(n, "lowsrc"),
                        
						ParseLink(n, "action"),
					}).
				Distinct().
				ToArray();
		}
        private void GetSrcLinks()
        {
            HtmlNodeCollection atts = m_Doc.DocumentNode.SelectNodes("//*[@src]");
            if (Equals(atts, null))
            {
                atts = m_Doc.DocumentNode.SelectNodes("//*[@data-src]");
                if (Equals(atts, null))
                {
                    Links = new string[0];
                    return;
                }
            }

            Links = atts.
                SelectMany(n => new[]
					{
						ParseLink(n, "src"),
                        ParseLink(n, "orgSrc"),
                        ParseLink(n, "data-src"),
                        
				
					}).
                Distinct().
                ToArray();
        }
		private void GetReferences()
		{
			HtmlNodeCollection hrefs = m_Doc.DocumentNode.SelectNodes("//a[@href]");

            if (Equals(hrefs, null))
			{
				References = new string[0];
				return;
			}

			References = hrefs.
				Select(href => href.Attributes["href"].Value).
				Distinct().
				ToArray();
		}
        private void GetReferencesText()
        {
            try
            {
                m_dicLink2Text.Clear();
                HtmlNodeCollection hrefs = m_Doc.DocumentNode.SelectNodes("//a[@href]");

                if (Equals(hrefs, null))
                {
                    return;
                }

                foreach (HtmlNode node in hrefs)
                {
                    if (!m_dicLink2Text.Keys.Contains(node.Attributes["href"].Value.ToString()))
                        if(!HttpUtility.HtmlDecode(node.InnerHtml).Contains("img src")
                            && !HttpUtility.HtmlDecode(node.InnerHtml).Contains("img ")
                            && !HttpUtility.HtmlDecode(node.InnerHtml).Contains(" src"))
                         m_dicLink2Text.Add(node.Attributes["href"].Value.ToString(), HttpUtility.HtmlDecode(node.InnerHtml));
                }
                int a = 0;
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.ToString());
            }

        }
		#endregion

		#region Class Methods

		private static string ParseLink(HtmlNode node, string name)
		{
			HtmlAttribute att = node.Attributes[name];
            if (Equals(att, null))
			{
				return null;
			}

            if ((name == "href") && (node.Name != "link" && node.Name != "a"))
			{
				return null;
			}

			return att.Value;
		}

		#endregion
	}
}