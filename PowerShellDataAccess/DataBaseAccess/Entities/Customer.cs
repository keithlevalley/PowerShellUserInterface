using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime CustomerAddDTM { get; set; }

        public Customer(int _customerId = 0, string _customerName = null,
                    string _customerEmail = null, DateTime _customerAddDTM = new DateTime())
        {
            this.CustomerId = _customerId;
            this.CustomerName = _customerName;
            this.CustomerEmail = _customerEmail;
            this.CustomerAddDTM = _customerAddDTM;
        }
    }
}
