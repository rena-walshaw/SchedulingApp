namespace Scheduling_App
{
    /// <summary>
    /// Represents a contact entity with an ID and a name.
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// Gets or sets the unique identifier for the contact.
        /// </summary>
        public int ContactId { get; set; }

        /// <summary>
        /// Gets or sets the name of the contact.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Contact"/> class with specified values.
        /// </summary>
        /// <param name="contactId">The unique identifier for the contact.</param>
        /// <param name="name">The name of the contact.</param>
        public Contact(int contactId, string name)
        {
            ContactId = contactId;
            Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Contact"/> class with default values.
        /// </summary>
        public Contact() { }
    }
}
