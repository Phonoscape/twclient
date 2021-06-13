using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using twclient.UserPanel;

namespace twclient
{
    public partial class FormTimeLine : Form
    {
        private Twitter twitter;

        private TreeNode oldNode;

        private Hashtable cacheLvi;
        private long selectedTweetId = 0;
        private DateTime addDateTime;

        static readonly float DpiScale = ((new System.Windows.Forms.Form()).CreateGraphics().DpiX) / 96;

        public FormTimeLine()
        {
            InitializeComponent();

            // UserControl Event
            panelControlMainTree1.treeView1.AfterSelect += PanelControlMainTree1_TreeView1_AfterSelect;
            panelControlMainTree1.treeView1.Click += PanelControlMainTree1_TreeView1_Click;

            panelTimeLine1.panelTimeLineList1.listView1.Click += PanelTimeLine1_panelTimeLineList1_ListView1_Click;
            panelTimeLine1.panelTimeLineList1.listView1.KeyUp += PanelTimeLine1_panelTimeLineList1_listView1_KeyUp;
            panelTimeLine1.panelTimeLineList1.listView1.RetrieveVirtualItem += PanelTimeLine1_panelTimeLineList1_ListView1_RetrieveVirtualItem;
            panelTimeLine1.panelTimeLineList1.listView1.DrawItem += PanelTimeLine1_panelTimeLineList1_ListView1_DrawItem;
            panelTimeLine1.panelTimeLineList1.listView1.DrawSubItem += PanelTimeLine1_panelTimeLineList1_ListView1_DrawSubItem;
            panelTimeLine1.panelTimeLineList1.listView1.DrawColumnHeader += PanelTimeLine1_panelTimeLineList1_ListView1_DrawColumnHeader;
            panelTimeLine1.panelTimeLineList1.listView1.SearchForVirtualItem += PanelTimeLine1_panelTimeLineList1_ListView1_SearchForVirtualItem;
            
            panelTimeLine1.panelTimeLineList1.listView1.ContextMenuStrip = contextMenuForListView;
            panelTimeLine1.panelTimeLineList1.listView1.VirtualListSize = 0;
            panelTimeLine1.panelTimeLineList1.listView1.VirtualMode = true;
            panelTimeLine1.panelTimeLineList1.listView1.Sorting = SortOrder.Descending;
            panelTimeLine1.panelTimeLineList1.listView1.OwnerDraw = true;

            //panelTimeLineContents1.richTextBox1.LinkClicked += panelTimeLineContents1_RichTextBox1_LinkClicked;
            //webBrowser1.DocumentCompleted += panelTimeLineContents1_webBrowser_DocumentCompleted;

            panelControlMainEdit1.buttonSend.Click += PanelControlMainEdit1_ButtonSend_Click;
            panelControlMainEdit1.buttonClear.Click += PanelControlMainEdit1_ButtonClear_Click;

            panelControlMainEdit1.checkBoxHash.CheckedChanged += PanelControlMainEdit1_CheckBoxHash_CheckedChanged;

            panelControlMainEdit1.comboBoxHash.SelectedIndexChanged += PanelControlMainEdit1_ComboBoxHash_SelectedIndexChanged;
            panelControlMainEdit1.comboBoxHash.Leave += PanelControlMainEdit1_ComboBoxHash_Leave;
            panelControlMainEdit1.comboBoxHash.KeyPress += PanelControlMainEdit1_ComboBoxHash_KeyPress;

            panelControlMainEdit1.textBoxTweet.KeyDown += PanelControlMainEdit1_TextBoxTweet_KeyDown;
            panelControlMainEdit1.textBoxTweet.KeyUp += PanelControlMainEdit1_TextBoxTweet_KeyUp;
            panelControlMainEdit1.textBoxTweet.KeyPress += PanelControlMainEdit1_TextBoxTweet_KeyPress;

            //listBoxTweetContents.MeasureItem += ListBoxTweetContents_MeasureItem1;
            controlListBox1.Resize += ControlListBox1_Resize;

            splitContainer2.SplitterMoved += SplitContainer2_SplitterMoved;

            toolStripStatusLabel1.Height = 64;

            //contextMenuUser.Click += ContextMenuUser_Click;
            foreach (ToolStripMenuItem obj in contextMenuUser.Items)
            {
                obj.Click += ContextMenuUser_Click;
            }

            //contextMenuSearch.Click += ContextMenuSearch_Click;
            foreach (var obj in contextMenuSearch.Items)
            {
                if (obj.GetType() == typeof(ToolStripMenuItem))
                    ((ToolStripMenuItem)obj).Click += ContextMenuSearch_Click;
            }

            foreach (var obj in contextMenuForListView.Items)
            {
                if (obj.GetType() == typeof(ToolStripMenuItem))
                    ((ToolStripMenuItem)obj).Click += ContextMenuForListView_Click;
            }
        }


        // UI EventHandler

        // SplitContainer

        private void SplitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {
            ContentsResize();
            //ListView1_Click();
        }

        // ComboBoxHash

        private bool changeComboBoxHash = false;

        private void PanelControlMainEdit1_ComboBoxHash_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Console.WriteLine("PanelControlMainEdit1_ComboBoxHash_SelectedIndexChanged");

            if (changeComboBoxHash)
            {
                changeComboBoxHash = false;
                return;
            }

            changeComboBoxHash = true;

            var items = panelControlMainEdit1.comboBoxHash.Items;
            var text = panelControlMainEdit1.comboBoxHash.SelectedItem.ToString();

            SetComboBox(text);
            twitter.SetHashTag(text);
        }

        private void PanelControlMainEdit1_ComboBoxHash_Leave(object sender, EventArgs e)
        {
            System.Console.WriteLine("PanelControlMainEdit1_ComboBoxHash_Leave");

            //twitter.SetHashTag(panelControlMainEdit1.comboBoxHash.Text);
            SetComboBox(panelControlMainEdit1.comboBoxHash.Text);
        }

        private void PanelControlMainEdit1_ComboBoxHash_KeyPress(object sender, KeyPressEventArgs e)
        {
            var keycode = e.KeyChar;

            if (keycode == '\r')
            {
                SetComboBox(panelControlMainEdit1.comboBoxHash.Text);
                e.Handled = true;
            }
        }



        private void SetComboBox(string txt)
        {
            System.Console.WriteLine("SetComboBox");

            foreach (var item in panelControlMainEdit1.comboBoxHash.Items)
            {
                if (item.ToString().Equals(txt))
                {
                    panelControlMainEdit1.comboBoxHash.Items.Remove(item);
                    break;
                }
            }

            panelControlMainEdit1.comboBoxHash.Items.Insert(0,txt);

            twitter.SetHashTag(txt);
            panelControlMainEdit1.comboBoxHash.Text = txt;
        }

        // CheckBoxHash

        private void PanelControlMainEdit1_CheckBoxHash_CheckedChanged(object sender, EventArgs e)
        {
            twitter.SetHashTagAdd(panelControlMainEdit1.checkBoxHash.Checked);
        }

        // TextBoxTweet

        private bool afterSend;

        private void PanelControlMainEdit1_TextBoxTweet_KeyDown(object sender, KeyEventArgs e)
        {
            var keyCode = e.KeyCode;
            var alt = e.Alt;
            var ctrl = e.Control;
            afterSend = false;

            switch (keyCode)
            {
                case Keys.Enter:
                    if (ctrl)
                    {
                        var txt = panelControlMainEdit1.textBoxTweet.Text;
                        Send_Click(txt);
                        afterSend = true;
                        e.SuppressKeyPress = true;
                        panelControlMainEdit1.textBoxTweet.Clear();
                    }
                    break;

            }
        }

