using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using PopeyeMarina.Data;
using PopeyeMarina.Models;
using PopeyeMarina.Theming;

namespace PopeyeMarina.Screens
{
    public partial class DashboardControl : UserControl
    {
        private class CustomerListItem
        {
            public int CustomerID { get; set; }
            public string Display { get; set; } = string.Empty;
            public override string ToString() => Display;
        }

        private const string FilterAll = "All";
        private const string FilterSlipLease = "Slip Lease";
        private const string FilterBoatHire = "Boat Hire";

        private List<ActivityRecord> _allActivity = new List<ActivityRecord>();
        private string _activityFilter = FilterAll;

        private Button? btnFilterAll;
        private Button? btnFilterLease;
        private Button? btnFilterHire;
        private ComboBox? cmbCustomer;
        private DataGridView? grid;

        public DashboardControl()
        {
            InitializeComponent();
            contentPanel.Controls.Add(BuildDashboardPanel());
        }

        private Panel BuildDashboardPanel()
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
                Text = "Marina activity overview"
            };

            // ---- Activity type filter (toggle, matching the Sailboat/Powerboat pattern) ----

            Label filterLabel = MakeFieldLabel("Activity", 16, 46);

            btnFilterAll = MakeToggleButton(FilterAll, 16, 64);
            btnFilterLease = MakeToggleButton(FilterSlipLease, 106, 64);
            btnFilterHire = MakeToggleButton(FilterBoatHire, 216, 64);

            btnFilterAll.Click += (s, e) => { _activityFilter = FilterAll; ApplyFilterVisuals(); RunFilter(); };
            btnFilterLease.Click += (s, e) => { _activityFilter = FilterSlipLease; ApplyFilterVisuals(); RunFilter(); };
            btnFilterHire.Click += (s, e) => { _activityFilter = FilterBoatHire; ApplyFilterVisuals(); RunFilter(); };

            // ---- Customer filter ----

            Label customerLabel = MakeFieldLabel("Customer", 350, 46);
            cmbCustomer = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = Theme.InputFont,
                Location = new Point(350, 64),
                Size = new Size(280, 25)
            };
            cmbCustomer.Items.Add(new CustomerListItem { CustomerID = -1, Display = "All customers" });

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

            cmbCustomer.SelectedIndex = 0;
            cmbCustomer.SelectedIndexChanged += (s, e) => RunFilter();

            card.Controls.AddRange(new Control[]
            {
                title, filterLabel, btnFilterAll, btnFilterLease, btnFilterHire,
                customerLabel, cmbCustomer
            });

            // ---- Grid ----

            grid = new DataGridView
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
            grid.Columns.Add("colCustomer", "Customer");
            grid.Columns.Add("colActivity", "Activity");
            grid.Columns.Add("colItem", "Item");
            grid.Columns.Add("colStartDate", "Start Date");
            grid.Columns.Add("colEndDate", "End Date");
            grid.Columns.Add("colStatus", "Status");

            root.Controls.Add(grid);
            root.Controls.Add(card);

            ApplyFilterVisuals();
            LoadActivity();

            return root;
        }

        private void ApplyFilterVisuals()
        {
            void Style(Button? button, bool selected)
            {
                if (button == null) return;
                button.BackColor = selected ? Theme.PrimaryButtonBackColor : Color.FromArgb(240, 241, 242);
                button.ForeColor = selected ? Theme.PrimaryButtonForeColor : Theme.SecondaryTextColor;
            }

            Style(btnFilterAll, _activityFilter == FilterAll);
            Style(btnFilterLease, _activityFilter == FilterSlipLease);
            Style(btnFilterHire, _activityFilter == FilterBoatHire);
        }

        private void LoadActivity()
        {
            _allActivity = new List<ActivityRecord>();

            try
            {
                Dictionary<int, string> customerNames = CustomerData.GetAllCustomers()
                    .ToDictionary(c => c.CustomerID, c => c.CustomerName);

                // GetAllRentalBoats returns active AND inactive boats, so historical
                // hires against a since-deactivated boat still resolve a real name.
                Dictionary<int, string> rentalBoatNames = RentalBoatData.GetAllRentalBoats()
                    .ToDictionary(b => b.RentalBoatID, b => b.BoatName);

                string CustomerName(int id) =>
                    customerNames.TryGetValue(id, out string? name) ? name : $"Customer #{id}";

                foreach (Lease lease in LeaseData.GetAllLeases())
                {
                    _allActivity.Add(new ActivityRecord
                    {
                        CustomerID = lease.CustomerID,
                        CustomerName = CustomerName(lease.CustomerID),
                        ActivityType = FilterSlipLease,
                        ItemLabel = $"Slip #{lease.SlipID}",
                        StartDate = lease.StartDate,
                        EndDate = lease.EndDate
                    });
                }

                foreach (BoatHire hire in BoatHireData.GetAllHires())
                {
                    string boatName = rentalBoatNames.TryGetValue(hire.RentalBoatID, out string? name)
                        ? name
                        : $"Boat #{hire.RentalBoatID}";

                    _allActivity.Add(new ActivityRecord
                    {
                        CustomerID = hire.CustomerID,
                        CustomerName = CustomerName(hire.CustomerID),
                        ActivityType = FilterBoatHire,
                        ItemLabel = boatName,
                        StartDate = hire.StartDate,
                        EndDate = hire.EndDate
                    });
                }

                _allActivity = _allActivity.OrderBy(a => a.StartDate).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load activity.\n\n" + ex.Message,
                    "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            RunFilter();
        }

        private void RunFilter()
        {
            if (grid == null || cmbCustomer == null)
            {
                return;
            }

            IEnumerable<ActivityRecord> filtered = _allActivity;

            if (_activityFilter != FilterAll)
            {
                filtered = filtered.Where(a => a.ActivityType == _activityFilter);
            }

            if (cmbCustomer.SelectedItem is CustomerListItem selectedCustomer && selectedCustomer.CustomerID != -1)
            {
                filtered = filtered.Where(a => a.CustomerID == selectedCustomer.CustomerID);
            }

            grid.Rows.Clear();

            foreach (ActivityRecord record in filtered)
            {
                grid.Rows.Add(
                    record.CustomerName,
                    record.ActivityType,
                    record.ItemLabel,
                    record.StartDate.ToShortDateString(),
                    record.EndDate?.ToShortDateString() ?? "—",
                    record.Status);
            }
        }

        private static Label MakeFieldLabel(string text, int x, int y) => new Label
        {
            AutoSize = true,
            Font = Theme.FieldLabelFont,
            ForeColor = Theme.SecondaryTextColor,
            Location = new Point(x, y),
            Text = text
        };

        private static Button MakeToggleButton(string text, int x, int y)
        {
            Button button = new Button
            {
                FlatStyle = FlatStyle.Flat,
                Font = Theme.ButtonFont,
                Location = new Point(x, y),
                Size = new Size(84, 30),
                Text = text,
                Cursor = Cursors.Hand
            };
            button.FlatAppearance.BorderSize = 0;
            return button;
        }
    }
}
