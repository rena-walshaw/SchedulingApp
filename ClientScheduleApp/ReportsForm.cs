using ClientScheduleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Scheduling_App
{
    /// <summary>
    /// The ReportsForm class provides different reporting options for appointments.
    /// It allows users to view reports based on appointments by month, user schedules, and customer appointments.
    /// </summary>
    public partial class ReportsForm : Form
    {
        private MainForm mainForm;

        /// <summary>
        /// Initializes the ReportsForm and stores a reference to the MainForm.
        /// </summary>
        /// <param name="main">The main form instance to return to when closing the report window.</param>
        public ReportsForm(MainForm main)
        {
            InitializeComponent();
            mainForm = main;
        }

        /// <summary>
        /// Generates a report of appointment counts grouped by month and type.
        /// Displays the report in the DataGridView.
        /// </summary>
        private void buttonAppointmentsByMonth_Click(object sender, EventArgs e)
        {
            var appointments = DatabaseHelper.GetAllAppointments();

            foreach (var appointment in appointments)
            {
                appointment.Start = appointment.Start.ToLocalTime();
            }

            var report = appointments
                .GroupBy(a => new { a.Start.Month, a.Type }) // Group by month and type of appointment
                .Select(g => new
                {
                    Month = g.Key.Month,
                    Type = g.Key.Type,
                    Count = g.Count() // Count number of appointments in each group
                })
                .OrderBy(r => r.Month) // Sort by month
                .ToList();

            reportsDataGridView.DataSource = report; // Display report in DataGridView
        }

        /// <summary>
        /// Generates a report of all appointments assigned to users, displaying schedules by user.
        /// Joins appointment data with user data and sorts results by username.
        /// </summary>
        private void buttonUserSchedule_Click(object sender, EventArgs e)
        {
            var appointments = DatabaseHelper.GetAllAppointments();
            var users = DatabaseHelper.GetAllUsers();

            // Convert times before displaying
            foreach (var appointment in appointments)
            {
                appointment.Start = appointment.Start.ToLocalTime();
                appointment.End = appointment.End.ToLocalTime();
            }

            var report = appointments
                .Join(users, // Join appointments with users based on UserId
                      a => a.UserId,
                      u => u.UserId,
                      (a, u) => new
                      {
                          UserName = u.UserName,
                          AppointmentTitle = a.Title,
                          Type = a.Type,
                          StartTime = a.Start,
                          EndTime = a.End
                      })
                .OrderBy(r => r.UserName) // Sort results by user name
                .ToList();

            reportsDataGridView.DataSource = report; // Display report in DataGridView
        }

        /// <summary>
        /// Generates a report of total appointments per customer.
        /// Groups appointments by customer and counts the number of appointments per customer.
        /// </summary>
        private void buttonCustomerAppointments_Click(object sender, EventArgs e)
        {
            var appointments = DatabaseHelper.GetAllAppointments();
            var customers = DatabaseHelper.GetAllCustomers();

            var report = appointments
                .Join(customers, // Join appointments with customers based on CustomerId
                      a => a.CustomerId,
                      c => c.CustomerId,
                      (a, c) => new { c.CustomerName, a.CustomerId })
                .GroupBy(a => a.CustomerName) // Group by customer name
                .Select(g => new
                {
                    Customer = g.Key,
                    TotalAppointments = g.Count() // Count total appointments per customer
                })
                .OrderByDescending(r => r.TotalAppointments) // Sort by highest number of appointments
                .ToList();

            reportsDataGridView.DataSource = report; // Display report in DataGridView
        }

        /// <summary>
        /// Closes the report form and returns to the MainForm.
        /// </summary>
        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
            mainForm.Show();
        }
    }
}

