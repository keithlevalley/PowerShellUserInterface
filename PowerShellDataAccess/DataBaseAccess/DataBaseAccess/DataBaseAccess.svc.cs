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
        public string[] DBRecord(string serializedObject, string operationType, string newSerializedObject = null)
        {
            IDBEntity entity = DBEntitySerializer.DeserializeIEntity(serializedObject);

            if (entity != null)
            {
                switch (operationType)
                {
                    case "Create":
                        return entity.CreateRecord().ToArray();
                    case "Read":
                        return entity.ReadRecord().ToArray<string>();
                    case "Update":
                        if (newSerializedObject == null)
                            throw new Exception("Update requires newSerializedObject string");
                        return entity.UpdateRecord(newSerializedObject).ToArray();
                    case "Delete":
                        return entity.DeleteRecord().ToArray();
                    default:
                        throw new Exception("operation not accepted");
                }
            }
            else throw new Exception("serializedObject is not appropriate data type");
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        //public CompositeType GetDataUsingDataContract(CompositeType composite)
        //{
        //    if (composite == null)
        //    {
        //        throw new ArgumentNullException("composite");
        //    }
        //    if (composite.BoolValue)
        //    {
        //        composite.StringValue += "Suffix";
        //    }
        //    return composite;
        //}
    }
}
