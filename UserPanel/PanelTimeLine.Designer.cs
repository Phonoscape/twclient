
namespace twclient.UserPanel
{
    partial class PanelTimeLine
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
            this.panelTimeLineList1 = new twclient.UserPanel.PanelTimeLineList();
            this.SuspendLayout();
            // 
            // panelTimeLineList1
            // 
            this.panelTimeLineList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTimeLineList1.Location = new System.Drawing.Point(0, 0);
            this.panelTimeLineList1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.panelTimeLineList1.Name = "panelTimeLineList1";
            this.panelTimeLineList1.Size = new System.Drawing.Size(678, 585);
            this.panelTimeLineList1.TabIndex = 1;
            // 
            // PanelTimeLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.panelTimeLineList1);
            this.Name = "PanelTimeLine";
            this.Size = new System.Drawing.Size(678, 585);
            this.ResumeLayout(false);

        }

        #endregion

        public PanelTimeLineList panelTimeLineList1;
    }
}
