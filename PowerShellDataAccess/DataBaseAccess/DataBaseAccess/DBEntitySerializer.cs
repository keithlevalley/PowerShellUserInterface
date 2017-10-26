using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataBaseAccess
{
    public class DBEntitySerializer
    {
        public static IDBEntity DeserializeIEntity(string serializedObject)
        {
            // need to add any new IEntity objects to this list
            // TODO don't like the behavior of Newtonsoft.Json...

            // TODO fix this code
            string tempString = "";
            tempString = serializedObject.Substring(2 + serializedObject.IndexOf('"'));
            tempString = tempString.Substring(0, (tempString.IndexOf('"')));

            if (tempString.Contains("UserId"))
            {
                try
                {
                    return JsonConvert.DeserializeObject<DBUser>(serializedObject);
                }
                catch (JsonException) { }
            }
            else if (tempString.Contains("CustomerId"))
            {
                try
                {
                    return JsonConvert.DeserializeObject<DBCustomer>(serializedObject);
                }
                catch (JsonException) { }
            }
            
            return null;
        }

        public static string SerializeIEntity(IDBEntity entity)
        {
            return JsonConvert.SerializeObject(entity);
        }
    }
}