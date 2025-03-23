using ClientScheduleApp;
using System;
using System.Windows.Forms;

namespace Scheduling_App
{
    /// <summary>
    /// Form for modifying an existing customer's information.
    /// Allows users to update customer details such as name, address, and phone number.
    /// </summary>
    public partial class ModifyCustomerForm : Form
    {
        // Holds the selected customer object whose details are being modified.
        private Customer selectedCustomer;

        /// <summary>
        /// Initializes the ModifyCustomerForm with the selected customer's details.
        /// </summary>
        /// <param name="customer">The customer object to modify.</param>
        public ModifyCustomerForm(Customer customer)
        {
            InitializeComponent();
            selectedCustomer = customer;
            PopulateCustomerDetails();
        }

        /// <summary>
        /// Populates the form fields with the selected customer's details.
        /// </summary>
        private void PopulateCustomerDetails()
        {
            textBoxCustomerID.Text = selectedCustomer.CustomerId.ToString();
            textBoxCustomerName.Text = selectedCustomer.CustomerName;
            textBoxAddress.Text = selectedCustomer.Address;
            textBoxPhone.Text = selectedCustomer.Phone;
        }

        /// <summary>
        /// Handles the Save button click event.
        /// Validates input fields and updates the customer details in the database.
        /// </summary>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Trim input fields to remove accidental spaces
                string customerName = textBoxCustomerName.Text.Trim();
                string address = textBoxAddress.Text.Trim();
                string phone = textBoxPhone.Text.Trim();

                // Ensure all fields are filled
                if (string.IsNullOrWhiteSpace(customerName) || string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(phone))
                {
                    MessageBox.Show("All fields must be filled out.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate phone number (only digits and dashes allowed)
                if (!System.Text.RegularExpressions.Regex.IsMatch(phone, @"^[0-9-]+$"))
                {
                    MessageBox.Show("Phone number can only contain digits and dashes.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Update the selected customer object with the new values
                selectedCustomer.CustomerName = customerName;
                selectedCustomer.Address = address;
                selectedCustomer.Phone = phone;

                // Save the updated customer details in the database
                DatabaseHelper.UpdateCustomer(selectedCustomer);
                MessageBox.Show("Customer updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the Cancel button click event.
        /// Closes the form without making any changes.
        /// </summary>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
