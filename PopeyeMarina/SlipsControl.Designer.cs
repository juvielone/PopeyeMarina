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
            this.headerLabel = new System.Windows.Forms.Label();
            this.formCardPanel = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtDoorType = new System.Windows.Forms.TextBox();
            this.doorTypeLabel = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.heightLabel = new System.Windows.Forms.Label();
            this.chkCovered = new System.Windows.Forms.CheckBox();
            this.cmbDock = new System.Windows.Forms.ComboBox();
            this.dockLabel = new System.Windows.Forms.Label();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.lengthLabel = new System.Windows.Forms.Label();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.widthLabel = new System.Windows.Forms.Label();
            this.cardTitleLabel = new System.Windows.Forms.Label();
            this.dgvSlips = new System.Windows.Forms.DataGridView();
            this.colSlipID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWidth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCovered = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.formCardPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSlips)).BeginInit();
            this.SuspendLayout();
            //
            // headerLabel
            //
            this.headerLabel.AutoSize = true;
            this.headerLabel.Font = Theme.ScreenHeaderFont;
            this.headerLabel.ForeColor = Theme.PrimaryTextColor;
            this.headerLabel.Location = new System.Drawing.Point(0, 0);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Text = "Slips";
            //
            // formCardPanel
            //
            this.formCardPanel.BackColor = Theme.CardBackColor;
            this.formCardPanel.BorderStyle = Theme.CardBorderStyle;
            this.formCardPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.formCardPanel.Location = new System.Drawing.Point(0, 44);
            this.formCardPanel.Size = new System.Drawing.Size(820, 230);
            this.formCardPanel.Controls.Add(this.btnCancel);
            this.formCardPanel.Controls.Add(this.btnSave);
            this.formCardPanel.Controls.Add(this.txtDoorType);
            this.formCardPanel.Controls.Add(this.doorTypeLabel);
            this.formCardPanel.Controls.Add(this.txtHeight);
            this.formCardPanel.Controls.Add(this.heightLabel);
            this.formCardPanel.Controls.Add(this.chkCovered);
            this.formCardPanel.Controls.Add(this.cmbDock);
            this.formCardPanel.Controls.Add(this.dockLabel);
            this.formCardPanel.Controls.Add(this.txtLength);
            this.formCardPanel.Controls.Add(this.lengthLabel);
            this.formCardPanel.Controls.Add(this.txtWidth);
            this.formCardPanel.Controls.Add(this.widthLabel);
            this.formCardPanel.Controls.Add(this.cardTitleLabel);
            this.formCardPanel.Name = "formCardPanel";
            //
            // cardTitleLabel
            //
            this.cardTitleLabel.AutoSize = true;
            this.cardTitleLabel.Font = Theme.CardTitleFont;
            this.cardTitleLabel.ForeColor = Theme.PrimaryTextColor;
            this.cardTitleLabel.Location = new System.Drawing.Point(16, 12);
            this.cardTitleLabel.Name = "cardTitleLabel";
            this.cardTitleLabel.Text = "Add new slip";
            //
            // widthLabel
            //
            this.widthLabel.AutoSize = true;
            this.widthLabel.Font = Theme.FieldLabelFont;
            this.widthLabel.ForeColor = Theme.SecondaryTextColor;
            this.widthLabel.Location = new System.Drawing.Point(16, 46);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Text = "Width (m)";
            //
            // txtWidth
            //
            this.txtWidth.Font = Theme.InputFont;
            this.txtWidth.Location = new System.Drawing.Point(16, 64);
            this.txtWidth.Size = new System.Drawing.Size(100, 25);
            this.txtWidth.Name = "txtWidth";
            //
            // lengthLabel
            //
            this.lengthLabel.AutoSize = true;
            this.lengthLabel.Font = Theme.FieldLabelFont;
            this.lengthLabel.ForeColor = Theme.SecondaryTextColor;
            this.lengthLabel.Location = new System.Drawing.Point(132, 46);
            this.lengthLabel.Name = "lengthLabel";
            this.lengthLabel.Text = "Length (m)";
            //
            // txtLength
            //
            this.txtLength.Font = Theme.InputFont;
            this.txtLength.Location = new System.Drawing.Point(132, 64);
            this.txtLength.Size = new System.Drawing.Size(100, 25);
            this.txtLength.Name = "txtLength";
            //
            // dockLabel
            //
            this.dockLabel.AutoSize = true;
            this.dockLabel.Font = Theme.FieldLabelFont;
            this.dockLabel.ForeColor = Theme.SecondaryTextColor;
            this.dockLabel.Location = new System.Drawing.Point(248, 46);
            this.dockLabel.Name = "dockLabel";
            this.dockLabel.Text = "Dock";
            //
            // cmbDock
            //
            this.cmbDock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDock.Font = Theme.InputFont;
            this.cmbDock.Location = new System.Drawing.Point(248, 64);
            this.cmbDock.Size = new System.Drawing.Size(220, 25);
            this.cmbDock.Name = "cmbDock";
            //
            // chkCovered
            //
            this.chkCovered.AutoSize = true;
            this.chkCovered.Font = Theme.InputFont;
            this.chkCovered.ForeColor = Theme.PrimaryTextColor;
            this.chkCovered.Location = new System.Drawing.Point(16, 118);
            this.chkCovered.Name = "chkCovered";
            this.chkCovered.Text = "Covered slip";
            this.chkCovered.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkCovered.CheckedChanged += new System.EventHandler(this.chkCovered_CheckedChanged);
            //
            // heightLabel
            //
            this.heightLabel.AutoSize = true;
            this.heightLabel.Font = Theme.FieldLabelFont;
            this.heightLabel.ForeColor = Theme.SecondaryTextColor;
            this.heightLabel.Location = new System.Drawing.Point(150, 118);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Text = "Height (m)";
            //
            // txtHeight
            //
            this.txtHeight.Font = Theme.InputFont;
            this.txtHeight.Location = new System.Drawing.Point(150, 136);
            this.txtHeight.Size = new System.Drawing.Size(100, 25);
            this.txtHeight.Name = "txtHeight";
            //
            // doorTypeLabel
            //
            this.doorTypeLabel.AutoSize = true;
            this.doorTypeLabel.Font = Theme.FieldLabelFont;
            this.doorTypeLabel.ForeColor = Theme.SecondaryTextColor;
            this.doorTypeLabel.Location = new System.Drawing.Point(266, 118);
            this.doorTypeLabel.Name = "doorTypeLabel";
            this.doorTypeLabel.Text = "Door type";
            //
            // txtDoorType
            //
            this.txtDoorType.Font = Theme.InputFont;
            this.txtDoorType.Location = new System.Drawing.Point(266, 136);
            this.txtDoorType.Size = new System.Drawing.Size(200, 25);
            this.txtDoorType.Name = "txtDoorType";
            //
            // btnSave
            //
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.BackColor = Theme.PrimaryButtonBackColor;
            this.btnSave.ForeColor = Theme.PrimaryButtonForeColor;
            this.btnSave.Font = Theme.ButtonFont;
            this.btnSave.Location = new System.Drawing.Point(16, 184);
            this.btnSave.Size = new System.Drawing.Size(140, 34);
            this.btnSave.Name = "btnSave";
            this.btnSave.Text = "Add Slip";
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            //
            // btnCancel
            //
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderSize = 1;
            this.btnCancel.BackColor = Theme.CardBackColor;
            this.btnCancel.ForeColor = Theme.SecondaryTextColor;
            this.btnCancel.Font = Theme.ButtonFont;
            this.btnCancel.Location = new System.Drawing.Point(164, 184);
            this.btnCancel.Size = new System.Drawing.Size(100, 34);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Visible = false;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            //
            // dgvSlips
            //
            this.dgvSlips.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSlips.Location = new System.Drawing.Point(0, 284);
            this.dgvSlips.Size = new System.Drawing.Size(820, 300);
            this.dgvSlips.AllowUserToAddRows = false;
            this.dgvSlips.AllowUserToDeleteRows = false;
            this.dgvSlips.ReadOnly = true;
            this.dgvSlips.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSlips.MultiSelect = false;
            this.dgvSlips.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSlips.BackgroundColor = Theme.ContentBackColor;
            this.dgvSlips.BorderStyle = Theme.CardBorderStyle;
            this.dgvSlips.RowHeadersVisible = false;
            this.dgvSlips.Name = "dgvSlips";
            this.dgvSlips.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSlipID,
            this.colDock,
            this.colWidth,
            this.colLength,
            this.colCovered});
            this.dgvSlips.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSlips_CellClick);
            //
            // colSlipID
            //
            this.colSlipID.HeaderText = "ID";
            this.colSlipID.Name = "colSlipID";
            this.colSlipID.Visible = false;
            //
            // colDock
            //
            this.colDock.HeaderText = "Dock";
            this.colDock.Name = "colDock";
            //
            // colWidth
            //
            this.colWidth.HeaderText = "Width (m)";
            this.colWidth.Name = "colWidth";
            //
            // colLength
            //
            this.colLength.HeaderText = "Length (m)";
            this.colLength.Name = "colLength";
            //
            // colCovered
            //
            this.colCovered.HeaderText = "Covered";
            this.colCovered.Name = "colCovered";
            //
            // SlipsControl
            //
            this.Controls.Add(this.dgvSlips);
            this.Controls.Add(this.formCardPanel);
            this.Controls.Add(this.headerLabel);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Name = "SlipsControl";
            this.Size = new System.Drawing.Size(820, 650);
            this.formCardPanel.ResumeLayout(false);
            this.formCardPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSlips)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
