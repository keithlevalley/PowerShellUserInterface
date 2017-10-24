using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBEntities
{
    public class Customer : IEntity
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime CustomerAddDTM { get; set; }

        public List<string> CreateRecord(IEntity entity)
        {
            throw new NotImplementedException();
        }

        public List<string> DeleteRecord(IEntity entity)
        {
            throw new NotImplementedException();
        }

        public IEntity deserialize(string serializedObject)
        {
            return JsonConvert.DeserializeObject<Customer>(serializedObject);
        }

        public string encryptEntity(string serializedObject)
        {
            // TODO setup public and private key for encryption
            return serializedObject;
        }

        public List<string> ReadRecord(IEntity entity)
        {
            throw new NotImplementedException();
        }

        public string serializeEntity()
        {
            return encryptEntity(JsonConvert.SerializeObject(this));
        }

        public List<string> UpdateRecord(IEntity entity, IEntity newEntity)
        {
            throw new NotImplementedException();
        }
    }
}
