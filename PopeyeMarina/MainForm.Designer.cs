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
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnLeases;
        private System.Windows.Forms.Button btnSlips;
        private System.Windows.Forms.Button btnCustomers;
        private System.Windows.Forms.Button btnRecords;
        private System.Windows.Forms.Button btnBoatHire;
        private System.Windows.Forms.Panel contentPanel;

        private void InitializeComponent()
        {
            sidebarPanel = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            logoLabel = new Label();
            btnDashboard = new Button();
            btnCustomers = new Button();
            btnBoatHire = new Button();
            btnRecords = new Button();
            btnSlips = new Button();
            btnLeases = new Button();
            contentPanel = new Panel();
            sidebarPanel.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // sidebarPanel
            // 
            sidebarPanel.BackColor = Color.FromArgb(118, 70, 166);
            sidebarPanel.Controls.Add(tableLayoutPanel1);
            sidebarPanel.Dock = DockStyle.Left;
            sidebarPanel.Location = new Point(0, 0);
            sidebarPanel.Name = "sidebarPanel";
            sidebarPanel.Size = new Size(220, 650);
            sidebarPanel.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(logoLabel, 0, 0);
            tableLayoutPanel1.Controls.Add(btnDashboard, 0, 1);
            tableLayoutPanel1.Controls.Add(btnCustomers, 0, 2);
            tableLayoutPanel1.Controls.Add(btnBoatHire, 0, 3);
            tableLayoutPanel1.Controls.Add(btnRecords, 0, 4);
            tableLayoutPanel1.Controls.Add(btnSlips, 0, 5);
            tableLayoutPanel1.Controls.Add(btnLeases, 0, 6);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 8;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.1676636F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11.9760466F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11.9760466F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11.9760466F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11.9760466F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11.9760466F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11.9760466F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11.9760466F));
            tableLayoutPanel1.Size = new Size(220, 650);
            tableLayoutPanel1.TabIndex = 8;
            // 
            // logoLabel
            // 
            logoLabel.AutoSize = true;
            logoLabel.BackColor = Color.Transparent;
            logoLabel.Dock = DockStyle.Fill;
            logoLabel.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            logoLabel.ForeColor = Color.FromArgb(93, 202, 165);
            logoLabel.Image = Properties.Resources.Icon;
            logoLabel.ImageAlign = ContentAlignment.MiddleLeft;
            logoLabel.Location = new Point(3, 0);
            logoLabel.Name = "logoLabel";
            logoLabel.Padding = new Padding(10, 0, 0, 0);
            logoLabel.Size = new Size(214, 105);
            logoLabel.TabIndex = 7;
            logoLabel.Text = "Popeye Marina";
            logoLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnDashboard
            // 
            btnDashboard.BackColor = Color.FromArgb(118, 70, 166);
            btnDashboard.Cursor = Cursors.Hand;
            btnDashboard.Dock = DockStyle.Fill;
            btnDashboard.FlatAppearance.BorderSize = 0;
            btnDashboard.FlatStyle = FlatStyle.Flat;
            btnDashboard.Font = new Font("Segoe UI", 10F);
            btnDashboard.ForeColor = Color.White;
            btnDashboard.Image = Properties.Resources.home;
            btnDashboard.Location = new Point(3, 108);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Padding = new Padding(20, 0, 0, 0);
            btnDashboard.Size = new Size(214, 71);
            btnDashboard.TabIndex = 5;
            btnDashboard.Tag = "Dashboard";
            btnDashboard.Text = "Dashboard";
            btnDashboard.TextAlign = ContentAlignment.MiddleLeft;
            btnDashboard.UseVisualStyleBackColor = false;
            btnDashboard.Click += NavButton_Click;
            // 
            // btnCustomers
            // 
            btnCustomers.BackColor = Color.FromArgb(118, 70, 166);
            btnCustomers.Cursor = Cursors.Hand;
            btnCustomers.Dock = DockStyle.Fill;
            btnCustomers.FlatAppearance.BorderSize = 0;
            btnCustomers.FlatStyle = FlatStyle.Flat;
            btnCustomers.Font = new Font("Segoe UI", 10F);
            btnCustomers.ForeColor = Color.FromArgb(155, 170, 181);
            btnCustomers.Image = Properties.Resources.cust;
            btnCustomers.Location = new Point(3, 185);
            btnCustomers.Name = "btnCustomers";
            btnCustomers.Padding = new Padding(20, 0, 0, 0);
            btnCustomers.Size = new Size(214, 71);
            btnCustomers.TabIndex = 2;
            btnCustomers.Tag = "Customers";
            btnCustomers.Text = "Customers";
            btnCustomers.TextAlign = ContentAlignment.MiddleLeft;
            btnCustomers.UseVisualStyleBackColor = false;
            btnCustomers.Click += NavButton_Click;
            // 
            // btnBoatHire
            // 
            btnBoatHire.BackColor = Color.FromArgb(118, 70, 166);
            btnBoatHire.Cursor = Cursors.Hand;
            btnBoatHire.Dock = DockStyle.Fill;
            btnBoatHire.FlatAppearance.BorderSize = 0;
            btnBoatHire.FlatStyle = FlatStyle.Flat;
            btnBoatHire.Font = new Font("Segoe UI", 10F);
            btnBoatHire.ForeColor = Color.FromArgb(155, 170, 181);
            btnBoatHire.Image = Properties.Resources.boat;
            btnBoatHire.Location = new Point(3, 262);
            btnBoatHire.Name = "btnBoatHire";
            btnBoatHire.Padding = new Padding(20, 0, 0, 0);
            btnBoatHire.Size = new Size(214, 71);
            btnBoatHire.TabIndex = 0;
            btnBoatHire.Tag = "Boat Hire";
            btnBoatHire.Text = "Boat Hire";
            btnBoatHire.TextAlign = ContentAlignment.MiddleLeft;
            btnBoatHire.UseVisualStyleBackColor = false;
            btnBoatHire.Click += NavButton_Click;
            // 
            // btnRecords
            // 
            btnRecords.BackColor = Color.FromArgb(118, 70, 166);
            btnRecords.Cursor = Cursors.Hand;
            btnRecords.Dock = DockStyle.Fill;
            btnRecords.FlatAppearance.BorderSize = 0;
            btnRecords.FlatStyle = FlatStyle.Flat;
            btnRecords.Font = new Font("Segoe UI", 10F);
            btnRecords.ForeColor = Color.FromArgb(155, 170, 181);
            btnRecords.Image = Properties.Resources.recs;
            btnRecords.Location = new Point(3, 339);
            btnRecords.Name = "btnRecords";
            btnRecords.Padding = new Padding(20, 0, 0, 0);
            btnRecords.Size = new Size(214, 71);
            btnRecords.TabIndex = 1;
            btnRecords.Tag = "Records";
            btnRecords.Text = "Records";
            btnRecords.TextAlign = ContentAlignment.MiddleLeft;
            btnRecords.UseVisualStyleBackColor = false;
            btnRecords.Click += NavButton_Click;
            // 
            // btnSlips
            // 
            btnSlips.BackColor = Color.FromArgb(118, 70, 166);
            btnSlips.Cursor = Cursors.Hand;
            btnSlips.Dock = DockStyle.Fill;
            btnSlips.FlatAppearance.BorderSize = 0;
            btnSlips.FlatStyle = FlatStyle.Flat;
            btnSlips.Font = new Font("Segoe UI", 10F);
            btnSlips.ForeColor = Color.FromArgb(155, 170, 181);
            btnSlips.Image = Properties.Resources.slips;
            btnSlips.Location = new Point(3, 416);
            btnSlips.Name = "btnSlips";
            btnSlips.Padding = new Padding(20, 0, 0, 0);
            btnSlips.Size = new Size(214, 71);
            btnSlips.TabIndex = 3;
            btnSlips.Tag = "Slips";
            btnSlips.Text = "Slips";
            btnSlips.TextAlign = ContentAlignment.MiddleLeft;
            btnSlips.UseVisualStyleBackColor = false;
            btnSlips.Click += NavButton_Click;
            // 
            // btnLeases
            // 
            btnLeases.BackColor = Color.FromArgb(118, 70, 166);
            btnLeases.Cursor = Cursors.Hand;
            btnLeases.Dock = DockStyle.Fill;
            btnLeases.FlatAppearance.BorderSize = 0;
            btnLeases.FlatStyle = FlatStyle.Flat;
            btnLeases.Font = new Font("Segoe UI", 10F);
            btnLeases.ForeColor = Color.FromArgb(155, 170, 181);
            btnLeases.Image = Properties.Resources.lease;
            btnLeases.Location = new Point(3, 493);
            btnLeases.Name = "btnLeases";
            btnLeases.Padding = new Padding(20, 0, 0, 0);
            btnLeases.Size = new Size(214, 71);
            btnLeases.TabIndex = 4;
            btnLeases.Tag = "Leases";
            btnLeases.Text = "Leases";
            btnLeases.TextAlign = ContentAlignment.MiddleLeft;
            btnLeases.UseVisualStyleBackColor = false;
            btnLeases.Click += NavButton_Click;
            // 
            // contentPanel
            // 
            contentPanel.BackColor = Color.White;
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.Location = new Point(220, 0);
            contentPanel.Name = "contentPanel";
            contentPanel.Padding = new Padding(24);
            contentPanel.Size = new Size(880, 650);
            contentPanel.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 650);
            Controls.Add(contentPanel);
            Controls.Add(sidebarPanel);
            MinimumSize = new Size(900, 550);
            Name = "MainForm";
            Text = "Popeye Marina - Harbor Master Admin";
            sidebarPanel.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        private TableLayoutPanel tableLayoutPanel1;
    }
}