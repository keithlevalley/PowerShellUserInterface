using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Security;

namespace DBEntities
{
    public static class DeSerializer
    {
        public static IEntity deSerializeObject(string serializedObject)
        {
                var entity = JsonConvert.DeserializeObject<User>(serializedObject);
                if (entity != null)
                    return entity;
                else throw new Exception();
        }
    }

    public interface IEntity
    {
        string serializeEntity();
        string encryptEntity(string serializedObject);
    }

    public class User : IEntity
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public DateTime UserAddDTM { get; set; }

        public User(int _userId, string _userName, string _userEmail, DateTime _userAddDTM)
        {
            this.UserId = _userId;
            this.UserName = _userName;
            this.UserEmail = _userEmail;
            this.UserAddDTM = _userAddDTM;
        }

        public string encryptEntity(string serializedObject)
        {
            // TODO setup public and private key for encryption
            return serializedObject;
        }

        public string serializeEntity()
        {
            return encryptEntity(JsonConvert.SerializeObject(this));
        }
    }

    public class Customer : IEntity
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime CustomerAddDTM { get; set; }

        public string encryptEntity(string serializedObject)
        {
            // TODO setup public and private key for encryption
            return serializedObject;
        }

        public string serializeEntity()
        {
            return encryptEntity(JsonConvert.SerializeObject(this));
        }
    }
}
