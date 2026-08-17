using System.Drawing;
using System.Windows.Forms;
using PopeyeMarina.Screens;
using PopeyeMarina.Theming;

namespace PopeyeMarina
{
    public partial class MainForm : Form
    {
        private Button? currentSelectedButton;

        public MainForm()
        {
            InitializeComponent();

            // Default to the Leases section on load
            SelectNav(btnLeases);
        }

        private void NavButton_Click(object sender, System.EventArgs e)
        {
            if (sender is Button clickedButton)
            {
                SelectNav(clickedButton);
            }
        }

        private void SelectNav(Button selectedButton)
        {
            // Reset previous selection back to muted styling
            if (currentSelectedButton != null)
            {
                currentSelectedButton.ForeColor = Theme.MutedTextColor;
                currentSelectedButton.Font = Theme.NavFont(selected: false);
            }

            // Highlight the newly selected nav item
            selectedButton.ForeColor = Theme.AccentColor;
            selectedButton.Font = Theme.NavFont(selected: true);
            currentSelectedButton = selectedButton;

            string sectionName = selectedButton.Tag as string ?? selectedButton.Text;
            LoadSectionPlaceholder(sectionName);
        }

        // Sections without a built screen yet (Slips, Leases, Records) still
        // fall through to the placeholder below until their own milestones.
        private void LoadSectionPlaceholder(string sectionName)
        {
            contentPanel.Controls.Clear();

            if (sectionName == "Leases")
            {
                contentPanel.Controls.Add(new LeasesControl());
                return;
            }

            if (sectionName == "Customers")
            {
                contentPanel.Controls.Add(new CustomersControl());
                return;
            }

            if (sectionName == "Slips")
            {
                contentPanel.Controls.Add(new SlipsControl());
                return;
            }

            if (sectionName == "Records")
            {
                contentPanel.Controls.Add(new RecordsControl());
                return;
            }

            Label placeholder = new Label
            {
                AutoSize = true,
                Font = Theme.ScreenHeaderFont,
                ForeColor = Theme.PrimaryTextColor,
                Location = new Point(24, 24),
                Text = sectionName + " (screen not yet built)"
            };

            contentPanel.Controls.Add(placeholder);
        }
    }
}