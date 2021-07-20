using System.Windows.Forms;

namespace twclient.UserPanel
{
    public partial class panelTimeLineContents1 : UserControl
    {
        public WebBrowser webBrowser1;

        public long tweetId { get; set; }
        public CoreTweet.Status status { get; set; }

        public panelTimeLineContents1(long id)
        {
            InitializeComponent();

            webBrowser1 = new WebBrowser();
            webBrowser1.Parent = this.panelWeb;
            webBrowser1.Dock = DockStyle.Fill;
            webBrowser1.ScrollBarsEnabled = false;

            tweetId = id;
        }
    }
}
