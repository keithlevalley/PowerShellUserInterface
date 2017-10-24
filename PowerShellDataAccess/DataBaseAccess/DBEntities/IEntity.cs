using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Security;

namespace DBEntities
{
    // When adding new IEntity objects they also must be added to the DBSET in the UserModel class

    public static class DeSerializer
    {
        public static IEntity deSerializeObject(string serializedObject)
        {
                var entity = JsonConvert.DeserializeObject<IEntity>(serializedObject);
                if (entity != null)
                    return entity;
                else throw new Exception();
        }
    }

    public interface IEntity
    {
        string serializeEntity();
        IEntity deserialize(string serializedObject);
        //string encryptEntity(string serializedObject); // TODO implement encryption of objects

        List<string> CreateRecord();
        List<string> ReadRecord();
        List<string> UpdateRecord(IEntity newEntity);
        List<string> DeleteRecord();
    }
}
