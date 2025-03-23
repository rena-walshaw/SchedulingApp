namespace Scheduling_App
{
    /// <summary>
    /// Partial class for AddAppointmentForm, defining UI components and layout.
    /// </summary>
    partial class AddAppointmentForm
    {
        // Components
        private System.ComponentModel.IContainer components = null;

        // Labels for form fields
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.Label locationLabel;
        private System.Windows.Forms.Label startDateLabel;
        private System.Windows.Forms.Label endDateLabel;
        private System.Windows.Forms.Label contactLabel;
        private System.Windows.Forms.Label urlLabel;

        // Input fields
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.TextBox locationTextBox;
        private System.Windows.Forms.DateTimePicker startDatePicker;
        private System.Windows.Forms.DateTimePicker startTimePicker;
        private System.Windows.Forms.DateTimePicker endDatePicker;
        private System.Windows.Forms.DateTimePicker endTimePicker;
        private System.Windows.Forms.ComboBox customerComboBox;
        private System.Windows.Forms.TextBox urlTextBox;

        // Buttons
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;

        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false otherwise.</param>
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
        /// Initializes and configures UI components for the AddAppointmentForm.
        /// </summary>
        private void InitializeComponent()
        {
            // Initialize Labels
            this.titleLabel = new System.Windows.Forms.Label();
            this.typeLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.locationLabel = new System.Windows.Forms.Label();
            this.startDateLabel = new System.Windows.Forms.Label();
            this.endDateLabel = new System.Windows.Forms.Label();
            this.contactLabel = new System.Windows.Forms.Label();
            this.urlLabel = new System.Windows.Forms.Label();

            // Initialize Input Fields
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.locationTextBox = new System.Windows.Forms.TextBox();
            this.startDatePicker = new System.Windows.Forms.DateTimePicker();
            this.startTimePicker = new System.Windows.Forms.DateTimePicker();
            this.endDatePicker = new System.Windows.Forms.DateTimePicker();
            this.endTimePicker = new System.Windows.Forms.DateTimePicker();
            this.customerComboBox = new System.Windows.Forms.ComboBox();
            this.urlTextBox = new System.Windows.Forms.TextBox();

            // Initialize Buttons
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();

            // Form Configuration
            this.SuspendLayout();

            //
            // titleLabel
            //
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(20, 20);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(36, 16);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Title:";
            // 
            // typeLabel
            // 
            this.typeLabel.AutoSize = true;
            this.typeLabel.Location = new System.Drawing.Point(20, 60);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(42, 16);
            this.typeLabel.TabIndex = 2;
            this.typeLabel.Text = "Type:";
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(20, 100);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(78, 16);
            this.descriptionLabel.TabIndex = 4;
            this.descriptionLabel.Text = "Description:";
            // 
            // locationLabel
            // 
            this.locationLabel.AutoSize = true;
            this.locationLabel.Location = new System.Drawing.Point(20, 140);
            this.locationLabel.Name = "locationLabel";
            this.locationLabel.Size = new System.Drawing.Size(61, 16);
            this.locationLabel.TabIndex = 6;
            this.locationLabel.Text = "Location:";
            // 
            // startDateLabel
            // 
            this.startDateLabel.AutoSize = true;
            this.startDateLabel.Location = new System.Drawing.Point(20, 180);
            this.startDateLabel.Name = "startDateLabel";
            this.startDateLabel.Size = new System.Drawing.Size(69, 16);
            this.startDateLabel.TabIndex = 8;
            this.startDateLabel.Text = "Start Date:";
            // 
            // endDateLabel
            // 
            this.endDateLabel.AutoSize = true;
            this.endDateLabel.Location = new System.Drawing.Point(20, 220);
            this.endDateLabel.Name = "endDateLabel";
            this.endDateLabel.Size = new System.Drawing.Size(66, 16);
            this.endDateLabel.TabIndex = 11;
            this.endDateLabel.Text = "End Date:";
            // 
            // contactLabel
            // 
            this.contactLabel.AutoSize = true;
            this.contactLabel.Location = new System.Drawing.Point(20, 260);
            this.contactLabel.Name = "contactLabel";
            this.contactLabel.Size = new System.Drawing.Size(55, 16);
            this.contactLabel.TabIndex = 14;
            this.contactLabel.Text = "Contact:";
            // 
            // urlLabel
            // 
            this.urlLabel.AutoSize = true;
            this.urlLabel.Location = new System.Drawing.Point(20, 306);
            this.urlLabel.Name = "urlLabel";
            this.urlLabel.Size = new System.Drawing.Size(27, 16);
            this.urlLabel.TabIndex = 18;
            this.urlLabel.Text = "Url:";
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new System.Drawing.Point(120, 20);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(250, 22);
            this.titleTextBox.TabIndex = 1;
            // 
            // typeComboBox
            // 
            this.typeComboBox.Location = new System.Drawing.Point(120, 60);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(250, 24);
            this.typeComboBox.TabIndex = 3;
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Location = new System.Drawing.Point(120, 100);
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(250, 22);
            this.descriptionTextBox.TabIndex = 5;
            // 
            // locationTextBox
            // 
            this.locationTextBox.Location = new System.Drawing.Point(120, 140);
            this.locationTextBox.Name = "locationTextBox";
            this.locationTextBox.Size = new System.Drawing.Size(250, 22);
            this.locationTextBox.TabIndex = 7;
            // 
            // startDatePicker
            // 
            this.startDatePicker.Location = new System.Drawing.Point(120, 180);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.Size = new System.Drawing.Size(150, 22);
            this.startDatePicker.TabIndex = 9;
            // 
            // startTimePicker
            // 
            this.startTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.startTimePicker.Location = new System.Drawing.Point(280, 180);
            this.startTimePicker.Name = "startTimePicker";
            this.startTimePicker.Size = new System.Drawing.Size(90, 22);
            this.startTimePicker.TabIndex = 10;
            // 
            // endDatePicker
            // 
            this.endDatePicker.Location = new System.Drawing.Point(120, 220);
            this.endDatePicker.Name = "endDatePicker";
            this.endDatePicker.Size = new System.Drawing.Size(150, 22);
            this.endDatePicker.TabIndex = 12;
            // 
            // endTimePicker
            // 
            this.endTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.endTimePicker.Location = new System.Drawing.Point(280, 220);
            this.endTimePicker.Name = "endTimePicker";
            this.endTimePicker.Size = new System.Drawing.Size(90, 22);
            this.endTimePicker.TabIndex = 13;
            // 
            // customerComboBox
            // 
            this.customerComboBox.Location = new System.Drawing.Point(120, 260);
            this.customerComboBox.Name = "customerComboBox";
            this.customerComboBox.Size = new System.Drawing.Size(250, 24);
            this.customerComboBox.TabIndex = 15;
            // 
            // urlTextBox
            // 
            this.urlTextBox.Location = new System.Drawing.Point(120, 303);
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.Size = new System.Drawing.Size(100, 22);
            this.urlTextBox.TabIndex = 19;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(120, 362);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(100, 30);
            this.saveButton.TabIndex = 16;
            this.saveButton.Text = "Save";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(270, 362);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 30);
            this.cancelButton.TabIndex = 17;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);

            // Form Configuration
            this.ClientSize = new System.Drawing.Size(446, 440);
            this.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                this.titleLabel, this.titleTextBox,
                this.typeLabel, this.typeComboBox,
                this.descriptionLabel, this.descriptionTextBox,
                this.locationLabel, this.locationTextBox,
                this.startDateLabel, this.startDatePicker, this.startTimePicker,
                this.endDateLabel, this.endDatePicker, this.endTimePicker,
                this.contactLabel, this.customerComboBox,
                this.urlLabel, this.urlTextBox,
                this.saveButton, this.cancelButton
            });
            this.Name = "AddAppointmentForm";
            this.Text = "Add Appointment";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
