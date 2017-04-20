using Newss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
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
                if (news[i].GetDate().CompareTo(ne.GetDate()) > 0)
                {
                    news.Insert(i, ne);
                    return;
                }
            }
            news.Add(ne);
        }

        public void ParseDetik(string pat)
        {
            string url = "http://rss.detik.com/index.php/";
            string[] set = {"detikcom", "indeks", "finance", "hot", "inet", "sport",
                            "otomotif", "wolipop", "health"};
            foreach (string st in set)
            {
                try
                {
                    XmlReader xml = XmlReader.Create(url + st);
                    SyndicationFeed feed = SyndicationFeed.Load(xml);
                    xml.Close();
                    foreach (SyndicationItem item in feed.Items)
                    {
                        News temp = new News(item.Title.Text, item.PublishDate.DateTime,
                                            item.Links.First().Uri.ToString());
                        try
                        {
                            if (st == "wolipop" || st == "health")
                            {
                                temp.ParseContent("text_detail");
                            }
                            else
                            {
                                temp.ParseContent("detail_text");
                            }
                            if (temp.StringMatchingKMP(pat) != -1)
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
}
