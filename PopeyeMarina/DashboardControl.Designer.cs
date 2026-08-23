using PopeyeMarina.Theming;

namespace PopeyeMarina.Screens
{
    partial class DashboardControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private System.Windows.Forms.Panel contentPanel;

        private void InitializeComponent()
        {
            this.contentPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            //
            // contentPanel
            //
            this.contentPanel.Location = new System.Drawing.Point(0, 0);
            this.contentPanel.Size = new System.Drawing.Size(820, 560);
            this.contentPanel.Anchor = System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Bottom
                | System.Windows.Forms.AnchorStyles.Left
                | System.Windows.Forms.AnchorStyles.Right;
            //
            // DashboardControl
            //
            this.Controls.Add(this.contentPanel);
            this.Name = "DashboardControl";
            this.Size = new System.Drawing.Size(840, 560);
            this.ResumeLayout(false);
        }
    }
}
