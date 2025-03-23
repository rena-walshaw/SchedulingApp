using ClientScheduleApp;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Scheduling_App
{
    /// <summary>
    /// Form for viewing, managing, and filtering appointments in the scheduling application.
    /// Allows users to add, update, delete, and filter appointments.
    /// </summary>
    public partial class AppointmentForm : Form
    {
        private MainForm mainForm;

        /// <summary>
        /// Initializes the AppointmentForm and loads the list of appointments.
        /// </summary>
        /// <param name="main">Reference to the main form.</param>
        public AppointmentForm(MainForm main)
        {
            InitializeComponent();
            mainForm = main;
            LoadAppointments();
        }

        /// <summary>
        /// Loads all appointments from the database and displays them in the DataGridView.
        /// </summary>
        private void LoadAppointments()
        {
            var appointments = DatabaseHelper.GetAllAppointments();

            foreach (var appointment in appointments)
            {
                appointment.Start = appointment.Start.ToLocalTime();
                appointment.End = appointment.End.ToLocalTime();
            }

            appointmentDataGridView.DataSource = DatabaseHelper.GetAllAppointments();
            FormatDataGridView();
        }

        /// <summary>
        /// Configures the appearance and behavior of the DataGridView for appointments.
        /// </summary>
        private void FormatDataGridView()
        {
            appointmentDataGridView.AutoResizeColumns();
            appointmentDataGridView.RowHeadersVisible = false;
            appointmentDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            appointmentDataGridView.MultiSelect = false;
            appointmentDataGridView.ReadOnly = true;
        }

        /// <summary>
        /// Filters appointments by the selected date from the calendar.
        /// </summary>
        /// <param name="sender">The object triggering the event.</param>
        /// <param name="e">Event arguments containing the selected date range.</param>
        private void monthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            DateTime selectedDate = e.Start.Date;
            List<Appointment> appointments = DatabaseHelper.GetAppointmentsByDate(selectedDate);

            foreach (var appointment in appointments)
            {
                appointment.Start = appointment.Start.ToLocalTime();
                appointment.End = appointment.End.ToLocalTime();
            }

            appointmentDataGridView.DataSource = appointments;
        }

        /// <summary>
        /// Opens the AddAppointmentForm to create a new appointment.
        /// Reloads the appointment list after adding a new appointment.
        /// </summary>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var addForm = new AddAppointmentForm(mainForm);
            addForm.ShowDialog();
            LoadAppointments();
        }

        /// <summary>
        /// Opens the ModifyAppointmentForm to edit a selected appointment.
        /// Reloads the appointment list after updating an appointment.
        /// </summary>
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (appointmentDataGridView.SelectedRows.Count > 0)
            {
                int appointmentId = Convert.ToInt32(appointmentDataGridView.SelectedRows[0].Cells["AppointmentId"].Value);
                Appointment appointment = DatabaseHelper.GetAppointmentById(appointmentId);

                if (appointment != null)
                {
                    appointment.Start = appointment.Start.ToLocalTime();
                    appointment.End = appointment.End.ToLocalTime();

                    var editForm = new ModifyAppointmentForm(mainForm, appointment);
                    editForm.ShowDialog();
                    LoadAppointments();
                }
                else
                {
                    MessageBox.Show("Appointment not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please select an appointment to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // <summary>
        /// Deletes the selected appointment after user confirmation.
        /// Reloads the appointment list after deletion.
        /// </summary>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (appointmentDataGridView.SelectedRows.Count > 0)
            {
                int appointmentId = Convert.ToInt32(appointmentDataGridView.SelectedRows[0].Cells["AppointmentId"].Value);
                DialogResult result = MessageBox.Show("Are you sure you want to delete this appointment?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    DatabaseHelper.DeleteAppointment(appointmentId);
                    LoadAppointments();
                }
            }
            else
            {
                MessageBox.Show("Please select an appointment to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Closes the AppointmentForm and returns to the main form.
        /// </summary>
        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
            mainForm.Show();
        }

        /// <summary>
        /// Updates the displayed appointments based on the selected filter (All, Week, or Month).
        /// </summary>
        /// <param name="sender">The object triggering the event.</param>
        /// <param name="e">Event arguments.</param>
        private void filterRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            List<Appointment> appointments;

            if (radioButtonWeek.Checked)
            {
                appointments = DatabaseHelper.GetAppointmentsByWeek();
            }
            else if (radioButtonMonth.Checked)
            {
                appointments = DatabaseHelper.GetAppointmentsByMonth();
            }
            else
            {
                appointments = DatabaseHelper.GetAllAppointments();
            }

            foreach (var appointment in appointments)
            {
                appointment.Start = appointment.Start.ToLocalTime();
                appointment.End = appointment.End.ToLocalTime();
            }

            appointmentDataGridView.DataSource = appointments;
        }
    }
}
