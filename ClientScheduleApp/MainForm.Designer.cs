using System.Windows.Forms;

namespace Scheduling_App
{
    /// <summary>
    /// Partial class for MainForm, responsible for initializing and setting up UI components.
    /// </summary>
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button buttonAppointments;
        private System.Windows.Forms.Button buttonCustomers;
        private System.Windows.Forms.Button buttonReports;
        private System.Windows.Forms.Button buttonLogout;

        /// <summary>
        /// Cleans up resources used by the form.
        /// </summary>
        /// <param name="disposing">True if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Initializes the UI components of MainForm.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelTitle = new System.Windows.Forms.Label();
            this.buttonAppointments = new System.Windows.Forms.Button();
            this.buttonCustomers = new System.Windows.Forms.Button();
            this.buttonReports = new System.Windows.Forms.Button();
            this.buttonLogout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold);
            this.labelTitle.Location = new System.Drawing.Point(69, 29);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(325, 36);
            this.labelTitle.TabIndex = 5;
            this.labelTitle.Text = "Scheduler Main Menu";
            // 
            // buttonAppointments
            // 
            this.buttonAppointments.Location = new System.Drawing.Point(105, 94);
            this.buttonAppointments.Name = "buttonAppointments";
            this.buttonAppointments.Size = new System.Drawing.Size(250, 40);
            this.buttonAppointments.TabIndex = 4;
            this.buttonAppointments.Text = "Appointments";
            this.buttonAppointments.UseVisualStyleBackColor = true;
            this.buttonAppointments.Click += new System.EventHandler(this.buttonAppointments_Click);
            // 
            // buttonCustomers
            // 
            this.buttonCustomers.Location = new System.Drawing.Point(105, 154);
            this.buttonCustomers.Name = "buttonCustomers";
            this.buttonCustomers.Size = new System.Drawing.Size(250, 40);
            this.buttonCustomers.TabIndex = 3;
            this.buttonCustomers.Text = "Customers";
            this.buttonCustomers.UseVisualStyleBackColor = true;
            this.buttonCustomers.Click += new System.EventHandler(this.buttonCustomers_Click);
            // 
            // buttonReports
            // 
            this.buttonReports.Location = new System.Drawing.Point(105, 214);
            this.buttonReports.Name = "buttonReports";
            this.buttonReports.Size = new System.Drawing.Size(250, 40);
            this.buttonReports.TabIndex = 2;
            this.buttonReports.Text = "Reports";
            this.buttonReports.UseVisualStyleBackColor = true;
            this.buttonReports.Click += new System.EventHandler(this.buttonReports_Click);
            // 
            // buttonLogout
            // 
            this.buttonLogout.Location = new System.Drawing.Point(105, 274);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Size = new System.Drawing.Size(250, 40);
            this.buttonLogout.TabIndex = 1;
            this.buttonLogout.Text = "Logout";
            this.buttonLogout.UseVisualStyleBackColor = true;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(457, 381);
            this.Controls.Add(this.buttonLogout);
            this.Controls.Add(this.buttonReports);
            this.Controls.Add(this.buttonCustomers);
            this.Controls.Add(this.buttonAppointments);
            this.Controls.Add(this.labelTitle);
            this.Name = "MainForm";
            this.Text = "Scheduler Main";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

    }
}
