using System;

namespace Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTime UserAddDTM { get; set; }
        public string UserEmail { get; set; }

        public User(int _userId = 0, string _userName = null,
                    string _userEmail = null, DateTime _userAddDTM = new DateTime())
        {
            this.UserId = _userId;
            this.UserName = _userName;
            this.UserEmail = _userEmail;
            this.UserAddDTM = _userAddDTM;
        }
    }
}
