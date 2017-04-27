using Newss;
using NewsAgt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace WebApplication2
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //BulletedList1.Items.Clear();
        }

        protected void Cari_Click(object sender, EventArgs e)
        {
            NewsAggregator na = new NewsAggregator();
            
            na.ParseViva(SearchBox.Text, RadioButtonList1.SelectedValue);
            if (na.news.Count == 0)
            {
                //ListItem li = new ListItem("Gak ada");
                //BulletedList1.Items.Add(li);
            }
            HtmlGenericControl nl = new HtmlGenericControl("nl");
            nl.InnerText = "Displaying " + na.news.Count.ToString() + " results:";
            list.Controls.Add(nl);

            foreach (News n in na.news)
            {
                int[] test;
                string pattern;
                pattern = SearchBox.Text;
                test = News.BuildLast(pattern);
                /*
                HtmlGenericControl tes = new HtmlGenericControl("tes");
                tes.InnerText = "";
                for (int i = 0; i < test.Length; i++)
                {
                    tes.InnerText = tes.InnerText + " " + test[i].ToString();
                }
                list.Controls.Add(tes);
                */
                HtmlGenericControl li = new HtmlGenericControl("li");
                list.Controls.Add(li);
                HtmlGenericControl a = new HtmlGenericControl("a");
                a.Attributes.Add("href", n.GetLink());
                a.InnerText = n.GetTitle();
                HtmlGenericControl p2 = new HtmlGenericControl("p");
                p2.InnerText = n.GetDate().ToLongDateString() + ' ' + n.GetDate().ToLongTimeString();
                HtmlGenericControl p = new HtmlGenericControl("p");
                p.InnerText = n.GetContent();
                li.Controls.Add(a);
                li.Controls.Add(p2);
                li.Controls.Add(p);
            }
        }
    }
}