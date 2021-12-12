using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace twclient
{
    public partial class FormPopupPicture : Form
    {
        private string url;
        private string id;
        private string user;

        public FormPopupPicture()
        {
            InitializeComponent();
        }

        public void SetBitmap(Bitmap bmp, string id, string user, string url)
        {
            pictureBox1.Image = bmp;
            this.Size = bmp.Size;
            this.url = url;
            this.id = id;
            this.user = user;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Close();
            }
            else if (e.Button == MouseButtons.Right)
            {
                contextMenuStripOpenBrowser.Show();
            }
        }

        private void toolStripMenuItemOpenBrowser_Click(object sender, EventArgs e)
        {
            var statusUrl = "https://twitter.com/" + user + "/status/" + id;
            try
            {
                Process.Start(statusUrl);
            }
            catch
            {
                var tmpUrl = statusUrl.Replace("&", "^&");
                Process.Start(new ProcessStartInfo("cmd", "/c start " + tmpUrl) { CreateNoWindow = true });
            }

            Close();
        }

        private void ToolStripMenuItemOpenBrowserImage_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                var tmpUrl = url.Replace("&", "^&");
                Process.Start(new ProcessStartInfo("cmd", "/c start " + tmpUrl) { CreateNoWindow = true });
            }

            Close();
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            Close();
        }
    }
}
