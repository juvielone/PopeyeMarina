using PopeyeMarina.Theming;

namespace PopeyeMarina.Screens
{
    partial class SlipsControl
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
        private System.Windows.Forms.Panel formCardPanel;
        private System.Windows.Forms.Label cardTitleLabel;

        private System.Windows.Forms.Label widthLabel;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.Label lengthLabel;
        private System.Windows.Forms.TextBox txtLength;
        private System.Windows.Forms.Label dockLabel;
        private System.Windows.Forms.ComboBox cmbDock;

        private System.Windows.Forms.CheckBox chkCovered;
        private System.Windows.Forms.Label heightLabel;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.Label doorTypeLabel;
        private System.Windows.Forms.TextBox txtDoorType;

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        private System.Windows.Forms.DataGridView dgvSlips;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSlipID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDock;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWidth;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCovered;

        private void InitializeComponent()
        {
            headerLabel = new Label();
            formCardPanel = new Panel();
            btnCancel = new Button();
            btnSave = new Button();
            txtDoorType = new TextBox();
            doorTypeLabel = new Label();
            txtHeight = new TextBox();
            heightLabel = new Label();
            chkCovered = new CheckBox();
            cmbDock = new ComboBox();
            dockLabel = new Label();
            txtLength = new TextBox();
            lengthLabel = new Label();
            txtWidth = new TextBox();
            widthLabel = new Label();
            cardTitleLabel = new Label();
            dgvSlips = new DataGridView();
            colSlipID = new DataGridViewTextBoxColumn();
            colDock = new DataGridViewTextBoxColumn();
            colWidth = new DataGridViewTextBoxColumn();
            colLength = new DataGridViewTextBoxColumn();
            colCovered = new DataGridViewTextBoxColumn();
            formCardPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSlips).BeginInit();
            SuspendLayout();
            // 
            // headerLabel
            // 
            headerLabel.AutoSize = true;
            headerLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            headerLabel.ForeColor = Color.FromArgb(20, 20, 20);
            headerLabel.Location = new Point(0, 0);
            headerLabel.Name = "headerLabel";
            headerLabel.Size = new Size(67, 32);
            headerLabel.TabIndex = 2;
            headerLabel.Text = "Slips";
            // 
            // formCardPanel
            // 
            formCardPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            formCardPanel.BackColor = Color.White;
            formCardPanel.BorderStyle = BorderStyle.FixedSingle;
            formCardPanel.Controls.Add(btnCancel);
            formCardPanel.Controls.Add(btnSave);
            formCardPanel.Controls.Add(txtDoorType);
            formCardPanel.Controls.Add(doorTypeLabel);
            formCardPanel.Controls.Add(txtHeight);
            formCardPanel.Controls.Add(heightLabel);
            formCardPanel.Controls.Add(chkCovered);
            formCardPanel.Controls.Add(cmbDock);
            formCardPanel.Controls.Add(dockLabel);
            formCardPanel.Controls.Add(txtLength);
            formCardPanel.Controls.Add(lengthLabel);
            formCardPanel.Controls.Add(txtWidth);
            formCardPanel.Controls.Add(widthLabel);
            formCardPanel.Controls.Add(cardTitleLabel);
            formCardPanel.Location = new Point(0, 44);
            formCardPanel.Name = "formCardPanel";
            formCardPanel.Size = new Size(738, 230);
            formCardPanel.TabIndex = 1;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.White;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCancel.ForeColor = Color.FromArgb(100, 100, 100);
            btnCancel.Location = new Point(164, 184);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 34);
            btnCancel.TabIndex = 0;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Visible = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(12, 34, 51);
            btnSave.Cursor = Cursors.Hand;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(16, 184);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(140, 34);
            btnSave.TabIndex = 1;
            btnSave.Text = "Add Slip";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // txtDoorType
            // 
            txtDoorType.Font = new Font("Segoe UI", 10F);
            txtDoorType.Location = new Point(266, 136);
            txtDoorType.Name = "txtDoorType";
            txtDoorType.Size = new Size(200, 25);
            txtDoorType.TabIndex = 2;
            // 
            // doorTypeLabel
            // 
            doorTypeLabel.AutoSize = true;
            doorTypeLabel.Font = new Font("Segoe UI", 8.5F);
            doorTypeLabel.ForeColor = Color.FromArgb(100, 100, 100);
            doorTypeLabel.Location = new Point(266, 118);
            doorTypeLabel.Name = "doorTypeLabel";
            doorTypeLabel.Size = new Size(59, 15);
            doorTypeLabel.TabIndex = 3;
            doorTypeLabel.Text = "Door type";
            // 
            // txtHeight
            // 
            txtHeight.Font = new Font("Segoe UI", 10F);
            txtHeight.Location = new Point(150, 136);
            txtHeight.Name = "txtHeight";
            txtHeight.Size = new Size(100, 25);
            txtHeight.TabIndex = 4;
            // 
            // heightLabel
            // 
            heightLabel.AutoSize = true;
            heightLabel.Font = new Font("Segoe UI", 8.5F);
            heightLabel.ForeColor = Color.FromArgb(100, 100, 100);
            heightLabel.Location = new Point(150, 118);
            heightLabel.Name = "heightLabel";
            heightLabel.Size = new Size(65, 15);
            heightLabel.TabIndex = 5;
            heightLabel.Text = "Height (m)";
            // 
            // chkCovered
            // 
            chkCovered.AutoSize = true;
            chkCovered.Cursor = Cursors.Hand;
            chkCovered.Font = new Font("Segoe UI", 10F);
            chkCovered.ForeColor = Color.FromArgb(20, 20, 20);
            chkCovered.Location = new Point(16, 118);
            chkCovered.Name = "chkCovered";
            chkCovered.Size = new Size(103, 23);
            chkCovered.TabIndex = 6;
            chkCovered.Text = "Covered slip";
            chkCovered.CheckedChanged += chkCovered_CheckedChanged;
            // 
            // cmbDock
            // 
            cmbDock.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDock.Font = new Font("Segoe UI", 10F);
            cmbDock.Location = new Point(248, 64);
            cmbDock.Name = "cmbDock";
            cmbDock.Size = new Size(220, 25);
            cmbDock.TabIndex = 7;
            // 
            // dockLabel
            // 
            dockLabel.AutoSize = true;
            dockLabel.Font = new Font("Segoe UI", 8.5F);
            dockLabel.ForeColor = Color.FromArgb(100, 100, 100);
            dockLabel.Location = new Point(248, 46);
            dockLabel.Name = "dockLabel";
            dockLabel.Size = new Size(34, 15);
            dockLabel.TabIndex = 8;
            dockLabel.Text = "Dock";
            // 
            // txtLength
            // 
            txtLength.Font = new Font("Segoe UI", 10F);
            txtLength.Location = new Point(132, 64);
            txtLength.Name = "txtLength";
            txtLength.Size = new Size(100, 25);
            txtLength.TabIndex = 9;
            // 
            // lengthLabel
            // 
            lengthLabel.AutoSize = true;
            lengthLabel.Font = new Font("Segoe UI", 8.5F);
            lengthLabel.ForeColor = Color.FromArgb(100, 100, 100);
            lengthLabel.Location = new Point(132, 46);
            lengthLabel.Name = "lengthLabel";
            lengthLabel.Size = new Size(66, 15);
            lengthLabel.TabIndex = 10;
            lengthLabel.Text = "Length (m)";
            // 
            // txtWidth
            // 
            txtWidth.Font = new Font("Segoe UI", 10F);
            txtWidth.Location = new Point(16, 64);
            txtWidth.Name = "txtWidth";
            txtWidth.Size = new Size(100, 25);
            txtWidth.TabIndex = 11;
            // 
            // widthLabel
            // 
            widthLabel.AutoSize = true;
            widthLabel.Font = new Font("Segoe UI", 8.5F);
            widthLabel.ForeColor = Color.FromArgb(100, 100, 100);
            widthLabel.Location = new Point(16, 46);
            widthLabel.Name = "widthLabel";
            widthLabel.Size = new Size(61, 15);
            widthLabel.TabIndex = 12;
            widthLabel.Text = "Width (m)";
            // 
            // cardTitleLabel
            // 
            cardTitleLabel.AutoSize = true;
            cardTitleLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            cardTitleLabel.ForeColor = Color.FromArgb(20, 20, 20);
            cardTitleLabel.Location = new Point(16, 12);
            cardTitleLabel.Name = "cardTitleLabel";
            cardTitleLabel.Size = new Size(99, 20);
            cardTitleLabel.TabIndex = 13;
            cardTitleLabel.Text = "Add new slip";
            // 
            // dgvSlips
            // 
            dgvSlips.AllowUserToAddRows = false;
            dgvSlips.AllowUserToDeleteRows = false;
            dgvSlips.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvSlips.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSlips.BackgroundColor = Color.White;
            dgvSlips.Columns.AddRange(new DataGridViewColumn[] { colSlipID, colDock, colWidth, colLength, colCovered });
            dgvSlips.Location = new Point(0, 284);
            dgvSlips.MultiSelect = false;
            dgvSlips.Name = "dgvSlips";
            dgvSlips.ReadOnly = true;
            dgvSlips.RowHeadersVisible = false;
            dgvSlips.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSlips.Size = new Size(738, 361);
            dgvSlips.TabIndex = 0;
            dgvSlips.CellClick += dgvSlips_CellClick;
            // 
            // colSlipID
            // 
            colSlipID.HeaderText = "ID";
            colSlipID.Name = "colSlipID";
            colSlipID.ReadOnly = true;
            colSlipID.Visible = false;
            // 
            // colDock
            // 
            colDock.HeaderText = "Dock";
            colDock.Name = "colDock";
            colDock.ReadOnly = true;
            // 
            // colWidth
            // 
            colWidth.HeaderText = "Width (m)";
            colWidth.Name = "colWidth";
            colWidth.ReadOnly = true;
            // 
            // colLength
            // 
            colLength.HeaderText = "Length (m)";
            colLength.Name = "colLength";
            colLength.ReadOnly = true;
            // 
            // colCovered
            // 
            colCovered.HeaderText = "Covered";
            colCovered.Name = "colCovered";
            colCovered.ReadOnly = true;
            // 
            // SlipsControl
            // 
            Controls.Add(dgvSlips);
            Controls.Add(formCardPanel);
            Controls.Add(headerLabel);
            Name = "SlipsControl";
            Size = new Size(738, 711);
            formCardPanel.ResumeLayout(false);
            formCardPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSlips).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
