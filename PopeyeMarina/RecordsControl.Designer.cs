using PopeyeMarina.Theming;

namespace PopeyeMarina.Screens
{
    partial class RecordsControl
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
        private System.Windows.Forms.Button btnBoatsTab;
        private System.Windows.Forms.Button btnDocksTab;
        private System.Windows.Forms.Panel subContentPanel;

        private void InitializeComponent()
        {
            this.headerLabel = new System.Windows.Forms.Label();
            this.btnBoatsTab = new System.Windows.Forms.Button();
            this.btnDocksTab = new System.Windows.Forms.Button();
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
            this.headerLabel.Text = "Records";
            //
            // btnBoatsTab
            //
            this.btnBoatsTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBoatsTab.FlatAppearance.BorderSize = 0;
            this.btnBoatsTab.Font = Theme.ButtonFont;
            this.btnBoatsTab.Location = new System.Drawing.Point(0, 44);
            this.btnBoatsTab.Size = new System.Drawing.Size(110, 34);
            this.btnBoatsTab.Name = "btnBoatsTab";
            this.btnBoatsTab.Text = "Boats";
            this.btnBoatsTab.Tag = "Boats";
            this.btnBoatsTab.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBoatsTab.Click += new System.EventHandler(this.SubTab_Click);
            //
            // btnDocksTab
            //
            this.btnDocksTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDocksTab.FlatAppearance.BorderSize = 0;
            this.btnDocksTab.Font = Theme.ButtonFont;
            this.btnDocksTab.Location = new System.Drawing.Point(114, 44);
            this.btnDocksTab.Size = new System.Drawing.Size(110, 34);
            this.btnDocksTab.Name = "btnDocksTab";
            this.btnDocksTab.Text = "Docks";
            this.btnDocksTab.Tag = "Docks";
            this.btnDocksTab.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDocksTab.Click += new System.EventHandler(this.SubTab_Click);
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
            // RecordsControl
            //
            this.Controls.Add(this.subContentPanel);
            this.Controls.Add(this.btnDocksTab);
            this.Controls.Add(this.btnBoatsTab);
            this.Controls.Add(this.headerLabel);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Name = "RecordsControl";
            this.Size = new System.Drawing.Size(820, 650);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
