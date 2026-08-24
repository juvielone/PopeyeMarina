using System;
using System.Drawing;
using System.Windows.Forms;
using PopeyeMarina.Core.Data;
using PopeyeMarina.Core.Models;
using PopeyeMarina.Theming;

namespace PopeyeMarina.Screens
{
    public partial class AddRentalBoatForm : Form
    {
        private TextBox txtBoatName = new TextBox();
        private Button btnSailboat = new Button();
        private Button btnPowerboat = new Button();
        private string _selectedType = "Sailboat";

        public AddRentalBoatForm()
        {
            InitializeComponent();
            BuildLayout();
        }

        private void BuildLayout()
        {
            Label nameLabel = MakeFieldLabel("Boat name", 16, 16);
            txtBoatName.Font = Theme.InputFont;
            txtBoatName.Location = new Point(16, 34);
            txtBoatName.Size = new Size(300, 25);

            Label typeLabel = MakeFieldLabel("Type", 16, 70);

            btnSailboat.FlatStyle = FlatStyle.Flat;
            btnSailboat.FlatAppearance.BorderSize = 0;
            btnSailboat.Font = Theme.ButtonFont;
            btnSailboat.Location = new Point(16, 88);
            btnSailboat.Size = new Size(145, 30);
            btnSailboat.Text = "Sailboat";
            btnSailboat.Cursor = Cursors.Hand;
            btnSailboat.Click += (s, e) => { _selectedType = "Sailboat"; ApplyTypeVisuals(); };

            btnPowerboat.FlatStyle = FlatStyle.Flat;
            btnPowerboat.FlatAppearance.BorderSize = 0;
            btnPowerboat.Font = Theme.ButtonFont;
            btnPowerboat.Location = new Point(171, 88);
            btnPowerboat.Size = new Size(145, 30);
            btnPowerboat.Text = "Powerboat";
            btnPowerboat.Cursor = Cursors.Hand;
            btnPowerboat.Click += (s, e) => { _selectedType = "Powerboat"; ApplyTypeVisuals(); };

            Button btnSave = new Button
            {
                FlatStyle = FlatStyle.Flat,
                BackColor = Theme.PrimaryButtonBackColor,
                ForeColor = Theme.PrimaryButtonForeColor,
                Font = Theme.ButtonFont,
                Location = new Point(16, 140),
                Size = new Size(140, 34),
                Text = "Add Boat",
                Cursor = Cursors.Hand
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;

            Button btnCancel = new Button
            {
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(240, 241, 242),
                ForeColor = Theme.SecondaryTextColor,
                Font = Theme.ButtonFont,
                Location = new Point(176, 140),
                Size = new Size(140, 34),
                Text = "Cancel",
                Cursor = Cursors.Hand,
                DialogResult = DialogResult.Cancel
            };
            btnCancel.FlatAppearance.BorderSize = 0;

            Controls.AddRange(new Control[]
            {
                nameLabel, txtBoatName, typeLabel, btnSailboat, btnPowerboat, btnSave, btnCancel
            });

            AcceptButton = btnSave;
            CancelButton = btnCancel;

            ApplyTypeVisuals();
        }

        private void ApplyTypeVisuals()
        {
            void Style(Button button, bool selected)
            {
                button.BackColor = selected ? Theme.PrimaryButtonBackColor : Color.FromArgb(240, 241, 242);
                button.ForeColor = selected ? Theme.PrimaryButtonForeColor : Theme.SecondaryTextColor;
            }

            Style(btnSailboat, _selectedType == "Sailboat");
            Style(btnPowerboat, _selectedType == "Powerboat");
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            string name = txtBoatName.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Enter a boat name.", "Missing Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                RentalBoatData.AddRentalBoat(new RentalBoat
                {
                    BoatName = name,
                    BoatType = _selectedType,
                    IsActive = true
                });

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to add rental boat.\n\n" + ex.Message,
                    "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
