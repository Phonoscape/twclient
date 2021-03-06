using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using twclient.Class;

namespace twclient.UserPanel
{
    public partial class PanelTimeLineList : UserControl
    {
        public BufferedListView listView1;

        public enum ListViewColumn
        {
            TWEETID = 0,
            USERIMAGE,
            USERNAME,
            USERID,
            TWEET,
            DATETIME
        };

        public PanelTimeLineList()
        {
            InitializeComponent();

            listView1 = new BufferedListView();
            this.Controls.Add(listView1);

            listView1.Dock = DockStyle.Fill;
            listView1.View = View.Details;
            listView1.MultiSelect = false;
            listView1.OwnerDraw = false;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.LabelWrap = false;
            
            var font = new Font(listView1.Font.FontFamily,10,FontStyle.Regular);
            listView1.Font = font;

            listView1.Columns.Add("TweetID");
            listView1.Columns.Add("IMG");
            listView1.Columns.Add("ユーザー名");
            listView1.Columns.Add("ID");
            listView1.Columns.Add("Tweet");
            listView1.Columns.Add("時間");

            listView1.Sorting = SortOrder.Descending;

            

        }
    }
}
