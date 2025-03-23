using ClientScheduleApp;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Scheduling_App
{
    /// <summary>
    /// Form for adding a new appointment to the scheduling system.
    /// </summary>
    public partial class AddAppointmentForm : Form
    {
        private MainForm mainForm; // Reference to the main application form.

        /// <summary>
        /// Initializes the AddAppointmentForm.
        /// </summary>
        /// <param name="main">Reference to the main form.</param>
        public AddAppointmentForm(MainForm main)
        {
            InitializeComponent();
            mainForm = main;
            LoadAppointmentTypes(); // Populate appointment types dropdown.
            LoadCustomers(); // Populate customers dropdown.
        }

        /// <summary>
        /// Loads predefined appointment types into the combo box.
        /// </summary>
        private void LoadAppointmentTypes()
        {
            typeComboBox.Items.AddRange(new string[] { "Scrum", "Presentation", "Lunch", "Interview", "Consultation" });
            typeComboBox.SelectedIndex = 0; // Set default selection.
        }

        /// <summary>
        /// Loads the list of customers from the database into the combo box.
        /// </summary>
        private void LoadCustomers()
        {
            var customers = DatabaseHelper.GetAllCustomers();

            if (customers == null || customers.Count == 0)
            {
                MessageBox.Show("No customers found.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                customerComboBox.DataSource = null;
                return;
            }

            customerComboBox.DataSource = customers;
            customerComboBox.DisplayMember = "CustomerName"; // Display customer name.
            customerComboBox.ValueMember = "CustomerId"; // Store customer ID.
        }

        /// <summary>
        /// Handles the save button click event to create a new appointment.
        /// </summary>
        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve input values
                string title = titleTextBox.Text.Trim();
                string type = typeComboBox.SelectedItem?.ToString();
                string description = descriptionTextBox.Text.Trim();
                string location = locationTextBox.Text.Trim();
                string url = urlTextBox.Text.Trim();
                int customerId = Convert.ToInt32(customerComboBox.SelectedValue);
                int userId = mainForm.LoggedInUser.UserId; // Retrieve logged-in user ID.

                // Validate required fields
                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description) ||
                    string.IsNullOrEmpty(location) || string.IsNullOrEmpty(url))
                {
                    MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Convert start and end times to local DateTime
                DateTime startLocal = DateTime.SpecifyKind(
                    startDatePicker.Value.Date + startTimePicker.Value.TimeOfDay,
                    DateTimeKind.Local
                );

                DateTime endLocal = DateTime.SpecifyKind(
                    endDatePicker.Value.Date + endTimePicker.Value.TimeOfDay,
                    DateTimeKind.Local
                );

                // Convert local times to UTC for database consistency
                DateTime startUTC = TimeZoneInfo.ConvertTimeToUtc(startLocal);
                DateTime endUTC = TimeZoneInfo.ConvertTimeToUtc(endLocal);

                // Validate business hours (9 AM - 5 PM EST, Monday-Friday)
                if (!DatabaseHelper.IsWithinBusinessHours(startUTC, endUTC))
                {
                    MessageBox.Show("Appointments must be scheduled between 9 AM - 5 PM EST, Monday-Friday.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check for overlapping appointments
                if (DatabaseHelper.AppointmentOverlaps(startUTC, endUTC, customerId, 0))
                {
                    MessageBox.Show("This appointment overlaps with an existing one. Please choose a different time.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Create new appointment object
                Appointment newAppointment = new Appointment(
                    0, customerId, userId, title, type, description, location, customerId.ToString(), url,
                    startUTC, endUTC, DateTime.UtcNow, mainForm.LoggedInUser.UserName, DateTime.UtcNow, mainForm.LoggedInUser.UserName
                );

                // Add appointment to the database
                DatabaseHelper.AddAppointment(newAppointment);
                MessageBox.Show("Appointment added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding appointment: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the cancel button click event to close the form.
        /// </summary>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
