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
    public partial class panelTimeLineContents2 : UserControl
    {
        public long tweetId { get; set; }

        public panelTimeLineContents2(long id)
        {
            InitializeComponent();

            tweetId = id;
        }
    }
}
