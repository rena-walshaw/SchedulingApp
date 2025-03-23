using System;
using System.Windows.Forms;

namespace Scheduling_App
{
    /// <summary>
    /// The main entry point for the Scheduling App.
    /// Initializes and starts the application.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main method that starts the Windows Forms application.
        /// </summary>
        [STAThread] // Specifies that the application runs in a single-threaded apartment (STA) mode.
        static void Main()
        {
            // Enables visual styles for the application to match modern UI appearance.
            Application.EnableVisualStyles();

            // Ensures that text rendering is compatible with older Windows Forms applications.
            Application.SetCompatibleTextRenderingDefault(false);

            // Starts the application by opening the login form.
            Application.Run(new LoginForm());
        }
    }
}
