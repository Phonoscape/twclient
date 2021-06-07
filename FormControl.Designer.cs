
namespace twclient
{
    partial class FormControl
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
            this.control_Main1 = new twclient.UserPanel.PanelControlMain();
            this.SuspendLayout();
            // 
            // control_Main1
            // 
            this.control_Main1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.control_Main1.Location = new System.Drawing.Point(0, 0);
            this.control_Main1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.control_Main1.Name = "control_Main1";
            this.control_Main1.Size = new System.Drawing.Size(800, 450);
            this.control_Main1.TabIndex = 0;
            // 
            // FormControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.control_Main1);
            this.Name = "FormControl";
            this.Text = "FormControl";
            this.Load += new System.EventHandler(this.FormControl_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UserPanel.PanelControlMain control_Main1;
    }
}