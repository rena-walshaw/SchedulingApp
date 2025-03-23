using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Scheduling_App;

namespace ClientScheduleApp
{
    /// <summary>
    /// Provides helper methods for database operations related to appointments, customers, users, and addresses.
    /// </summary>
    public static class DatabaseHelper
    {
        // Database connection string
        private static string connectionString = "server=localhost;user=sqlUser;database=client_schedule;port=3306;password=Passw0rd!";

         /// <summary>
        /// Establishes a new database connection.
        /// </summary>
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        /// <summary>
        /// Tests the database connection.
        /// </summary>
        public static void TestConnection()
        {
            using (var conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    MessageBox.Show("Database connection successful!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database connection failed: " + ex.Message);
                }
            }
        }
        /// <summary>
        /// Validates user credentials by checking if the given username and password exist in the database.
        /// </summary>
        /// <summary>
        /// Validates user credentials by checking if the given username and password exist in the database.
        /// </summary>
        public static bool ValidateUser(string username, string password)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM user WHERE userName = @username AND password = @password";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (var reader = cmd.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
        }

        /// <summary>
        /// Retrieves a user from the database based on the provided username.
        /// </summary>
        /// <param name="username">The username of the user to retrieve.</param>
        /// <returns>
        /// Returns a <see cref="User"/> object if a matching user is found; otherwise, returns null.
        /// </returns>
        public static User GetUser(string username)
        {
            using (var conn = GetConnection())
            {
                string query = "SELECT * FROM user WHERE userName = @username";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User(
                            reader.GetInt32("userId"),
                            reader.GetString("userName"),
                            reader.GetString("password"),
                            reader.GetInt32("active"),
                            reader.GetDateTime("createDate"),
                            reader.GetString("createdBy"),
                            reader.GetDateTime("lastUpdate"),
                            reader.GetString("lastUpdateBy")
                        );
                    }
                }
            }
            return null; // If user not found
        }

        /// <summary>
        /// Retrieves all users from the database.
        /// </summary>
        /// <returns>
        /// A list of <see cref="User"/> objects containing all user records.
        /// If an error occurs, an empty list is returned, and an error message is displayed.
        /// </returns>
        public static List<User> GetAllUsers()
        {
            List<User> users = new List<User>(); // Initialize an empty list to store users
            using (var conn = GetConnection()) // Establish a database connection
            {
                try
                {
                    conn.Open(); // Open the connection

                    // SQL query to fetch all user records with relevant fields
                    string query = "SELECT userId, userName, password, active, createDate, createdBy, lastUpdate, lastUpdateBy FROM user";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader()) // Execute query and read results
                    {
                        while (reader.Read()) // Loop through each user record
                        {
                            users.Add(new User(
                                reader.GetInt32("userId"),
                                reader.GetString("userName"),
                                reader.GetString("password"),
                                reader.GetInt32("active"),
                                reader.GetDateTime("createDate"),
                                reader.GetString("createdBy"),
                                reader.GetDateTime("lastUpdate"),
                                reader.GetString("lastUpdateBy")
                            ));
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Display an error message if the database query fails
                    MessageBox.Show("Error retrieving users: " + ex.Message);
                }
            }
            return users; // Return the list of users (empty if an error occurred)
        }

        /// <summary>
        /// Adds a new customer record to the database.
        /// </summary>
        /// <param name="customerName">The name of the customer to be added.</param>
        /// <param name="addressID">The ID of the associated address for the customer.</param>
        public static void AddCustomer(string customerName, int addressID)
        {
            using (var conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO customer 
                            (customerName, addressId, active, createDate, createdBy, lastUpdate, lastUpdateBy) 
                             VALUES (@customerName, @addressID, 1, NOW(), 'admin', NOW(), 'admin');";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@customerName", customerName);
                        cmd.Parameters.AddWithValue("@addressID", addressID);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding customer: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Retrieves all customers from the database.
        /// </summary>
        public static List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();

            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "SELECT customerId, customerName FROM customer";

                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(new Customer(
                            reader.GetInt32("customerId"),
                            reader.GetString("customerName"),
                            "", // Placeholder for address
                            ""  // Placeholder for phone
                        ));
                    }
                }
            }

            return customers;
        }

        /// <summary>
        /// Retrieves a customer by ID.
        /// </summary>
        public static Customer GetCustomerById(int id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = @"
            SELECT c.CustomerId, c.CustomerName, a.Address, a.Phone 
            FROM customer c
            JOIN address a ON c.addressId = a.addressId
            WHERE c.CustomerId = @CustomerId";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Customer(
                                reader.GetInt32("CustomerId"),
                                reader.GetString("CustomerName"),
                                reader.GetString("Address"),
                                reader.GetString("Phone")
                            );
                        }
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// Updates an existing customer's information in the database.
        /// </summary>
        /// <param name="customer">The customer object containing the updated details.</param>
        public static void UpdateCustomer(Customer customer)
        {
            using (var conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"
                UPDATE customer c
                JOIN address a ON c.addressId = a.addressId
                SET c.CustomerName = @CustomerName,
                    a.Address = @Address,
                    a.Phone = @Phone
                WHERE c.CustomerId = @CustomerId";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
                        cmd.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                        cmd.Parameters.AddWithValue("@Address", customer.Address);
                        cmd.Parameters.AddWithValue("@Phone", customer.Phone);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating customer: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Deletes a customer from the database based on the provided customer ID.
        /// </summary>
        /// <param name="customerId">The ID of the customer to be deleted.</param>
        public static void DeleteCustomer(int customerId)
        {
            using (var conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM customer WHERE customerId = @customerId";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@customerId", customerId);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            MessageBox.Show("No customer found with the given ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("Customer deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (MySqlException ex) when (ex.Number == 1451)
                {
                    MessageBox.Show("Cannot delete this customer because they have related records (appointments, etc.).",
                                    "Deletion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting customer: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Adds a new address record to the database and returns the generated address ID.
        /// </summary>
        /// <param name="address">The street address of the customer.</param>
        /// <param name="cityId">The ID of the associated city.</param>
        /// <param name="zipCode">The postal code for the address.</param>
        /// <param name="phone">The contact phone number associated with the address.</param>
        public static int AddAddress(string address, int cityId, string zipCode, string phone)
        {
            using (var conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO address 
                            (address, address2, cityId, postalCode, phone, createDate, createdBy, lastUpdate, lastUpdateBy)
                             VALUES (@address, '', @cityId, @zipCode, @phone, NOW(), 'admin', NOW(), 'admin'); 
                             SELECT LAST_INSERT_ID();";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@address", address);
                        cmd.Parameters.AddWithValue("@cityId", cityId);
                        cmd.Parameters.AddWithValue("@zipCode", zipCode);
                        cmd.Parameters.AddWithValue("@phone", phone);

                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding address: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
            }
        }

        /// <summary>
        /// Retrieves a list of all countries from the database.
        /// </summary>
        /// <returns>
        /// A list of key-value pairs where the key is the country ID (int) and the value is the country name (string).
        /// </returns>
        public static List<KeyValuePair<int, string>> GetAllCountries()
        {
            List<KeyValuePair<int, string>> countries = new List<KeyValuePair<int, string>>();

            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "SELECT countryId, country FROM country";

                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        countries.Add(new KeyValuePair<int, string>(
                            reader.GetInt32("countryId"),
                            reader.GetString("country")
                        ));
                    }
                }
            }
            return countries;
        }

        /// <summary>
        /// Retrieves a list of cities associated with a specified country from the database.
        /// </summary>
        /// <param name="countryId">The unique identifier of the country.</param>
        /// <returns>
        /// A list of key-value pairs where the key is the city ID and the value is the city name.
        /// </returns>
        public static List<KeyValuePair<int, string>> GetCitiesByCountry(int countryId)
        {
            List<KeyValuePair<int, string>> cities = new List<KeyValuePair<int, string>>();

            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "SELECT cityId, city FROM city WHERE countryId = @countryId";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@countryId", countryId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cities.Add(new KeyValuePair<int, string>(
                                reader.GetInt32("cityId"),
                                reader.GetString("city")
                            ));
                        }
                    }
                }
            }
            return cities;
        }

        /// <summary>
        /// Adds a new appointment to the database.
        /// </summary>
        public static void AddAppointment(Appointment appointment)
        {
            using (var conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO appointment 
                    (customerId, userId, title, type, description, location, contact, url, start, end, createDate, createdBy, lastUpdate, lastUpdateBy) 
                    VALUES (@customerId, @userId, @title, @type, @description, @location, @contact, @url, @start, @end, @createDate, @createdBy, @lastUpdate, @lastUpdateBy)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@customerId", appointment.CustomerId);
                        cmd.Parameters.AddWithValue("@userId", appointment.UserId);
                        cmd.Parameters.AddWithValue("@title", appointment.Title);
                        cmd.Parameters.AddWithValue("@type", appointment.Type);
                        cmd.Parameters.AddWithValue("@description", appointment.Description);
                        cmd.Parameters.AddWithValue("@location", appointment.Location);
                        cmd.Parameters.AddWithValue("@contact", appointment.Contact);
                        cmd.Parameters.AddWithValue("@url", appointment.Url);
                        cmd.Parameters.AddWithValue("@start", appointment.Start);
                        cmd.Parameters.AddWithValue("@end", appointment.End);
                        cmd.Parameters.AddWithValue("@createDate", appointment.CreateDate);
                        cmd.Parameters.AddWithValue("@createdBy", appointment.CreatedBy);
                        cmd.Parameters.AddWithValue("@lastUpdate", appointment.LastUpdate);
                        cmd.Parameters.AddWithValue("@lastUpdateBy", appointment.LastUpdateBy);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding appointment: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Retrieves all appointments from the database.
        /// </summary>
        public static List<Appointment> GetAllAppointments()
        {
            List<Appointment> appointments = new List<Appointment>();
            using (var conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM appointment";
                    using (var cmd = new MySqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            appointments.Add(new Appointment(
                                reader.GetInt32("appointmentId"),
                                reader.GetInt32("customerId"),
                                reader.GetInt32("userId"),
                                reader.GetString("title"),
                                reader.GetString("type"),
                                reader.GetString("description"),
                                reader.GetString("location"),
                                reader.GetString("contact"),
                                reader.GetString("url"),
                                reader.GetDateTime("start").ToLocalTime(),
                                reader.GetDateTime("end").ToLocalTime(),
                                reader.GetDateTime("createDate"),
                                reader.GetString("createdBy"),
                                reader.GetDateTime("lastUpdate"),
                                reader.GetString("lastUpdateBy")
                            ));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error retrieving appointments: " + ex.Message);
                }
            }
            return appointments;
        }

        /// <summary>
        /// Retrieves an appointment by ID.
        /// </summary>
        public static Appointment GetAppointmentById(int id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM appointment WHERE appointmentId = @appointmentId";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@appointmentId", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Appointment(
                                reader.GetInt32("appointmentId"),
                                reader.GetInt32("customerId"),
                                reader.GetInt32("userId"),
                                reader.GetString("title"),
                                reader.GetString("type"),
                                reader.GetString("description"),
                                reader.GetString("location"),
                                reader.GetString("contact"),
                                reader.GetString("url"),
                                reader.GetDateTime("start"),
                                reader.GetDateTime("end"),
                                reader.GetDateTime("createDate"),
                                reader.GetString("createdBy"),
                                reader.GetDateTime("lastUpdate"),
                                reader.GetString("lastUpdateBy")
                            );
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Retrieves a list of appointments scheduled on a specific date.
        /// </summary>
        /// <param name="date">The date for which appointments should be retrieved.</param>
        /// <returns>A list of Appointment objects occurring on the specified date.</returns>
        public static List<Appointment> GetAppointmentsByDate(DateTime date)
        {
            List<Appointment> appointments = new List<Appointment>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM appointment WHERE DATE(start) = @SelectedDate";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SelectedDate", date.ToString("yyyy-MM-dd"));
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            appointments.Add(new Appointment(
                                reader.GetInt32("appointmentId"),
                                reader.GetInt32("customerId"),
                                reader.GetInt32("userId"),
                                reader.GetString("title"),
                                reader.GetString("type"),
                                reader.GetString("description"),
                                reader.GetString("location"),
                                reader.GetString("contact"),
                                reader.GetString("url"),
                                reader.GetDateTime("start").ToLocalTime(),
                                reader.GetDateTime("end").ToLocalTime(),
                                reader.GetDateTime("createDate"),
                                reader.GetString("createdBy"),
                                reader.GetDateTime("lastUpdate"),
                                reader.GetString("lastUpdateBy")
                            ));
                        }
                    }
                }
            }
            return appointments;
        }

        /// <summary>
        /// Retrieves a list of appointments scheduled for the current week.
        /// </summary>
        /// <returns>A list of Appointment objects occurring within the current week.</returns>
        public static List<Appointment> GetAppointmentsByWeek()
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM appointment WHERE WEEK(Start) = WEEK(NOW())";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    List<Appointment> appointments = new List<Appointment>();
                    while (reader.Read())
                    {
                        appointments.Add(new Appointment(reader));
                    }
                    return appointments;
                }
            }
        }

        /// <summary>
        /// Retrieves a list of appointments scheduled for the current month.
        /// </summary>
        /// <returns>A list of Appointment objects occurring within the current month.</returns>
        public static List<Appointment> GetAppointmentsByMonth()
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM appointment WHERE MONTH(Start) = MONTH(NOW())";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    List<Appointment> appointments = new List<Appointment>();
                    while (reader.Read())
                    {
                        appointments.Add(new Appointment(reader));
                    }
                    return appointments;
                }
            }
        }

        /// <summary>
        /// Retrieves a list of upcoming appointments for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user whose appointments should be retrieved.</param>
        /// <returns>A list of the next 5 upcoming Appointment objects for the user.</returns>
        public static List<Appointment> GetUpcomingAppointments(int userId)
        {
            List<Appointment> appointments = new List<Appointment>();

            using (var conn = GetConnection())
            {
                string query = "SELECT * FROM appointment WHERE userId = @userId AND start >= NOW() ORDER BY start LIMIT 5";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        appointments.Add(new Appointment(
                            reader.GetInt32("appointmentId"),
                            reader.GetInt32("customerId"),
                            reader.GetInt32("userId"),
                            reader.GetString("title"),
                            reader.GetString("type"),
                            reader.GetString("description"),
                            reader.GetString("location"),
                            reader.GetString("contact"),
                            reader.GetString("url"),
                            reader.GetDateTime("start"),
                            reader.GetDateTime("end"),
                            reader.GetDateTime("createDate"),
                            reader.GetString("createdBy"),
                            reader.GetDateTime("lastUpdate"),
                            reader.GetString("lastUpdateBy")
                        ));
                    }
                }
            }
            return appointments;
        }

        /// <summary>
        /// Updates an existing appointment in the database.
        /// </summary>
        /// <param name="appointment">The Appointment object containing updated details.</param>
        public static void UpdateAppointment(Appointment appointment)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = @"UPDATE appointment 
                         SET title = @Title, type = @Type, description = @Description, location = @Location, 
                             start = @Start, end = @End, customerId = @CustomerId, contact = @Contact 
                         WHERE appointmentId = @AppointmentId";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Title", appointment.Title);
                    cmd.Parameters.AddWithValue("@Type", appointment.Type);
                    cmd.Parameters.AddWithValue("@Description", appointment.Description);
                    cmd.Parameters.AddWithValue("@Location", appointment.Location);
                    cmd.Parameters.AddWithValue("@Start", appointment.Start);
                    cmd.Parameters.AddWithValue("@End", appointment.End);
                    cmd.Parameters.AddWithValue("@CustomerId", appointment.CustomerId);
                    cmd.Parameters.AddWithValue("@Contact", appointment.Contact);
                    cmd.Parameters.AddWithValue("@AppointmentId", appointment.AppointmentId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Deletes an appointment by ID.
        /// </summary>
        public static void DeleteAppointment(int appointmentId)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM appointment WHERE AppointmentId = @AppointmentId";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Checks if a given appointment time overlaps with any existing appointments for a specific customer.
        /// </summary>
        /// <param name="start">The start time of the new appointment.</param>
        /// <param name="end">The end time of the new appointment.</param>
        /// <param name="customerId">The ID of the customer for whom the appointment is being scheduled.</param>
        /// <param name="appointmentId">The ID of the appointment being checked (0 for new appointments).</param>
        /// <returns>True if the appointment overlaps with an existing one, otherwise false.</returns>
        public static bool AppointmentOverlaps(DateTime start, DateTime end, int customerId, int appointmentId)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = @"
            SELECT COUNT(*) FROM appointment 
            WHERE customerId = @CustomerId 
            AND appointmentId != @AppointmentId  
            AND (
                (start BETWEEN @Start AND @End)  
                OR 
                (end BETWEEN @Start AND @End)  
                OR 
                (@Start BETWEEN start AND end)  
                OR 
                (@End BETWEEN start AND end)  
                OR
                (@Start <= start AND @End >= end) 
            )";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);
                    cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);
                    cmd.Parameters.AddWithValue("@Start", start);
                    cmd.Parameters.AddWithValue("@End", end);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        /// <summary>
        /// Checks if a given appointment falls within standard business hours (9 AM - 5 PM EST, Monday-Friday).
        /// </summary>
        /// <param name="startUTC">The UTC start time of the appointment.</param>
        /// <param name="endUTC">The UTC end time of the appointment.</param>
        /// <returns>True if the appointment is within business hours, otherwise false.</returns>
        public static bool IsWithinBusinessHours(DateTime startUTC, DateTime endUTC)
        {
            TimeZoneInfo estZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

            DateTime startEST = TimeZoneInfo.ConvertTimeFromUtc(startUTC, estZone);
            DateTime endEST = TimeZoneInfo.ConvertTimeFromUtc(endUTC, estZone);

            // Reject Weekend Appointments
            if (startEST.DayOfWeek == DayOfWeek.Saturday || startEST.DayOfWeek == DayOfWeek.Sunday)
            {
                return false;
            }

            DateTime businessStart = startEST.Date.AddHours(9);  // 9:00 AM EST
            DateTime businessEnd = startEST.Date.AddHours(17).AddMinutes(0);  // 5:00 PM EST

            bool withinHours = (startEST >= businessStart && endEST <= businessEnd);
            return withinHours;
        }

    }
}