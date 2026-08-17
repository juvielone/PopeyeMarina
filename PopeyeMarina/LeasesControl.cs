using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using PopeyeMarina.Data;
using PopeyeMarina.Models;
using PopeyeMarina.Theming;

namespace PopeyeMarina.Screens
{
    public partial class LeasesControl : UserControl
    {
        private class CustomerListItem
        {
            public int CustomerID { get; set; }
            public string Display { get; set; } = string.Empty;
            public override string ToString() => Display;
        }

        private Button? _currentSubTab;

        public LeasesControl()
        {
            InitializeComponent();
            SelectSubTab(btnCreateTab);
        }

        private void SubTab_Click(object sender, EventArgs e)
        {
            if (sender is Button clicked)
            {
                SelectSubTab(clicked);
            }
        }

        private void SelectSubTab(Button selected)
        {
            if (_currentSubTab != null)
            {
                _currentSubTab.BackColor = Color.FromArgb(240, 241, 242);
                _currentSubTab.ForeColor = Theme.SecondaryTextColor;
            }

            selected.BackColor = Theme.PrimaryButtonBackColor;
            selected.ForeColor = Theme.PrimaryButtonForeColor;
            _currentSubTab = selected;

            subContentPanel.Controls.Clear();

            string tab = selected.Tag as string ?? selected.Text;
            subContentPanel.Controls.Add(tab == "Search" ? BuildSearchLeasesPanel() : BuildCreatePanel());
        }

        // ---------- Create Lease ----------

        private Panel BuildCreatePanel()
        {
            Panel root = new Panel { Dock = DockStyle.Fill };
            root.Controls.Add(new CreateLeaseControl());
            return root;
        }

        // ---------- Search Leases ----------

        private Panel BuildSearchLeasesPanel()
        {
            Panel root = new Panel { Dock = DockStyle.Fill };

            Panel card = new Panel
            {
                BackColor = Theme.CardBackColor,
                BorderStyle = Theme.CardBorderStyle,
                Location = new Point(0, 0),
                Size = new Size(820, 110),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            Label title = new Label
            {
                AutoSize = true,
                Font = Theme.CardTitleFont,
                ForeColor = Theme.PrimaryTextColor,
                Location = new Point(16, 12),
                Text = "Search leases by customer"
            };

            Label customerLabel = new Label
            {
                AutoSize = true,
                Font = Theme.FieldLabelFont,
                ForeColor = Theme.SecondaryTextColor,
                Location = new Point(16, 46),
                Text = "Customer"
            };

            ComboBox cmbCustomer = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = Theme.InputFont,
                Location = new Point(16, 64),
                Size = new Size(300, 25)
            };

            try
            {
                foreach (Customer customer in CustomerData.GetAllCustomers())
                {
                    cmbCustomer.Items.Add(new CustomerListItem
                    {
                        CustomerID = customer.CustomerID,
                        Display = customer.CustomerName
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load customers.\n\n" + ex.Message,
                    "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Button btnDelete = new Button
            {
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(178, 44, 44),
                ForeColor = Color.White,
                Font = Theme.ButtonFont,
                Location = new Point(336, 63),
                Size = new Size(140, 27),
                Text = "Delete Selected",
                Enabled = false,
                Cursor = Cursors.Hand
            };
            btnDelete.FlatAppearance.BorderSize = 0;

            DataGridView grid = new DataGridView
            {
                Location = new Point(0, 120),
                Size = new Size(820, 370),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Theme.ContentBackColor,
                BorderStyle = Theme.CardBorderStyle,
                RowHeadersVisible = false
            };
            grid.Columns.Add("colLeaseID", "ID");
            grid.Columns.Add("colSlip", "Slip");
            grid.Columns.Add("colVessel", "Vessel");
            grid.Columns.Add("colType", "Type");
            grid.Columns.Add("colStartDate", "Start Date");
            grid.Columns.Add("colEndDate", "End Date");
            grid.Columns.Add("colAmount", "Amount");
            grid.Columns["colLeaseID"].Visible = false;

            grid.SelectionChanged += (s, e) =>
            {
                btnDelete.Enabled = grid.SelectedRows.Count > 0;
            };

            void RunSearch()
            {
                if (cmbCustomer.SelectedItem is not CustomerListItem selectedCustomer)
                {
                    return;
                }

                try
                {
                    grid.Rows.Clear();
                    btnDelete.Enabled = false;

                    foreach (Lease lease in LeaseData.GetLeasesByCustomer(selectedCustomer.CustomerID))
                    {
                        grid.Rows.Add(
                            lease.LeaseID,
                            $"Slip {lease.SlipID}",
                            lease.StateRegoNo,
                            lease.LeaseType,
                            lease.StartDate.ToShortDateString(),
                            lease.EndDate?.ToShortDateString() ?? "—",
                            lease.Amount.ToString("C"));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to search leases.\n\n" + ex.Message,
                        "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            cmbCustomer.SelectedIndexChanged += (s, e) => RunSearch();

            btnDelete.Click += (s, e) =>
            {
                if (grid.SelectedRows.Count == 0)
                {
                    return;
                }

                DataGridViewRow row = grid.SelectedRows[0];
                int leaseId = Convert.ToInt32(row.Cells["colLeaseID"].Value);
                string vessel = row.Cells["colVessel"].Value?.ToString() ?? "";
                string slip = row.Cells["colSlip"].Value?.ToString() ?? "";

                DialogResult confirm = MessageBox.Show(
                    $"Delete lease #{leaseId} ({vessel} at {slip})? This cannot be undone.",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm != DialogResult.Yes)
                {
                    return;
                }

                try
                {
                    LeaseData.DeleteLease(leaseId);
                    RunSearch();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to delete lease.\n\n" + ex.Message,
                        "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            card.Controls.AddRange(new Control[] { title, customerLabel, cmbCustomer, btnDelete });

            root.Controls.Add(grid);
            root.Controls.Add(card);

            return root;
        }
    }
}
