using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Customer : IEntity
    {
        public int DBCustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime CustomerAddDTM { get; set; }

        public Customer()
        {
            this.DBCustomerId = 0;
            this.CustomerName = "Default Name";
            this.CustomerEmail = "Default Email";
            this.CustomerAddDTM = DateTime.Now;
        }

        public Customer(int _customerId)
        {
            this.DBCustomerId = _customerId;
            this.CustomerName = "Default Name";
            this.CustomerEmail = "Default Email";
            this.CustomerAddDTM = DateTime.Now;
        }

        public Customer(string _customerName, string _customerEmail, DateTime _customerAddDTM)
        {
            this.DBCustomerId = 0;
            this.CustomerName = _customerName;
            this.CustomerEmail = _customerEmail;
            this.CustomerAddDTM = _customerAddDTM;
        }
    }
}
