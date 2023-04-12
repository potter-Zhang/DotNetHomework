using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Collections;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace WebCrawler
{


    internal class Crawler
    {
        private Hashtable urls = new Hashtable();
        private int count = 0;
        private string path = @"C:\Users\Harry\Desktop\";
        private StringBuilder sb = new StringBuilder();
        private string[] validForms = { "text/html" };
        private component comp;

        public string Message 
        { get
            {
                return sb.ToString();
            } 
        }

        struct component
        {
            public string protocol;
            public string hostName;
            public string filePath;
        };


        public void Add(string url)
        {
            if (urls.ContainsKey(url))
            {
                MessageBox.Show(url + " has already been crawled");
                return;
            }
            urls.Add(url, false);
        }

        void GetComponent(string url)
        {

            int index = url.IndexOf("/");
            comp.protocol = url.Substring(0, index);
            url = url.Substring(index + 2);
            int nextIndex = url.IndexOf("/");
            if (nextIndex != -1)
            {
                comp.hostName = url.Substring(0, nextIndex);
                comp.filePath = url.Substring(nextIndex + 1);
            }
            else
            {
                comp.hostName = url;
                comp.filePath = "";
            }

        }

        public void SetPath(string path)
        {
            if (path != null && path != "" && path[path.Length - 1] != '/')
            {
                this.path = @path + '/';
            }

        }

        string AddPrefix(string url)
        {
            string http = "http://";
            string https = "https://";
            if (url.StartsWith(http) || url.StartsWith(https))
            {
                return url;
            }
            return https + url;
        }
        public string Crawl()
        {
            sb.Clear();
            while (true)
            {
                string current = null;
                foreach (string url in urls.Keys)
                {
                    if ((bool)urls[url])
                        continue;
                    current = url;
                }
                if (current == null || count > 10)
                    break;
                current = AddPrefix(current);
                GetComponent(current);
                sb.AppendLine("Crawling " + current + " page!");
                string html = Download(current);
                urls[current] = true;
                
                Parse(html, current);


            }

            return sb.ToString();


        }
        public string Download(string url)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                string html = webClient.DownloadString(url);
                string fileName = count.ToString() + ".txt";
                File.WriteAllText(path + fileName, html, Encoding.UTF8);
                sb.AppendLine("[Succeeded]");

                var content = webClient.ResponseHeaders["Content-Type"];
                bool toParse = false;
                if (content != null)
                {
                    foreach (string s in validForms)
                    {
                        toParse |= content.StartsWith(s, StringComparison.OrdinalIgnoreCase);
                    }
                }
                //MessageBox.Show(sb.ToString());
                if (toParse)
                {
                    count++;
                    return html;
                }

                return "";
            }
            catch (Exception ex)
            {
                sb.AppendLine("[Failed] Error: " + ex.Message);
                //MessageBox.Show(sb.ToString());
                return "";
            }
        }

        bool Relative2Absolute(string url, out string completeUrl)
        {
            MatchCollection matches = new Regex(comp.hostName).Matches(url);
            // same protocol
            if (url.StartsWith("//"))
            {
                completeUrl = comp.protocol + url;
                return matches.Count != 0;
            }

            // have protocol
            if (url.StartsWith("http://") || url.StartsWith("https://"))
            {
                completeUrl = url;
                return matches.Count != 0;
            }

            // root dir
            if (url.StartsWith("/"))
            {
                completeUrl = comp.protocol + "//" + comp.hostName + url;
                return true;
            }

            // ./
            if (url.StartsWith("./"))
            {
                completeUrl = comp.protocol + "//" + comp.hostName + "/" + comp.filePath + "/" + url.Substring(2);
                return true;
            }

            // ../
            string pre = comp.protocol + "//" + comp.hostName + "/" + comp.filePath.Substring(0, Math.Max(0, comp.filePath.LastIndexOf("/")));
            while (url.StartsWith("../"))
            {
                url = url.Substring(3);
                pre = pre.Substring(0, pre.LastIndexOf("/"));
            }

            completeUrl = pre + "/" + url;
            return true;

        }

        bool Validate(string url)
        {
            int k = url.IndexOf(';');
            return k == -1;
            // MatchCollection matches = new Regex(".").Matches(url);
        }

        public void Parse(string html, string webPagePath)
        {
            string strRef = "(href|HREF)[]*=[]*[\"\'][^\"\'#>]+[\"\']";
            MatchCollection matches = new Regex(strRef).Matches(html);

            foreach (Match match in matches)
            {
                strRef = match.Value.Substring(match.Value.IndexOf('=') + 1).Trim('"', '\"', '#', ' ', '>');
                if (strRef.Length == 0)
                    continue;
                if (!Validate(strRef) || !Relative2Absolute(strRef, out strRef))
                    continue;
                //strRef = Path(webPagePath, strRef);
                if (urls[strRef] == null)
                {
                    urls[strRef] = false;
                }
            }

        }

        bool CorrectFormat(string strRef)
        {
            string format = ".*[.](html|htm|aspx|php|jsp)";
            MatchCollection matches1 = new Regex(format).Matches(strRef);
            //int index = strRef.IndexOf('/');
            //format = ".*;.*";
            //MatchCollection matches2 = new Regex(format).Matches(strRef.Substring(Math.Max(index, 0)));
            //format = ".*[.].*";
            //MatchCollection matches3 = new Regex(format).Matches(strRef.Substring(Math.Max(index, 0)));
            return matches1.Count != 0;// || matches2.Count == 0;
        }



        string Path(string webPagePath, string hrefPath)
        {
            if (hrefPath.Length >= 4 && hrefPath.Substring(0, 4) == "http")
                return hrefPath;
            Uri uri1 = new Uri(webPagePath, UriKind.Absolute);
            Uri uri2 = new Uri(hrefPath, UriKind.Relative);
            return new Uri(uri1, uri2).OriginalString;

        }

    }
}

