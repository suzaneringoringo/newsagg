using Newss;
using NewsAgt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BulletedList1.Items.Clear();
        }

        protected void Cari_Click(object sender, EventArgs e)
        {
            NewsAggregator na = new NewsAggregator();
            na.ParseDetik("a");
            if (na.news.Count == 0)
            {
                ListItem li = new ListItem("Gak ada");
                BulletedList1.Items.Add(li);
            }
            foreach (News n in na.news)
            {
                ListItem li = new ListItem(n.GetContent(), n.GetLink());
                BulletedList1.Items.Add(li);
            }
        }
    }
}