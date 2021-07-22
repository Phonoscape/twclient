
namespace twclient.UserPanel
{
    partial class PanelControlMainEdit
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.comboBoxHash = new System.Windows.Forms.ComboBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.checkBoxHash = new System.Windows.Forms.CheckBox();
            this.buttonHash = new System.Windows.Forms.Button();
            this.buttonSend = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanelStatus = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelInfo = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxMsg1 = new System.Windows.Forms.TextBox();
            this.textBoxMsg2 = new System.Windows.Forms.TextBox();
            this.tableLayoutPanelEdit = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelPicture = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBoxTweet = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanelStatus.SuspendLayout();
            this.tableLayoutPanelInfo.SuspendLayout();
            this.tableLayoutPanelEdit.SuspendLayout();
            this.tableLayoutPanelPicture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 137);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(521, 43);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.comboBoxHash);
            this.panel2.Controls.Add(this.buttonClear);
            this.panel2.Controls.Add(this.checkBoxHash);
            this.panel2.Controls.Add(this.buttonHash);
            this.panel2.Controls.Add(this.buttonSend);
            this.panel2.Location = new System.Drawing.Point(5, 7);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(514, 30);
            this.panel2.TabIndex = 2;
            // 
            // comboBoxHash
            // 
            this.comboBoxHash.FormattingEnabled = true;
            this.comboBoxHash.Location = new System.Drawing.Point(111, 6);
            this.comboBoxHash.Name = "comboBoxHash";
            this.comboBoxHash.Size = new System.Drawing.Size(202, 23);
            this.comboBoxHash.TabIndex = 3;
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(355, 4);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 4;
            this.buttonClear.Text = "クリア";
            this.buttonClear.UseVisualStyleBackColor = true;
            // 
            // checkBoxHash
            // 
            this.checkBoxHash.AutoSize = true;
            this.checkBoxHash.Location = new System.Drawing.Point(3, 9);
            this.checkBoxHash.Name = "checkBoxHash";
            this.checkBoxHash.Size = new System.Drawing.Size(103, 19);
            this.checkBoxHash.TabIndex = 3;
            this.checkBoxHash.Text = "ハッシュタグ追加";
            this.checkBoxHash.UseVisualStyleBackColor = true;
            // 
            // buttonHash
            // 
            this.buttonHash.Location = new System.Drawing.Point(317, 4);
            this.buttonHash.Name = "buttonHash";
            this.buttonHash.Size = new System.Drawing.Size(32, 23);
            this.buttonHash.TabIndex = 2;
            this.buttonHash.Text = "#";
            this.buttonHash.UseVisualStyleBackColor = true;
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(436, 4);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSend.TabIndex = 2;
            this.buttonSend.Text = "投稿";
            this.buttonSend.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tableLayoutPanelStatus);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(521, 43);
            this.panel3.TabIndex = 2;
            // 
            // tableLayoutPanelStatus
            // 
            this.tableLayoutPanelStatus.ColumnCount = 2;
            this.tableLayoutPanelStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelStatus.Controls.Add(this.tableLayoutPanelInfo, 0, 0);
            this.tableLayoutPanelStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelStatus.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelStatus.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelStatus.Name = "tableLayoutPanelStatus";
            this.tableLayoutPanelStatus.RowCount = 1;
            this.tableLayoutPanelStatus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelStatus.Size = new System.Drawing.Size(521, 43);
            this.tableLayoutPanelStatus.TabIndex = 0;
            // 
            // tableLayoutPanelInfo
            // 
            this.tableLayoutPanelInfo.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanelInfo.ColumnCount = 1;
            this.tableLayoutPanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelInfo.Controls.Add(this.textBoxMsg1, 0, 0);
            this.tableLayoutPanelInfo.Controls.Add(this.textBoxMsg2, 0, 1);
            this.tableLayoutPanelInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelInfo.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelInfo.Name = "tableLayoutPanelInfo";
            this.tableLayoutPanelInfo.RowCount = 2;
            this.tableLayoutPanelInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelInfo.Size = new System.Drawing.Size(385, 58);
            this.tableLayoutPanelInfo.TabIndex = 1;
            // 
            // textBoxMsg1
            // 
            this.textBoxMsg1.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxMsg1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxMsg1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxMsg1.Location = new System.Drawing.Point(0, 0);
            this.textBoxMsg1.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxMsg1.Name = "textBoxMsg1";
            this.textBoxMsg1.ReadOnly = true;
            this.textBoxMsg1.Size = new System.Drawing.Size(385, 24);
            this.textBoxMsg1.TabIndex = 0;
            this.textBoxMsg1.WordWrap = false;
            // 
            // textBoxMsg2
            // 
            this.textBoxMsg2.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxMsg2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxMsg2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxMsg2.Location = new System.Drawing.Point(0, 29);
            this.textBoxMsg2.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxMsg2.Name = "textBoxMsg2";
            this.textBoxMsg2.ReadOnly = true;
            this.textBoxMsg2.Size = new System.Drawing.Size(385, 24);
            this.textBoxMsg2.TabIndex = 1;
            this.textBoxMsg2.WordWrap = false;
            // 
            // tableLayoutPanelEdit
            // 
            this.tableLayoutPanelEdit.ColumnCount = 2;
            this.tableLayoutPanelEdit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
            this.tableLayoutPanelEdit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelEdit.Controls.Add(this.tableLayoutPanelPicture, 0, 0);
            this.tableLayoutPanelEdit.Controls.Add(this.textBoxTweet, 0, 0);
            this.tableLayoutPanelEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelEdit.Location = new System.Drawing.Point(0, 43);
            this.tableLayoutPanelEdit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanelEdit.Name = "tableLayoutPanelEdit";
            this.tableLayoutPanelEdit.RowCount = 1;
            this.tableLayoutPanelEdit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelEdit.Size = new System.Drawing.Size(521, 94);
            this.tableLayoutPanelEdit.TabIndex = 3;
            // 
            // tableLayoutPanelPicture
            // 
            this.tableLayoutPanelPicture.ColumnCount = 4;
            this.tableLayoutPanelPicture.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelPicture.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelPicture.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelPicture.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelPicture.Controls.Add(this.pictureBox3, 0, 0);
            this.tableLayoutPanelPicture.Controls.Add(this.pictureBox2, 0, 0);
            this.tableLayoutPanelPicture.Controls.Add(this.pictureBox4, 1, 0);
            this.tableLayoutPanelPicture.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanelPicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelPicture.Location = new System.Drawing.Point(347, 0);
            this.tableLayoutPanelPicture.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelPicture.Name = "tableLayoutPanelPicture";
            this.tableLayoutPanelPicture.RowCount = 1;
            this.tableLayoutPanelPicture.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelPicture.Size = new System.Drawing.Size(174, 94);
            this.tableLayoutPanelPicture.TabIndex = 3;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox3.Location = new System.Drawing.Point(88, 2);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(39, 90);
            this.pictureBox3.TabIndex = 3;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox2.Location = new System.Drawing.Point(45, 2);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(39, 90);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox4.Location = new System.Drawing.Point(131, 2);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(41, 90);
            this.pictureBox4.TabIndex = 1;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(2, 2);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(39, 90);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // textBoxTweet
            // 
            this.textBoxTweet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxTweet.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxTweet.Location = new System.Drawing.Point(3, 3);
            this.textBoxTweet.Multiline = true;
            this.textBoxTweet.Name = "textBoxTweet";
            this.textBoxTweet.Size = new System.Drawing.Size(341, 88);
            this.textBoxTweet.TabIndex = 2;
            // 
            // PanelControlMainEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.tableLayoutPanelEdit);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Name = "PanelControlMainEdit";
            this.Size = new System.Drawing.Size(521, 180);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanelStatus.ResumeLayout(false);
            this.tableLayoutPanelInfo.ResumeLayout(false);
            this.tableLayoutPanelInfo.PerformLayout();
            this.tableLayoutPanelEdit.ResumeLayout(false);
            this.tableLayoutPanelEdit.PerformLayout();
            this.tableLayoutPanelPicture.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ComboBox comboBoxHash;
        public System.Windows.Forms.Button buttonClear;
        public System.Windows.Forms.Button buttonHash;
        public System.Windows.Forms.CheckBox checkBoxHash;
        public System.Windows.Forms.Button buttonSend;
        public System.Windows.Forms.TextBox textBoxMsg1;
        public System.Windows.Forms.TextBox textBoxMsg2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelStatus;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelInfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelEdit;
        public System.Windows.Forms.TextBox textBoxTweet;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelPicture;
        public System.Windows.Forms.PictureBox pictureBox4;
        public System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.PictureBox pictureBox3;
        public System.Windows.Forms.PictureBox pictureBox2;
    }
}
