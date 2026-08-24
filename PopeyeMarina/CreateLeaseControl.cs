using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using PopeyeMarina.Core.Data;
using PopeyeMarina.Core.Models;
using PopeyeMarina.Theming;

namespace PopeyeMarina.Screens
{
    public partial class CreateLeaseControl : UserControl
    {
        private class CustomerListItem
        {
            public int CustomerID { get; set; }
            public string Display { get; set; } = string.Empty;
            public override string ToString() => Display;
        }

        private class BoatListItem
        {
            public string StateRegoNo { get; set; } = string.Empty;
            public string Display { get; set; } = string.Empty;
            public override string ToString() => Display;
        }

        private class SlipListItem
        {
            public int SlipID { get; set; }
            public string Display { get; set; } = string.Empty;
            public override string ToString() => Display;
        }

        private List<Slip> _vacantSlips = new List<Slip>();
        private bool _isAnnual = true;

        public CreateLeaseControl()
        {
            InitializeComponent();

            LoadCustomers();
            LoadVacantSlips();

            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Today.AddDays(1);

            ApplyLeaseTypeVisibility();
            UpdateComputedFields();
        }

        // ---------- Loading ----------

        private void LoadCustomers()
        {
            try
            {
                cmbCustomer.Items.Clear();
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
        }

        private void LoadVacantSlips()
        {
            try
            {
                _vacantSlips = SlipData.GetVacantSlips();

                cmbSlip.Items.Clear();
                foreach (Slip slip in _vacantSlips)
                {
                    string coveredTag = slip.IsCovered ? ", covered" : "";
                    cmbSlip.Items.Add(new SlipListItem
                    {
                        SlipID = slip.SlipID,
                        Display = $"Slip {slip.SlipID} — {slip.Width}m x {slip.SlipLength}m{coveredTag}"
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load vacant slips.\n\n" + ex.Message,
                    "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbVessel.Items.Clear();
            cmbVessel.Enabled = false;

            if (cmbCustomer.SelectedItem is not CustomerListItem selectedCustomer)
            {
                return;
            }

            try
            {
                List<Boat> boats = BoatData.GetBoatsByCustomer(selectedCustomer.CustomerID);

                foreach (Boat boat in boats)
                {
                    cmbVessel.Items.Add(new BoatListItem
                    {
                        StateRegoNo = boat.StateRegoNo,
                        Display = $"{boat.StateRegoNo} — {boat.Manufacturer} ({boat.BoatType})"
                    });
                }

                cmbVessel.Enabled = boats.Count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load boats for this customer.\n\n" + ex.Message,
                    "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------- Annual / Daily toggle ----------

        private void LeaseTypeToggle_Click(object sender, EventArgs e)
        {
            _isAnnual = sender == btnAnnual;
            ApplyLeaseTypeVisibility();
            UpdateComputedFields();
        }

        private void ApplyLeaseTypeVisibility()
        {
            btnAnnual.BackColor = _isAnnual ? Theme.PrimaryButtonBackColor : Color.FromArgb(240, 241, 242);
            btnAnnual.ForeColor = _isAnnual ? Theme.PrimaryButtonForeColor : Theme.SecondaryTextColor;
            btnDaily.BackColor = !_isAnnual ? Theme.PrimaryButtonBackColor : Color.FromArgb(240, 241, 242);
            btnDaily.ForeColor = !_isAnnual ? Theme.PrimaryButtonForeColor : Theme.SecondaryTextColor;

            // Annual: EndDate is derived server-side (StartDate + 1 year), shown read-only.
            // Daily: EndDate is picked by the user; NumberOfDays is derived from the gap.
            dtpEndDate.Visible = !_isAnnual;
            endDateComputedLabel.Visible = _isAnnual;

            chkPayMonthly.Visible = _isAnnual;
            numberOfDaysLabel.Visible = !_isAnnual;
        }

        // ---------- Derived/read-only display (client-side preview only — not persisted here) ----------

        private void dtpStartDate_ValueChanged(object sender, EventArgs e) => UpdateComputedFields();
        private void dtpEndDate_ValueChanged(object sender, EventArgs e) => UpdateComputedFields();

        private void UpdateComputedFields()
        {
            endDateComputedLabel.Text = dtpStartDate.Value.Date.AddYears(1).ToShortDateString();

            int days = (dtpEndDate.Value.Date - dtpStartDate.Value.Date).Days;
            numberOfDaysLabel.Text = days > 0
                ? $"Number of days: {days}"
                : "Number of days: — (end date must be after start date)";
        }

        // ---------- Submit ----------

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (cmbCustomer.SelectedItem is not CustomerListItem selectedCustomer)
            {
                Warn("Select a customer.");
                return;
            }

            if (cmbVessel.SelectedItem is not BoatListItem selectedVessel)
            {
                Warn("Select the customer's vessel.");
                return;
            }

            if (cmbSlip.SelectedItem is not SlipListItem selectedSlipItem)
            {
                Warn("Select an available slip.");
                return;
            }

            Slip? selectedSlip = _vacantSlips.FirstOrDefault(s => s.SlipID == selectedSlipItem.SlipID);
            if (selectedSlip == null)
            {
                Warn("The selected slip is no longer available. Refresh and try again.");
                return;
            }

            Lease lease;

            if (_isAnnual)
            {
                lease = new AnnualLease
                {
                    PayMonthly = chkPayMonthly.Checked
                    // EndDate is derived and set inside LeaseData.CreateLease — not set here.
                };
            }
            else
            {
                int numberOfDays = (dtpEndDate.Value.Date - dtpStartDate.Value.Date).Days;
                if (numberOfDays <= 0)
                {
                    Warn("End date must be after the start date.");
                    return;
                }

                lease = new DailyLease
                {
                    NumberOfDays = numberOfDays,
                    EndDate = dtpEndDate.Value.Date
                };
            }

            lease.StartDate = dtpStartDate.Value.Date;
            lease.LeaseType = _isAnnual ? "Annual" : "Daily";
            lease.SlipID = selectedSlip.SlipID;
            lease.StateRegoNo = selectedVessel.StateRegoNo;
            lease.CustomerID = selectedCustomer.CustomerID;

            try
            {
                LeaseData.CreateLease(lease, selectedSlip);

                MessageBox.Show(
                    $"Lease created successfully. Amount: {lease.Amount:C}",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to create lease.\n\n" + ex.Message,
                    "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetForm()
        {
            cmbCustomer.SelectedIndex = -1;
            cmbVessel.Items.Clear();
            cmbVessel.Enabled = false;

            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Today.AddDays(1);
            chkPayMonthly.Checked = false;

            _isAnnual = true;
            ApplyLeaseTypeVisibility();
            UpdateComputedFields();

            // Slip list must be reloaded — the slip just leased is no longer vacant.
            LoadVacantSlips();
        }

        private static void Warn(string message) =>
            MessageBox.Show(message, "Missing or Invalid Information",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}
