using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public ListViewItem BottomItem()
        {
            if (this.TopItem != null)
            {
                for (int i = this.TopItem.Index; i < this.Items.Count; i++)
                {
                    var tmpItem = this.Items[i];

                    if (tmpItem.Position.Y > this.ClientSize.Height)
                    {
                        return this.Items[i - 1];
                    }
                }
            }
            return null;
        }
    }
}
