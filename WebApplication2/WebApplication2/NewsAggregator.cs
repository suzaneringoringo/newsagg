using Newss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Xml;

namespace NewsAgt
{
    public class NewsAggregator
    {
        public List<News> news;

        public NewsAggregator()
        {
            news = new List<News>();
        }

        public NewsAggregator(NewsAggregator ne)
        {
            news = new List<News>();
            foreach (News temp in ne.news)
            {
                news.Add(temp);
            }
        }

        public News GetNews(int ind)
        {
            return news[ind];
        }

        public void Add(News ne)
        {
            for (int i = 0; i < news.Count; ++i)
            {
                if (news[i].GetDate().CompareTo(ne.GetDate()) <= 0)
                {
                    if (news[i].GetTitle().CompareTo(ne.GetTitle()) != 0)
                    {
                        news.Insert(i, ne);
                    }
                    return;
                }
            }
            news.Add(ne);
        }

        public static SyndicationFeed getFeed(string url)
        {
            XmlReader xml = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(xml);
            xml.Close();
            return feed;
        }

        public void ParseDetik(string pat, string radio)
        {
            string url = "http://rss.detik.com/index.php/";
            string[] set = { "detikcom" , "indeks", "finance", "hot", "inet", "sport",
                            "otomotif", "wolipop", "health"};
            foreach (string st in set)
            {
                try
                {
                    SyndicationFeed feed = getFeed(url + st);
                    foreach (SyndicationItem item in feed.Items)
                    {
                        News temp = new News(item.Title.Text, item.PublishDate.DateTime,
                                            item.Links.First().Uri.ToString(), item.Summary.Text);
                        try
                        {
                            if (st == "wolipop" || st == "health")
                            {
                                temp.ParseContentDetik("text_detail");
                            }
                            else
                            {
                                temp.ParseContentDetik("detail_text");
                            }
                            if (temp.StringMatching(pat, radio) != -1)
                            {
                                Add(temp);
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public void ParseViva(string pat, string radio)
        {
            string url = "http://rss.viva.co.id/get/all";
            try
            {
                SyndicationFeed feed = getFeed(url);
                foreach (SyndicationItem item in feed.Items)
                {
                    News temp = new News(item.Title.Text, item.PublishDate.DateTime,
                                        item.Links.First().Uri.ToString(), item.Summary.Text);
                    try
                    {
                        temp.ParseContentViva();
                        if (temp.StringMatching(pat, radio) != -1)
                        {
                            Add(temp);
                        }
                    }
                    catch (Exception)
                    {
                        
                    }
                }
            }
            catch (Exception)
            {
                
            }
        }

        public void ParseAntara(string pat, string radio)
        {
            string url = "http://www.antaranews.com/rss/terkini";
            try
            {
                SyndicationFeed feed = getFeed(url);
                foreach (SyndicationItem item in feed.Items)
                {
                    News temp = new News(item.Title.Text, item.PublishDate.DateTime,
                                        item.Links.First().Uri.ToString(), item.Summary.Text);
                    try
                    {
                        temp.ParseContentAntara();
                        if (temp.StringMatching(pat, radio) != -1)
                        {
                            Add(temp);
                        }
                    }
                    catch (Exception)
                    {
                        
                    }
                }
            }
            catch (Exception)
            {
                
            }
        }
    }
}