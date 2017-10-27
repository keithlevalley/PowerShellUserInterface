using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class User : IEntity
    {
        public int DBUserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public DateTime UserAddDTM { get; set; }

        public User()
        {
            this.DBUserId = 0;
            this.UserName = "Default Name";
            this.UserEmail = "Default Email";
            this.UserAddDTM = DateTime.Now;
        }

        public User(int _userId)
        {
            this.DBUserId = _userId;
            this.UserName = "Default Name";
            this.UserEmail = "Default Email";
            this.UserAddDTM = DateTime.Now;
        }

        public User(string _userName, string _userEmail, DateTime _userAddDTM)
        {
            this.DBUserId = 0;
            this.UserName = _userName;
            this.UserEmail = _userEmail;
            this.UserAddDTM = _userAddDTM;
        }

        public User(int _userId, string _userName, string _userEmail, DateTime _userAddDTM)
        {
            this.DBUserId = _userId;
            this.UserName = _userName;
            this.UserEmail = _userEmail;
            this.UserAddDTM = _userAddDTM;
        }
    }
}