        private void PanelControlMainEdit1_TextBoxTweet_KeyUp(object sender, KeyEventArgs e)
        {
            var keyCode = e.KeyCode;
            var alt = e.Alt;
            var ctrl = e.Control;

            switch (keyCode)
            {
                case Keys.Enter:
                    if (afterSend)
                    {
                        panelControlMainEdit1.textBoxTweet.Clear();
                        afterSend = false;
                    }
                    break;
            }
        }

        private void PanelControlMainEdit1_TextBoxTweet_KeyPress(object sender, KeyPressEventArgs e)
        {
            int len = panelControlMainEdit1.textBoxTweet.TextLength;
           
            if ((e.KeyChar & (char)Keys.KeyCode) == (char)Keys.LineFeed && Control.ModifierKeys == Keys.Control )
            {
                //Send_Click(panelControlMainEdit1.textBoxTweet.Text);
                //panelControlMainEdit1.textBoxTweet.Clear();
            }
        }


        // Form UI Event Handler

        private void FormTimeLine_Load(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.Open();

            twitter = new Twitter(this);
            twitter.Start();
            //twitter.startTimeLine(TimeLine.TimeLineType.TIMELINE_HOME, 0);

            cacheLvi = new Hashtable();

            int x = settings.GetValueInt(Settings.PARAM_MAINFORM_X);
            int y = settings.GetValueInt(Settings.PARAM_MAINFORM_Y);
            int w = settings.GetValueInt(Settings.PARAM_MAINFORM_W);
            int h = settings.GetValueInt(Settings.PARAM_MAINFORM_H);

            w = w != -1 ? w : 640;
            h = h != -1 ? h : 480;

            int split_h = settings.GetValueInt(Settings.PARAM_MAINFORM_SPLIT_UPDOWN);
            int sprit_w1 = settings.GetValueInt(Settings.PARAM_MAINFORM_SPLIT_UP_LEFTRIGHT);
            int sprit_w2 = settings.GetValueInt(Settings.PARAM_MAINFORM_SPLIT_DOWN_LEFTRIGHT);

            split_h = split_h != -1 ? split_h : 240;
            sprit_w1 = sprit_w1 != -1 ? sprit_w1 : 480;
            sprit_w2 = sprit_w2 != -1 ? sprit_w2 : 480;

            this.Location = new Point(x, y);
            this.Size = new Size(w, h);

            this.splitContainer1.SplitterDistance = split_h;
            this.splitContainer2.SplitterDistance = sprit_w1;
            this.splitContainer3.SplitterDistance = sprit_w2;

            for (int i = 0; i < this.panelTimeLine1.panelTimeLineList1.listView1.Columns.Count; i++)
            {
                int item_w = settings.GetValueInt(Settings.PARAM_MAINFORM_TWEETLINE_ITEM_W + string.Format("{0:D2}", i));
                item_w = item_w != -1 ? item_w : 40;

                this.panelTimeLine1.panelTimeLineList1.listView1.Columns[i].Width = item_w;
            }

            panelTimeLine1.panelTimeLineList1.listView1.SmallImageList = new ImageList();
            panelTimeLine1.panelTimeLineList1.listView1.SmallImageList.ImageSize = new Size(24, 24);

            TimeLine tl;
            tl = AddTimeLine(TimeLine.TimeLineType.TIMELINE_HOME, "HOME", true);
            tl = AddTimeLine(TimeLine.TimeLineType.TIMELINE_USER, "USER", true);
            tl.SetUserId(twitter.GetTokenUser());
            //            tl = addTimeLine(TimeLine.TimeLineType.TIMELINE_NOTIFICATION, "NOTIFICATION");
            tl = AddTimeLine(TimeLine.TimeLineType.TIMELINE_SEARCH, "SEARCH", true);
            tl = AddTimeLine(TimeLine.TimeLineType.TIMELINE_LIKE, "LIKE", true);
            //            tl = addTimeLine(TimeLine.TimeLineType.TIMELINE_MESSAGE, "MESSAGE");
            tl = AddTimeLine(TimeLine.TimeLineType.TIMELINE_MENTION, "MENTION", true);
            tl = AddTimeLine(TimeLine.TimeLineType.TIMELINE_LISTS, "LISTS", true);

            // User検索読み込み
            LoadUser();
            // Search検索読み込み
            LoadSearch();

            // List一覧取得
            var lists = twitter.GetList();
            Hashtable keys = new Hashtable();
            foreach (var lst in lists)
            {
                keys.Add(lst.Name,lst);
            }
            ArrayList sort = new ArrayList(keys.Keys);
            sort.Sort();

            foreach (var lst in sort)
            {
                tl = AddTimeLine(TimeLine.TimeLineType.TIMELINE_LISTS, ((CoreTweet.List)keys[lst]).Name + "(" + ((CoreTweet.List)keys[lst]).FullName + ")", true);
                tl.SetListId(((CoreTweet.List)keys[lst]).Id);
            }
        }

        public int PixcelCalc(int v)
        {
            return (int)(v * DpiScale);
        }

        private void FormTimeLine_Shown(object sender, EventArgs e)
        {
            TreeNode node = panelControlMainTree1.treeView1.TopNode;
            panelControlMainTree1.treeView1.SelectedNode = node;
            //panelControlMainTree1.treeView1.Focus();
            //PanelControlMainTree1_TreeView1_AfterSelect(panelControlMainTree1.treeView1, new TreeNodeMouseClickEventArgs(node, MouseButtons.Left, 1, 0, 0));
        }

        private void FormTimeLine_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings settings = new Settings();
            settings.Open();

            if (this.WindowState == FormWindowState.Normal)
            {
                settings.SetValueInt(Settings.PARAM_MAINFORM_X, this.Location.X);
                settings.SetValueInt(Settings.PARAM_MAINFORM_Y, this.Location.Y);
                settings.SetValueInt(Settings.PARAM_MAINFORM_W, this.Size.Width);
                settings.SetValueInt(Settings.PARAM_MAINFORM_H, this.Size.Height);
            }
            else
            {
                settings.SetValueInt(Settings.PARAM_MAINFORM_X, this.RestoreBounds.Location.X);
                settings.SetValueInt(Settings.PARAM_MAINFORM_Y, this.RestoreBounds.Location.Y);
                settings.SetValueInt(Settings.PARAM_MAINFORM_W, this.RestoreBounds.Size.Width);
                settings.SetValueInt(Settings.PARAM_MAINFORM_H, this.RestoreBounds.Size.Height);
            }

            settings.SetValueInt(Settings.PARAM_MAINFORM_SPLIT_UPDOWN, this.splitContainer1.SplitterDistance);
            settings.SetValueInt(Settings.PARAM_MAINFORM_SPLIT_UP_LEFTRIGHT, this.splitContainer2.SplitterDistance);
            settings.SetValueInt(Settings.PARAM_MAINFORM_SPLIT_DOWN_LEFTRIGHT, this.splitContainer3.SplitterDistance);

            for (int i = 0; i < this.panelTimeLine1.panelTimeLineList1.listView1.Columns.Count; i++)
            {
                settings.SetValueInt(Settings.PARAM_MAINFORM_TWEETLINE_ITEM_W + string.Format("{0:D2}", i),
                    this.panelTimeLine1.panelTimeLineList1.listView1.Columns[i].Width);
            }

