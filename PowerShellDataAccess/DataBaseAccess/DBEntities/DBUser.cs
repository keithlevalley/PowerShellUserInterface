using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Data.Entity;
using Entities;

namespace DBEntities
{
    public class DBUser : User, IEntity
    {
        public string encryptEntity(string serializedObject)
        {
            // TODO setup public and private key for encryption
            return serializedObject;
        }

        public string serializeEntity()
        {
            return encryptEntity(JsonConvert.SerializeObject(this));
        }

        public List<string> CreateRecord()
        {
            var returnArray = new List<string>();

            using (UserModel ctx = new UserModel())
            {
                ctx.Users.Add(this);
                if (ctx.SaveChanges() == 1)
                    returnArray.Add(this.serializeEntity());
                else throw new Exception("Unable to add User to the database");
            }
            
            return returnArray;
        }

        public List<string> ReadRecord()
        {
            var returnArray = new List<string>();

            using (UserModel ctx = new UserModel())
                {
                    var query = ctx.Users.Where<DBUser>(e => e.UserId == this.UserId);
                }
            return returnArray;
        }

        public List<string> UpdateRecord(IEntity newEntity)
        {
            throw new NotImplementedException();
        }

        public List<string> DeleteRecord()
        {
            throw new NotImplementedException();
        }

        public IEntity deserialize(string serializedObject)
        {
            return JsonConvert.DeserializeObject<DBUser>(serializedObject);
        }
    }
}
