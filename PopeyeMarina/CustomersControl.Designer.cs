using PopeyeMarina.Theming;

namespace PopeyeMarina.Screens
{
    partial class CustomersControl
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
        private System.Windows.Forms.Panel addCardPanel;
        private System.Windows.Forms.Label cardTitleLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label addressLabel;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label phoneLabel;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Button btnAddCustomer;
        private System.Windows.Forms.DataGridView dgvCustomers;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustomerID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPhone;

        private void InitializeComponent()
        {
            this.headerLabel = new System.Windows.Forms.Label();
            this.addCardPanel = new System.Windows.Forms.Panel();
            this.btnAddCustomer = new System.Windows.Forms.Button();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.phoneLabel = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.addressLabel = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.cardTitleLabel = new System.Windows.Forms.Label();
            this.dgvCustomers = new System.Windows.Forms.DataGridView();
            this.colCustomerID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addCardPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).BeginInit();
            this.SuspendLayout();
            //
            // headerLabel
            //
            this.headerLabel.AutoSize = true;
            this.headerLabel.Font = Theme.ScreenHeaderFont;
            this.headerLabel.ForeColor = Theme.PrimaryTextColor;
            this.headerLabel.Location = new System.Drawing.Point(0, 0);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Text = "Customers";
            //
            // addCardPanel
            //
            this.addCardPanel.BackColor = Theme.CardBackColor;
            this.addCardPanel.BorderStyle = Theme.CardBorderStyle;
            this.addCardPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.addCardPanel.Location = new System.Drawing.Point(0, 44);
            this.addCardPanel.Size = new System.Drawing.Size(820, 150);
            this.addCardPanel.Padding = new System.Windows.Forms.Padding(16);
            this.addCardPanel.Controls.Add(this.btnAddCustomer);
            this.addCardPanel.Controls.Add(this.txtPhone);
            this.addCardPanel.Controls.Add(this.phoneLabel);
            this.addCardPanel.Controls.Add(this.txtAddress);
            this.addCardPanel.Controls.Add(this.addressLabel);
            this.addCardPanel.Controls.Add(this.txtName);
            this.addCardPanel.Controls.Add(this.nameLabel);
            this.addCardPanel.Controls.Add(this.cardTitleLabel);
            this.addCardPanel.Name = "addCardPanel";
            //
            // cardTitleLabel
            //
            this.cardTitleLabel.AutoSize = true;
            this.cardTitleLabel.Font = Theme.CardTitleFont;
            this.cardTitleLabel.ForeColor = Theme.PrimaryTextColor;
            this.cardTitleLabel.Location = new System.Drawing.Point(16, 12);
            this.cardTitleLabel.Name = "cardTitleLabel";
            this.cardTitleLabel.Text = "Add new customer";
            //
            // nameLabel
            //
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = Theme.FieldLabelFont;
            this.nameLabel.ForeColor = Theme.SecondaryTextColor;
            this.nameLabel.Location = new System.Drawing.Point(16, 46);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Text = "Name";
            //
            // txtName
            //
            this.txtName.Font = Theme.InputFont;
            this.txtName.Location = new System.Drawing.Point(16, 64);
            this.txtName.Size = new System.Drawing.Size(230, 25);
            this.txtName.Name = "txtName";
            //
            // addressLabel
            //
            this.addressLabel.AutoSize = true;
            this.addressLabel.Font = Theme.FieldLabelFont;
            this.addressLabel.ForeColor = Theme.SecondaryTextColor;
            this.addressLabel.Location = new System.Drawing.Point(262, 46);
            this.addressLabel.Name = "addressLabel";
            this.addressLabel.Text = "Address";
            //
            // txtAddress
            //
            this.txtAddress.Font = Theme.InputFont;
            this.txtAddress.Location = new System.Drawing.Point(262, 64);
            this.txtAddress.Size = new System.Drawing.Size(300, 25);
            this.txtAddress.Name = "txtAddress";
            //
            // phoneLabel
            //
            this.phoneLabel.AutoSize = true;
            this.phoneLabel.Font = Theme.FieldLabelFont;
            this.phoneLabel.ForeColor = Theme.SecondaryTextColor;
            this.phoneLabel.Location = new System.Drawing.Point(578, 46);
            this.phoneLabel.Name = "phoneLabel";
            this.phoneLabel.Text = "Phone";
            //
            // txtPhone
            //
            this.txtPhone.Font = Theme.InputFont;
            this.txtPhone.Location = new System.Drawing.Point(578, 64);
            this.txtPhone.Size = new System.Drawing.Size(160, 25);
            this.txtPhone.Name = "txtPhone";
            //
            // btnAddCustomer
            //
            this.btnAddCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCustomer.FlatAppearance.BorderSize = 0;
            this.btnAddCustomer.BackColor = Theme.PrimaryButtonBackColor;
            this.btnAddCustomer.ForeColor = Theme.PrimaryButtonForeColor;
            this.btnAddCustomer.Font = Theme.ButtonFont;
            this.btnAddCustomer.Location = new System.Drawing.Point(16, 104);
            this.btnAddCustomer.Size = new System.Drawing.Size(140, 34);
            this.btnAddCustomer.Name = "btnAddCustomer";
            this.btnAddCustomer.Text = "Add Customer";
            this.btnAddCustomer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddCustomer.Click += new System.EventHandler(this.btnAddCustomer_Click);
            //
            // dgvCustomers
            //
            this.dgvCustomers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCustomers.Location = new System.Drawing.Point(0, 210);
            this.dgvCustomers.Size = new System.Drawing.Size(820, 380);
            this.dgvCustomers.AllowUserToAddRows = false;
            this.dgvCustomers.AllowUserToDeleteRows = false;
            this.dgvCustomers.ReadOnly = true;
            this.dgvCustomers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCustomers.MultiSelect = false;
            this.dgvCustomers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCustomers.BackgroundColor = Theme.ContentBackColor;
            this.dgvCustomers.BorderStyle = Theme.CardBorderStyle;
            this.dgvCustomers.RowHeadersVisible = false;
            this.dgvCustomers.Name = "dgvCustomers";
            this.dgvCustomers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCustomerID,
            this.colName,
            this.colAddress,
            this.colPhone});
            //
            // colCustomerID
            //
            this.colCustomerID.HeaderText = "ID";
            this.colCustomerID.Name = "colCustomerID";
            this.colCustomerID.Visible = false;
            //
            // colName
            //
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            //
            // colAddress
            //
            this.colAddress.HeaderText = "Address";
            this.colAddress.Name = "colAddress";
            //
            // colPhone
            //
            this.colPhone.HeaderText = "Phone";
            this.colPhone.Name = "colPhone";
            //
            // CustomersControl
            //
            this.Controls.Add(this.dgvCustomers);
            this.Controls.Add(this.addCardPanel);
            this.Controls.Add(this.headerLabel);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Name = "CustomersControl";
            this.Size = new System.Drawing.Size(820, 600);
            this.addCardPanel.ResumeLayout(false);
            this.addCardPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}