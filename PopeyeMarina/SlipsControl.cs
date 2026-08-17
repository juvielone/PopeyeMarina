using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using PopeyeMarina.Data;
using PopeyeMarina.Models;

namespace PopeyeMarina.Screens
{
    public partial class SlipsControl : UserControl
    {
        // Wraps a Dock for display in the combo box (shows DockID + Location,
        // stores the real DockID for the actual value).
        private class DockListItem
        {
            public int DockID { get; set; }
            public string Display { get; set; } = string.Empty;
            public override string ToString() => Display;
        }

        private List<Dock> _docks = new List<Dock>();
        private int? _editingSlipId; // null = Add mode, set = Edit mode

        public SlipsControl()
        {
            InitializeComponent();
            LoadDocks();
            LoadSlips();
            UpdateCoveredFieldsVisibility();
        }

        private void LoadDocks()
        {
            try
            {
                _docks = DockData.GetAllDocks();

                cmbDock.Items.Clear();
                foreach (Dock dock in _docks)
                {
                    cmbDock.Items.Add(new DockListItem
                    {
                        DockID = dock.DockID,
                        Display = $"Dock {dock.DockID} — {dock.Location}"
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to load docks.\n\n" + ex.Message,
                    "Load Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadSlips()
        {
            try
            {
                dgvSlips.Rows.Clear();

                foreach (Slip slip in SlipData.GetAllSlips())
                {
                    Dock? dock = _docks.FirstOrDefault(d => d.DockID == slip.DockID);
                    string dockDisplay = dock != null
                        ? $"Dock {dock.DockID} — {dock.Location}"
                        : $"Dock {slip.DockID}";

                    dgvSlips.Rows.Add(
                        slip.SlipID,
                        dockDisplay,
                        slip.Width,
                        slip.SlipLength,
                        slip.IsCovered ? "Yes" : "No");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to load slips.\n\n" + ex.Message,
                    "Load Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void chkCovered_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCoveredFieldsVisibility();
        }

        private void UpdateCoveredFieldsVisibility()
        {
            heightLabel.Visible = chkCovered.Checked;
            txtHeight.Visible = chkCovered.Checked;
            doorTypeLabel.Visible = chkCovered.Checked;
            txtDoorType.Visible = chkCovered.Checked;
        }

        private void dgvSlips_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = dgvSlips.Rows[e.RowIndex];
            int slipId = Convert.ToInt32(row.Cells["colSlipID"].Value);

            Slip? slip = SlipData.GetAllSlips().FirstOrDefault(s => s.SlipID == slipId);
            if (slip == null)
            {
                return;
            }

            EnterEditMode(slip);
        }

        private void EnterEditMode(Slip slip)
        {
            _editingSlipId = slip.SlipID;

            cardTitleLabel.Text = $"Edit slip #{slip.SlipID}";
            btnSave.Text = "Save Changes";
            btnCancel.Visible = true;

            txtWidth.Text = slip.Width.ToString(CultureInfo.InvariantCulture);
            txtLength.Text = slip.SlipLength.ToString(CultureInfo.InvariantCulture);

            DockListItem? dockItem = cmbDock.Items
                .Cast<DockListItem>()
                .FirstOrDefault(d => d.DockID == slip.DockID);
            cmbDock.SelectedItem = dockItem;

            chkCovered.Checked = slip.IsCovered;

            if (slip is CoveredSlip coveredSlip)
            {
                txtHeight.Text = coveredSlip.Height.ToString(CultureInfo.InvariantCulture);
                txtDoorType.Text = coveredSlip.DoorType;
            }
            else
            {
                txtHeight.Clear();
                txtDoorType.Clear();
            }

            UpdateCoveredFieldsVisibility();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ResetToAddMode();
        }

        private void ResetToAddMode()
        {
            _editingSlipId = null;

            cardTitleLabel.Text = "Add new slip";
            btnSave.Text = "Add Slip";
            btnCancel.Visible = false;

            txtWidth.Clear();
            txtLength.Clear();
            txtHeight.Clear();
            txtDoorType.Clear();
            chkCovered.Checked = false;
            cmbDock.SelectedIndex = -1;

            UpdateCoveredFieldsVisibility();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Required-field validation before touching the database
            if (!decimal.TryParse(txtWidth.Text.Trim(), out decimal width) || width <= 0)
            {
                ShowValidationError("Enter a valid slip width greater than 0.");
                return;
            }

            if (!decimal.TryParse(txtLength.Text.Trim(), out decimal length) || length <= 0)
            {
                ShowValidationError("Enter a valid slip length greater than 0.");
                return;
            }

            if (cmbDock.SelectedItem is not DockListItem selectedDock)
            {
                ShowValidationError("Select a dock for this slip.");
                return;
            }

            decimal height = 0;
            string doorType = string.Empty;

            if (chkCovered.Checked)
            {
                if (!decimal.TryParse(txtHeight.Text.Trim(), out height) || height <= 0)
                {
                    ShowValidationError("Enter a valid covered-slip height greater than 0.");
                    return;
                }

                doorType = txtDoorType.Text.Trim();
                if (string.IsNullOrEmpty(doorType))
                {
                    ShowValidationError("Enter a door type for the covered slip.");
                    return;
                }
            }

            Slip slip = chkCovered.Checked
                ? new CoveredSlip { Height = height, DoorType = doorType }
                : new Slip();

            slip.Width = width;
            slip.SlipLength = length;
            slip.DockID = selectedDock.DockID;
            slip.IsCovered = chkCovered.Checked;

            try
            {
                if (_editingSlipId == null)
                {
                    SlipData.AddSlip(slip);
                }
                else
                {
                    slip.SlipID = _editingSlipId.Value;
                    SlipData.UpdateSlip(slip);
                }

                ResetToAddMode();
                LoadSlips();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to save slip.\n\n" + ex.Message,
                    "Save Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ShowValidationError(string message)
        {
            MessageBox.Show(message, "Missing or Invalid Information",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
