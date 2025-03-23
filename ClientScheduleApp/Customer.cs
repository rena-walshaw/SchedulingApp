namespace Scheduling_App
{
    /// <summary>
    /// Represents a customer entity with ID, name, address, and phone number.
    /// </summary>
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Customer"/> class with specified values.
        /// </summary>
        public Customer(int customerId, string customerName, string address, string phone)
        {
            CustomerId = customerId;
            CustomerName = customerName;
            Address = address;
            Phone = phone;
        }
    }
}
