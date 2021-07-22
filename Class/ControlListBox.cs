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
        public List<Control> Items { get => items; set => items = value; }

        public ControlListBox()
        {
            items = new List<Control>();

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
            var id = Items.Count;
            var top = GetTop(id);

            //cl.Parent = panelControlListBox;
            cl.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            
            cl.Location = new Point(0,top);

            cl.SizeChanged += Cl_SizeChanged;

            return Items.Count();
        }

        public int Insert(int index, Control cl)
        {
            AutoScroll = true;

            Items.Insert(index, cl);

            var top = GetTop(index);

            //cl.Parent = panelControlListBox;
            cl.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            cl.Location = new Point(0, top);

            cl.SizeChanged += Cl_SizeChanged;

            return Items.Count();
        }

        private void Cl_SizeChanged(object sender, EventArgs e)
        {
            var index = Items.IndexOf((Control)sender);

            for (int i = index; i < items.Count; i++)
            {
                Items[i].Top = GetTop(i);
                Console.WriteLine("Size: {0} / {1}", i, Items[i].Top);
            }

            this.Update();
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

            //AutoScroll = false;
            VerticalScroll.Enabled = false;
            VerticalScroll.Visible = true;
            HorizontalScroll.Enabled = false;
            HorizontalScroll.Visible = false;
        }

        public void Remove(int id)
        {
            Items.RemoveAt(id);
        }

        public void Replace()
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
