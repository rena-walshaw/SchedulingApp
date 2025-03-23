namespace Scheduling_App
{
    /// <summary>
    /// The designer class for ReportsForm.
    /// This class initializes and arranges UI elements for the Reports window.
    /// </summary>
    partial class ReportsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.DataGridView reportsDataGridView;
        private System.Windows.Forms.Button buttonAppointmentsByMonth;
        private System.Windows.Forms.Button buttonUserSchedule;
        private System.Windows.Forms.Button buttonCustomerAppointments;
        private System.Windows.Forms.Button buttonBack;

        /// <summary>
        /// Disposes of the resources used by the form.
        /// </summary>
        /// <param name="disposing">True if disposing managed resources; otherwise, false.</param>
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
        /// Initializes UI components for the ReportsForm.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelTitle = new System.Windows.Forms.Label();
            this.reportsDataGridView = new System.Windows.Forms.DataGridView();
            this.buttonAppointmentsByMonth = new System.Windows.Forms.Button();
            this.buttonUserSchedule = new System.Windows.Forms.Button();
            this.buttonCustomerAppointments = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.reportsDataGridView)).BeginInit();
            this.SuspendLayout();

            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.labelTitle.Location = new System.Drawing.Point(20, 20);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(200, 29);
            this.labelTitle.Text = "Reports Overview";

            // 
            // reportsDataGridView
            // 
            this.reportsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.reportsDataGridView.Location = new System.Drawing.Point(20, 60);
            this.reportsDataGridView.Name = "reportsDataGridView";
            this.reportsDataGridView.Size = new System.Drawing.Size(600, 260);
            this.reportsDataGridView.TabIndex = 0;

            // 
            // buttonAppointmentsByMonth
            // 
            this.buttonAppointmentsByMonth.Location = new System.Drawing.Point(20, 340);
            this.buttonAppointmentsByMonth.Name = "buttonAppointmentsByMonth";
            this.buttonAppointmentsByMonth.Size = new System.Drawing.Size(180, 40);
            this.buttonAppointmentsByMonth.Text = "Appointments by Month";
            this.buttonAppointmentsByMonth.UseVisualStyleBackColor = true;
            this.buttonAppointmentsByMonth.Click += new System.EventHandler(this.buttonAppointmentsByMonth_Click);

            // 
            // buttonUserSchedule
            // 
            this.buttonUserSchedule.Location = new System.Drawing.Point(220, 340);
            this.buttonUserSchedule.Name = "buttonUserSchedule";
            this.buttonUserSchedule.Size = new System.Drawing.Size(150, 40);
            this.buttonUserSchedule.Text = "User Schedule";
            this.buttonUserSchedule.UseVisualStyleBackColor = true;
            this.buttonUserSchedule.Click += new System.EventHandler(this.buttonUserSchedule_Click);

            // 
            // buttonCustomerAppointments
            // 
            this.buttonCustomerAppointments.Location = new System.Drawing.Point(390, 340);
            this.buttonCustomerAppointments.Name = "buttonCustomerAppointments";
            this.buttonCustomerAppointments.Size = new System.Drawing.Size(200, 40);
            this.buttonCustomerAppointments.Text = "Customer Appointments";
            this.buttonCustomerAppointments.UseVisualStyleBackColor = true;
            this.buttonCustomerAppointments.Click += new System.EventHandler(this.buttonCustomerAppointments_Click);

            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(20, 400);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(100, 40);
            this.buttonBack.Text = "Back";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);

            // 
            // ReportsForm
            // 
            this.ClientSize = new System.Drawing.Size(640, 460);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.reportsDataGridView);
            this.Controls.Add(this.buttonAppointmentsByMonth);
            this.Controls.Add(this.buttonUserSchedule);
            this.Controls.Add(this.buttonCustomerAppointments);
            this.Controls.Add(this.buttonBack);
            this.Name = "ReportsForm";
            this.Text = "Reports";

            ((System.ComponentModel.ISupportInitialize)(this.reportsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
