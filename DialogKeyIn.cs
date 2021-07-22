using System;
using System.Windows.Forms;

namespace twclient
{
    public partial class DialogKeyIn : Form
    {
        private string key;
        private string secret;

        public DialogKeyIn(Control parent)
        {
            InitializeComponent();
        }

        public string GetKey() { return key; }
        public string GetSecret() { return secret; }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            key = textBoxKey.Text;
            secret = textBoxSecret.Text;

            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
