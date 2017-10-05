using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBEntities;

namespace DataBaseAccessTest
{
    class Program
    {
        static void Main(string[] args)
        {
            localhost.Service1 dataAccess = new localhost.Service1();

            User newUser = new User()
            {
                UserId = 1,
                UserName = "joe",
                UserEmail = "joe@email.com",
                UserAddDTM = DateTime.Now
            };

            string temp = dataAccess.CreateRecord(newUser.serializeEntity());

            User dataUser = DeSerializer.deSerializeObject(temp) as User;

        }
    }
}
