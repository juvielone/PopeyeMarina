using System;
using System.Windows.Forms;
using PopeyeMarina.Data;
using PopeyeMarina.Models;

namespace PopeyeMarina.Screens
{
    public partial class CustomersControl : UserControl
    {
        public CustomersControl()
        {
            InitializeComponent();
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            try
            {
                dgvCustomers.Rows.Clear();

                foreach (Customer customer in CustomerData.GetAllCustomers())
                {
                    dgvCustomers.Rows.Add(
                        customer.CustomerID,
                        customer.CustomerName,
                        customer.Address,
                        customer.PhoneNo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to load customers.\n\n" + ex.Message,
                    "Load Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string address = txtAddress.Text.Trim();
            string phone = txtPhone.Text.Trim();

            // Required-field validation before touching the database
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(phone))
            {
                MessageBox.Show(
                    "Name, address, and phone number are all required.",
                    "Missing Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                CustomerData.AddCustomer(new Customer
                {
                    CustomerName = name,
                    Address = address,
                    PhoneNo = phone
                });

                ClearForm();
                LoadCustomers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to add customer.\n\n" + ex.Message,
                    "Save Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            txtName.Clear();
            txtAddress.Clear();
            txtPhone.Clear();
            txtName.Focus();
        }
    }
}
