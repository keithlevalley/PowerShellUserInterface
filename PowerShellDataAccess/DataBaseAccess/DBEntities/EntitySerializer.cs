﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBEntities
{
    static public class EntitySerializer
    {
        public static IEntity DeserializeIEntity(string serializedObject)
        {
            // need to add any new IEntity objects to this list
            // TODO don't like the way this works...
            try
            {
                return JsonConvert.DeserializeObject<DBUser>(serializedObject);
            }
            catch (JsonException){}
            try
            {
                return JsonConvert.DeserializeObject<Customer>(serializedObject);
            }
            catch (JsonException) { }

            return null;
        }

        public static string SerializeIEntity(IEntity entity)
        {
            return JsonConvert.SerializeObject(entity);
        }
    }
}