using System;

namespace Scheduling_App
{
    /// <summary>
    /// Represents an appointment in the scheduling system.
    /// </summary>
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Contact { get; set; }
        public string Url { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdate { get; set; }
        public string LastUpdateBy { get; set; }

        /// <summary>
        /// Constructor to create a new Appointment instance.
        /// </summary>
        public Appointment(int appointmentId, int customerId, int userId, string title, string type, string description,
            string location, string contact, string url, DateTime start, DateTime end, DateTime createDate, string createdBy,
            DateTime lastUpdate, string lastUpdateBy)
        {
            AppointmentId = appointmentId;
            CustomerId = customerId;
            UserId = userId;
            Title = title;
            Type = type;
            Description = description;
            Location = location;
            Contact = contact;
            Url = url;
            Start = start;
            End = end;
            CreateDate = createDate;
            CreatedBy = createdBy;
            LastUpdate = lastUpdate;
            LastUpdateBy = lastUpdateBy;
        }

        /// <summary>
        /// Constructor that initializes an appointment from a MySQL DataReader.
        /// This allows easy conversion of database records into Appointment objects.
        /// </summary>
        /// <param name="reader">MySQL DataReader containing appointment data.</param>
        public Appointment(MySql.Data.MySqlClient.MySqlDataReader reader)
        {
            AppointmentId = reader.GetInt32("appointmentId");
            CustomerId = reader.GetInt32("customerId");
            UserId = reader.GetInt32("userId");
            Title = reader.GetString("title");
            Type = reader.GetString("type");
            Description = reader.GetString("description");
            Location = reader.GetString("location");
            Contact = reader.GetString("contact");
            Url = reader.GetString("url");
            Start = reader.GetDateTime("start");
            End = reader.GetDateTime("end");
            CreateDate = reader.GetDateTime("createDate");
            CreatedBy = reader.GetString("createdBy");
            LastUpdate = reader.GetDateTime("lastUpdate");
            LastUpdateBy = reader.GetString("lastUpdateBy");
        }
    }
}

