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
            IEntity entity = EntitySerializer.DeserializeIEntity(serializedObject);

            if (entity is IEntity)
            {
                switch (operationType)
                {
                    case "Create":
                        return CreateRecord(entity).ToArray();
                    case "Read":
                        return ReadRecord(entity).ToArray<string>();
                    case "Update":
                        if (newSerializedObject == null)
                            throw new Exception("Update requires newSerializedObject string");
                        IEntity newEntity = EntitySerializer.DeserializeIEntity(newSerializedObject);
                        return UpdateRecord(entity, newEntity).ToArray();
                    case "Delete":
                        return DeleteRecord(entity).ToArray();
                    default:
                        throw new Exception("operation not accepted");
                }
            }
            else throw new Exception("serializedObject is not appropriate data type");
        }

        private List<string> UpdateRecord(IEntity entity, IEntity newSerializedObject)
        {
            var returnArray = new List<string>();

            using (UserModel ctx = new UserModel())
            {
                
            }
        }

        private List<string> ReadRecord(IEntity entity)
        {
            var returnArray = new List<string>();
            // TODO find a way to clean this up for adding of future IEntity types
            if (entity is DBUser)
            {
                using (UserModel ctx = new UserModel())
                {
                    var query = ctx.Users.Where<DBUser>(e => e.UserId == entity.UserId))
                }
            }
            else if (entity is Customer)
            {
                using (UserModel ctx = new UserModel())
                {
                    ctx.Customers.Add((Customer)entity);
                    if (ctx.SaveChanges() == 1)
                        returnArray.Add(entity.serializeEntity());
                    else returnArray.Add("Database unable to add record");
                }
            }
            else throw new Exception("Internal error when evaluating object inside Create method");

            return returnArray;
        }

        private List<string> CreateRecord(IEntity entity)
        {
            var returnArray = new List<string>();
                // TODO find a way to clean this up for adding of future IEntity types
            if (entity is DBUser)
            {
                using (UserModel ctx = new UserModel())
                {
                    ctx.Users.Add((DBUser)entity);
                    if (ctx.SaveChanges() == 1)
                        returnArray.Add(entity.serializeEntity());
                    else returnArray.Add("Database unable to add record");
                }
            }
            else if (entity is Customer)
            {
                using (UserModel ctx = new UserModel())
                {
                    ctx.Customers.Add((Customer)entity);
                    if (ctx.SaveChanges() == 1)
                        returnArray.Add(entity.serializeEntity());
                    else returnArray.Add("Database unable to add record");
                }
            }   
            else throw new Exception("Internal error when evaluating object inside Create method");

            return returnArray;
        }

        private List<string> DeleteRecord(IEntity entity)
        {
            var returnArray = new List<string>();
            // TODO find a way to clean this up for adding of future IEntity types
            if (entity is DBUser)
            {
                using (UserModel ctx = new UserModel())
                {
                    ctx.Users.Remove((DBUser)entity);
                    int temp = ctx.SaveChanges();
                    returnArray.Add(temp + " record(s) removed");
                }
            }
            else if (entity is Customer)
            {
                using (UserModel ctx = new UserModel())
                {
                    ctx.Customers.Remove((Customer)entity);
                    int temp = ctx.SaveChanges();
                    returnArray.Add(temp + " record(s) removed");
                }
            }
            else throw new Exception("Internal error when evaluating object inside Delete method");

            return returnArray;
        }

        private List<string> UpdateRecord(string serializedObject, string newSerializedObject)
        {
            throw new NotImplementedException();
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
