using PopeyeMarina.Theming;

namespace PopeyeMarina.Screens
{
    partial class LeasesControl
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

        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.Button btnCreateTab;
        private System.Windows.Forms.Button btnSearchTab;
        private System.Windows.Forms.Panel subContentPanel;

        private void InitializeComponent()
        {
            this.headerLabel = new System.Windows.Forms.Label();
            this.btnCreateTab = new System.Windows.Forms.Button();
            this.btnSearchTab = new System.Windows.Forms.Button();
            this.subContentPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            //
            // headerLabel
            //
            this.headerLabel.AutoSize = true;
            this.headerLabel.Font = Theme.ScreenHeaderFont;
            this.headerLabel.ForeColor = Theme.PrimaryTextColor;
            this.headerLabel.Location = new System.Drawing.Point(0, 0);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Text = "Leases";
            //
            // btnCreateTab
            //
            this.btnCreateTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateTab.FlatAppearance.BorderSize = 0;
            this.btnCreateTab.Font = Theme.ButtonFont;
            this.btnCreateTab.Location = new System.Drawing.Point(0, 44);
            this.btnCreateTab.Size = new System.Drawing.Size(140, 34);
            this.btnCreateTab.Name = "btnCreateTab";
            this.btnCreateTab.Text = "Create Lease";
            this.btnCreateTab.Tag = "Create";
            this.btnCreateTab.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreateTab.Click += new System.EventHandler(this.SubTab_Click);
            //
            // btnSearchTab
            //
            this.btnSearchTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchTab.FlatAppearance.BorderSize = 0;
            this.btnSearchTab.Font = Theme.ButtonFont;
            this.btnSearchTab.Location = new System.Drawing.Point(144, 44);
            this.btnSearchTab.Size = new System.Drawing.Size(140, 34);
            this.btnSearchTab.Name = "btnSearchTab";
            this.btnSearchTab.Text = "Search Leases";
            this.btnSearchTab.Tag = "Search";
            this.btnSearchTab.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearchTab.Click += new System.EventHandler(this.SubTab_Click);
            //
            // subContentPanel
            //
            this.subContentPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.subContentPanel.Location = new System.Drawing.Point(0, 90);
            this.subContentPanel.Size = new System.Drawing.Size(820, 500);
            this.subContentPanel.Name = "subContentPanel";
            //
            // LeasesControl
            //
            this.Controls.Add(this.subContentPanel);
            this.Controls.Add(this.btnSearchTab);
            this.Controls.Add(this.btnCreateTab);
            this.Controls.Add(this.headerLabel);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Name = "LeasesControl";
            this.Size = new System.Drawing.Size(820, 650);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
