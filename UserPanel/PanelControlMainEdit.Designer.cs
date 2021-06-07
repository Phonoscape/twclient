
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
            this.textBoxTweet = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(521, 42);
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
            this.panel2.Location = new System.Drawing.Point(5, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(514, 30);
            this.panel2.TabIndex = 2;
            // 
            // comboBoxHash
            // 
            this.comboBoxHash.FormattingEnabled = true;
            this.comboBoxHash.Location = new System.Drawing.Point(111, 6);
            this.comboBoxHash.Name = "comboBoxHash";
            this.comboBoxHash.Size = new System.Drawing.Size(202, 20);
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
            this.checkBoxHash.Size = new System.Drawing.Size(100, 16);
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
            // textBoxTweet
            // 
            this.textBoxTweet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxTweet.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxTweet.Location = new System.Drawing.Point(0, 42);
            this.textBoxTweet.Multiline = true;
            this.textBoxTweet.Name = "textBoxTweet";
            this.textBoxTweet.Size = new System.Drawing.Size(521, 138);
            this.textBoxTweet.TabIndex = 1;
            // 
            // PanelControlMainEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.textBoxTweet);
            this.Controls.Add(this.panel1);
            this.Name = "PanelControlMainEdit";
            this.Size = new System.Drawing.Size(521, 180);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox comboBoxHash;
        public System.Windows.Forms.Button buttonClear;
        public System.Windows.Forms.Button buttonHash;
        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.TextBox textBoxTweet;
        public System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.CheckBox checkBoxHash;
        public System.Windows.Forms.Button buttonSend;
    }
}
