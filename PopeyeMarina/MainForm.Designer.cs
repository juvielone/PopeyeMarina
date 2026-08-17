using PopeyeMarina.Theming;

namespace PopeyeMarina
{
    partial class MainForm
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

        private System.Windows.Forms.Panel sidebarPanel;
        private System.Windows.Forms.Label logoLabel;
        private System.Windows.Forms.Label subtitleLabel;
        private System.Windows.Forms.Button btnLeases;
        private System.Windows.Forms.Button btnSlips;
        private System.Windows.Forms.Button btnCustomers;
        private System.Windows.Forms.Button btnRecords;
        private System.Windows.Forms.Panel contentPanel;

        private void InitializeComponent()
        {
            this.sidebarPanel = new System.Windows.Forms.Panel();
            this.logoLabel = new System.Windows.Forms.Label();
            this.subtitleLabel = new System.Windows.Forms.Label();
            this.btnLeases = new System.Windows.Forms.Button();
            this.btnSlips = new System.Windows.Forms.Button();
            this.btnCustomers = new System.Windows.Forms.Button();
            this.btnRecords = new System.Windows.Forms.Button();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.sidebarPanel.SuspendLayout();
            this.SuspendLayout();
            //
            // sidebarPanel
            //
            this.sidebarPanel.BackColor = Theme.SidebarColor;
            this.sidebarPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebarPanel.Width = Theme.SidebarWidth;
            this.sidebarPanel.Controls.Add(this.btnRecords);
            this.sidebarPanel.Controls.Add(this.btnCustomers);
            this.sidebarPanel.Controls.Add(this.btnSlips);
            this.sidebarPanel.Controls.Add(this.btnLeases);
            this.sidebarPanel.Controls.Add(this.subtitleLabel);
            this.sidebarPanel.Controls.Add(this.logoLabel);
            this.sidebarPanel.Name = "sidebarPanel";
            //
            // logoLabel
            //
            this.logoLabel.AutoSize = true;
            this.logoLabel.BackColor = System.Drawing.Color.Transparent;
            this.logoLabel.ForeColor = Theme.AccentColor;
            this.logoLabel.Font = Theme.LogoFont;
            this.logoLabel.Location = new System.Drawing.Point(20, 24);
            this.logoLabel.Name = "logoLabel";
            this.logoLabel.Text = "Popeye Marina";
            //
            // subtitleLabel
            //
            this.subtitleLabel.AutoSize = true;
            this.subtitleLabel.BackColor = System.Drawing.Color.Transparent;
            this.subtitleLabel.ForeColor = Theme.MutedTextColor;
            this.subtitleLabel.Font = Theme.SubtitleFont;
            this.subtitleLabel.Location = new System.Drawing.Point(20, 58);
            this.subtitleLabel.Name = "subtitleLabel";
            this.subtitleLabel.Text = "Harbor master admin";
            //
            // btnLeases
            //
            this.btnLeases.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLeases.FlatAppearance.BorderSize = 0;
            this.btnLeases.BackColor = Theme.SidebarColor;
            this.btnLeases.ForeColor = System.Drawing.Color.White;
            this.btnLeases.Font = Theme.NavFont(selected: false);
            this.btnLeases.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLeases.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnLeases.Location = new System.Drawing.Point(0, 110);
            this.btnLeases.Size = new System.Drawing.Size(Theme.SidebarWidth, Theme.NavButtonHeight);
            this.btnLeases.Name = "btnLeases";
            this.btnLeases.Text = "Leases";
            this.btnLeases.Tag = "Leases";
            this.btnLeases.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLeases.Click += new System.EventHandler(this.NavButton_Click);
            //
            // btnSlips
            //
            this.btnSlips.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSlips.FlatAppearance.BorderSize = 0;
            this.btnSlips.BackColor = Theme.SidebarColor;
            this.btnSlips.ForeColor = Theme.MutedTextColor;
            this.btnSlips.Font = Theme.NavFont(selected: false);
            this.btnSlips.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSlips.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnSlips.Location = new System.Drawing.Point(0, 110 + Theme.NavButtonHeight);
            this.btnSlips.Size = new System.Drawing.Size(Theme.SidebarWidth, Theme.NavButtonHeight);
            this.btnSlips.Name = "btnSlips";
            this.btnSlips.Text = "Slips";
            this.btnSlips.Tag = "Slips";
            this.btnSlips.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSlips.Click += new System.EventHandler(this.NavButton_Click);
            //
            // btnCustomers
            //
            this.btnCustomers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCustomers.FlatAppearance.BorderSize = 0;
            this.btnCustomers.BackColor = Theme.SidebarColor;
            this.btnCustomers.ForeColor = Theme.MutedTextColor;
            this.btnCustomers.Font = Theme.NavFont(selected: false);
            this.btnCustomers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCustomers.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnCustomers.Location = new System.Drawing.Point(0, 110 + (Theme.NavButtonHeight * 2));
            this.btnCustomers.Size = new System.Drawing.Size(Theme.SidebarWidth, Theme.NavButtonHeight);
            this.btnCustomers.Name = "btnCustomers";
            this.btnCustomers.Text = "Customers";
            this.btnCustomers.Tag = "Customers";
            this.btnCustomers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCustomers.Click += new System.EventHandler(this.NavButton_Click);
            //
            // btnRecords
            //
            this.btnRecords.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecords.FlatAppearance.BorderSize = 0;
            this.btnRecords.BackColor = Theme.SidebarColor;
            this.btnRecords.ForeColor = Theme.MutedTextColor;
            this.btnRecords.Font = Theme.NavFont(selected: false);
            this.btnRecords.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRecords.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnRecords.Location = new System.Drawing.Point(0, 110 + (Theme.NavButtonHeight * 3));
            this.btnRecords.Size = new System.Drawing.Size(Theme.SidebarWidth, Theme.NavButtonHeight);
            this.btnRecords.Name = "btnRecords";
            this.btnRecords.Text = "Records";
            this.btnRecords.Tag = "Records";
            this.btnRecords.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRecords.Click += new System.EventHandler(this.NavButton_Click);
            //
            // contentPanel
            //
            this.contentPanel.BackColor = Theme.ContentBackColor;
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Padding = new System.Windows.Forms.Padding(Theme.ContentPadding);
            this.contentPanel.Name = "contentPanel";
            //
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 650);
            this.MinimumSize = new System.Drawing.Size(900, 550);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.sidebarPanel);
            this.Name = "MainForm";
            this.Text = "Popeye Marina - Harbor Master Admin";
            this.sidebarPanel.ResumeLayout(false);
            this.sidebarPanel.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}