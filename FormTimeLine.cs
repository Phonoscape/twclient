using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using twclient.UserPanel;
using static twclient.UserPanel.PanelTimeLineList;

namespace twclient
{
    public partial class FormTimeLine : Form
    {
        private Twitter twitter;

        private TreeNode oldNode;

        private Hashtable cacheLvi;
        private long selectedTweetId = 0;
        private DateTime addDateTime;

        const int MAXIMAGE = 4;
        private Bitmap[] tweetImage;

        private Hashtable userImage;
        private ArrayList imageGetWaitList;

        private FormPopupPicture formPic = null;

        const int MAX_CONTENTS_LEVEL = 50;
        static readonly float DpiScale = ((new System.Windows.Forms.Form()).CreateGraphics().DpiX) / 96;

        public FormTimeLine()
        {
            InitializeComponent();

            // UserControl Event
            panelControlMainTree1.treeView1.AfterSelect += PanelControlMainTree1_TreeView1_AfterSelect;
            panelControlMainTree1.treeView1.Click += PanelControlMainTree1_TreeView1_Click;

            panelControlMainTree1.treeView1.AllowDrop = true;
            panelControlMainTree1.treeView1.ItemDrag += PanelControlMainTree1_TreeView1_ItemDrag;
            panelControlMainTree1.treeView1.DragEnter += PanelControlMainTree1_TreeView1_DragEnter;
            panelControlMainTree1.treeView1.DragDrop += PanelControlMainTree1_TreeView1_DragDrop;
            panelControlMainTree1.treeView1.DragOver += PanelControlMainTree1_TreeView1_DragOver;

            panelTimeLineList1.listView1.Click += PanelTimeLine1_panelTimeLineList1_ListView1_Click;
            panelTimeLineList1.listView1.KeyUp += PanelTimeLine1_panelTimeLineList1_ListView1_KeyUp;
            panelTimeLineList1.listView1.RetrieveVirtualItem += PanelTimeLine1_panelTimeLineList1_ListView1_RetrieveVirtualItem;
            panelTimeLineList1.listView1.DrawItem += PanelTimeLine1_panelTimeLineList1_ListView1_DrawItem;
            panelTimeLineList1.listView1.DrawSubItem += PanelTimeLine1_panelTimeLineList1_ListView1_DrawSubItem;
            panelTimeLineList1.listView1.DrawColumnHeader += PanelTimeLine1_panelTimeLineList1_ListView1_DrawColumnHeader;
            panelTimeLineList1.listView1.SearchForVirtualItem += PanelTimeLine1_panelTimeLineList1_ListView1_SearchForVirtualItem;

            panelTimeLineList1.listView1.ContextMenuStrip = contextMenuForListView;
            panelTimeLineList1.listView1.VirtualListSize = 0;
            panelTimeLineList1.listView1.VirtualMode = true;
            panelTimeLineList1.listView1.Sorting = SortOrder.Descending;
            panelTimeLineList1.listView1.OwnerDraw = true;

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

            panelControlMainEdit1.textBoxMsg1.Tag = Twitter.TweetType.Normal;
            panelControlMainEdit1.textBoxMsg2.Tag = 0;

            panelControlMainEdit1.pictureBox1.AllowDrop = true;
            panelControlMainEdit1.pictureBox2.AllowDrop = true;
            panelControlMainEdit1.pictureBox3.AllowDrop = true;
            panelControlMainEdit1.pictureBox4.AllowDrop = true;

            panelControlMainEdit1.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            panelControlMainEdit1.pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            panelControlMainEdit1.pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            panelControlMainEdit1.pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;

            panelControlMainEdit1.pictureBox1.DragEnter += PanelControlMainEdit1_PictureBox_DragEnter;
            panelControlMainEdit1.pictureBox2.DragEnter += PanelControlMainEdit1_PictureBox_DragEnter;
            panelControlMainEdit1.pictureBox3.DragEnter += PanelControlMainEdit1_PictureBox_DragEnter;
            panelControlMainEdit1.pictureBox4.DragEnter += PanelControlMainEdit1_PictureBox_DragEnter;

            panelControlMainEdit1.pictureBox1.DragDrop += PanelControlMainEdit1_PictureBox_DragDrop;
            panelControlMainEdit1.pictureBox2.DragDrop += PanelControlMainEdit1_PictureBox_DragDrop;
            panelControlMainEdit1.pictureBox3.DragDrop += PanelControlMainEdit1_PictureBox_DragDrop;
            panelControlMainEdit1.pictureBox4.DragDrop += PanelControlMainEdit1_PictureBox_DragDrop;

            panelControlMainEdit1.pictureBox1.MouseMove += PanelControlMainEdit1_PictureBox1_MouseMove;
            panelControlMainEdit1.pictureBox2.MouseMove += PanelControlMainEdit1_PictureBox1_MouseMove;
            panelControlMainEdit1.pictureBox3.MouseMove += PanelControlMainEdit1_PictureBox1_MouseMove;
            panelControlMainEdit1.pictureBox4.MouseMove += PanelControlMainEdit1_PictureBox1_MouseMove;

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

            tweetImage = new Bitmap[MAXIMAGE];
            userImage = new Hashtable();
            imageGetWaitList = new ArrayList();
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

            panelControlMainEdit1.comboBoxHash.Items.Insert(0, txt);

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

                        panelControlMainEdit1.Enabled = false;
                        Send_Click(txt);
                        panelControlMainEdit1.Enabled = true;

                        afterSend = true;
                        e.SuppressKeyPress = true;
                        panelControlMainEdit1.textBoxTweet.Clear();
                        panelControlMainEdit1.textBoxTweet.Focus();

                        panelControlMainEdit1.textBoxMsg1.Tag = Twitter.TweetType.Normal;

                        panelControlMainEdit1.textBoxMsg1.Clear();
                        panelControlMainEdit1.textBoxMsg2.Clear();
                    }
                    break;
                case Keys.V:
                    if (ctrl)
                    {
                        ImageFromClipboard();
                    }
                    break;

            }
        }

        private void ImageFromClipboard()
        {
            var img = Clipboard.GetImage();

            if (img != null)
            {
                SetPictureBox(img);
            }
        }

        private void SetPictureBox(Image img)
        {
            PictureBox pb = null;
            int no = 0;

            if (tweetImage[0] == null)
            {
                pb = panelControlMainEdit1.pictureBox1;
                no = 1;
            }
            else if (tweetImage[1] == null)
            {
                pb = panelControlMainEdit1.pictureBox2;
                no = 2;
            }
            else if (tweetImage[2] == null)
            {
                pb = panelControlMainEdit1.pictureBox3;
                no = 3;
            }
            else if (tweetImage[3] == null)
            {
                pb = panelControlMainEdit1.pictureBox4;
                no = 4;
            }

            if (pb != null && img != null)
            {
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                img.Tag = null;
                pb.Image = img;
                tweetImage[no - 1] = (Bitmap)img;
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

            if ((e.KeyChar & (char)Keys.KeyCode) == (char)Keys.LineFeed && Control.ModifierKeys == Keys.Control)
            {
                //Send_Click(panelControlMainEdit1.textBoxTweet.Text);
                //panelControlMainEdit1.textBoxTweet.Clear();
            }
        }

        private void PanelControlMainEdit1_TextBoxMsg2_Click(object sender, EventArgs e)
        {
            ListView1_Click(long.Parse(panelControlMainEdit1.textBoxMsg2.Tag.ToString()));
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

            for (int i = 0; i < this.panelTimeLineList1.listView1.Columns.Count; i++)
            {
                int item_w = settings.GetValueInt(Settings.PARAM_MAINFORM_TWEETLINE_ITEM_W + string.Format("{0:D2}", i));
                item_w = item_w != -1 ? item_w : 40;

                this.panelTimeLineList1.listView1.Columns[i].Width = item_w;
            }

            panelTimeLineList1.listView1.SmallImageList = new ImageList();
            panelTimeLineList1.listView1.SmallImageList.ImageSize = new Size(24, 24);

            TimeLine tl;
            tl = AddTimeLine(TimeLine.TimeLineType.TIMELINE_HOME, Resource.Resource1.String_FormTimeLine_TimelineName_HOME, true);
            tl = AddTimeLine(TimeLine.TimeLineType.TIMELINE_USER, Resource.Resource1.String_FormTimeLine_TimelineName_USER, true);
            tl.SetUserId(twitter.GetTokenUser());
            //            tl = addTimeLine(TimeLine.TimeLineType.TIMELINE_NOTIFICATION, "NOTIFICATION");
            tl = AddTimeLine(TimeLine.TimeLineType.TIMELINE_SEARCH, Resource.Resource1.String_FormTimeLine_TimelineName_SEARCH, true);
            tl = AddTimeLine(TimeLine.TimeLineType.TIMELINE_LIKE, Resource.Resource1.String_FormTimeLine_TimelineName_LIKE, true);
            //            tl = addTimeLine(TimeLine.TimeLineType.TIMELINE_MESSAGE, "MESSAGE");
            tl = AddTimeLine(TimeLine.TimeLineType.TIMELINE_MENTION, Resource.Resource1.String_FormTimeLine_TimelineName_MENTION, true);
            tl = AddTimeLine(TimeLine.TimeLineType.TIMELINE_LISTS, Resource.Resource1.String_FormTimeLine_TimelineName_LISTS, true);

            // User検索読み込み
            LoadUser();
            // Search検索読み込み
            LoadSearch();

            // List一覧取得
            var lists = twitter.GetList();
            Hashtable keys = new Hashtable();
            foreach (var lst in lists)
            {
                keys.Add(lst.Name, lst);
            }
            ArrayList sort = new ArrayList(keys.Keys);
            sort.Sort();

            foreach (var lst in sort)
            {
                tl = AddTimeLine(TimeLine.TimeLineType.TIMELINE_LISTS, ((CoreTweet.List)keys[lst]).Name + "(" + ((CoreTweet.List)keys[lst]).FullName + ")", true);
                tl.SetListId(((CoreTweet.List)keys[lst]).Id);
            }

            panelControlMainTree1.treeView1.ExpandAll();
        }

        public int PixcelCalc(int v)
        {
            return (int)(v * DpiScale);
        }

        private void FormTimeLine_Shown(object sender, EventArgs e)
        {
            TreeNode node = panelControlMainTree1.treeView1.Nodes[0];
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

            for (int i = 0; i < this.panelTimeLineList1.listView1.Columns.Count; i++)
            {
                settings.SetValueInt(Settings.PARAM_MAINFORM_TWEETLINE_ITEM_W + string.Format("{0:D2}", i),
                    this.panelTimeLineList1.listView1.Columns[i].Width);
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

        private void PanelControlMainTree1_TreeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            var node = (TreeNode)e.Item;

            if (node.Parent != null)
            {
                if (node.Parent == panelControlMainTree1.treeView1.Nodes[(int)TimeLine.TimeLineType.TIMELINE_USER] ||
                    node.Parent == panelControlMainTree1.treeView1.Nodes[(int)TimeLine.TimeLineType.TIMELINE_SEARCH])
                {
                    panelControlMainTree1.treeView1.SelectedNode = node;
                    DoDragDrop(node, DragDropEffects.Move);
                }
            }
        }

        private void PanelControlMainTree1_TreeView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void PanelControlMainTree1_TreeView1_DragOver(object sender, DragEventArgs e)
        {
            var tv = (TreeView)sender;

            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                var node = tv.GetNodeAt(tv.PointToClient(new Point(e.X, e.Y)));

                if (node == null)
                {
                    return;
                }

                TreeNode topNode = null;
                TreeNode wn = node;
                while (wn.IsVisible)
                {
                    topNode = (TreeNode)wn.Clone();
                    wn = wn.PrevNode;
                    if (wn == null)
                    {
                        topNode = null;
                        break;
                    }
                }

                TreeNode bottomNode = node;
                wn = node;
                while (wn.IsVisible)
                {
                    bottomNode = (TreeNode)wn.Clone();
                    wn = wn.NextNode;
                    if (wn == null)
                    {
                        bottomNode = null;
                        break;
                    }
                }

                //Debug.WriteLine(node.Text);
                //Debug.WriteLine(topNode.Text);
                //Debug.WriteLine(bottomNode.Text);

                if (topNode != null)
                {
                    if (node.Text == topNode.Text)
                    {
                        var ty = node.Parent?.Tag;

                        if (ty != null)
                        {
                            var subIndex = panelControlMainTree1.treeView1.Nodes[(int)ty].Nodes.IndexOfKey(topNode.Name);

                            if (subIndex > 0)
                            {
                                panelControlMainTree1.treeView1.Nodes[(int)ty].Nodes[subIndex - 1].EnsureVisible();
                            }
                        }
                    }
                }

                if (bottomNode != null)
                {
                    if (node.Text == bottomNode.Text)
                    {
                        var ty = node.Parent?.Tag;

                        if (ty != null)
                        {
                            var subIndex = panelControlMainTree1.treeView1.Nodes[(int)ty].Nodes.IndexOfKey(bottomNode.Name);

                            if (subIndex > -1 && subIndex < panelControlMainTree1.treeView1.Nodes[(int)ty].Nodes.Count - 1)
                            {
                                panelControlMainTree1.treeView1.Nodes[(int)ty].Nodes[subIndex + 1].EnsureVisible();
                            }
                        }
                    }
                }
            }
        }

        private void PanelControlMainTree1_TreeView1_DragDrop(object sender, DragEventArgs e)
        {
            var tv = (TreeView)sender;

            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                TreeNode srcNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
                TreeNode dstNode = tv.GetNodeAt(tv.PointToClient(new Point(e.X, e.Y)));

                var ty = (TimeLine.TimeLineType)dstNode.Tag;
                var index = dstNode.Index;

                if (srcNode.Parent == dstNode.Parent)
                {
                    ChangeSort(srcNode, dstNode);
                }

                panelControlMainTree1.treeView1.SelectedNode = panelControlMainTree1.treeView1.Nodes[(int)ty].Nodes[index];
            }
        }

        private void ChangeSort(TreeNode srcNode, TreeNode dstNode)
        {
            int srcIndex = srcNode.Index;
            int dstIndex = dstNode.Index;

            TreeNode parentNode = srcNode.Parent;
            TreeNode moveNode = (TreeNode)srcNode.Clone();

            if (srcIndex < dstIndex)
            {
                parentNode.Nodes.Insert(dstIndex + 1, moveNode);
                parentNode.Nodes.Remove(srcNode);
            }
            else if (srcIndex > dstIndex)
            {
                parentNode.Nodes.Insert(dstIndex, moveNode);
                parentNode.Nodes.Remove(srcNode);
            }

            var srcSubIndex = srcIndex + 1;
            var dstSubIndex = dstIndex + 1;

            twitter.MoveTimeLineSubIndex((TimeLine.TimeLineType)parentNode.Tag, srcSubIndex, dstSubIndex);
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

            tb.KeyDown += PanelControlMainTree1_TreeView1_AddUser_Tb_KeyDown;
            //            tb.LostFocus += panelControlMainTree1_treeView1_Tb_LostFocus;
            tb.Leave += PanelControlMainTree1_TreeView1_Tb_Leave;

        }

        private void MakeAddSearch()
        {
            MakeTextBox(false);

            tb.KeyDown += PanelControlMainTree1_TreeView1_AddSearch_Tb_KeyDown;
            //            tb.LostFocus += panelControlMainTree1_treeView1_Tb_LostFocus;
            tb.Leave += PanelControlMainTree1_TreeView1_Tb_Leave;

        }

        private void MakeChangeSearch()
        {
            MakeTextBox(true);

            tb.KeyDown += PanelControlMainTree1_TreeView1_ChangeSearch_Tb_KeyDown;
            //            tb.LostFocus += panelControlMainTree1_treeView1_Tb_LostFocus;
            tb.Leave += PanelControlMainTree1_TreeView1_Tb_Leave;

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

        private void PanelControlMainTree1_TreeView1_AddUser_Tb_KeyDown(object sender, EventArgs e)
        {
            KeyEventArgs args = (KeyEventArgs)e;

            switch (args.KeyCode)
            {
                case Keys.Enter:
                    PanelControlMainTree1_TreeView1_AddUser_Tb_Enter();
                    break;
                case Keys.Escape:
                    tb.Dispose();
                    break;
            }

        }
        private void PanelControlMainTree1_TreeView1_AddSearch_Tb_KeyDown(object sender, EventArgs e)
        {
            KeyEventArgs args = (KeyEventArgs)e;

            switch (args.KeyCode)
            {
                case Keys.Enter:
                    PanelControlMainTree1_TreeView1_AddSearch_Tb_Enter();
                    break;
                case Keys.Escape:
                    tb.Dispose();
                    break;
            }

        }

        private void PanelControlMainTree1_TreeView1_ChangeSearch_Tb_KeyDown(object sender, EventArgs e)
        {
            KeyEventArgs args = (KeyEventArgs)e;

            switch (args.KeyCode)
            {
                case Keys.Enter:
                    PanelControlMainTree1_TreeView1_ChangeSearch_Tb_Enter();
                    break;
                case Keys.Escape:
                    tb.Dispose();
                    break;
            }

        }

        private void PanelControlMainTree1_TreeView1_Tb_Leave(object sender, EventArgs e)
        {
            tb.Dispose();
        }

        private void PanelControlMainTree1_TreeView1_AddUser_Tb_Enter()
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

        private void PanelControlMainTree1_TreeView1_AddSearch_Tb_Enter()
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
        private void PanelControlMainTree1_TreeView1_ChangeSearch_Tb_Enter()
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

        private void PanelControlMainTree1_TreeView1_Tb_LostFocus(object sender, EventArgs e)
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

            foreach (var word in words)
            {
                if (word == Resource.Resource1.String_FormTimeLine_SearchWord_OR)
                {
                    continue;
                }
                else if (word.Substring(0, 1) != "#")
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
            var lv = panelTimeLineList1.listView1;
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
            lvi.SubItems.Add(line.User.ProfileImageUrlHttps, color, bkColor, font);
            lvi.SubItems.Add(line.User.Name, color, bkColor, font);
            lvi.SubItems.Add("@" + line.User.ScreenName, color, bkColor, font);

            var txt = line.FullText.ToString();
            var decode = WebUtility.HtmlDecode(txt);
            txt = decode.Replace('\n', ' ');
            txt = txt.Replace("&", "&&");
            lvi.SubItems.Add(txt, color, bkColor, font);

            var time = line.CreatedAt.LocalDateTime;
            var timediff = addDateTime - time;
            string timeMsg = "";

            if (timediff.TotalSeconds < 0)
            {
                timeMsg = "0" + Resource.Resource1.String_FormTimeLine_Seconds;
            }
            else if (timediff.TotalSeconds < 60)
            {
                timeMsg = ((int)timediff.TotalSeconds).ToString() + Resource.Resource1.String_FormTimeLine_Seconds;
            }
            else if (timediff.TotalSeconds < 60 * 60)
            {
                timeMsg = ((int)timediff.TotalMinutes).ToString() + Resource.Resource1.String_FormTimeLine_Minitus;
            }
            else
            {
                timeMsg = string.Format("{0:d04}/{1:d02}/{2:d02} {3:d02}:{4:d02}:{5:d02}",
                            line.CreatedAt.LocalDateTime.Year,
                            line.CreatedAt.LocalDateTime.Month,
                            line.CreatedAt.LocalDateTime.Day,
                            line.CreatedAt.LocalDateTime.Hour,
                            line.CreatedAt.LocalDateTime.Minute,
                            line.CreatedAt.LocalDateTime.Second);
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
            var lv = panelTimeLineList1.listView1;
            var locate = e.Bounds.Location;
            var size = e.Bounds.Size;

            var index = e.ItemIndex;
            Brush br;

            Image img = null;

            if (e.Item.Selected)
            {
                br = SystemBrushes.ControlLight;
                e.Graphics.FillRectangle(br, new Rectangle(locate, size));
            }

            if (e.ColumnIndex == (int)ListViewColumn.USERIMAGE)
            {
                var url = e.SubItem.Text;

                if (userImage[url] != null)
                {
                    img = (Image)userImage[url];
                }

                if (img != null)
                {
                    e.Graphics.DrawImage(img, locate.X, locate.Y, size.Height, size.Height);
                }
            }
            else if (e.ColumnIndex == (int)ListViewColumn.DATETIME)
            {
                e.DrawText(TextFormatFlags.Right | TextFormatFlags.Bottom);
            }
            else
            {
                e.DrawText(TextFormatFlags.Left | TextFormatFlags.Bottom);
            }
        }

        private void PanelTimeLine1_panelTimeLineList1_ListView1_Refresh()
        {
            panelTimeLineList1.listView1.Items.Clear();
            SetListView();
        }

        private void PanelTimeLine1_panelTimeLineList1_ListView1_Click(object sender, EventArgs e)
        {
            ListView1_Click();
        }

        private void PanelTimeLine1_panelTimeLineList1_ListView1_KeyUp(object sender, KeyEventArgs e)
        {
            ListView1_Click();
        }

        private void ListView1_Click(long tweetId = 0, int max = 0)
        {
            ListView lv = panelTimeLineList1.listView1;
            bool sameTweet = false;

            try
            {
                //tweetId = long.Parse(lv.SelectedItems[0].SubItems[0].Text);
                var index = lv.SelectedIndices[0];

                //if (index == 0) return;

                if (tweetId == 0)
                {
                    tweetId = twitter.SelectTimeLine().GetTimeLine()[index].Id;
                    if (tweetId == selectedTweetId) sameTweet = true;
                    selectedTweetId = tweetId;
                }
                else
                {
                    selectedTweetId = 0;
                }

                if (tweetId == 0) return;
            }
            catch
            {
                return;
            }

            //var tl = twitter.GetTimeLineFromId(tweetId);
            var tl = twitter.GetTimeLineFromAPI(tweetId);

            if (tl == null) return;

            toolStripMenuItemRetweet.Enabled = (bool)!tl.IsRetweeted;
            toolStripMenuItemUnRetweet.Enabled = (bool)tl.IsRetweeted;
            toolStripMenuItemReply.Enabled = true;
            toolStripMenuItemReteetWith.Enabled = true;
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
            TweetDraw(tl, false, max);
        }

        private async void TweetDraw(CoreTweet.Status tl, bool retFlag = false, int level = MAX_CONTENTS_LEVEL)
        {
            if (level < 0) return;

            //makeContents(tl);
            if (!retFlag) await Task.Run(() => MakeContents(tl));

            var ret = tl.RetweetedStatus;
            var inRep = tl.InReplyToStatusId;
            var qt = tl.QuotedStatus;

            if (ret != null)
            {
                TweetDraw(ret, true, level - 1);
            }

            if (inRep != null)
            {
                var inRepSt = twitter.GetTimeLineFromAPI((long)inRep);
                if (inRepSt != null)
                {
                    TweetDraw(inRepSt, false, level - 1);
                }
            }

            if (qt != null)
            {
                TweetDraw(qt, false, level - 1);
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

            contents.buttonReply.Click += Contents_ButtonReply_Click;
            contents.buttonRT.Click += Contents_ButtonRT_Click;
            contents.buttonLike.Click += Contents_ButtonLike_Click;
            contents.buttonTrace.Click += Contents_ButtonTrace_Click;

            /*
            int top = 0;
            foreach(var item in customListBox1.Items)
            {
                top += ((panelTimeLineContents1)item).Height;
            }
            contents.Top = top;
            */

            Bitmap bitmap = MakeBitmapFromUrl(tl.User.ProfileImageUrlHttps);

            contents.status = tl;

            var userId = tl.User.ScreenName.ToString();
            contents.textBoxUserName.Text = tl.User.Name.ToString();
            contents.textBoxUserName.Tag = userId;
            contents.textBoxUserId.Text = "@" + userId;
            contents.textBoxUserId.Tag = userId;
            contents.textBoxDateTime.Text = tl.CreatedAt.LocalDateTime.ToString();

            contents.buttonReply.Text = Resource.Resource1.String_Contents_Button_Reply;
            contents.buttonReply.Tag = tl.Id;
            if ((bool)!tl.IsRetweeted)
            {
                contents.buttonRT.Text = string.Format(Resource.Resource1.String_Contents_Button_Retweet, tl.RetweetCount.ToString());
            }
            else
            {
                contents.buttonRT.Text = string.Format(Resource.Resource1.String_Contents_Button_UnRetweet, tl.RetweetCount.ToString());
            }
            contents.buttonRT.Tag = tl.Id;

            if ((bool)!tl.IsFavorited)
            {
                contents.buttonLike.Text = string.Format(Resource.Resource1.String_Contents_Button_Fav, tl.FavoriteCount.ToString());
            }
            else
            {
                contents.buttonLike.Text = string.Format(Resource.Resource1.String_Contents_Button_UnFav, tl.FavoriteCount.ToString());
            }
            contents.buttonLike.Tag = tl.Id;

            if (tl.RetweetedStatus != null || tl.QuotedStatus != null || tl.InReplyToStatusId != null)
            {
                contents.buttonTrace.Enabled = true;
                contents.buttonTrace.Tag = rtl.Id;
            }
            else 
            {
                contents.buttonTrace.Enabled = false;
            }

            contents.Tag = tl.Id;

            if (retweet)
            {
                contents.textBoxRetweet.Text = String.Format(Resource.Resource1.String_FormTimeLine_RetweetBy, rtl.User.Name.ToString(), rtl.User.ScreenName.ToString());
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

            contents.Width = controlListBox1.GetWidthWithoutScrollbar() - 1;
            contents.Height = h * 4 + contents.panel2.Height;
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

            //controlListBox1.Add(contents);
            controlListBox1.Insert(0, contents);

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
            if (userImage[url] != null)
            {
                return (Bitmap)userImage[url];
            }

            WebClient webClient = new WebClient();
            Stream stream = null;
            try
            {
                stream = webClient.OpenRead(url);
            }
            catch (System.Net.WebException e)
            {
                SetStatusMenu(e.Message);
                return null;
            }

            Bitmap bitmap = new Bitmap(stream);
            stream.Close();

            userImage[url] = bitmap;

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

            try
            {
                StreamReader sr = new StreamReader(cssFileName, Encoding.UTF8);

                cssStr = sr.ReadToEnd();

            }
            catch (System.IO.FileNotFoundException)
            {
                return "";
            }

            return cssStr;
        }

        private string contentTagName = "";
        private string contentUrl = "";
        private string contentAlt = "";
        private string contentTxt = "";
        
        private string webInId = "";
        private string webInUser = "";

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
                else if (contentUrl.Substring(0, 4) == "http")
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

            //var lv = panelTimeLineList1.listView1.SelectedItems[0].SubItems[0].Text.ToString();
            var lv = panelTimeLineList1.listView1;
            var index = lv.SelectedIndices[0];

            //if (index == 0) return;

            var tweetId = twitter.SelectTimeLine().GetTimeLine()[index].Id;

            if (obj == toolStripMenuItemOpenTrace)
            {
                ListView1_Click(0, MAX_CONTENTS_LEVEL);
            }
            else if (obj == toolStripMenuItemRetweet)
            {
                if (MessageBox.Show(this, Resource.Resource1.String_FormTimeLine_RetweetMessage, Resource.Resource1.String_FormTimeLine_RetweetTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DoRetweet(tweetId);
                }
            }
            else if (obj == toolStripMenuItemUnRetweet)
            {
                if (MessageBox.Show(this, Resource.Resource1.String_FormTimeLine_CancelRetweetMessage, Resource.Resource1.String_FormTimeLine_CancelRetweetTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DoUnRetweet(tweetId);
                }
            }
            else if (obj == toolStripMenuItemLike)
            {
                if (MessageBox.Show(this, Resource.Resource1.String_FormTimeLine_FavoriteMessage, Resource.Resource1.String_FormTimeLine_FavoriteTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DoLike(tweetId);
                }
            }
            else if (obj == toolStripMenuItemUnLike)
            {
                if (MessageBox.Show(this, Resource.Resource1.String_FormTimeLine_CancelFavoriteMessage, Resource.Resource1.String_FormTimeLine_CancelFavoriteTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DoUnLike(tweetId);
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
                twitter.OpenUrl(status.User.ScreenName, tweetId.ToString());
            }
            else if (obj == toolStripMenuItemDel)
            {
                if (MessageBox.Show(this, Resource.Resource1.String_FormTimeLine_DeleteTweetMessage, Resource.Resource1.String_FormTimeLine_DeleteTweetTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    twitter.DeleteTweet(tweetId);
                    PanelTimeLine1_panelTimeLineList1_ListView1_Refresh();
                }
            }
            else if (obj == toolStripMenuItemReply)
            {
                DoReply(tweetId);
            }
            else if (obj == toolStripMenuItemReteetWith)
            {
                DoRetweetWith(tweetId);
            }
        }

        private void DoRetweetWith(long tweetId)
        {
            var status = twitter.GetTimeLineFromId(tweetId);
            string url = "https://twitter.com/" + status.User.ScreenName + "/status/" + tweetId.ToString();

            var font1 = new Font(panelControlMainEdit1.textBoxMsg1.Font, FontStyle.Bold);
            var font2 = new Font(panelControlMainEdit1.textBoxMsg1.Font, FontStyle.Bold);

            panelControlMainEdit1.textBoxMsg1.ForeColor = Color.Blue;
            panelControlMainEdit1.textBoxMsg1.Font = font1;
            panelControlMainEdit1.textBoxMsg2.ForeColor = Color.Blue;
            panelControlMainEdit1.textBoxMsg2.Font = font2;

            panelControlMainEdit1.textBoxMsg1.Text = Resource.Resource1.String_FormTimeLine_Quote;
            panelControlMainEdit1.textBoxMsg1.Tag = Twitter.TweetType.RetweetWith;
            panelControlMainEdit1.textBoxMsg2.Text = url;
            panelControlMainEdit1.textBoxMsg2.Tag = tweetId.ToString();

            panelControlMainEdit1.textBoxMsg2.Click += PanelControlMainEdit1_TextBoxMsg2_Click;
            panelControlMainEdit1.textBoxMsg2.Cursor = Cursors.Hand;
        }

        private void DoReply(long tweetId)
        {
            var status = twitter.GetTimeLineFromId(tweetId);
            string url = "https://twitter.com/" + status.User.ScreenName + "/status/" + tweetId.ToString();

            var font1 = new Font(panelControlMainEdit1.textBoxMsg1.Font, FontStyle.Bold);
            var font2 = new Font(panelControlMainEdit1.textBoxMsg1.Font, FontStyle.Bold);

            panelControlMainEdit1.textBoxMsg1.ForeColor = Color.Red;
            panelControlMainEdit1.textBoxMsg1.Font = font1;
            panelControlMainEdit1.textBoxMsg2.ForeColor = Color.Red;
            panelControlMainEdit1.textBoxMsg2.Font = font2;

            panelControlMainEdit1.textBoxMsg1.Text = String.Format(Resource.Resource1.String_FormTimeLine_Reply, status.User.ScreenName);
            panelControlMainEdit1.textBoxMsg1.Tag = Twitter.TweetType.Reply;
            panelControlMainEdit1.textBoxMsg2.Text = url;
            panelControlMainEdit1.textBoxMsg2.Tag = tweetId.ToString();

            panelControlMainEdit1.textBoxMsg2.Click += PanelControlMainEdit1_TextBoxMsg2_Click;
            panelControlMainEdit1.textBoxMsg2.Cursor = Cursors.Hand;
        }

        private void DoLike(long tweetId)
        {
            twitter.Like(tweetId);
            twitter.GetTimeLine(tweetId);
            PanelTimeLine1_panelTimeLineList1_ListView1_Refresh();
        }

        private void DoUnLike(long tweetId)
        {
            twitter.UnLike(tweetId);
            twitter.GetTimeLine(tweetId);
            PanelTimeLine1_panelTimeLineList1_ListView1_Refresh();
        }

        private void DoRetweet(long tweetId)
        {
            twitter.Retweet(tweetId);
            twitter.GetTimeLine(tweetId);
            PanelTimeLine1_panelTimeLineList1_ListView1_Refresh();
        }

        private void DoUnRetweet(long tweetId)
        {
            twitter.UnRetweet(tweetId);
            twitter.GetTimeLine(tweetId);
            PanelTimeLine1_panelTimeLineList1_ListView1_Refresh();
        }


        // WebBrowser

        private string beforeContentUrl = "";
        //private ToolTip contentToolTip = null;

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

                //contentToolTip?.Dispose();
                //contentToolTip = null;

                formPic?.Dispose();
                formPic = null;
            }
            else if (contentTagName == "img")
            {
                contentUrl = clickedElement.GetAttribute("src");
                webInId = clickedElement.GetAttribute("id");
                webInUser = clickedElement.GetAttribute("user");
                SetStatusMenu(contentUrl);

                panelTimeLineContents1 workDoc = null;

                if (contentUrl != beforeContentUrl)
                {
                    foreach (var doc in controlListBox1.Items)
                    {
                        if (((HtmlDocument)sender) == ((panelTimeLineContents1)doc).webBrowser1.Document)
                        {
                            workDoc = (panelTimeLineContents1)doc;
                            break;
                        }
                    }

                    if (workDoc != null)
                    {
                        /*
                        contentToolTip?.Dispose();
                        contentToolTip = new ToolTip();
                        contentToolTip.OwnerDraw = true;
                        contentToolTip.Popup += PanelTimeLineContents1_webBrowser_Document_ContentToolTip_Popup;
                        contentToolTip.Draw += PanelTimeLineContents1_webBrowser_Document_ContentToolTip_Draw;
                        Bitmap bmp = MakeBitmapFromUrl(contentUrl);
                        contentToolTip.Tag = bmp;
                        Debug.WriteLine("BMP Size x:{0} y:{1}", bmp.Size.Width, bmp.Size.Height);

                        var pos = this.PointToClient(Control.MousePosition);

                        Debug.WriteLine("Pos x:{0} y:{1}", pos.X, pos.Y);

                        contentToolTip.Show(contentUrl, this, pos, 3000);
                        */

                        var pos = this.PointToClient(Control.MousePosition);

                        if (formPic != null)
                        {
                            formPic.Dispose();
                        }
                        formPic = new FormPopupPicture();
                        Bitmap bmp = MakeBitmapFromUrl(contentUrl);

                        var dsktop = Screen.FromControl(this).WorkingArea;
                        var dskw = dsktop.Width;
                        var dskh = dsktop.Height;

                        var mx = Control.MousePosition.X;
                        var my = Control.MousePosition.Y;
                        var bw = bmp.Width;
                        var bh = bmp.Height;

                        var dw = dskw - bw;
                        var dh = dskh - bh;

                        var x = mx < dw ? mx : dw;
                        var y = my < dh ? my : dh;

                        formPic.Location = new Point(x, y);
                        formPic.SetBitmap(bmp, webInId, webInUser, contentUrl);
                        formPic.FormClosed += FormPic_FormClosed;
                        formPic.Show();
                    }
                }

                beforeContentUrl = contentUrl;
            }
            else
            {
                SetStatusMenu("");
                beforeContentUrl = "";

                //contentToolTip?.Dispose();
                //contentToolTip = null;

                formPic?.Dispose();
                formPic = null;
            }
        }

        private void FormPic_FormClosed(object sender, FormClosedEventArgs e)
        {
            formPic?.Dispose();
            formPic = null;
        }

        private void PanelTimeLineContents1_webBrowser_Document_ContentToolTip_Popup(object sender, PopupEventArgs e)
        {
            var obj = (ToolTip)sender;
            e.ToolTipSize = ((Bitmap)obj.Tag).Size;
        }

        private void PanelTimeLineContents1_webBrowser_Document_ContentToolTip_Draw(object sender, DrawToolTipEventArgs e)
        {
            var obj = (ToolTip)sender;
            Bitmap bmp = (Bitmap)obj.Tag;

            e.DrawBackground();
            e.Graphics.DrawImage(bmp, new Point(0, 0));
            e.DrawBorder();
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
                    twitter.OpenUrl(contentUrl);
                }
            }
            else if (contentTagName == "img")
            {
                contentUrl = clickedElement.GetAttribute("src");
                twitter.OpenUrl(contentUrl);
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
                    twitter.OpenUrl(contentUrl);
                }
                else
                {
                    var tweetId = (long)((Control)sender).Tag;
                    var status = twitter.GetTimeLineFromId(tweetId);

                    twitter.OpenUrl(status.User.ScreenName, tweetId.ToString());
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
/*
            foreach (var i in controlListBox1.Items)
            {
                if (i.GetType().Equals(typeof(panelTimeLineContents1)))
                {
                    var ct = (panelTimeLineContents1)i;
                    var hTable = ct.tableLayoutPanel1.Height;
                    var hWeb = ct.webBrowser1.Document.Body.ScrollRectangle.Height;
                    var hfooter = ct.panel2.Height;
                    //((panelTimeLineContents1)i).Height = h;
                    i.Height = hTable + hWeb + hfooter;

                    Debug.WriteLine("DocumentCompleted: hSplit {0}", hTable);
                    Debug.WriteLine("DocumentCompleted: hWeb {0}", hWeb);
                }
                else
                {
                    ((Panel)i).Height = controlListBox1.Height;
                }
            }

            controlListBox1.Replace();
            controlListBox1.Refresh();
*/
            var web = (WebBrowser)sender;
            panelTimeLineContents1 ct = (panelTimeLineContents1)web.Parent.Parent;
            var hTable = ct.tableLayoutPanel1.Height;
            var hWeb = ct.webBrowser1.Document.Body.ScrollRectangle.Height;
            var hfooter = ct.panel2.Height;
            //((panelTimeLineContents1)i).Height = h;
            ct.Height = hTable + hWeb + hfooter;

            Debug.WriteLine("DocumentCompleted: hSplit {0}", hTable);
            Debug.WriteLine("DocumentCompleted: hWeb {0}", hWeb);
        }

        private void Contents_ButtonReply_Click(object sender, EventArgs e)
        {
            var bt = (Button)sender;
            var id = long.Parse(bt.Tag.ToString());

            DoReply(id);
            panelControlMainEdit1.textBoxTweet.Focus();
        }

        private void Contents_ButtonRT_Click(object sender, EventArgs e)
        {
            var bt = (Button)sender;
            var id = long.Parse(bt.Tag.ToString());
            var st = twitter.GetTimeLineFromAPI(id);

            if ((bool)st.IsRetweeted)
            {
                DoUnRetweet(id);
                bt.Text = string.Format(Resource.Resource1.String_Contents_Button_Retweet, st.RetweetCount.ToString());
            }
            else
            {
                if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                {
                    DoRetweetWith(id);
                    panelControlMainEdit1.textBoxTweet.Focus();
                }
                else
                {
                    DoRetweet(id);
                    bt.Text = string.Format(Resource.Resource1.String_Contents_Button_UnRetweet, st.RetweetCount.ToString());
                }
            }
        }
        private void Contents_ButtonLike_Click(object sender, EventArgs e)
        {
            var bt = (Button)sender;
            var id = long.Parse(bt.Tag.ToString());
            var st = twitter.GetTimeLineFromAPI(id);

            if ((bool)st.IsFavorited)
            {
                DoUnLike(id);
                bt.Text = string.Format(Resource.Resource1.String_Contents_Button_Fav, st.RetweetCount.ToString());
            }
            else
            {
                DoLike(id);
                bt.Text = string.Format(Resource.Resource1.String_Contents_Button_UnFav, st.RetweetCount.ToString());
            }
        }

        private void Contents_ButtonTrace_Click(object sender, EventArgs e)
        {
            var bt = (Button)sender;
            var id = long.Parse(bt.Tag.ToString());
            var st = twitter.GetTimeLineFromAPI(id);

            ListView1_Click(id, MAX_CONTENTS_LEVEL);
        }

        private void ListBoxTweetContents_MeasureItem1(object sender, MeasureItemEventArgs e)
        {
            if (controlListBox1.Items[e.Index].GetType().Equals(typeof(panelTimeLineContents1)))
            {
                var h = ((panelTimeLineContents1)controlListBox1.Items[e.Index]).Height;
                e.ItemHeight = h;
                Debug.WriteLine("MeasureItem: {0}", h);
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
            panelControlMainEdit1.Enabled = false;
            Send_Click(panelControlMainEdit1.textBoxTweet.Text);
            panelControlMainEdit1.Enabled = true;

            panelControlMainEdit1.textBoxTweet.Clear();
            panelControlMainEdit1.textBoxTweet.Focus();

            panelControlMainEdit1.textBoxMsg1.Tag = Twitter.TweetType.Normal;

            panelControlMainEdit1.textBoxMsg1.Clear();
            panelControlMainEdit1.textBoxMsg2.Clear();

            panelControlMainEdit1.textBoxMsg2.Click -= PanelControlMainEdit1_TextBoxMsg2_Click;
            panelControlMainEdit1.textBoxMsg2.Cursor = Cursors.IBeam;

        }

        private void Send_Click(string txt)
        {
            var type = (Twitter.TweetType)panelControlMainEdit1.textBoxMsg1.Tag;
            var msg2Tag = panelControlMainEdit1.textBoxMsg2.Tag;
            var sub = (long)0;
            var len = txt.Length;

            if (msg2Tag != null)
            {
                sub = long.Parse(msg2Tag.ToString());
            }

            if (panelControlMainEdit1.checkBoxHash.Checked)
            {
                txt += " " + panelControlMainEdit1.comboBoxHash.Text;
            }

            if (type == Twitter.TweetType.Normal)
            {
                twitter.Send(txt, tweetImage);
            }
            else if (type == Twitter.TweetType.Reply && sub != 0)
            {
                twitter.Reply(txt, sub);
            }
            else if (type == Twitter.TweetType.RetweetWith)
            {
                txt += " " + panelControlMainEdit1.textBoxMsg2.Text;
                twitter.Send(txt, tweetImage);
            }


            panelControlMainEdit1.textBoxMsg1.Tag = Twitter.TweetType.Normal;

            panelControlMainEdit1.textBoxMsg1.Clear();
            panelControlMainEdit1.textBoxMsg2.Clear();

            panelControlMainEdit1.textBoxMsg2.Click -= PanelControlMainEdit1_TextBoxMsg2_Click;
            panelControlMainEdit1.textBoxMsg2.Cursor = Cursors.IBeam;

            panelControlMainEdit1.pictureBox1.Image = null;
            panelControlMainEdit1.pictureBox2.Image = null;
            panelControlMainEdit1.pictureBox3.Image = null;
            panelControlMainEdit1.pictureBox4.Image = null;
            ImageClear();
        }

        private void PanelControlMainEdit1_ButtonClear_Click(object sender, EventArgs e)
        {
            panelControlMainEdit1.textBoxTweet.Clear();

            panelControlMainEdit1.textBoxMsg1.Tag = Twitter.TweetType.Normal;

            panelControlMainEdit1.textBoxMsg1.Clear();
            panelControlMainEdit1.textBoxMsg2.Clear();

            panelControlMainEdit1.textBoxMsg2.Click -= PanelControlMainEdit1_TextBoxMsg2_Click;
            panelControlMainEdit1.textBoxMsg2.Cursor = Cursors.IBeam;

            panelControlMainEdit1.pictureBox1.Image = null;
            panelControlMainEdit1.pictureBox2.Image = null;
            panelControlMainEdit1.pictureBox3.Image = null;
            panelControlMainEdit1.pictureBox4.Image = null;
            ImageClear();
        }

        private void ImageClear()
        {
            for (int i = 0; i < MAXIMAGE; i++)
            {
                if (tweetImage[i] != null)
                {
                    tweetImage[i] = null;
                }
            }
        }

        // PictureBox

        PictureBox srcPb = null;

        private void PanelControlMainEdit1_PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void PanelControlMainEdit1_PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            var pb = (PictureBox)sender;

            if (e.Button == MouseButtons.Left)
            {
                if (pb.Image == null)
                {
                    return;
                }

                srcPb = pb;
                pb.DoDragDrop(pb.Image, DragDropEffects.Move);
            }
        }

        private void PanelControlMainEdit1_PictureBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void PanelControlMainEdit1_PictureBox_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Effect == DragDropEffects.Copy)
            {
                string[] fileNames = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                int pbId = 0;

                try
                {
                    Bitmap bmp = new Bitmap(fileNames[0]);
                    var pb = (PictureBox)sender;
                    pb.Image = bmp;
                    pb.Tag = fileNames[0];

                    if (sender == panelControlMainEdit1.pictureBox1) pbId = 1;
                    else if (sender == panelControlMainEdit1.pictureBox2) pbId = 2;
                    else if (sender == panelControlMainEdit1.pictureBox3) pbId = 3;
                    else if (sender == panelControlMainEdit1.pictureBox4) pbId = 4;
                    else pbId = -0;

                    if (pbId > 0)
                    {
                        tweetImage[pbId - 1] = new Bitmap(bmp);
                        tweetImage[pbId - 1].Tag = fileNames[0];
                    }
                }
                catch
                {

                }
            }
            else if (e.Effect == DragDropEffects.Move)
            {
                var dstPb = (PictureBox)sender;

                Bitmap tmpBm = null;
                string tmpBm_Tag;

                if (srcPb != dstPb)
                {
                    if (dstPb.Image != null)
                    {
                        tmpBm = new Bitmap(dstPb.Image);
                        tmpBm_Tag = (string)dstPb.Tag;
                    }
                    else
                    {
                        tmpBm = null;
                        tmpBm_Tag = "";
                    }

                    dstPb.Image = new Bitmap(srcPb.Image);
                    dstPb.Tag = srcPb.Tag;
                    srcPb.Image = tmpBm;
                    srcPb.Tag = tmpBm_Tag;

                    int srcNo = 0;
                    if (srcPb == panelControlMainEdit1.pictureBox1) srcNo = 1;
                    else if (srcPb == panelControlMainEdit1.pictureBox2) srcNo = 2;
                    else if (srcPb == panelControlMainEdit1.pictureBox3) srcNo = 3;
                    else if (srcPb == panelControlMainEdit1.pictureBox4) srcNo = 4;

                    int dstNo = 0;
                    if (dstPb == panelControlMainEdit1.pictureBox1) dstNo = 1;
                    else if (dstPb == panelControlMainEdit1.pictureBox2) dstNo = 2;
                    else if (dstPb == panelControlMainEdit1.pictureBox3) dstNo = 3;
                    else if (dstPb == panelControlMainEdit1.pictureBox4) dstNo = 4;

                    tmpBm = tweetImage[dstNo - 1];
                    tweetImage[dstNo - 1] = tweetImage[srcNo - 1];
                    tweetImage[srcNo - 1] = tmpBm;
                }
            }
        }

        private void PanelControlMainEdit1_PictureBox1_DragLeave(object sender, EventArgs e)
        {
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
                try
                {
                    Invoke(new Action<string>(SetStatusMenu), msg);
                }
                catch
                {
                }
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
                    childNode.Name = tabName;
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

            panelTimeLineList1.listView1.BeginUpdate();

            //SetListViewItem(status);
            cacheLvi.Clear();
            panelTimeLineList1.listView1.VirtualListSize = twitter.SelectTimeLine().GetTimeLine().Count;
            selectedTweetId = 0;

            panelTimeLineList1.listView1.EndUpdate();

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
                catch (System.ObjectDisposedException)
                {

                }
                return;
            }

            if (twitter.SelectTimeLine().GetNewTimeLine().Count == 0) return;

            addDateTime = DateTime.Now.ToLocalTime();

            foreach (var st in twitter.SelectTimeLine().GetNewTimeLine())
            {
                if (st.RetweetedStatus == null)
                {
                    //GetImageFromAPI(st.User.ProfileImageUrlHttps);
                    AddListGetImageFromAPI(st.User.ProfileImageUrlHttps);
                }
                else
                {
                    //GetImageFromAPI(st.RetweetedStatus.User.ProfileImageUrlHttps);
                    AddListGetImageFromAPI(st.RetweetedStatus.User.ProfileImageUrlHttps);
                }
            }

            var oldTop = panelTimeLineList1.listView1.TopItem;
            var oldTopIndex = 0;

            if (oldTop != null)
            {
                oldTopIndex = panelTimeLineList1.listView1.TopItem.Index;
            }

            panelTimeLineList1.listView1.BeginUpdate();

            //SetListViewItem(status);
            cacheLvi.Clear();
            panelTimeLineList1.listView1.VirtualListSize = twitter.SelectTimeLine().GetTimeLine().Count;

            if (oldTopIndex != 0)
            {
                panelTimeLineList1.listView1.EnsureVisible(twitter.SelectTimeLine().GetTimeLine().Count - 1);
                panelTimeLineList1.listView1.EnsureVisible(oldTopIndex + twitter.SelectTimeLine().GetNewTimeLine().Count);
            }
            else
            {
                if (panelTimeLineList1.listView1.Items.Count != 0)
                {
                    panelTimeLineList1.listView1.EnsureVisible(0);
                }
            }

            var sel = panelTimeLineList1.listView1.SelectedIndices;
            if (sel.Count != 0)
            {
                var index = sel[0];
                index += twitter.SelectTimeLine().GetNewTimeLine().Count();
                panelTimeLineList1.listView1.SelectedIndices.Clear();
                panelTimeLineList1.listView1.SelectedIndices.Add(index);
            }

            panelTimeLineList1.listView1.EndUpdate();

            GetImageFromAPITask();
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

                var item = panelTimeLineList1.listView1.Items.Add(items);
                item.ForeColor = color;
            }
            */

            cacheLvi.Clear();

            panelTimeLineList1.listView1.VirtualListSize = twitter.SelectTimeLine().GetTimeLine().Count;
        }

        public void AddListGetImageFromAPI(string url)
        {
            if (userImage[url] == null)
            {
                imageGetWaitList.Add(url);
            }    
        }

        public void GetImageFromAPI(string url)
        {
            Task.Run(() =>
            {
                Bitmap bitmap = MakeBitmapFromUrl((string)url);
                Image img = bitmap;
                userImage[url] = img;

                ListViewUpdate(url);
            });
        }

        private void ListViewUpdate(string url)
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action<string>(ListViewUpdate),url);
                return;
            }

            var tl = twitter.SelectTimeLine().GetTimeLine();
            for (int i = 0; i < tl.Count; i++)
            {
                var tmpUrl = panelTimeLineList1.listView1.Items[i].SubItems[(int)ListViewColumn.USERIMAGE].Text;

                if (tmpUrl == url)
                {
                    try
                    {
                        panelTimeLineList1.listView1.RedrawItems(i, i, false);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void GetImageFromAPITask()
        {
            Task.Run(() =>
            {
                while (imageGetWaitList.Count != 0)
                {
                    ArrayList tmpList = new ArrayList(imageGetWaitList);
                    imageGetWaitList.Clear();

                    Image img = null;
                    foreach (var url in tmpList)
                    {
                        System.Console.WriteLine("get: " + url);
                        Bitmap bitmap = MakeBitmapFromUrl((string)url);
                        if (bitmap == null) break;
                        img = bitmap;
                        userImage[url] = img;
                    }

                    foreach (var url in tmpList)
                    {
                        ListViewUpdate((string)url);
                    }
                }
            });
        }
    }
}
