using ClientScheduleApp;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Scheduling_App
{
    /// <summary>
    /// Form for adding a new customer to the database.
    /// Allows users to enter customer details, select country and city, and save the record.
    /// </summary>
    public partial class AddCustomerForm : Form
    {
        /// <summary>
        /// Initializes the form, loads the list of countries, 
        /// and sets up event handling for country selection changes.
        /// </summary>
        public AddCustomerForm()
        {
            InitializeComponent();
            LoadCountries();
            comboBoxCountry.SelectedIndexChanged += CountryComboBox_SelectedIndexChanged;
        }

        /// <summary>
        /// Loads the list of countries from the database into the country dropdown.
        /// Attaches an event handler to update the city list when a country is selected.
        /// </summary>
        private void LoadCountries()
        {
            var countries = DatabaseHelper.GetAllCountries();

            if (countries.Count == 0)
            {
                MessageBox.Show("No countries found in the database.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            comboBoxCountry.DataSource = new BindingSource(countries, null);
            comboBoxCountry.DisplayMember = "Value"; // Display country name
            comboBoxCountry.ValueMember = "Key"; // Store country ID
            comboBoxCountry.SelectedIndexChanged += CountryComboBox_SelectedIndexChanged;

            // Automatically select the first country and trigger the event to load cities
            if (comboBoxCountry.Items.Count > 0)
            {
                comboBoxCountry.SelectedIndex = 0;
                CountryComboBox_SelectedIndexChanged(null, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Event handler triggered when a country is selected.
        /// Loads the corresponding cities for the selected country into the city dropdown.
        /// </summary>
        private void CountryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCountry.SelectedItem == null) return;

            // Get the selected country ID
            int selectedCountryId = ((KeyValuePair<int, string>)comboBoxCountry.SelectedItem).Key;
            var cities = DatabaseHelper.GetCitiesByCountry(selectedCountryId);

            if (cities.Count == 0)
            {
                MessageBox.Show("No cities found for the selected country.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxCity.DataSource = null;
                return;
            }

            // Populate the city dropdown with the retrieved city data
            comboBoxCity.DataSource = new BindingSource(cities, null);
            comboBoxCity.DisplayMember = "Value"; // Display city name
            comboBoxCity.ValueMember = "Key"; // Store city ID
        }

        /// <summary>
        /// Handles the save button click event.
        /// Validates input fields, ensures all required data is provided,
        /// and adds the new customer to the database.
        /// </summary>
        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Trim input fields to remove accidental spaces
                string name = textBoxName.Text.Trim();
                string address = textBoxAddress.Text.Trim();
                string zipCode = textBoxZipCode.Text.Trim();
                string phone = textBoxPhone.Text.Trim();

                // Validate that all required fields are filled
                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(address) ||
                    comboBoxCity.SelectedItem == null || comboBoxCountry.SelectedItem == null ||
                    string.IsNullOrWhiteSpace(zipCode) || string.IsNullOrWhiteSpace(phone))
                {
                    MessageBox.Show("All fields must be filled.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate phone number format (only digits and dashes allowed)
                if (!System.Text.RegularExpressions.Regex.IsMatch(phone, @"^[0-9-]+$"))
                {
                    MessageBox.Show("Phone number can only contain digits and dashes.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get the selected city ID from the dropdown
                int cityId = Convert.ToInt32(comboBoxCity.SelectedValue);

                // Save the address and retrieve its generated ID
                int addressId = DatabaseHelper.AddAddress(address, cityId, zipCode, phone);

                // Save the customer with the retrieved address ID
                DatabaseHelper.AddCustomer(name, addressId);

                MessageBox.Show("Customer added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding customer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the cancel button click event, closing the form without saving changes.
        /// </summary>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
