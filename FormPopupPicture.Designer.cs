
namespace twclient
{
    partial class FormPopupPicture
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.contextMenuStripOpenBrowser = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemOpenBrowser = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemOpenBrowserImage = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuStripOpenBrowser.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.ContextMenuStrip = this.contextMenuStripOpenBrowser;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(800, 450);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.pictureBox1.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
            // 
            // contextMenuStripOpenBrowser
            // 
            this.contextMenuStripOpenBrowser.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStripOpenBrowser.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemOpenBrowser,
            this.ToolStripMenuItemOpenBrowserImage});
            this.contextMenuStripOpenBrowser.Name = "contextMenuStripOpenBrowser";
            this.contextMenuStripOpenBrowser.Size = new System.Drawing.Size(231, 68);
            // 
            // toolStripMenuItemOpenBrowser
            // 
            this.toolStripMenuItemOpenBrowser.Name = "toolStripMenuItemOpenBrowser";
            this.toolStripMenuItemOpenBrowser.Size = new System.Drawing.Size(230, 32);
            this.toolStripMenuItemOpenBrowser.Text = "ブラウザで開く";
            this.toolStripMenuItemOpenBrowser.Click += new System.EventHandler(this.toolStripMenuItemOpenBrowser_Click);
            // 
            // ToolStripMenuItemOpenBrowserImage
            // 
            this.ToolStripMenuItemOpenBrowserImage.Name = "ToolStripMenuItemOpenBrowserImage";
            this.ToolStripMenuItemOpenBrowserImage.Size = new System.Drawing.Size(230, 32);
            this.ToolStripMenuItemOpenBrowserImage.Text = "画像をブラウザで開く";
            this.ToolStripMenuItemOpenBrowserImage.Click += new System.EventHandler(this.ToolStripMenuItemOpenBrowserImage_Click);
            // 
            // FormPopupPicture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormPopupPicture";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormPopupPicture";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.contextMenuStripOpenBrowser.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripOpenBrowser;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpenBrowser;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemOpenBrowserImage;
    }
}