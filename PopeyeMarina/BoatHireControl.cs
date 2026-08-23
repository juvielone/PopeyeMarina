using System;
using System.Windows.Forms;
using PopeyeMarina.Data;
using PopeyeMarina.Models;
using PopeyeMarina.Theming;
using System.Drawing;

namespace PopeyeMarina.Screens
{
    public partial class BoatHireControl : UserControl
    {
        private class CustomerListItem
        {
            public int CustomerID { get; set; }
            public string Display { get; set; } = string.Empty;
            public override string ToString() => Display;
        }

        private class RentalBoatListItem
        {
            public int RentalBoatID { get; set; }
            public string Display { get; set; } = string.Empty;
            public override string ToString() => Display;
        }

        public BoatHireControl()
        {
            InitializeComponent();
            contentPanel.Controls.Add(BuildHirePanel());
        }

        private Panel BuildHirePanel()
        {
            Panel root = new Panel { Dock = DockStyle.Fill };

            Panel card = new Panel
            {
                BackColor = Theme.CardBackColor,
                BorderStyle = Theme.CardBorderStyle,
                Location = new Point(0, 0),
                Size = new Size(820, 230),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            Label title = new Label
            {
                AutoSize = true,
                Font = Theme.CardTitleFont,
                ForeColor = Theme.PrimaryTextColor,
                Location = new Point(16, 12),
                Text = "Hire a rental boat"
            };

            Label customerLabel = MakeFieldLabel("Customer", 16, 46);
            ComboBox cmbCustomer = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = Theme.InputFont,
                Location = new Point(16, 64),
                Size = new Size(280, 25)
            };

            Label startLabel = MakeFieldLabel("Start date", 16, 100);
            DateTimePicker dtpStart = new DateTimePicker
            {
                Font = Theme.InputFont,
                Location = new Point(16, 118),
                Size = new Size(280, 25),
                Format = DateTimePickerFormat.Short
            };

            Label endLabel = MakeFieldLabel("End date", 314, 100);
            DateTimePicker dtpEnd = new DateTimePicker
            {
                Font = Theme.InputFont,
                Location = new Point(314, 118),
                Size = new Size(280, 25),
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Today.AddDays(1)
            };

            Label durationValue = new Label
            {
                AutoSize = true,
                Font = Theme.FieldLabelFont,
                ForeColor = Theme.PrimaryTextColor,
                Location = new Point(604, 118),
                Text = "Duration: —"
            };

            Label boatLabel = MakeFieldLabel("Available rental boats for these dates", 16, 154);
            ComboBox cmbBoat = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = Theme.InputFont,
                Location = new Point(16, 172),
                Size = new Size(578, 25)
            };

            void UpdateDuration()
            {
                if (dtpEnd.Value.Date < dtpStart.Value.Date)
                {
                    durationValue.Text = "Duration: invalid range";
                    return;
                }

                int days = (dtpEnd.Value.Date - dtpStart.Value.Date).Days;
                durationValue.Text = $"Duration: {days} day{(days == 1 ? "" : "s")}";
            }

            void RefreshAvailableBoats()
            {
                cmbBoat.Items.Clear();
                UpdateDuration();

                if (dtpEnd.Value.Date < dtpStart.Value.Date)
                {
                    return;
                }

                try
                {
                    foreach (RentalBoat boat in RentalBoatData.GetAvailableRentalBoats(dtpStart.Value, dtpEnd.Value))
                    {
                        cmbBoat.Items.Add(new RentalBoatListItem
                        {
                            RentalBoatID = boat.RentalBoatID,
                            Display = $"{boat.BoatName} ({boat.BoatType})"
                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to load available rental boats.\n\n" + ex.Message,
                        "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

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

            dtpStart.ValueChanged += (s, e) => RefreshAvailableBoats();
            dtpEnd.ValueChanged += (s, e) => RefreshAvailableBoats();
            RefreshAvailableBoats();

            card.Controls.AddRange(new Control[]
            {
                title, customerLabel, cmbCustomer, startLabel, dtpStart, endLabel, dtpEnd,
                durationValue, boatLabel, cmbBoat
            });

            Button btnCreate = MakePrimaryButton("Create Hire", 16, 250);
            btnCreate.Click += (s, e) =>
            {
                if (cmbCustomer.SelectedItem is not CustomerListItem selectedCustomer)
                {
                    Warn("Select a customer.");
                    return;
                }

                if (cmbBoat.SelectedItem is not RentalBoatListItem selectedBoat)
                {
                    Warn("Select a rental boat. If none appear, no boats are available for the chosen dates.");
                    return;
                }

                if (dtpEnd.Value.Date < dtpStart.Value.Date)
                {
                    Warn("End date cannot be before start date.");
                    return;
                }

                try
                {
                    BoatHireData.CreateHire(new BoatHire
                    {
                        CustomerID = selectedCustomer.CustomerID,
                        RentalBoatID = selectedBoat.RentalBoatID,
                        StartDate = dtpStart.Value.Date,
                        EndDate = dtpEnd.Value.Date
                    });

                    MessageBox.Show(
                        $"Hire created for {selectedCustomer.Display}: {selectedBoat.Display}, " +
                        $"{dtpStart.Value:d} to {dtpEnd.Value:d}.\n\nView it on the Dashboard.",
                        "Hire Created", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    RefreshAvailableBoats();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to create hire.\n\n" + ex.Message,
                        "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            root.Controls.Add(btnCreate);
            root.Controls.Add(card);

            return root;
        }

        private static Label MakeFieldLabel(string text, int x, int y) => new Label
        {
            AutoSize = true,
            Font = Theme.FieldLabelFont,
            ForeColor = Theme.SecondaryTextColor,
            Location = new Point(x, y),
            Text = text
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

        private static void Warn(string message) =>
            MessageBox.Show(message, "Missing or Invalid Information",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}
