using PopeyeMarina.Theming;

namespace PopeyeMarina.Screens
{
    partial class CreateLeaseControl
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

        // Client information card
        private System.Windows.Forms.Panel clientCardPanel;
        private System.Windows.Forms.Label clientCardTitleLabel;
        private System.Windows.Forms.Label customerLabel;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Label vesselLabel;
        private System.Windows.Forms.ComboBox cmbVessel;

        // Slip and dates card
        private System.Windows.Forms.Panel slipCardPanel;
        private System.Windows.Forms.Label slipCardTitleLabel;
        private System.Windows.Forms.Label slipLabel;
        private System.Windows.Forms.ComboBox cmbSlip;
        private System.Windows.Forms.Label startDateLabel;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label endDateLabel;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label endDateComputedLabel;

        // Segmented toggle + type-specific fields
        private System.Windows.Forms.Button btnAnnual;
        private System.Windows.Forms.Button btnDaily;
        private System.Windows.Forms.CheckBox chkPayMonthly;
        private System.Windows.Forms.Label numberOfDaysLabel;

        private System.Windows.Forms.Button btnGenerate;

        private void InitializeComponent()
        {
            this.headerLabel = new System.Windows.Forms.Label();
            this.clientCardPanel = new System.Windows.Forms.Panel();
            this.cmbVessel = new System.Windows.Forms.ComboBox();
            this.vesselLabel = new System.Windows.Forms.Label();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.customerLabel = new System.Windows.Forms.Label();
            this.clientCardTitleLabel = new System.Windows.Forms.Label();
            this.slipCardPanel = new System.Windows.Forms.Panel();
            this.endDateComputedLabel = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.endDateLabel = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.startDateLabel = new System.Windows.Forms.Label();
            this.cmbSlip = new System.Windows.Forms.ComboBox();
            this.slipLabel = new System.Windows.Forms.Label();
            this.slipCardTitleLabel = new System.Windows.Forms.Label();
            this.btnAnnual = new System.Windows.Forms.Button();
            this.btnDaily = new System.Windows.Forms.Button();
            this.chkPayMonthly = new System.Windows.Forms.CheckBox();
            this.numberOfDaysLabel = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.clientCardPanel.SuspendLayout();
            this.slipCardPanel.SuspendLayout();
            this.SuspendLayout();
            //
            // headerLabel
            //
            this.headerLabel.AutoSize = true;
            this.headerLabel.Font = Theme.ScreenHeaderFont;
            this.headerLabel.ForeColor = Theme.PrimaryTextColor;
            this.headerLabel.Location = new System.Drawing.Point(0, 0);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Text = "Create Lease";
            //
            // clientCardPanel
            //
            this.clientCardPanel.BackColor = Theme.CardBackColor;
            this.clientCardPanel.BorderStyle = Theme.CardBorderStyle;
            this.clientCardPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.clientCardPanel.Location = new System.Drawing.Point(0, 44);
            this.clientCardPanel.Size = new System.Drawing.Size(820, 110);
            this.clientCardPanel.Controls.Add(this.cmbVessel);
            this.clientCardPanel.Controls.Add(this.vesselLabel);
            this.clientCardPanel.Controls.Add(this.cmbCustomer);
            this.clientCardPanel.Controls.Add(this.customerLabel);
            this.clientCardPanel.Controls.Add(this.clientCardTitleLabel);
            this.clientCardPanel.Name = "clientCardPanel";
            //
            // clientCardTitleLabel
            //
            this.clientCardTitleLabel.AutoSize = true;
            this.clientCardTitleLabel.Font = Theme.CardTitleFont;
            this.clientCardTitleLabel.ForeColor = Theme.PrimaryTextColor;
            this.clientCardTitleLabel.Location = new System.Drawing.Point(16, 12);
            this.clientCardTitleLabel.Name = "clientCardTitleLabel";
            this.clientCardTitleLabel.Text = "Client information";
            //
            // customerLabel
            //
            this.customerLabel.AutoSize = true;
            this.customerLabel.Font = Theme.FieldLabelFont;
            this.customerLabel.ForeColor = Theme.SecondaryTextColor;
            this.customerLabel.Location = new System.Drawing.Point(16, 46);
            this.customerLabel.Name = "customerLabel";
            this.customerLabel.Text = "Customer";
            //
            // cmbCustomer
            //
            this.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomer.Font = Theme.InputFont;
            this.cmbCustomer.Location = new System.Drawing.Point(16, 64);
            this.cmbCustomer.Size = new System.Drawing.Size(280, 25);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.SelectedIndexChanged += new System.EventHandler(this.cmbCustomer_SelectedIndexChanged);
            //
            // vesselLabel
            //
            this.vesselLabel.AutoSize = true;
            this.vesselLabel.Font = Theme.FieldLabelFont;
            this.vesselLabel.ForeColor = Theme.SecondaryTextColor;
            this.vesselLabel.Location = new System.Drawing.Point(312, 46);
            this.vesselLabel.Name = "vesselLabel";
            this.vesselLabel.Text = "Registered vessel";
            //
            // cmbVessel
            //
            this.cmbVessel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVessel.Font = Theme.InputFont;
            this.cmbVessel.Location = new System.Drawing.Point(312, 64);
            this.cmbVessel.Size = new System.Drawing.Size(280, 25);
            this.cmbVessel.Enabled = false;
            this.cmbVessel.Name = "cmbVessel";
            //
            // slipCardPanel
            //
            this.slipCardPanel.BackColor = Theme.CardBackColor;
            this.slipCardPanel.BorderStyle = Theme.CardBorderStyle;
            this.slipCardPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.slipCardPanel.Location = new System.Drawing.Point(0, 164);
            this.slipCardPanel.Size = new System.Drawing.Size(820, 130);
            this.slipCardPanel.Controls.Add(this.endDateComputedLabel);
            this.slipCardPanel.Controls.Add(this.dtpEndDate);
            this.slipCardPanel.Controls.Add(this.endDateLabel);
            this.slipCardPanel.Controls.Add(this.dtpStartDate);
            this.slipCardPanel.Controls.Add(this.startDateLabel);
            this.slipCardPanel.Controls.Add(this.cmbSlip);
            this.slipCardPanel.Controls.Add(this.slipLabel);
            this.slipCardPanel.Controls.Add(this.slipCardTitleLabel);
            this.slipCardPanel.Name = "slipCardPanel";
            //
            // slipCardTitleLabel
            //
            this.slipCardTitleLabel.AutoSize = true;
            this.slipCardTitleLabel.Font = Theme.CardTitleFont;
            this.slipCardTitleLabel.ForeColor = Theme.PrimaryTextColor;
            this.slipCardTitleLabel.Location = new System.Drawing.Point(16, 12);
            this.slipCardTitleLabel.Name = "slipCardTitleLabel";
            this.slipCardTitleLabel.Text = "Slip and dates";
            //
            // slipLabel
            //
            this.slipLabel.AutoSize = true;
            this.slipLabel.Font = Theme.FieldLabelFont;
            this.slipLabel.ForeColor = Theme.SecondaryTextColor;
            this.slipLabel.Location = new System.Drawing.Point(16, 46);
            this.slipLabel.Name = "slipLabel";
            this.slipLabel.Text = "Available slip";
            //
            // cmbSlip
            //
            this.cmbSlip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSlip.Font = Theme.InputFont;
            this.cmbSlip.Location = new System.Drawing.Point(16, 64);
            this.cmbSlip.Size = new System.Drawing.Size(300, 25);
            this.cmbSlip.Name = "cmbSlip";
            //
            // startDateLabel
            //
            this.startDateLabel.AutoSize = true;
            this.startDateLabel.Font = Theme.FieldLabelFont;
            this.startDateLabel.ForeColor = Theme.SecondaryTextColor;
            this.startDateLabel.Location = new System.Drawing.Point(332, 46);
            this.startDateLabel.Name = "startDateLabel";
            this.startDateLabel.Text = "Start date";
            //
            // dtpStartDate
            //
            this.dtpStartDate.Font = Theme.InputFont;
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(332, 64);
            this.dtpStartDate.Size = new System.Drawing.Size(150, 25);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            //
            // endDateLabel
            //
            this.endDateLabel.AutoSize = true;
            this.endDateLabel.Font = Theme.FieldLabelFont;
            this.endDateLabel.ForeColor = Theme.SecondaryTextColor;
            this.endDateLabel.Location = new System.Drawing.Point(498, 46);
            this.endDateLabel.Name = "endDateLabel";
            this.endDateLabel.Text = "End date";
            //
            // dtpEndDate
            //
            this.dtpEndDate.Font = Theme.InputFont;
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(498, 64);
            this.dtpEndDate.Size = new System.Drawing.Size(150, 25);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.dtpEndDate_ValueChanged);
            //
            // endDateComputedLabel
            //
            // Read-only stand-in shown instead of dtpEndDate when Annual is selected,
            // since EndDate is derived (StartDate + 1 year) rather than user-entered.
            this.endDateComputedLabel.AutoSize = true;
            this.endDateComputedLabel.Font = Theme.InputFont;
            this.endDateComputedLabel.ForeColor = Theme.PrimaryTextColor;
            this.endDateComputedLabel.Location = new System.Drawing.Point(498, 67);
            this.endDateComputedLabel.Name = "endDateComputedLabel";
            this.endDateComputedLabel.Text = "—";
            //
            // btnAnnual
            //
            this.btnAnnual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnnual.FlatAppearance.BorderSize = 0;
            this.btnAnnual.Font = Theme.ButtonFont;
            this.btnAnnual.Location = new System.Drawing.Point(0, 304);
            this.btnAnnual.Size = new System.Drawing.Size(200, 40);
            this.btnAnnual.Name = "btnAnnual";
            this.btnAnnual.Text = "Annual contract";
            this.btnAnnual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAnnual.Click += new System.EventHandler(this.LeaseTypeToggle_Click);
            //
            // btnDaily
            //
            this.btnDaily.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDaily.FlatAppearance.BorderSize = 0;
            this.btnDaily.Font = Theme.ButtonFont;
            this.btnDaily.Location = new System.Drawing.Point(204, 304);
            this.btnDaily.Size = new System.Drawing.Size(200, 40);
            this.btnDaily.Name = "btnDaily";
            this.btnDaily.Text = "Daily transient";
            this.btnDaily.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDaily.Click += new System.EventHandler(this.LeaseTypeToggle_Click);
            //
            // chkPayMonthly
            //
            this.chkPayMonthly.AutoSize = true;
            this.chkPayMonthly.Font = Theme.InputFont;
            this.chkPayMonthly.ForeColor = Theme.PrimaryTextColor;
            this.chkPayMonthly.Location = new System.Drawing.Point(0, 356);
            this.chkPayMonthly.Name = "chkPayMonthly";
            this.chkPayMonthly.Text = "Customer pays monthly";
            this.chkPayMonthly.Cursor = System.Windows.Forms.Cursors.Hand;
            //
            // numberOfDaysLabel
            //
            // Read-only, live-computed from Start/End date — not a user input.
            this.numberOfDaysLabel.AutoSize = true;
            this.numberOfDaysLabel.Font = Theme.InputFont;
            this.numberOfDaysLabel.ForeColor = Theme.PrimaryTextColor;
            this.numberOfDaysLabel.Location = new System.Drawing.Point(0, 356);
            this.numberOfDaysLabel.Name = "numberOfDaysLabel";
            this.numberOfDaysLabel.Text = "Number of days: —";
            //
            // btnGenerate
            //
            this.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerate.FlatAppearance.BorderSize = 0;
            this.btnGenerate.BackColor = Theme.PrimaryButtonBackColor;
            this.btnGenerate.ForeColor = Theme.PrimaryButtonForeColor;
            this.btnGenerate.Font = Theme.ButtonFont;
            this.btnGenerate.Location = new System.Drawing.Point(0, 396);
            this.btnGenerate.Size = new System.Drawing.Size(160, 38);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Text = "Generate Lease";
            this.btnGenerate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            //
            // CreateLeaseControl
            //
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.numberOfDaysLabel);
            this.Controls.Add(this.chkPayMonthly);
            this.Controls.Add(this.btnDaily);
            this.Controls.Add(this.btnAnnual);
            this.Controls.Add(this.slipCardPanel);
            this.Controls.Add(this.clientCardPanel);
            this.Controls.Add(this.headerLabel);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Name = "CreateLeaseControl";
            this.Size = new System.Drawing.Size(820, 650);
            this.clientCardPanel.ResumeLayout(false);
            this.clientCardPanel.PerformLayout();
            this.slipCardPanel.ResumeLayout(false);
            this.slipCardPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
