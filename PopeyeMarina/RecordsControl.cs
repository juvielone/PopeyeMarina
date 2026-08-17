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
    public partial class RecordsControl : UserControl
    {
        private class CustomerListItem
        {
            public int CustomerID { get; set; }
            public string Display { get; set; } = string.Empty;
            public override string ToString() => Display;
        }

        private Button? _currentSubTab;

        public RecordsControl()
        {
            InitializeComponent();
            SelectSubTab(btnBoatsTab);
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
            subContentPanel.Controls.Add(tab == "Docks" ? BuildDocksPanel() : BuildBoatsPanel());
        }

        // ---------- Boats ----------

        private Panel BuildBoatsPanel()
        {
            Panel root = new Panel { Dock = DockStyle.Fill };

            Panel card = new Panel
            {
                BackColor = Theme.CardBackColor,
                BorderStyle = Theme.CardBorderStyle,
                Location = new Point(0, 0),
                Size = new Size(820, 290),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            Label title = new Label
            {
                AutoSize = true,
                Font = Theme.CardTitleFont,
                ForeColor = Theme.PrimaryTextColor,
                Location = new Point(16, 12),
                Text = "Add new boat"
            };

            // Row 1
            Label regoLabel = MakeFieldLabel("State rego no.", 16, 46);
            TextBox txtRego = MakeInput(16, 64, 150);

            Label lengthLabel = MakeFieldLabel("Length (m)", 182, 46);
            TextBox txtLength = MakeInput(182, 64, 100);

            Label yearLabel = MakeFieldLabel("Model year", 298, 46);
            TextBox txtYear = MakeInput(298, 64, 100);

            // Row 2
            Label manufacturerLabel = MakeFieldLabel("Manufacturer", 16, 100);
            TextBox txtManufacturer = MakeInput(16, 118, 250);

            Label customerLabel = MakeFieldLabel("Owner", 282, 100);
            ComboBox cmbCustomer = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = Theme.InputFont,
                Location = new Point(282, 118),
                Size = new Size(250, 25)
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

            // Boat type toggle
            Button btnSailboat = MakeToggleButton("Sailboat", 16, 154);
            Button btnPowerboat = MakeToggleButton("Powerboat", 132, 154);

            // Sailboat-specific fields
            Label keelLabel = MakeFieldLabel("Keel depth (m)", 16, 194);
            TextBox txtKeelDepth = MakeInput(16, 212, 100);
            Label sailsLabel = MakeFieldLabel("Number of sails", 132, 194);
            TextBox txtSails = MakeInput(132, 212, 100);
            Label motorLabel = MakeFieldLabel("Motor type (optional)", 248, 194);
            TextBox txtMotorType = MakeInput(248, 212, 200);

            // Powerboat-specific fields (share the same row, toggled visibility)
            Label enginesLabel = MakeFieldLabel("Number of engines", 16, 194);
            TextBox txtEngines = MakeInput(16, 212, 100);
            Label fuelLabel = MakeFieldLabel("Fuel type", 132, 194);
            TextBox txtFuelType = MakeInput(132, 212, 200);

            bool isSailboat = true;

            void ApplyBoatTypeVisibility()
            {
                keelLabel.Visible = isSailboat;
                txtKeelDepth.Visible = isSailboat;
                sailsLabel.Visible = isSailboat;
                txtSails.Visible = isSailboat;
                motorLabel.Visible = isSailboat;
                txtMotorType.Visible = isSailboat;

                enginesLabel.Visible = !isSailboat;
                txtEngines.Visible = !isSailboat;
                fuelLabel.Visible = !isSailboat;
                txtFuelType.Visible = !isSailboat;

                btnSailboat.BackColor = isSailboat ? Theme.PrimaryButtonBackColor : Color.FromArgb(240, 241, 242);
                btnSailboat.ForeColor = isSailboat ? Theme.PrimaryButtonForeColor : Theme.SecondaryTextColor;
                btnPowerboat.BackColor = !isSailboat ? Theme.PrimaryButtonBackColor : Color.FromArgb(240, 241, 242);
                btnPowerboat.ForeColor = !isSailboat ? Theme.PrimaryButtonForeColor : Theme.SecondaryTextColor;
            }

            btnSailboat.Click += (s, e) => { isSailboat = true; ApplyBoatTypeVisibility(); };
            btnPowerboat.Click += (s, e) => { isSailboat = false; ApplyBoatTypeVisibility(); };
            ApplyBoatTypeVisibility();

            Button btnAdd = MakePrimaryButton("Add Boat", 16, 250);
            btnAdd.Click += (s, e) =>
            {
                string rego = txtRego.Text.Trim();
                string manufacturer = txtManufacturer.Text.Trim();

                if (string.IsNullOrEmpty(rego) || string.IsNullOrEmpty(manufacturer))
                {
                    Warn("State rego number and manufacturer are required.");
                    return;
                }

                if (!decimal.TryParse(txtLength.Text.Trim(), out decimal boatLength) || boatLength <= 0)
                {
                    Warn("Enter a valid boat length greater than 0.");
                    return;
                }

                if (!int.TryParse(txtYear.Text.Trim(), out int modelYear))
                {
                    Warn("Enter a valid model year.");
                    return;
                }

                if (cmbCustomer.SelectedItem is not CustomerListItem selectedCustomer)
                {
                    Warn("Select the boat's owner.");
                    return;
                }

                Boat boat;

                if (isSailboat)
                {
                    if (!int.TryParse(txtSails.Text.Trim(), out int numberOfSails) || numberOfSails <= 0)
                    {
                        Warn("Enter a valid number of sails.");
                        return;
                    }

                    if (!decimal.TryParse(txtKeelDepth.Text.Trim(), out decimal keelDepth) || keelDepth <= 0)
                    {
                        Warn("Enter a valid keel depth greater than 0.");
                        return;
                    }

                    boat = new Sailboat
                    {
                        KeelDepth = keelDepth,
                        NumberOfSails = numberOfSails,
                        MotorType = string.IsNullOrWhiteSpace(txtMotorType.Text) ? null : txtMotorType.Text.Trim()
                    };
                }
                else
                {
                    if (!int.TryParse(txtEngines.Text.Trim(), out int numberOfEngines) || numberOfEngines <= 0)
                    {
                        Warn("Enter a valid number of engines.");
                        return;
                    }

                    string fuelType = txtFuelType.Text.Trim();
                    if (string.IsNullOrEmpty(fuelType))
                    {
                        Warn("Enter the fuel type.");
                        return;
                    }

                    boat = new Powerboat
                    {
                        NumberOfEngines = numberOfEngines,
                        FuelType = fuelType
                    };
                }

                boat.StateRegoNo = rego;
                boat.BoatLength = boatLength;
                boat.Manufacturer = manufacturer;
                boat.ModelYear = modelYear;
                boat.BoatType = isSailboat ? "Sailboat" : "Powerboat";
                boat.CustomerID = selectedCustomer.CustomerID;

                try
                {
                    BoatData.AddBoat(boat);
                    MessageBox.Show("Boat added successfully.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtRego.Clear();
                    txtLength.Clear();
                    txtYear.Clear();
                    txtManufacturer.Clear();
                    cmbCustomer.SelectedIndex = -1;
                    txtKeelDepth.Clear();
                    txtSails.Clear();
                    txtMotorType.Clear();
                    txtEngines.Clear();
                    txtFuelType.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to add boat.\n\n" + ex.Message,
                        "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            card.Controls.AddRange(new Control[]
            {
                title, regoLabel, txtRego, lengthLabel, txtLength, yearLabel, txtYear,
                manufacturerLabel, txtManufacturer, customerLabel, cmbCustomer,
                btnSailboat, btnPowerboat,
                keelLabel, txtKeelDepth, sailsLabel, txtSails, motorLabel, txtMotorType,
                enginesLabel, txtEngines, fuelLabel, txtFuelType,
                btnAdd
            });

            root.Controls.Add(card);
            return root;
        }

        // ---------- Docks ----------

        private Panel BuildDocksPanel()
        {
            Panel root = new Panel { Dock = DockStyle.Fill };

            Panel card = new Panel
            {
                BackColor = Theme.CardBackColor,
                BorderStyle = Theme.CardBorderStyle,
                Location = new Point(0, 0),
                Size = new Size(820, 130),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            Label title = new Label
            {
                AutoSize = true,
                Font = Theme.CardTitleFont,
                ForeColor = Theme.PrimaryTextColor,
                Location = new Point(16, 12),
                Text = "Add new dock"
            };

            Label locationLabel = MakeFieldLabel("Location", 16, 46);
            TextBox txtLocation = MakeInput(16, 64, 300);

            CheckBox chkElectricity = new CheckBox
            {
                AutoSize = true,
                Font = Theme.InputFont,
                ForeColor = Theme.PrimaryTextColor,
                Location = new Point(336, 66),
                Text = "Has electricity",
                Cursor = Cursors.Hand
            };

            CheckBox chkWater = new CheckBox
            {
                AutoSize = true,
                Font = Theme.InputFont,
                ForeColor = Theme.PrimaryTextColor,
                Location = new Point(336, 92),
                Text = "Has water",
                Cursor = Cursors.Hand
            };

            DataGridView grid = new DataGridView
            {
                Location = new Point(0, 150),
                Size = new Size(820, 340),
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
            grid.Columns.Add("colDockID", "ID");
            grid.Columns.Add("colLocation", "Location");
            grid.Columns.Add("colElectricity", "Electricity");
            grid.Columns.Add("colWater", "Water");
            grid.Columns["colDockID"].Visible = false;

            void LoadDocks()
            {
                try
                {
                    grid.Rows.Clear();
                    foreach (Dock dock in DockData.GetAllDocks())
                    {
                        grid.Rows.Add(
                            dock.DockID,
                            dock.Location,
                            dock.HasElectricity ? "Yes" : "No",
                            dock.HasWater ? "Yes" : "No");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to load docks.\n\n" + ex.Message,
                        "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            Button btnAdd = MakePrimaryButton("Add Dock", 16, 90);
            btnAdd.Click += (s, e) =>
            {
                string location = txtLocation.Text.Trim();
                if (string.IsNullOrEmpty(location))
                {
                    Warn("Dock location is required.");
                    return;
                }

                try
                {
                    DockData.AddDock(new Dock
                    {
                        Location = location,
                        HasElectricity = chkElectricity.Checked,
                        HasWater = chkWater.Checked
                    });

                    txtLocation.Clear();
                    chkElectricity.Checked = false;
                    chkWater.Checked = false;
                    LoadDocks();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to add dock.\n\n" + ex.Message,
                        "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            card.Controls.AddRange(new Control[]
            {
                title, locationLabel, txtLocation, chkElectricity, chkWater, btnAdd
            });

            root.Controls.Add(grid);
            root.Controls.Add(card);

            LoadDocks();
            return root;
        }

        // ---------- Shared control-building helpers ----------

        private static Label MakeFieldLabel(string text, int x, int y) => new Label
        {
            AutoSize = true,
            Font = Theme.FieldLabelFont,
            ForeColor = Theme.SecondaryTextColor,
            Location = new Point(x, y),
            Text = text
        };

        private static TextBox MakeInput(int x, int y, int width) => new TextBox
        {
            Font = Theme.InputFont,
            Location = new Point(x, y),
            Size = new Size(width, 25)
        };

        private static Button MakePrimaryButton(string text, int x, int y) => new Button
        {
            FlatStyle = FlatStyle.Flat,
            BackColor = Theme.PrimaryButtonBackColor,
            ForeColor = Theme.PrimaryButtonForeColor,
            Font = Theme.ButtonFont,
            Location = new Point(x, y),
            Size = new Size(140, 34),
            Text = text,
            Cursor = Cursors.Hand
        };

        private static Button MakeToggleButton(string text, int x, int y)
        {
            Button button = new Button
            {
                FlatStyle = FlatStyle.Flat,
                Font = Theme.ButtonFont,
                Location = new Point(x, y),
                Size = new Size(110, 30),
                Text = text,
                Cursor = Cursors.Hand
            };
            button.FlatAppearance.BorderSize = 0;
            return button;
        }

        private static void Warn(string message) =>
            MessageBox.Show(message, "Missing or Invalid Information",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}