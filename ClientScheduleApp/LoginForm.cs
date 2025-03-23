using ClientScheduleApp;
using System;
using System.Windows.Forms;

namespace Scheduling_App
{
    /// <summary>
    /// Represents the login form where users enter credentials to access the application.
    /// </summary>
    public partial class LoginForm : Form
    {
        /// <summary>
        /// Initializes the LoginForm components.
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the login button click event. 
        /// Validates user credentials and logs successful login attempts.
        /// </summary>
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            // Retrieve user input for username and password
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;

            // Validate the user's credentials using the database
            if (DatabaseHelper.ValidateUser(username, password))
            {
                // Log the successful login attempt
                Logging.LogActivity(username);

                // Display success message
                MessageBox.Show("Login successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Retrieve the logged-in user information
                User loggedInUser = DatabaseHelper.GetUser(username);

                // Open the main application form and pass the logged-in user data
                MainForm mainForm = new MainForm(loggedInUser);
                mainForm.Show();

                // Hide the login form
                this.Hide();
            }
            else
            {
                // Display an error message for invalid credentials
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the exit button click event to close the application.
        /// </summary>
        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