            settings.Flash();

            SaveUser();
            SaveSearch();
        }

        // UserControl event

        private void PanelControlMainTree1_TreeView1_Click(object sender, EventArgs e)
        {
            MouseEventArgs args = (MouseEventArgs)e;
            var node = ((TreeView)sender).GetNodeAt(new Point(args.X, args.Y));

            //if (panelControlMainTree1.treeView1.SelectedNode.Parent == null) return;
            int index;
            bool parent;

            if (args.Button == MouseButtons.Right)
            {
                index = -1;
                parent = true;
                panelControlMainTree1.treeView1.SelectedNode = node;


                if (panelControlMainTree1.treeView1.SelectedNode.Parent != null)
                {
                    index = panelControlMainTree1.treeView1.SelectedNode.Parent.Index;
                    parent = false;
                }
                else
                {
                    index = panelControlMainTree1.treeView1.SelectedNode.Index;
                    parent = true;
                }

                if (index == (int)TimeLine.TimeLineType.TIMELINE_USER)
                {
                    panelControlMainTree1.treeView1.SelectedNode.ContextMenuStrip = contextMenuUser;

                    if (parent)
                    {
                        toolStripMenuDelUser.Enabled = false;
                    }
                    else
                    {
                        toolStripMenuDelUser.Enabled = true;
                    }

                    contextMenuUser.Show();
                }
                else if (index == (int)TimeLine.TimeLineType.TIMELINE_SEARCH)
                {
                    panelControlMainTree1.treeView1.SelectedNode.ContextMenuStrip = contextMenuSearch;

                    if (parent)
                    {
                        toolStripMenuChangeSearch.Enabled = false;
                        toolStripMenuDelSearch.Enabled = false;
                        toolStripMenuSetHash.Enabled = false;
                    }
                    else
                    {
                        toolStripMenuChangeSearch.Enabled = true;
                        toolStripMenuDelSearch.Enabled = true;
                        toolStripMenuSetHash.Enabled = true;
                    }

                    contextMenuSearch.Show();
                }
            }
            else if (args.Button == MouseButtons.Left)
            {
                //panelControlMainTree1.treeView1.SelectedNode = ((TreeView)sender).SelectedNode;
            }
        }

        private void PanelControlMainTree1_TreeView1_AfterSelect(object sender, EventArgs e)
        {
            TreeView args = (TreeView)sender;

            if (args.SelectedNode == null) return;

            int index = args.SelectedNode.Index;
            int subIndex = 0;

            if (args.SelectedNode.Parent != null)
            {
                subIndex = index + 1;
                index = args.SelectedNode.Parent.Index;
            }

            //panelControlMainTree1.treeView1.SelectedNode = args.SelectedNode;

            //if (oldNode != null) { oldNode.NodeFont = new Font(oldNode.NodeFont, oldNode.NodeFont.Style & FontStyle.Regular); }
            //args.Node.NodeFont = new Font(args.Node.NodeFont, args.Node.NodeFont.Style | FontStyle.Bold);

            if (oldNode != null)
            {
                //oldNode.BackColor = new Color();
                oldNode.BackColor = Color.White;
            }
            //args.Node.BackColor = new Color();
            args.SelectedNode.BackColor = Color.DarkSeaGreen;

            oldNode = args.SelectedNode;

            var tl = twitter.StartTimeLine((TimeLine.TimeLineType)index, subIndex);
            SetComboBox(tl.GetHashTag());
            panelControlMainEdit1.checkBoxHash.Checked = tl.GetHashAddTag();
        }

        private void ContextMenuUser_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem obj = (ToolStripMenuItem)sender;

            if (obj == toolStripMenuAddUser)
            {
                MakeAddUser();
            }
            else if (obj == toolStripMenuDelUser)
            {
                DelNode();
            }
        }

        private void ContextMenuSearch_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem obj = (ToolStripMenuItem)sender;

            if (obj == toolStripMenuAddSearch)
            {
                MakeAddSearch();
            }
            else if (obj == toolStripMenuChangeSearch)
            {
                MakeChangeSearch();
            }
            else if (obj == toolStripMenuDelSearch)
            {
                DelNode();
            }
            else if (obj == toolStripMenuSetHash)
            {
                SetHash();
            }
        }

        private TextBox tb;

        private void MakeAddUser()
        {
            MakeTextBox(false);

            tb.KeyDown += PanelControlMainTree1_treeView1_AddUser_Tb_KeyDown;
            //            tb.LostFocus += panelControlMainTree1_treeView1_Tb_LostFocus;
            tb.Leave += PanelControlMainTree1_treeView1_Tb_Leave;

        }

        private void MakeAddSearch()
        {
            MakeTextBox(false);

            tb.KeyDown += PanelControlMainTree1_treeView1_AddSearch_Tb_KeyDown;
            //            tb.LostFocus += panelControlMainTree1_treeView1_Tb_LostFocus;
            tb.Leave += PanelControlMainTree1_treeView1_Tb_Leave;

        }

        private void MakeChangeSearch()
        {
            MakeTextBox(true);

            tb.KeyDown += PanelControlMainTree1_treeView1_ChangeSearch_Tb_KeyDown;
            //            tb.LostFocus += panelControlMainTree1_treeView1_Tb_LostFocus;
            tb.Leave += PanelControlMainTree1_treeView1_Tb_Leave;

        }

        private void MakeTextBox(bool bText)
        {
            TreeNode node = panelControlMainTree1.treeView1.SelectedNode;

            tb = new TextBox();
            tb.Parent = panelControlMainTree1.treeView1;
            var rect = node.Bounds;
            tb.Size = new Size(panelControlMainTree1.treeView1.Size.Width - rect.Location.X - 1, rect.Size.Height);
            tb.Location = rect.Location;
            tb.Font = panelControlMainTree1.treeView1.Font;
            if (bText) tb.Text = node.Text;
            tb.Focus();
        }

        private void PanelControlMainTree1_treeView1_AddUser_Tb_KeyDown(object sender, EventArgs e)
        {
            KeyEventArgs args = (KeyEventArgs)e;

            switch (args.KeyCode)
            {
                case Keys.Enter:
                    PanelControlMainTree1_treeView1_AddUser_Tb_Enter();
                    break;
                case Keys.Escape:
                    tb.Dispose();
                    break;
            }

        }
        private void PanelControlMainTree1_treeView1_AddSearch_Tb_KeyDown(object sender, EventArgs e)
        {
            KeyEventArgs args = (KeyEventArgs)e;

            switch (args.KeyCode)
            {
                case Keys.Enter:
                    PanelControlMainTree1_treeView1_AddSearch_Tb_Enter();
                    break;
                case Keys.Escape:
                    tb.Dispose();
                    break;
            }

        }

        private void PanelControlMainTree1_treeView1_ChangeSearch_Tb_KeyDown(object sender, EventArgs e)
        {
            KeyEventArgs args = (KeyEventArgs)e;

            switch (args.KeyCode)
            {
                case Keys.Enter:
                    PanelControlMainTree1_treeView1_ChangeSearch_Tb_Enter();
                    break;
                case Keys.Escape:
                    tb.Dispose();
                    break;
            }

        }

        private void PanelControlMainTree1_treeView1_Tb_Leave(object sender, EventArgs e)
        {
            tb.Dispose();
        }

        private void PanelControlMainTree1_treeView1_AddUser_Tb_Enter()
        {
            TreeNode node = panelControlMainTree1.treeView1.SelectedNode;
            var text = tb.Text;
            //node.Text = text;

            if (text[0] != '@') text = "@" + text;

            int index;

            if (panelControlMainTree1.treeView1.SelectedNode.Parent != null)
            {
                index = panelControlMainTree1.treeView1.SelectedNode.Parent.Index;
            }
            else
            {
                index = panelControlMainTree1.treeView1.SelectedNode.Index;
            }

            var id = twitter.SearchUserId(text);
            if (id != 0)
            {
                var tl = AddTimeLine((TimeLine.TimeLineType)index, text);
                tl.SetUserId(id);
            }
            tb.Dispose();
        }

        private void PanelControlMainTree1_treeView1_AddSearch_Tb_Enter()
        {
            TreeNode node = panelControlMainTree1.treeView1.SelectedNode;
            var text = tb.Text;
            //node.Text = text;

            int index;

            if (panelControlMainTree1.treeView1.SelectedNode.Parent != null)
            {
                index = panelControlMainTree1.treeView1.SelectedNode.Parent.Index;
            }
            else
            {
                index = panelControlMainTree1.treeView1.SelectedNode.Index;
            }

            var tl = AddTimeLine((TimeLine.TimeLineType)index, text);
            tl.SetSearchStr(text);

            tb.Dispose();
        }
        private void PanelControlMainTree1_treeView1_ChangeSearch_Tb_Enter()
        {
            TreeNode node = panelControlMainTree1.treeView1.SelectedNode;
            var text = tb.Text;
            node.Text = text;

            int index = panelControlMainTree1.treeView1.SelectedNode.Parent.Index;
            int subIndex = panelControlMainTree1.treeView1.SelectedNode.Index + 1;

            var tl = twitter.SelectTimeLine((TimeLine.TimeLineType)index, subIndex);
            tl.SetSearchStr(text);
            tl.SetTabName(text);

            tb.Dispose();
        }

        private void PanelControlMainTree1_treeView1_Tb_LostFocus(object sender, EventArgs e)
        {
            tb.Dispose();
        }

        private void DelNode()
        {
            var node = panelControlMainTree1.treeView1.SelectedNode;

            var index = node.Parent.Index;
            var subNodeIndex = node.Index;

            twitter.DelSubIndex((TimeLine.TimeLineType)index, subNodeIndex + 1);

            System.Collections.IList list = panelControlMainTree1.treeView1.Nodes[index].Nodes;
            for (int i = subNodeIndex + 1; i < list.Count + 1; i++)
            {
                twitter.ChangeSubIndex((TimeLine.TimeLineType)index, i + 1, i);
            }
            panelControlMainTree1.treeView1.Nodes[index].Nodes.Remove(node);

            if (subNodeIndex > 0)
            {
                panelControlMainTree1.treeView1.SelectedNode = panelControlMainTree1.treeView1.Nodes[index].Nodes[subNodeIndex - 1];
            }
            else
            {
                panelControlMainTree1.treeView1.SelectedNode = panelControlMainTree1.treeView1.Nodes[index];
            }
        }

        private void SetHash()
        {
            var node = panelControlMainTree1.treeView1.SelectedNode;
            var search = node.Text;

            var words = search.Split(' ');
            string hash = "";

            foreach(var word in words)
            {
                if (word == "OR")
                {
                    continue;
                }
                else if (word.Substring(0,1) != "#")
                {
                    hash += " #" + word;
                }
                else
                {
                    hash += " " + word;
                }
            }

            hash.Trim(' ');
            SetComboBox(hash);
        }

        // ListView

        private void PanelTimeLine1_panelTimeLineList1_ListView1_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            var lv = panelTimeLine1.panelTimeLineList1.listView1;
            var select = lv.SelectedIndices;

            if (cacheLvi.ContainsKey(e.ItemIndex))
            {
                e.Item = (ListViewItem)cacheLvi[e.ItemIndex];

                System.Console.WriteLine("RetrieveVirtualItem Cache: {0}", e.ItemIndex);
                return;
            }

            ListViewItem lvi = new ListViewItem();
            lvi.UseItemStyleForSubItems = false;
            var status = twitter.SelectTimeLine().GetTimeLine()[e.ItemIndex];
            CoreTweet.Status line;
            Color color;
            Color bkColor;
            Font font = new Font("Meiryo UI", 10);

            if (status.RetweetedStatus != null)
            {
                line = status.RetweetedStatus;
                color = Color.DarkGreen;
            }
            else
            {
                line = status;
                color = SystemColors.WindowText;
            }

            bkColor = SystemColors.AppWorkspace;

            lvi.Text = line.Id.ToString();
            lvi.SubItems.Add(line.User.Name, color, bkColor, font);
            lvi.SubItems.Add("@" + line.User.ScreenName, color, bkColor, font);

            var txt = line.FullText.ToString();
            var decode = WebUtility.HtmlDecode(txt);
            txt = decode.Replace('\n', ' ');
            lvi.SubItems.Add(txt, color, bkColor, font);

            var time = line.CreatedAt.LocalDateTime;
            var timediff = addDateTime - time;
            string timeMsg = "";

            if (timediff.TotalSeconds < 0)
            {
                timeMsg = "0 秒";
            }
            else if (timediff.TotalSeconds < 60)
            {
                timeMsg = ((int)timediff.TotalSeconds).ToString() + " 秒";
            }
            else if (timediff.TotalSeconds < 60 * 60)
            {
                timeMsg = ((int)timediff.TotalMinutes).ToString() + " 分";
            }
            else
            {
                timeMsg = line.CreatedAt.LocalDateTime.ToString();
            }

            lvi.SubItems.Add(timeMsg, color, bkColor, font);

            lvi.ForeColor = color;
            lvi.BackColor = bkColor;

            if (line.Id == selectedTweetId)
            {
                lvi.Selected = true;
            }
            else
            { 
                lvi.Selected = false; 
            }

            e.Item = lvi;
            cacheLvi.Add(e.ItemIndex, lvi);

            System.Console.WriteLine("RetrieveVirtualItem: {0}", e.ItemIndex);
        }

        private void PanelTimeLine1_panelTimeLineList1_ListView1_SearchForVirtualItem(object sender, SearchForVirtualItemEventArgs e)
        {
        }

        private void PanelTimeLine1_panelTimeLineList1_ListView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            //e.DrawDefault = true;
            e.DrawText(TextFormatFlags.Left | TextFormatFlags.Bottom);
        }


        private void PanelTimeLine1_panelTimeLineList1_ListView1_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            var locate = e.Bounds.Location;
            var size = e.Bounds.Size;

            //e.DrawText(TextFormatFlags.Default);
        }

        private void PanelTimeLine1_panelTimeLineList1_ListView1_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            var lv = panelTimeLine1.panelTimeLineList1.listView1;
            var locate = e.Bounds.Location;
            var size = e.Bounds.Size;

            var index = e.ItemIndex;
            Brush br;

            if (e.Item.Selected)
            {
                br = SystemBrushes.ControlLight;
                e.Graphics.FillRectangle(br, new Rectangle(locate, size));
            }

            e.DrawText(TextFormatFlags.Left | TextFormatFlags.Bottom);
        }
        private void PanelTimeLine1_panelTimeLineList1_ListView1_Refresh()
        {
            panelTimeLine1.panelTimeLineList1.listView1.Items.Clear();
            SetListView();
        }

        private void PanelTimeLine1_panelTimeLineList1_ListView1_Click(object sender, EventArgs e)
        {
            ListView1_Click();
        }

        private void PanelTimeLine1_panelTimeLineList1_listView1_KeyUp(object sender, KeyEventArgs e)
        {
            ListView1_Click();
        }

        private void ListView1_Click()
        {
            ListView lv = panelTimeLine1.panelTimeLineList1.listView1;
            long tweetId;
            bool sameTweet = false;

            try
            {
                //tweetId = long.Parse(lv.SelectedItems[0].SubItems[0].Text);
                var index = lv.SelectedIndices[0];

                //if (index == 0) return;

                tweetId = twitter.SelectTimeLine().GetTimeLine()[index].Id;
                if (tweetId == selectedTweetId) sameTweet = true;
                selectedTweetId = tweetId;

                if (tweetId == 0) return;
            }
            catch
            {
                return;
            }

            var tl = twitter.GetTimeLineFromId(tweetId);

            toolStripMenuItemRetweet.Enabled = (bool)!tl.IsRetweeted;
            toolStripMenuItemUnRetweet.Enabled = (bool)tl.IsRetweeted;
            toolStripMenuItemReteetAt.Enabled = false;
            toolStripMenuItemReteetWith.Enabled = false;
            toolStripMenuItemLike.Enabled = (bool)!tl.IsFavorited;
            toolStripMenuItemUnLike.Enabled = (bool)tl.IsFavorited;
            toolStripMenuItemDel.Enabled = false;

            if (twitter.CheckSelfTweet(tweetId))
            {
                toolStripMenuItemDel.Enabled = true;
            }

            if (sameTweet) return;

            foreach (var item in controlListBox1.Items)
            {
                ((panelTimeLineContents1)item).Dispose();
            }

            controlListBox1.Items.Clear();
            TweetDraw(tl);
        }

        private async void TweetDraw(CoreTweet.Status tl,bool retFlag = false, int level = 0)
        {
            if (level > 100) return;

            //makeContents(tl);
            if (!retFlag) await Task.Run(() => MakeContents(tl));

            var ret = tl.RetweetedStatus;
            var inRep = tl.InReplyToStatusId;
            var qt = tl.QuotedStatus;

            if (ret != null)
            {
                TweetDraw(ret, true, level + 1);
            }

            if ( inRep != null)
            {
                var inRepSt = twitter.GetTimeLineFromAPI((long)inRep);
                if (inRepSt != null)
                {
                    //TweetDraw(inRepSt, false, level + 1);
                }
            }

            if (qt != null)
            {
                TweetDraw(qt, false, level + 1);
            }
        }

        private void MakeContents(CoreTweet.Status tl)
        {
            if (this.InvokeRequired)
            {
                try
                {
                    Invoke(new Action<CoreTweet.Status>(MakeContents), tl);
                }
                catch (System.ObjectDisposedException)
                {

                }
                return;
            }

            bool retweet = false;
            var rtl = tl;

            if (tl == null) return;

            if (tl.RetweetedStatus != null)
            {
                tl = tl.RetweetedStatus;
                retweet = true;
            }

            panelTimeLineContents1 contents = new panelTimeLineContents1(tl.Id);

            contents.textBoxUserId.Click += Contents_User_Click;
            contents.textBoxUserName.Click += Contents_User_Click;
            contents.pictureBox1.Click += Contents_User_Click;

            /*
            int top = 0;
            foreach(var item in customListBox1.Items)
            {
                top += ((panelTimeLineContents1)item).Height;
            }
            contents.Top = top;
            */

            Bitmap bitmap = MakeBitmapFromUrl(tl.User.ProfileImageUrlHttps);

            var userId = tl.User.ScreenName.ToString();

            contents.textBoxUserName.Text = tl.User.Name.ToString();
            contents.textBoxUserName.Tag = userId;
            contents.textBoxUserId.Text = "@" + userId;
            contents.textBoxUserId.Tag = userId;
            contents.textBoxDateTime.Text = tl.CreatedAt.LocalDateTime.ToString();

            contents.Tag = tl.Id;

            if (retweet)
            {
                contents.textBoxRetweet.Text = "RT by " + rtl.User.Name.ToString() + "(" + rtl.User.ScreenName.ToString() + ")";
                contents.textBoxRetweet.Tag = rtl.User.ScreenName.ToString();
                contents.textBoxRetweet.Click += Contents_User_Click;
                contents.textBoxRetweet.Cursor = Cursors.Hand;
            }

            //var h = (int)(contents.textBoxUserName.Font.Height * DpiScale);
            //var h = (int)(contents.textBoxUserName.Height * DpiScale);
            var h = contents.textBoxUserName.Height;
            //var h = (int)contents.textBoxUserName.Font.GetHeight(contents.textBoxUserName.CreateGraphics());


            //contents.tableLayoutPanel1.Height = h * 4;
            contents.panel1.Height = h * 4;
            //contents.tableLayoutPanel1.ColumnStyles[0].SizeType = SizeType.Absolute;
            contents.tableLayoutPanel1.ColumnStyles[0].Width = h * 4;

            //contents.pictureBox1.Width = h * 4;
            //contents.pictureBox1.Height = h * 4;

            //for (int i = 0; i < contents.tableLayoutPanel1.RowCount; i++)
            //{
            //    contents.tableLayoutPanel1.RowStyles[i].Height = h;
            //}

            contents.Width = controlListBox1.GetWidthWithoutScrollbar();
            contents.Height = h * 4;
            contents.pictureBox1.Image = bitmap;
            contents.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            contents.pictureBox1.Tag = userId;

            var cssStr = ReadCss();

            List<string> eventHandlerName = new List<string>();
            var body = twitter.MakeHtmlBody(tl, eventHandlerName);
            var html = "<html><body>" + cssStr + body + "</body></html>";

            contents.webBrowser1.Height = 0;
            contents.webBrowser1.DocumentText = html;
            contents.webBrowser1.DocumentCompleted += PanelTimeLineContents1_webBrowser_DocumentCompleted;
            contents.webBrowser1.Document.Click += PanelTimeLineContents1_webBrowser_Document_Click;
            contents.webBrowser1.Document.ContextMenuShowing += PanelTimeLineContents1_webBrowser_Document_ContextMenuShowing;
            contents.webBrowser1.Document.MouseMove += PanelTimeLineContents1_webBrowser_Document_MouseMove;
            contents.webBrowser1.Document.MouseDown += PanelTimeLineContents1_webBrowser_Document_MouseDown;

            contents.webBrowser1.IsWebBrowserContextMenuEnabled = false;
            contents.webBrowser1.ContextMenuStrip = contextMenuForWebView;

            foreach (var obj in contextMenuForWebView.Items)
            {
                if (obj.GetType() == typeof(ToolStripMenuItem))
                    ((ToolStripMenuItem)obj).Click += ContextMenuForWebView_Click;
            }

//            listBoxTweetContents.Controls.Add(contents);
            controlListBox1.Add(contents);
            contents.Parent = (Control)controlListBox1;

            //contents.Dock = DockStyle.Fill;
            contents.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            contents.Left = 0;
            contents.Top = 0;
            //contents.Width = controlListBox1.ClientSize.Width;

            //Panel dp = new Panel();
            //dp.Dock = DockStyle.Fill;
            //dp.Height = (int)(listBoxTweetContents.Height);
            //listBoxTweetContents.Controls.Add(dp);
            //listBoxTweetContents.Items.Add("");

            //            listBoxTweetContents.Controls.SetChildIndex(contents, listBoxTweetContents.Items.Count - 1);

            //            listBoxTweetContents.Update();
        }

        private Bitmap MakeBitmapFromUrl(string url)
        {
            WebClient webClient = new WebClient();
            Stream stream = null;
            try
            {
                stream = webClient.OpenRead(url);
            }
            catch(System.Net.WebException e)
            {
                SetStatusMenu(e.Message);
                return null;
            }

            Bitmap bitmap = new Bitmap(stream);
            stream.Close();

            return bitmap;
        }

        private void Contents_User_Click(object sender, EventArgs e)
        {
            var user = ((Control)sender).Tag.ToString();
            var tl = AddTimeLine(TimeLine.TimeLineType.TIMELINE_USER, "@" + user);
            tl.SetUserId(twitter.SearchUserId(user));
        }

        private string ReadCss()
        {
            string exeFile = Application.StartupPath;
            var cssFileName = exeFile + "\\css.ini";

            string cssStr;

            try {
                StreamReader sr = new StreamReader(cssFileName, Encoding.UTF8);

                cssStr = sr.ReadToEnd();

            }
            catch(System.IO.FileNotFoundException)
            {
                return "";
            }

            return cssStr;
        }

        private string contentTagName = "";
        private string contentUrl = "";
        private string contentAlt = "";
        private string contentTxt = "";

        private void PanelTimeLineContents1_webBrowser_Document_ContextMenuShowing(object sender, HtmlElementEventArgs e)
        {
            HtmlElement clickedElement = ((HtmlDocument)sender).GetElementFromPoint(e.MousePosition);
            contentTagName = clickedElement.TagName.ToLower();

            if (contentTagName == "a")
            {
                contentUrl = clickedElement.GetAttribute("href");
                contentAlt = clickedElement.GetAttribute("alt");
                contentTxt = clickedElement.InnerText.ToString();

                if (contentTxt.Substring(0, 1) == "#")
                {
                    toolStripMenuWebViewOpen.Enabled = false;
                    toolStripMenuWebViewSearch.Enabled = true;
                    toolStripMenuWebViewAddHash.Enabled = true;
                    toolStripMenuWebViewCopy.Enabled = true;
                    e.ReturnValue = true;
                }
                else if (contentTxt.Substring(0, 1) == "@")
                {
                    toolStripMenuWebViewOpen.Enabled = false;
                    toolStripMenuWebViewSearch.Enabled = true;
                    toolStripMenuWebViewAddHash.Enabled = false;
                    toolStripMenuWebViewCopy.Enabled = true;
                    e.ReturnValue = true;
                }
                else if (contentUrl.Substring(0,4) == "http")
                {
                    toolStripMenuWebViewOpen.Enabled = true;
                    toolStripMenuWebViewSearch.Enabled = false;
                    toolStripMenuWebViewAddHash.Enabled = false;
                    toolStripMenuWebViewCopy.Enabled = true;
                    e.ReturnValue = true;
                }
            }
            else if (contentTagName == "img")
            {
                toolStripMenuWebViewOpen.Enabled = true;
                toolStripMenuWebViewSearch.Enabled = false;
                toolStripMenuWebViewAddHash.Enabled = false;
                toolStripMenuWebViewCopy.Enabled = true;
                e.ReturnValue = true;
            }
            else 
            {
                toolStripMenuWebViewOpen.Enabled = false;
                toolStripMenuWebViewSearch.Enabled = false;
                toolStripMenuWebViewAddHash.Enabled = false;
                toolStripMenuWebViewCopy.Enabled = false;
                e.ReturnValue = false;
            }
        }

        private void ContextMenuForListView_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem obj = (ToolStripMenuItem)sender;

            //var lv = panelTimeLine1.panelTimeLineList1.listView1.SelectedItems[0].SubItems[0].Text.ToString();
            var lv = panelTimeLine1.panelTimeLineList1.listView1;
            var index = lv.SelectedIndices[0];

            //if (index == 0) return;

            var tweetId = twitter.SelectTimeLine().GetTimeLine()[index].Id;

            if (obj == toolStripMenuItemRetweet)
            {
                if (MessageBox.Show(this, "Retweetしますか？", "Retweet", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    twitter.Retweet(tweetId);
                    twitter.GetTimeLine(tweetId);
                    PanelTimeLine1_panelTimeLineList1_ListView1_Refresh();
                }
            }
            else if (obj == toolStripMenuItemUnRetweet)
            {
                if (MessageBox.Show(this, "Retweetを取り消ししますか？", "Cancel Retweet", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    twitter.UnRetweet(tweetId);
                    twitter.GetTimeLine(tweetId);
                    PanelTimeLine1_panelTimeLineList1_ListView1_Refresh();
                }
            }
            else if (obj == toolStripMenuItemLike)
            {
                if (MessageBox.Show(this, "Favoriteしますか？", "Favorite", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    twitter.Like(tweetId);
                    twitter.GetTimeLine(tweetId);
                    PanelTimeLine1_panelTimeLineList1_ListView1_Refresh();
                }
            }
            else if (obj == toolStripMenuItemUnLike)
            {
                if (MessageBox.Show(this, "Favoriteを取り消ししますか？", "Cancel Favorite", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    twitter.UnLike(tweetId);
                    twitter.GetTimeLine(tweetId);
                    PanelTimeLine1_panelTimeLineList1_ListView1_Refresh();
                }
            }
            else if (obj == toolStripMenuItemUser)
            {
                var status = twitter.GetTimeLineFromId(tweetId);
                if (status.RetweetedStatus != null) status = status.RetweetedStatus;
                var userName = status.User.ScreenName;
                var userId = status.User.Id;

                var tl = AddTimeLine(TimeLine.TimeLineType.TIMELINE_USER, "@" + userName);
                tl.SetUserId((long)userId);
            }
            else if (obj == toolStripMenuItemOpen)
            {
                var status = twitter.GetTimeLineFromId(tweetId);
                string url = "https://twitter.com/" + status.User.ScreenName + "/status/" + tweetId.ToString();
                OpenUrl(url);
            }
            else if (obj == toolStripMenuItemDel)
            {
                if (MessageBox.Show(this, "Tweetを削除しますか？", "Delete Tweet", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    twitter.DeleteTweet(tweetId);
                    PanelTimeLine1_panelTimeLineList1_ListView1_Refresh();
                }
            }
        }


        // WebBrowser

        private string beforeContentUrl = "";
        private ToolTip contentToolTip = null;

        private void PanelTimeLineContents1_webBrowser_Document_MouseMove(object sender, HtmlElementEventArgs e)
        {
            HtmlElement clickedElement = ((HtmlDocument)sender).GetElementFromPoint(e.MousePosition);
            contentTagName = clickedElement.TagName.ToLower();

            if (contentTagName == "a")
            {
                contentUrl = clickedElement.GetAttribute("href");
                contentAlt = clickedElement.GetAttribute("alt");
                contentTxt = clickedElement.InnerText.ToString();
                if (contentTxt.Substring(0, 1) != "#" && contentTxt.Substring(0, 1) != "@")
                {
                    SetStatusMenu(contentUrl);
                    beforeContentUrl = contentUrl;
                }
                else 
                { 
                    SetStatusMenu("");
                    beforeContentUrl = "";
                }

//                contentToolTip?.Dispose();
//                contentToolTip = null;
            }
            else if (contentTagName == "img")
            {
                contentUrl = clickedElement.GetAttribute("src");
                SetStatusMenu(contentUrl);
/*
                if (contentUrl != beforeContentUrl)
                {
                    contentToolTip?.Dispose();
                    contentToolTip = new ToolTip();
                    Bitmap bmp = MakeBitmapFromUrl(contentUrl);
                    PictureBox pb = new PictureBox();
                    pb.Image = bmp;

                    //contentToolTip.ShowAlways = true;
                    //contentToolTip.SetToolTip(this, contentUrl);
                    var x = ((HtmlDocument)sender).Window.Position.X;
                    var y = ((HtmlDocument)sender).Window.Position.Y;

                    contentToolTip.Show(contentUrl, this, new Point(x,y));
                }
*/
                beforeContentUrl = contentUrl;
            }
            else 
            {
                SetStatusMenu("");
                beforeContentUrl = "";

//                contentToolTip?.Dispose();
//                contentToolTip = null;
            }
        }

        private void PanelTimeLineContents1_webBrowser_Document_MouseDown(object sender, HtmlElementEventArgs e)
        {
        }

        private void PanelTimeLineContents1_webBrowser_Document_Click(object sender, HtmlElementEventArgs e)
        {
            HtmlElement clickedElement = ((HtmlDocument)sender).GetElementFromPoint(e.MousePosition);
            contentTagName = clickedElement.TagName.ToLower();

            if (contentTagName == "a")
            {
                contentUrl = clickedElement.GetAttribute("href");
                contentAlt = clickedElement.GetAttribute("alt");
                contentTxt = clickedElement.InnerText.ToString();

                if (contentTxt.Substring(0, 1) == "#")
                {
                    var tl = AddTimeLine(TimeLine.TimeLineType.TIMELINE_SEARCH, contentTxt);
                    tl.SetSearchStr(contentTxt);
                }
                else if (contentTxt.Substring(0, 1) == "@")
                {
                    var tl = AddTimeLine(TimeLine.TimeLineType.TIMELINE_USER, contentTxt);
                    tl.SetUserId(long.Parse(contentAlt));
                }
                else
                {
                    OpenUrl(contentUrl);
                }
            }
            else if (contentTagName == "img")
            {
                contentUrl = clickedElement.GetAttribute("src");
                OpenUrl(contentUrl);
            }

            e.ReturnValue = false;
        }

        private void ContextMenuForWebView_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem obj = (ToolStripMenuItem)sender;

            if (obj == toolStripMenuWebViewOpen)
            {
                if (contentTagName == "a")
                {
                    var tl = AddTimeLine(TimeLine.TimeLineType.TIMELINE_SEARCH, contentTxt);
                    tl.SetSearchStr(contentTxt);
                }
                else if (contentTagName == "img")
                {
                    OpenUrl(contentUrl);
                }
                else
                {
                    var tweetId = (long)((Control)sender).Tag;
                    var status = twitter.GetTimeLineFromId(tweetId);
                    string url = "https://twitter.com/" + status.User.ScreenName + "/status/" + tweetId.ToString();
                    OpenUrl(url);
                }
            }
            else if (obj == toolStripMenuWebViewAddHash)
            {
                //panelControlMainEdit1.comboBoxHash.Text = contentTxt;
                //panelControlMainEdit1.comboBoxHash.Focus();
                //this.Focus();

                SetComboBox(contentTxt);
            }
            else if (obj == toolStripMenuWebViewSearch)
            {
                if (contentTxt.Substring(0, 1) == "#")
                {
                    var tl = AddTimeLine(TimeLine.TimeLineType.TIMELINE_SEARCH, contentTxt);
                    tl.SetSearchStr(contentTxt);
                }
                else if (contentTxt.Substring(0, 1) == "@")
                { 
                    var tl = AddTimeLine(TimeLine.TimeLineType.TIMELINE_USER, contentTxt);
                    tl.SetUserId(long.Parse(contentAlt));
                }
            }
            else if (obj == toolStripMenuWebViewCopy)
            {
                if (contentTagName == "a")
                {
                    if (contentTxt.Substring(0, 1) == "#")
                    {
                        Clipboard.SetText(contentTxt);
                    }
                    else if (contentTxt.Substring(0, 1) == "@")
                    {
                        Clipboard.SetText(contentTxt);
                    }
                }
                else if (contentTagName == "img")
                {
                    Clipboard.SetText(contentUrl);
                }   
            }
        }

        private void PanelTimeLineContents1_webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            foreach (var i in controlListBox1.Items)
            {
                if (i.GetType().Equals(typeof(panelTimeLineContents1)))
                {
                    var hTable = ((panelTimeLineContents1)i).tableLayoutPanel1.Height;
                    var hWeb = ((panelTimeLineContents1)i).webBrowser1.Document.Body.ScrollRectangle.Height;
                    //((panelTimeLineContents1)i).Height = h;
                    i.Height = hTable + hWeb;

                    Console.WriteLine("DocumentCompleted: hSplit {0}", hTable);
                    Console.WriteLine("DocumentCompleted: hWeb {0}", hWeb);
                }
                else
                {
                    ((Panel)i).Height = controlListBox1.Height;
                }
            }
            controlListBox1.RePlace();
            controlListBox1.Refresh();


        }


        private void ListBoxTweetContents_MeasureItem1(object sender, MeasureItemEventArgs e)
        {
            if (controlListBox1.Items[e.Index].GetType().Equals(typeof(panelTimeLineContents1)))
            {
                var h = ((panelTimeLineContents1)controlListBox1.Items[e.Index]).Height;
                e.ItemHeight = h;
                Console.WriteLine("MeasureItem: {0}", h);
            }
            else
            {
                var h = ((Panel)controlListBox1.Controls[e.Index]).Height;
                e.ItemHeight = h;
            }

        }


        private void ListBoxTweetContents_DrawItem(object sender, DrawItemEventArgs e)
        {
            //var c = (PanelTimeLineContents)listBoxTweetContents.Items[e.Index];
            //c.Update();
        }

        private void ListBoxTweetContents_DrawItem_1(object sender, DrawItemEventArgs e)
        {
            e.DrawFocusRectangle();
        }

        private void ControlListBox1_Resize(object sender, EventArgs e)
        {
            ContentsResize();
        }

        private void ContentsResize()
        {
            foreach (var item in controlListBox1.Items)
            {
                item.Width = controlListBox1.GetWidthWithoutScrollbar();
            }
        }


        private void PanelControlMainEdit1_ButtonSend_Click(object sender, EventArgs e)
        {
            Send_Click(panelControlMainEdit1.textBoxTweet.Text);
            panelControlMainEdit1.textBoxTweet.Clear();
        }

        private void Send_Click(string txt)
        {
            //var txt = panelControlMainEdit1.textBoxTweet.Text;
            var len = txt.Length;

            if (panelControlMainEdit1.checkBoxHash.Checked)
            {
                txt += " " + panelControlMainEdit1.comboBoxHash.Text;
            }

            if (len > 0)
            {
                twitter.Send(txt);
                //panelControlMainEdit1.textBoxTweet.Clear();
            }
        }

        private void PanelControlMainEdit1_ButtonClear_Click(object sender, EventArgs e)
        {
            panelControlMainEdit1.textBoxTweet.Clear();
        }

        private void FormTimeLine_ResizeEnd(object sender, EventArgs e)
        {
            this.ResumeLayout();
            this.Invalidate();
            this.PerformLayout();
        }

        private void FormTimeLine_ResizeBegin(object sender, EventArgs e)
        {
            this.SuspendLayout();
        }

        public void SetStatusMenu(string msg)
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action<string>(SetStatusMenu), msg);
                return;
            }

            if (statusStrip1.Items[0].Text == msg) return;

            statusStrip1.Items[0].Text = msg;
        }

        public TimeLine AddTimeLine(TimeLine.TimeLineType ty, string tabName, bool init = false)
        {
            for (int i = 0; i < panelControlMainTree1.treeView1.Nodes.Count; i++)
            {
                var node = panelControlMainTree1.treeView1.Nodes[i];

                if ((TimeLine.TimeLineType)node.Tag == ty)
                {
                    int j = 0;
                    for (j = 0; j < node.Nodes.Count; j++)
                    {
                        if (node.Nodes[j].Text == tabName)
                        {
                            panelControlMainTree1.treeView1.SelectedNode = node.Nodes[j];
                            return twitter.SelectTimeLine(ty, tabName);
                        }
                    }

                    var childTl = twitter.AddTimeLine(ty, tabName);
                    childTl.SetSubIndex(j + 1);
               
                    var childNode = panelControlMainTree1.treeView1.Nodes[i].Nodes.Add(tabName);
                    childNode.Tag = ty;
                    if (!init) panelControlMainTree1.treeView1.SelectedNode = childNode;

                    return childTl;
                }
            }

            var treenode = panelControlMainTree1.treeView1.Nodes.Add(tabName);
            treenode.Tag = ty;

            var tl = twitter.AddTimeLine(ty, tabName);
            return tl;
        }

        // Setting

        public void SaveUser()
        {
            List<List<string>> users = new List<List<string>>();
            Hashtable forSort = new Hashtable();
            List<string> param;

            foreach (var tl in twitter.GetTimeLineList())
            {
                if (tl.GetType() == TimeLine.TimeLineType.TIMELINE_USER && tl.GetSubIndex() != 0)
                {
                    param = tl.saveParam();

                    //users.Add(param);
                    forSort.Add(tl.GetSubIndex(), param);
                }
            }

            ArrayList keys = new ArrayList(forSort.Keys);
            keys.Sort();

            foreach (var key in keys)
            {
                users.Add((List<string>)forSort[key]);
            }

            ListSettings ls = new ListSettings(ListSettings.USER_FILE);

            ls.Write(users);
        }

        public void LoadUser()
        {
            ListSettings ls = new ListSettings(ListSettings.USER_FILE);
            List<List<string>> users = ls.Read();

            foreach (var user in users)
            {
                var tl = AddTimeLine(TimeLine.TimeLineType.TIMELINE_USER, user[0], true);
                tl.loadParam(user);
            }
        }

        public void SaveSearch()
        {
            List<List<string>> users = new List<List<string>>();
            Hashtable forSort = new Hashtable();
            List<string> param;

            foreach (var tl in twitter.GetTimeLineList())
            {
                if (tl.GetType() == TimeLine.TimeLineType.TIMELINE_SEARCH && tl.GetSubIndex() != 0)
                {
                    param = tl.saveParam();

                    //users.Add(param);
                    forSort.Add(tl.GetSubIndex(), param);
                }
            }

            ArrayList keys = new ArrayList(forSort.Keys);
            keys.Sort();

            foreach (var key in keys)
            {
                users.Add((List<string>)forSort[key]);
            }

            ListSettings ls = new ListSettings(ListSettings.SEARCH_FILE);

            ls.Write(users);
        }

        public void LoadSearch()
        {
            ListSettings ls = new ListSettings(ListSettings.SEARCH_FILE);
            List<List<string>> searches = ls.Read();

            foreach (var search in searches)
            {
                var tl = AddTimeLine(TimeLine.TimeLineType.TIMELINE_SEARCH, search[0], true);
                tl.loadParam(search);
            }
        }

        public void SetListView()
        {
            if (this.InvokeRequired)
            {
                try
                {
                    Invoke(new Action(SetListView));
                }
                catch (System.ObjectDisposedException)
                {

                }
                return;
            }

            panelTimeLine1.panelTimeLineList1.listView1.BeginUpdate();

            //SetListViewItem(status);
            cacheLvi.Clear();
            panelTimeLine1.panelTimeLineList1.listView1.VirtualListSize = twitter.SelectTimeLine().GetTimeLine().Count;
            selectedTweetId = 0;

            panelTimeLine1.panelTimeLineList1.listView1.EndUpdate();

            addDateTime = DateTime.Now.ToLocalTime();
        }

        public void SetListViewNew()
        {
            if (this.InvokeRequired)
            {
                try
                {
                    Invoke(new Action(SetListViewNew));
                }
                catch(System.ObjectDisposedException)
                {

                }
                return;
            }

            if (twitter.SelectTimeLine().GetNewTimeLine().Count == 0) return;
            
            var oldTop = panelTimeLine1.panelTimeLineList1.listView1.TopItem;
            var oldTopIndex = 0;

            if (oldTop != null)
            {
                oldTopIndex = panelTimeLine1.panelTimeLineList1.listView1.TopItem.Index;
            }

            panelTimeLine1.panelTimeLineList1.listView1.BeginUpdate();

            //SetListViewItem(status);
            cacheLvi.Clear();
            panelTimeLine1.panelTimeLineList1.listView1.VirtualListSize = twitter.SelectTimeLine().GetTimeLine().Count;

            if (oldTopIndex != 0)
            {
                panelTimeLine1.panelTimeLineList1.listView1.EnsureVisible(twitter.SelectTimeLine().GetTimeLine().Count -1);
                panelTimeLine1.panelTimeLineList1.listView1.EnsureVisible(oldTopIndex + twitter.SelectTimeLine().GetNewTimeLine().Count);
            }
            else
            {
                panelTimeLine1.panelTimeLineList1.listView1.EnsureVisible(0);
            }

            var sel = panelTimeLine1.panelTimeLineList1.listView1.SelectedIndices;
            if (sel.Count != 0)
            {
                var index = sel[0];
                index += twitter.SelectTimeLine().GetNewTimeLine().Count();
                panelTimeLine1.panelTimeLineList1.listView1.SelectedIndices.Clear();
                panelTimeLine1.panelTimeLineList1.listView1.SelectedIndices.Add(index);
            }

            panelTimeLine1.panelTimeLineList1.listView1.EndUpdate();

            addDateTime = DateTime.Now.ToLocalTime();
        }

        private void SetListViewItem(List<CoreTweet.Status> status)
        {
            /*
            Color color;

            foreach (var sel in status)
            {
                ListViewItem items = new ListViewItem();
                var line = sel;

                if (sel.RetweetedStatus != null)
                {
                    line = sel.RetweetedStatus;
                    color = Color.DarkGreen;
                }
                else
                {
                    line = sel;
                    color = SystemColors.WindowText;
                }

                items.Text = sel.Id.ToString();
                //items.SubItems.Add(line.Id.ToString());
                items.SubItems.Add(line.User.Name);
                items.SubItems.Add(line.User.ScreenName);

                var txt = line.FullText.ToString();
                var decode = WebUtility.HtmlDecode(txt);
                txt = decode.Replace('\n', ' ');
                items.SubItems.Add(txt);

                items.SubItems.Add(line.CreatedAt.ToLocalTime().ToString());

                var item = panelTimeLine1.panelTimeLineList1.listView1.Items.Add(items);
                item.ForeColor = color;
            }
            */

            cacheLvi.Clear();

            panelTimeLine1.panelTimeLineList1.listView1.VirtualListSize = twitter.SelectTimeLine().GetTimeLine().Count;
        }

        public void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                url = url.Replace("&", "^&");
                Process.Start(new ProcessStartInfo("cmd", "/c start " + url) { CreateNoWindow = true });
            }
        }
    }
}
