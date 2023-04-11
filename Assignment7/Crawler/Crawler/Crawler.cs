using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Collections;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace Crawler
{

    
    internal class Crawler
    {
        private Hashtable urls = new Hashtable();
        private int count = 0;
        private string path = @"C:\Users\Harry\Desktop\";
        private StringBuilder sb = new StringBuilder();
        private string[] validForms = { "text/html" };

        public void Add(string url)
        {
            if (urls.ContainsKey(url))
            {
                MessageBox.Show(url + " has already been crawled");
                return;
            }
            urls.Add(url, false);
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
            if (url.StartsWith(http) || url.StartsWith(https) )
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
                foreach(string url in urls.Keys)
                {
                    if ((bool)urls[url])
                        continue;
                    current = url;
                }
                if (current == null || count > 10)
                    break;
                current = AddPrefix(current);
                sb.AppendLine("Crawling " + current + " page!");
                string html = Download(current);
                urls[current] = true;
                count++;
                Parse(html, current);


            }
           
            return sb.ToString();
            

        }
        public string Download(string url)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.Encoding= Encoding.UTF8;
                string html = webClient.DownloadString(url);
                string fileName = count.ToString() + ".txt";
                File.WriteAllText(path + fileName, html, Encoding.UTF8);
                sb.AppendLine("Succeeded!");

                var content = webClient.ResponseHeaders["Content-Type"];
                bool toParse = false;
                if (content != null)
                {   
                    foreach(string s in validForms)
                    {
                        toParse |= content.StartsWith(s, StringComparison.OrdinalIgnoreCase);
                    }  
                } 
                else
                {
                    toParse= true;
                }
                //MessageBox.Show(sb.ToString());
                return toParse ? html : "";
            }
            catch (Exception ex) 
            {
                sb.AppendLine("Failed! Error: " + ex.Message);
                //MessageBox.Show(sb.ToString());
                return "";
            }
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
                if (!CorrectFormat(strRef))
                    continue;
                strRef = Path(webPagePath, strRef);
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
