using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace twclient.UserPanel
{
    public partial class panelTimeLineContents1 : UserControl
    {
        public long tweetId { get; set; }

        public panelTimeLineContents1(long id)
        {
            InitializeComponent();

            tweetId = id;
        }
    }
}
