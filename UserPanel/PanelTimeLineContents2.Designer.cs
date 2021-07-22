
namespace twclient.UserPanel
{
    partial class panelTimeLineContents2
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.webView2_1 = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.textBoxUserId = new System.Windows.Forms.TextBox();
            this.textBoxDateTime = new System.Windows.Forms.TextBox();
            this.textBoxRetweet = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelWeb = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonReply = new System.Windows.Forms.Button();
            this.buttonRT = new System.Windows.Forms.Button();
            this.buttonTrace = new System.Windows.Forms.Button();
            this.buttonLike = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // webView2_1
            // 
            this.webView2_1.CreationProperties = null;
            this.webView2_1.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView2_1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView2_1.Location = new System.Drawing.Point(0, 64);
            this.webView2_1.Name = "webView2_1";
            this.webView2_1.Size = new System.Drawing.Size(391, 262);
            this.webView2_1.TabIndex = 11;
            this.webView2_1.ZoomFactor = 1D;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(391, 64);
            this.panel1.TabIndex = 10;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(391, 64);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.textBoxUserName, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBoxUserId, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.textBoxDateTime, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.textBoxRetweet, 0, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(65, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(326, 64);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxUserName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBoxUserName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxUserName.Location = new System.Drawing.Point(0, 0);
            this.textBoxUserName.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(326, 16);
            this.textBoxUserName.TabIndex = 0;
            // 
            // textBoxUserId
            // 
            this.textBoxUserId.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxUserId.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBoxUserId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxUserId.Location = new System.Drawing.Point(0, 16);
            this.textBoxUserId.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxUserId.Name = "textBoxUserId";
            this.textBoxUserId.Size = new System.Drawing.Size(326, 16);
            this.textBoxUserId.TabIndex = 1;
            // 
            // textBoxDateTime
            // 
            this.textBoxDateTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxDateTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxDateTime.Location = new System.Drawing.Point(0, 32);
            this.textBoxDateTime.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxDateTime.Name = "textBoxDateTime";
            this.textBoxDateTime.Size = new System.Drawing.Size(326, 16);
            this.textBoxDateTime.TabIndex = 2;
            // 
            // textBoxRetweet
            // 
            this.textBoxRetweet.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxRetweet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxRetweet.Location = new System.Drawing.Point(0, 48);
            this.textBoxRetweet.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxRetweet.Name = "textBoxRetweet";
            this.textBoxRetweet.Size = new System.Drawing.Size(326, 16);
            this.textBoxRetweet.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // panelWeb
            // 
            this.panelWeb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelWeb.Location = new System.Drawing.Point(2, 98);
            this.panelWeb.Name = "panelWeb";
            this.panelWeb.Size = new System.Drawing.Size(548, 330);
            this.panelWeb.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tableLayoutPanel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(2, 428);
            this.panel2.Margin = new System.Windows.Forms.Padding(1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(548, 64);
            this.panel2.TabIndex = 3;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.buttonLike, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.buttonTrace, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.buttonReply, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.buttonRT, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(548, 64);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // buttonReply
            // 
            this.buttonReply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonReply.Location = new System.Drawing.Point(0, 0);
            this.buttonReply.Margin = new System.Windows.Forms.Padding(0);
            this.buttonReply.Name = "buttonReply";
            this.buttonReply.Size = new System.Drawing.Size(274, 32);
            this.buttonReply.TabIndex = 0;
            this.buttonReply.Text = "button1";
            this.buttonReply.UseVisualStyleBackColor = true;
            // 
            // buttonRT
            // 
            this.buttonRT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonRT.Location = new System.Drawing.Point(274, 0);
            this.buttonRT.Margin = new System.Windows.Forms.Padding(0);
            this.buttonRT.Name = "buttonRT";
            this.buttonRT.Size = new System.Drawing.Size(274, 32);
            this.buttonRT.TabIndex = 1;
            this.buttonRT.Text = "button2";
            this.buttonRT.UseVisualStyleBackColor = true;
            // 
            // buttonTrace
            // 
            this.buttonTrace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonTrace.Location = new System.Drawing.Point(274, 32);
            this.buttonTrace.Margin = new System.Windows.Forms.Padding(0);
            this.buttonTrace.Name = "buttonTrace";
            this.buttonTrace.Size = new System.Drawing.Size(274, 32);
            this.buttonTrace.TabIndex = 3;
            this.buttonTrace.Text = "Trace";
            this.buttonTrace.UseVisualStyleBackColor = true;
            // 
            // buttonLike
            // 
            this.buttonLike.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonLike.Location = new System.Drawing.Point(0, 32);
            this.buttonLike.Margin = new System.Windows.Forms.Padding(0);
            this.buttonLike.Name = "buttonLike";
            this.buttonLike.Size = new System.Drawing.Size(274, 32);
            this.buttonLike.TabIndex = 4;
            this.buttonLike.Text = "button3";
            this.buttonLike.UseVisualStyleBackColor = true;
            // 
            // panelTimeLineContents1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panelWeb);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "panelTimeLineContents2";
            this.Size = new System.Drawing.Size(391, 326);
            ((System.ComponentModel.ISupportInitialize)(this.webView2_1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.TextBox textBoxUserName;
        public System.Windows.Forms.TextBox textBoxUserId;
        public System.Windows.Forms.TextBox textBoxDateTime;
        public System.Windows.Forms.TextBox textBoxRetweet;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        public System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelWeb;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        public System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Button buttonReply;
        public System.Windows.Forms.Button buttonRT;
        public System.Windows.Forms.Button buttonLike;
        public System.Windows.Forms.Button buttonTrace;
    }
}
