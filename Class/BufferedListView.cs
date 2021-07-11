using System.Windows.Forms;

namespace twclient.Class
{
    public class BufferedListView : ListView
    {
        public BufferedListView()
        {
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Meiryou UI", 10);
        }
    }
}
