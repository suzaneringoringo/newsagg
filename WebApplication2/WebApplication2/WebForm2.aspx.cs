using Newss;
using NewsAgt;
using System;
using System.Web.UI.HtmlControls;

namespace WebApplication2
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        public float lat;
        public float lng;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Cari_Click(object sender, EventArgs e)
        {
            NewsAggregator na = new NewsAggregator();

            News.BuildLast(SearchBox.Text);
            News.computeFail(SearchBox.Text);
            na.ParseDetik(SearchBox.Text, RadioButtonList1.SelectedValue);
            na.ParseAntara(SearchBox.Text, RadioButtonList1.SelectedValue);
            na.ParseViva(SearchBox.Text, RadioButtonList1.SelectedValue);
            if (na.news.Count == 0)
            {
                result.InnerText = "No result found";
                return;
            }
            result.InnerText = "Displaying " + na.news.Count.ToString() + " results:";

            foreach (News n in na.news)
            {
                string pattern;
                pattern = SearchBox.Text;
                HtmlGenericControl li = new HtmlGenericControl("li");
                list.Controls.Add(li);
                HtmlGenericControl img = new HtmlGenericControl("img");
                img.Attributes.Add("src", n.GetImage());
                img.Attributes.Add("width", "100");
                img.Attributes.Add("hspace", "7");
                img.Attributes.Add("align", "left");
                HtmlGenericControl a = new HtmlGenericControl("a");
                a.Attributes.Add("href", n.GetLink());
                a.InnerText = n.GetTitle();
                HtmlGenericControl p2 = new HtmlGenericControl("p");
                p2.InnerText = n.GetDate().ToLongDateString() + ' ' + n.GetDate().ToLongTimeString();
                HtmlGenericControl p = new HtmlGenericControl("p");
                p.InnerText = n.GetContent();
                li.Controls.Add(img);
                li.Controls.Add(a);
                li.Controls.Add(p2);
                li.Controls.Add(p);
            }
        }
    }
}