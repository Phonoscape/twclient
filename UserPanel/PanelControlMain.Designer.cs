
namespace twclient.UserPanel
{
    partial class PanelControlMain
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.control_Main_Edit1 = new twclient.UserPanel.PanelControlMainEdit();
            this.control_Main_Tree1 = new twclient.UserPanel.PanelControlMainTree();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.control_Main_Edit1);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.control_Main_Tree1);
            this.splitContainer1.Size = new System.Drawing.Size(789, 464);
            this.splitContainer1.SplitterDistance = 478;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 0;
            // 
            // control_Main_Edit1
            // 
            this.control_Main_Edit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.control_Main_Edit1.Location = new System.Drawing.Point(0, 0);
            this.control_Main_Edit1.Margin = new System.Windows.Forms.Padding(6);
            this.control_Main_Edit1.Name = "control_Main_Edit1";
            this.control_Main_Edit1.Size = new System.Drawing.Size(478, 464);
            this.control_Main_Edit1.TabIndex = 0;
            // 
            // control_Main_Tree1
            // 
            this.control_Main_Tree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.control_Main_Tree1.Location = new System.Drawing.Point(0, 0);
            this.control_Main_Tree1.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.control_Main_Tree1.Name = "control_Main_Tree1";
            this.control_Main_Tree1.Size = new System.Drawing.Size(305, 464);
            this.control_Main_Tree1.TabIndex = 1;
            // 
            // PanelControlMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PanelControlMain";
            this.Size = new System.Drawing.Size(789, 464);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private PanelControlMainEdit control_Main_Edit1;
        private PanelControlMainTree control_Main_Tree1;
    }
}
