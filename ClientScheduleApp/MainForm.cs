using ClientScheduleApp;
using System;
using System.Windows.Forms;

namespace Scheduling_App
{
    /// <summary>
    /// Represents the main application dashboard, providing access to appointments, customers, and reports.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Stores the currently logged-in user information.
        /// </summary>
        public User LoggedInUser { get; private set; }

        /// <summary>
        /// Initializes the MainForm with user-specific data.
        /// </summary>
        /// <param name="user">The logged-in user.</param>
        public MainForm(User user)
        {
            InitializeComponent();
            LoggedInUser = user;

            // Display a personalized welcome message
            labelTitle.Text = $"Welcome, {LoggedInUser.UserName}";

            // Check for upcoming appointments upon login
            CheckUpcomingAppointments();
        }

        /// <summary>
        /// Checks if the user has any upcoming appointments and notifies them if any are scheduled within the next 15 minutes.
        /// </summary>
        private void CheckUpcomingAppointments()
        {
            var upcomingAppointments = DatabaseHelper.GetUpcomingAppointments(LoggedInUser.UserId);

            if (upcomingAppointments.Count == 0)
            {
                return;
            }

            DateTime nowUTC = DateTime.UtcNow;
            DateTime nowLocal = TimeZoneInfo.ConvertTimeFromUtc(nowUTC, TimeZoneInfo.Local);

            var soonestAppointment = upcomingAppointments
                .Find(a => (a.Start.ToLocalTime() - nowLocal).TotalMinutes <= 15 && (a.Start.ToLocalTime() - nowLocal).TotalMinutes > 0);

            if (soonestAppointment != null)
            {
                MessageBox.Show($"Reminder: You have an upcoming appointment '{soonestAppointment.Title}' at {soonestAppointment.Start.ToLocalTime():hh:mm tt}.",
                    "Upcoming Appointment", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Opens the Appointment Management Form.
        /// </summary>
        private void buttonAppointments_Click(object sender, EventArgs e)
        {
            var appointmentForm = new AppointmentForm(this);
            appointmentForm.Show();
            Hide();
        }

        /// <summary>
        /// Opens the Customer Management Form.
        /// </summary>
        private void buttonCustomers_Click(object sender, EventArgs e)
        {
            var customerForm = new CustomerForm();
            customerForm.Show();
            Hide();
        }

        /// <summary>
        /// Opens the Reports Form.
        /// </summary>
        private void buttonReports_Click(object sender, EventArgs e)
        {
            ReportsForm reportsForm = new ReportsForm(this);
            reportsForm.Show();
            Hide();
        }

        /// <summary>
        /// Logs out the user by closing the current session and restarting the application.
        /// </summary>
        private void buttonLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
                Application.Restart();
            }
        }
    }
}
