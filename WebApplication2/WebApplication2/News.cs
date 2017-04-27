﻿using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace Newss
{
    public class News
    {
        private string title;
        private DateTime pubDate;
        private string link;
        private string content;
        private HtmlDocument document;
        private static HtmlWeb web = new HtmlWeb();

        public News()
        {
            title = "";
            pubDate = new DateTime();
            link = "";
            content = "";
            document = new HtmlDocument();
        }

        public News(string ti, DateTime date, string li)
        {
            title = ti;
            pubDate = date;
            link = li;
            content = "";
            document = new HtmlDocument();
        }

        public News(News ne)
        {
            title = ne.title;
            pubDate = ne.pubDate;
            link = ne.link;
            content = ne.content;
            document = ne.document;
        }

        public string GetTitle()
        {
            return title;
        }

        public DateTime GetDate()
        {
            return pubDate;
        }

        public string GetLink()
        {
            return link;
        }

        public string GetContent()
        {
            return content;
        }

        public void RemoveComments(HtmlNode node)
        {
            foreach (var n in node.ChildNodes.ToArray())
            {
                RemoveComments(n);
            }
            if (node.NodeType == HtmlNodeType.Comment)
            {
                node.Remove();
            }
        }

        public void RemoveScriptAndStyle(HtmlNode node)
        {
            node.Descendants()
            .Where(n => n.Name == "script" || n.Name == "style" || n.Name == "div")
            .ToList()
            .ForEach(n => n.Remove());
        }

        public void RemoveAAndSpan(HtmlNode node)
        {
            node.Descendants()
            .Where(n => n.Name == "a" || n.Name == "span")
            .ToList()
            .ForEach(n => n.Remove());
        }

        public void RemoveSpanScriptAndIns(HtmlNode node)
        {
            node.Descendants()
            .Where(n => n.Name == "span" || n.Name == "script" || n.Name == "ins" || n.Name == "div")
            .ToList()
            .ForEach(n => n.Remove());
        }

        public void LoadLink()
        {
            document = web.Load(link);
        }

        public void ParseContentDetik(string st)
        {
            LoadLink();
            StringBuilder sb = new StringBuilder();
            foreach (HtmlNode node in document.DocumentNode.SelectNodes("//div"))
            {
                if (node.Attributes["class"] != null && node.Attributes["class"].Value == st)
                {
                    RemoveComments(node);
                    RemoveScriptAndStyle(node);
                    sb.Append(node.InnerText);
                    break;
                }
            }
            content = sb.ToString().Trim();
        }

        public void ParseContentViva()
        {
            LoadLink();
            StringBuilder sb = new StringBuilder();
            foreach (HtmlNode node in document.DocumentNode.SelectNodes("//span"))
            {
                if (node.Attributes["itemprop"] != null && node.Attributes["itemprop"].Value == "description")
                {
                    sb.Append(node.InnerText);
                    break;
                }
            }
            content = sb.ToString().Trim();
        }

        public void ParseContentTempo()
        {
            LoadLink();
            StringBuilder sb = new StringBuilder();
            foreach (HtmlNode node in document.DocumentNode.SelectNodes("//p"))
            {
                RemoveAAndSpan(node);
                sb.Append(node.InnerText);
                //break;
            }
            content = sb.ToString().Trim();
        }

        public void ParseContentAntara()
        {
            LoadLink();
            StringBuilder sb = new StringBuilder();
            foreach (HtmlNode node in document.DocumentNode.SelectNodes("//div"))
            {
                if (node.Attributes["id"] != null && node.Attributes["id"].Value == "content_news")
                {
                    RemoveSpanScriptAndIns(node);
                    sb.Append(node.InnerText);
                    break;
                }
            }
            content = sb.ToString().Trim();
        }

        public int StringMatching(string pat, string rad)
        {
            int ind;
            if (rad == "Boyer-Moore")
            {
                ind = StringMatchingBoyerMoore(pat);
            }
            else if (rad == "KMP")
            {
                ind = StringMatchingKMP(pat);
            }
            else
            {
                ind = StringMatchingRegex(pat);
            }
            if ((ind != -1) && (ind >= title.Length))
            {
                if (ind < ((content.Length) + (title.Length) - 1))
                {
                    content = (content.Substring(ind - title.Length, 200) + " . . . .");
                }
                else
                {
                    content = content.Substring(ind - title.Length, 200);
                }
                if (ind >= title.Length)
                {
                    content = (". . . " + content);
                }
            }
            else
            {
                if (content.Length > 200)
                {
                    content = content.Substring(0, 200) + " . . . .";
                }
                else
                {
                    content = content.Substring(0, 200);
                }
            }
            return ind;
        }

        public int StringMatchingKMP(string pattern)
        {
            string temp;
            temp = title + content;
            int n = temp.Length;
            int m = pattern.Length;

            int[] fail = computeFail(pattern);

            int i = 0;
            int j = 0;

            while (i < n)
            {
                if (char.ToUpperInvariant(pattern[j]) == char.ToUpperInvariant(temp[i]))
                {
                    if (j == (m - 1))
                    {
                        return (i - m + 1);
                    }
                    i++;
                    j++;
                }
                else if (j > 0)
                {
                    j = fail[j - 1];
                }
                else
                {
                    i++;
                }
            }
            return -1;
        }

        public int StringMatchingBoyerMoore(string pat)
        {
            int[] last = BuildLast(pat);
            for (int l = 0; l < 128; l++)
            {
                Console.Write(last[l] + " ");
            }
            Console.WriteLine();
            string temp;
            temp = title + content;
            int n = temp.Length;
            int m = pat.Length;
            int i = m - 1;

            if (i > (n - 1))
            {
                return -1; // no match if pattern is longert than text
            }
            else
            {
                int j = m - 1;
                do
                {
                    if (char.ToUpperInvariant(pat[j]) == char.ToUpperInvariant(temp[i]))
                    {
                        if (j == 0)
                        {
                            return i; // match
                        }
                        else
                        {
                            i--;
                            j--;
                        }
                    }
                    else
                    {
                        int lo = last[temp[i]];
                        i = i + m - Math.Min(j, 1 + lo);
                        j = m - 1;
                    }
                } while (i <= (n - 1));
            }
            return -1;
        }

        public int StringMatchingRegex(string pat)
        {
            Regex reg = new Regex(pat, RegexOptions.IgnoreCase);
            Match mat = reg.Match(content);
            return StringMatchingKMP(mat.Groups[0].ToString());
        }

        public void Print()
        {
            Console.WriteLine(title);
            Console.WriteLine(pubDate.ToString());
            Console.WriteLine(content);
        }

        public static int[] computeFail(String pattern)
        {
            int[] fail = new int[pattern.Length];
            fail[0] = 0;

            int m = pattern.Length;
            int j = 0;
            int i = 1;

            while (i < m)
            {
                if (char.ToUpperInvariant(pattern[j]) == char.ToUpperInvariant(pattern[i]))
                {
                    fail[i] = j + 1;
                    i++;
                    j++;
                }
                else if (j > 0)
                {
                    j = fail[j - 1];
                }
                else
                {
                    fail[i] = 0;
                    i++;
                }
            }
            return fail;
        }

        public static int[] BuildLast(String pattern)
        /* Return array storing index of last
         * occurence of each ASCII char in pattern. */
        {
            int[] last = new int[128]; // ASCII char set

            for (int i = 0; i < 128; i++)
            {
                last[i] = -1; // initialize array
            }

            for (int i = 0; i < pattern.Length; i++)
            {
                last[pattern[i]] = i;
            }

            return last;
        }
    }
}