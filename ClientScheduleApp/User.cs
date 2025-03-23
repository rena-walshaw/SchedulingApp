using System;

namespace Scheduling_App
{
    /// <summary>
    /// Represents a user in the scheduling application.
    /// </summary>
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Active { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdate { get; set; }

        /// <summary>
        /// Constructor to initialize a new User object with all properties.
        /// </summary>
        public User(int userId, string userName, string password, int active, DateTime createDate, string createdBy, DateTime lastUpdate, string lastUpdatedBy)
        {
            UserId = userId;
            UserName = userName;
            Password = password;
            Active = active;
            CreateDate = createDate;
            CreatedBy = createdBy;
            LastUpdate = lastUpdate;
            LastUpdatedBy = lastUpdatedBy;
        }
    }
}

