using ClientScheduleApp;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Scheduling_App
{
    /// <summary>
    /// Form for modifying an existing appointment.
    /// Allows users to edit appointment details, validate inputs, and update the database.
    /// </summary>
    public partial class ModifyAppointmentForm : Form
    {
        private Appointment appointment; // The appointment being modified
        private MainForm mainForm; // Reference to the main form

        /// <summary>
        /// Initializes the form and loads the selected appointment details.
        /// </summary>
        /// <param name="mainForm">Reference to the main application form.</param>
        /// <param name="appointment">The appointment to modify.</param>
        public ModifyAppointmentForm(MainForm mainForm, Appointment appointment)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.appointment = appointment;
            PopulateFields();
        }

        /// <summary>
        /// Populates the form fields with the current appointment details.
        /// </summary>
        private void PopulateFields()
        {
            textBoxTitle.Text = appointment.Title;
            textBoxDescription.Text = appointment.Description;
            comboBoxType.SelectedItem = appointment.Type;
            textBoxLocation.Text = appointment.Location;
            textBoxUrl.Text = appointment.Url;

            // Load customers into the ComboBox and select the current customer
            var customers = DatabaseHelper.GetAllCustomers();
            comboBoxCustomer.DataSource = customers;
            comboBoxCustomer.DisplayMember = "CustomerName";
            comboBoxCustomer.ValueMember = "CustomerId";
            comboBoxCustomer.SelectedValue = appointment.CustomerId;

            // Validate and set the contact ID if applicable
            if (int.TryParse(appointment.Contact, out int contactId))
            {
                comboBoxCustomer.SelectedValue = contactId;
            }
            else
            {
                MessageBox.Show("Invalid contact ID format.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Set date and time pickers with existing appointment values
            dateTimePickerStartDate.Value = appointment.Start.Date;
            dateTimePickerStartTime.Value = appointment.Start;
            dateTimePickerEndDate.Value = appointment.End.Date;
            dateTimePickerEndTime.Value = appointment.End;
        }

        /// <summary>
        /// Handles the save button click event, validates user input, and updates the appointment.
        /// </summary>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Ensure all required fields are filled
                if (string.IsNullOrWhiteSpace(textBoxTitle.Text) ||
                    string.IsNullOrWhiteSpace(textBoxDescription.Text) ||
                    comboBoxType.SelectedItem == null ||
                    string.IsNullOrWhiteSpace(textBoxLocation.Text) ||
                    comboBoxCustomer.SelectedItem == null)
                {
                    throw new ApplicationException("All fields must be completed.");
                }

                // Convert start and end times to UTC for database consistency
                DateTime startLocal = DateTime.SpecifyKind(
                    dateTimePickerStartDate.Value.Date + dateTimePickerStartTime.Value.TimeOfDay,
                    DateTimeKind.Local
                );
                DateTime endLocal = DateTime.SpecifyKind(
                    dateTimePickerEndDate.Value.Date + dateTimePickerEndTime.Value.TimeOfDay,
                    DateTimeKind.Local
                );

                DateTime startUTC = TimeZoneInfo.ConvertTimeToUtc(startLocal);
                DateTime endUTC = TimeZoneInfo.ConvertTimeToUtc(endLocal);

                // Validate business hours (9 AM - 5 PM EST, Monday-Friday)
                if (!DatabaseHelper.IsWithinBusinessHours(startUTC, endUTC))
                {
                    MessageBox.Show("Appointments must be scheduled between 9 AM - 5 PM EST, Monday-Friday.",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check for overlapping appointments
                if (DatabaseHelper.AppointmentOverlaps(startUTC, endUTC, appointment.CustomerId, appointment.AppointmentId))
                {
                    MessageBox.Show("This appointment overlaps with an existing one. Please choose a different time.",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Update appointment details with user input
                appointment.Title = textBoxTitle.Text;
                appointment.Description = textBoxDescription.Text;
                appointment.Type = comboBoxType.SelectedItem.ToString();
                appointment.Location = textBoxLocation.Text;
                appointment.Url = textBoxUrl.Text;
                appointment.Start = startUTC;
                appointment.End = endUTC;
                appointment.CustomerId = Convert.ToInt32(comboBoxCustomer.SelectedValue);

                // Save the updated appointment to the database
                DatabaseHelper.UpdateAppointment(appointment);
                MessageBox.Show("Appointment updated successfully.", "Success", MessageBoxButtons.OK);
                Close();
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the cancel button click event to close the form without saving changes.
        /// </summary>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

