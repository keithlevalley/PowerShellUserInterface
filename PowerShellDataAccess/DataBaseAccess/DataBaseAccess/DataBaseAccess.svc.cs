using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DBEntities;

namespace DataBaseAccess
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IDataBaseAccess
    {
        public string CreateRecord(string serializedObject)
        {
            var entity = DeSerializer.deSerializeObject(serializedObject);
            int returnCode;

            using (UserModel ctx = new UserModel())
            {
                ctx.Users.Add((User)entity);
                returnCode = ctx.SaveChanges();
            }

            if (returnCode == 1)
                return entity.serializeEntity();
            else return null;
        }

        public int DeleteRecord(string serializedObject)
        {
            throw new NotImplementedException();
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public string[] ReadObject(string[] serializedObject)
        {
            throw new NotImplementedException();
        }

        public string[] UpdateRecord(string serializedObject, string newSerializedObject)
        {
            throw new NotImplementedException();
        }
    }
}
