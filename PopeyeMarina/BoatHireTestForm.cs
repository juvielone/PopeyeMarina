using PopeyeMarina.Data;
using PopeyeMarina.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PopeyeMarina
{
    // Throwaway sandbox form for testing RentalBoatData / BoatHireData directly.
    // Not part of the graded shell — do not wire this into MainForm's nav.
    // Point Program.cs at this temporarily, then revert to MainForm when done.
    public class BoatHireTestForm : Form
    {
        private ListBox lstBoats = new ListBox { Location = new Point(10, 30), Size = new Size(250, 150) };
        private ComboBox cmbCustomer = new ComboBox { Location = new Point(10, 200), Size = new Size(250, 25), DropDownStyle = ComboBoxStyle.DropDownList };
        private ComboBox cmbBoat = new ComboBox { Location = new Point(10, 230), Size = new Size(250, 25), DropDownStyle = ComboBoxStyle.DropDownList };
        private DateTimePicker dtpStart = new DateTimePicker { Location = new Point(10, 260), Size = new Size(250, 25) };
        private DateTimePicker dtpEnd = new DateTimePicker { Location = new Point(10, 290), Size = new Size(250, 25) };
        private ListBox lstHires = new ListBox { Location = new Point(280, 30), Size = new Size(350, 320) };
        private TextBox txtLog = new TextBox { Location = new Point(10, 400), Size = new Size(620, 100), Multiline = true, ScrollBars = ScrollBars.Vertical, ReadOnly = true };

        public BoatHireTestForm()
        {
            Text = "Boat Hire — Backend Test Harness (throwaway)";
            ClientSize = new Size(650, 520);

            Button btnLoadBoats = new Button { Text = "Load Rental Boats", Location = new Point(10, 5), Size = new Size(150, 25) };
            btnLoadBoats.Click += (s, e) => LoadBoats();

            Button btnCheckAvail = new Button { Text = "Check Availability For Dates", Location = new Point(10, 320), Size = new Size(250, 25) };
            btnCheckAvail.Click += (s, e) => CheckAvailability();

            Button btnCreateHire = new Button { Text = "Create Hire", Location = new Point(10, 350), Size = new Size(120, 25) };
            btnCreateHire.Click += (s, e) => CreateHire();

            Button btnLoadHiresByCustomer = new Button { Text = "Load Hires For Selected Customer", Location = new Point(280, 5), Size = new Size(230, 25) };
            btnLoadHiresByCustomer.Click += (s, e) => LoadHiresByCustomer();

            Button btnDeleteHire = new Button { Text = "Delete Selected Hire", Location = new Point(520, 5), Size = new Size(110, 25) };
            btnDeleteHire.Click += (s, e) => DeleteSelectedHire();

            Controls.AddRange(new Control[]
            {
                btnLoadBoats, lstBoats, cmbCustomer, cmbBoat, dtpStart, dtpEnd,
                btnCheckAvail, btnCreateHire, btnLoadHiresByCustomer, btnDeleteHire,
                lstHires, txtLog
            });

            LoadCustomers();
            LoadBoats();
        }

        private void Log(string message) => txtLog.AppendText(message + Environment.NewLine);

        private void LoadCustomers()
        {
            try
            {
                cmbCustomer.Items.Clear();
                foreach (Customer c in CustomerData.GetAllCustomers())
                {
                    cmbCustomer.Items.Add(new ComboItem(c.CustomerID, c.CustomerName));
                }
                Log($"Loaded {cmbCustomer.Items.Count} customers.");
            }
            catch (Exception ex)
            {
                Log("ERROR loading customers: " + ex.Message);
            }
        }

        private void LoadBoats()
        {
            try
            {
                lstBoats.Items.Clear();
                cmbBoat.Items.Clear();
                foreach (RentalBoat b in RentalBoatData.GetAllRentalBoats())
                {
                    lstBoats.Items.Add($"[{b.RentalBoatID}] {b.BoatName} ({b.BoatType})");
                    cmbBoat.Items.Add(new ComboItem(b.RentalBoatID, b.BoatName));
                }
                Log($"Loaded {lstBoats.Items.Count} active rental boats.");
            }
            catch (Exception ex)
            {
                Log("ERROR loading rental boats: " + ex.Message);
            }
        }

        private void CheckAvailability()
        {
            try
            {
                lstBoats.Items.Clear();
                var available = RentalBoatData.GetAvailableRentalBoats(dtpStart.Value, dtpEnd.Value);
                foreach (RentalBoat b in available)
                {
                    lstBoats.Items.Add($"[{b.RentalBoatID}] {b.BoatName} ({b.BoatType}) — AVAILABLE");
                }
                Log($"{available.Count} boat(s) available for {dtpStart.Value:d} to {dtpEnd.Value:d}.");
            }
            catch (Exception ex)
            {
                Log("ERROR checking availability: " + ex.Message);
            }
        }

        private void CreateHire()
        {
            if (cmbCustomer.SelectedItem is not ComboItem customer)
            {
                Log("Select a customer first.");
                return;
            }
            if (cmbBoat.SelectedItem is not ComboItem boat)
            {
                Log("Select a rental boat first.");
                return;
            }

            try
            {
                BoatHireData.CreateHire(new BoatHire
                {
                    CustomerID = customer.Id,
                    RentalBoatID = boat.Id,
                    StartDate = dtpStart.Value.Date,
                    EndDate = dtpEnd.Value.Date
                });

                Log($"Hire created: {customer.Name} hired {boat.Name}, {dtpStart.Value:d}–{dtpEnd.Value:d}.");
            }
            catch (Exception ex)
            {
                Log("ERROR creating hire (this may be an expected overlap rejection): " + ex.Message);
            }
        }

        private void LoadHiresByCustomer()
        {
            if (cmbCustomer.SelectedItem is not ComboItem customer)
            {
                Log("Select a customer first.");
                return;
            }

            try
            {
                lstHires.Items.Clear();
                var hires = BoatHireData.GetHiresByCustomer(customer.Id);
                foreach (BoatHire h in hires)
                {
                    lstHires.Items.Add(new HireItem(h));
                }
                Log($"Loaded {hires.Count} hire(s) for {customer.Name}.");
            }
            catch (Exception ex)
            {
                Log("ERROR loading hires: " + ex.Message);
            }
        }

        private void DeleteSelectedHire()
        {
            if (lstHires.SelectedItem is not HireItem item)
            {
                Log("Select a hire from the list first.");
                return;
            }

            try
            {
                BoatHireData.DeleteHire(item.Hire.HireID);
                Log($"Deleted hire #{item.Hire.HireID}.");
                LoadHiresByCustomer();
            }
            catch (Exception ex)
            {
                Log("ERROR deleting hire: " + ex.Message);
            }
        }

        private class ComboItem
        {
            public int Id { get; }
            public string Name { get; }
            public ComboItem(int id, string name) { Id = id; Name = name; }
            public override string ToString() => Name;
        }

        private class HireItem
        {
            public BoatHire Hire { get; }
            public HireItem(BoatHire hire) { Hire = hire; }
            public override string ToString() =>
                $"#{Hire.HireID} — Boat {Hire.RentalBoatID} — {Hire.StartDate:d} to {Hire.EndDate:d} — {Hire.Status} ({Hire.DurationDays} days)";
        }
    }
}
