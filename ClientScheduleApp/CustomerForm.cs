using ClientScheduleApp;
using System;
using System.Windows.Forms;

namespace Scheduling_App
{
    /// <summary>
    /// Represents the customer management form where users can view, add, modify, and delete customers.
    /// </summary>
    public partial class CustomerForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerForm"/> class.
        /// </summary>
        public CustomerForm()
        {
            InitializeComponent();
            LoadCustomers();
        }

        /// <summary>
        /// Loads customer data into the DataGridView.
        /// </summary>
        private void LoadCustomers()
        {
            customerDataGridView.DataSource = DatabaseHelper.GetAllCustomers();
            FormatDataGridView();
        }

        /// <summary>
        /// Formats the DataGridView to improve readability and user experience.
        /// </summary>
        private void FormatDataGridView()
        {
            customerDataGridView.AutoResizeColumns();
            customerDataGridView.RowHeadersVisible = false;
            customerDataGridView.ReadOnly = true;
            customerDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            customerDataGridView.MultiSelect = false;
            customerDataGridView.ClearSelection();
        }

        /// <summary>
        /// Handles the click event for the Add button to open the Add Customer form.
        /// </summary>
        private void addButton_Click(object sender, EventArgs e)
        {
            AddCustomerForm addCustomerForm = new AddCustomerForm();

            // Refresh the customer list after the form is closed
            addCustomerForm.FormClosed += (s, args) => LoadCustomers();
            addCustomerForm.Show();
        }

        /// <summary>
        /// Handles the click event for the Modify button to open the Modify Customer form.
        /// </summary>
        private void modifyButton_Click(object sender, EventArgs e)
        {
            if (customerDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a customer to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int customerId = Convert.ToInt32(customerDataGridView.SelectedRows[0].Cells["CustomerId"].Value);
            Scheduling_App.Customer customer = DatabaseHelper.GetCustomerById(customerId);

            if (customer != null)
            {
                ModifyCustomerForm modifyCustomerForm = new ModifyCustomerForm(customer);

                // Refresh the customer list after the form is closed
                modifyCustomerForm.FormClosed += (s, args) => LoadCustomers();
                modifyCustomerForm.Show();
            }
            else
            {
                MessageBox.Show("Customer not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Handles the click event for the Delete button to remove a customer.
        /// </summary>
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (customerDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a customer to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmDelete = MessageBox.Show("Are you sure you want to delete this customer?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmDelete == DialogResult.Yes)
            {
                int customerId = Convert.ToInt32(customerDataGridView.SelectedRows[0].Cells["CustomerId"].Value);

                // Deletes the selected customer from the database
                DatabaseHelper.DeleteCustomer(customerId);

                // Refreshes the customer list after deletion
                LoadCustomers();
            }
        }

        /// <summary>
        /// Handles the click event for the Back button to return to the main form.
        /// </summary>
        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.OpenForms["MainForm"].Show();
        }
    }
}
