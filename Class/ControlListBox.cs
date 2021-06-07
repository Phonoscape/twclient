using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace twclient.Class
{
    public class ControlListBox : Panel
    {
        private List<Control> items;
        private List<int> itemH;

        public List<Control> Items { get => items; set => items = value; }
        public List<int> ItemH { get => itemH; set => itemH = value; }

        public ControlListBox()
        {
            items = new List<Control>();
            itemH = new List<int>();

            AutoScroll = true;
            Anchor = AnchorStyles.Top | AnchorStyles.Left;

            //VerticalScroll.Enabled = false;
            //VerticalScroll.Visible = true;
            //HorizontalScroll.Enabled = false;
            //HorizontalScroll.Visible = false;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        public int Add(Control cl)
        {
            AutoScroll = true;

            Items.Add(cl);
            ItemH.Add(cl.Height);
            var id = Items.Count;
            var top = GetTop(id);

            //cl.Parent = panelControlListBox;
            cl.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            
            cl.Location = new Point(0,top);

            return Items.Count();
        }

        public int GetTop(int id)
        {
            int top = 0;

            for (int i = 0;i < id; i++)
            {
                top += Items[i].Height;
            }

            return top;
        }

        public int GetWidthWithoutScrollbar()
        {
            return ClientSize.Width;
        }

        public void Clear()
        {
            Items.Clear();
            ItemH.Clear();

            //AutoScroll = false;
            VerticalScroll.Enabled = false;
            VerticalScroll.Visible = true;
            HorizontalScroll.Enabled = false;
            HorizontalScroll.Visible = false;
        }

        public void Remove(int id)
        {
            Items.RemoveAt(id);
            ItemH.RemoveAt(id);
        }

        public void RePlace()
        {
            foreach(var item in Items)
            {
                item.Top = GetTop(Items.IndexOf(item));
            }
        }

        private void panelControlListBox_Layout(object sender, LayoutEventArgs e)
        {
            //AutoScroll = false;
            VerticalScroll.Enabled = false;
            VerticalScroll.Visible = true;
            HorizontalScroll.Enabled = false;
            HorizontalScroll.Visible = false;
        }
    }
}
