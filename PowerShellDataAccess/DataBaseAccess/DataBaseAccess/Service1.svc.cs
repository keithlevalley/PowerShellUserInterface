using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DataBaseAccess
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string[] DBRecord(string serializedObject, string operationType, string newSerializedObject = null)
        {
            IDBEntity entity = DBEntitySerializer.DeserializeIEntity(serializedObject);
            var returnString = new List<string>();
            var returnDBObject = new List<IDBEntity>();

            try
            {
                if (entity != null)
                {
                    switch (operationType)
                    {
                        case "Create":
                            returnDBObject = entity.CreateRecord();
                            break;
                        case "Read":
                            returnDBObject = entity.ReadRecord();
                            break;
                        case "Update":
                            if (newSerializedObject == null)
                                throw new Exception("Update requires newSerializedObject string");
                            returnDBObject = entity.UpdateRecord(newSerializedObject);
                            break;
                        case "Delete":
                            returnDBObject = entity.DeleteRecord();
                            break;
                        default:
                            throw new Exception("operation not accepted");
                    }
                    foreach (var DBObject in returnDBObject)
                    {
                        returnString.Add(DBEntitySerializer.SerializeIEntity(DBObject));
                    }
                    return returnString.ToArray();
                }
                else throw new Exception("serializedObject is not appropriate data type");
            }
            catch (Exception ex)
            {
                returnString.Add(ex.ToString());
                return returnString.ToArray();
            }
            
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
    }
}
