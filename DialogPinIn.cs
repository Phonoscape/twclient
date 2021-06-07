using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace twclient
{
    public partial class DialogPinIn : Form
    {
        private string pin;

        public DialogPinIn(Control parent)
        {
            InitializeComponent();
        }

        public string GetPin() { return pin; }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            pin = textBoxPin.Text;

            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
