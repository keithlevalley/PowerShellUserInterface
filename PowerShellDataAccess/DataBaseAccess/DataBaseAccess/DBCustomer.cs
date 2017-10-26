using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Entities;

namespace DataBaseAccess
{
    public class DBCustomer : Customer, IDBEntity
    {
        public List<string> CreateRecord()
        {
            var returnArray = new List<string>();

            using (DBModel ctx = new DBModel())
            {
                ctx.Customers.Add(this);
                if (ctx.SaveChanges() == 1)
                    returnArray.Add(DBEntitySerializer.SerializeIEntity(this));
                else throw new Exception("Unable to add Customer to the database");
            }

            return returnArray;
        }

        public List<string> ReadRecord()
        {
            var returnArray = new List<string>();
            IQueryable<DBCustomer> query;

            using (DBModel ctx = new DBModel())
            {
                query = ctx.Customers.Where<DBCustomer>(e => e.DBCustomerId == this.DBCustomerId);

                foreach (var record in query)
                {
                    returnArray.Add(DBEntitySerializer.SerializeIEntity(record));
                }
            }

            return returnArray;
        }

        public List<string> UpdateRecord(string updatedEntity)
        {
            var returnArray = new List<string>();
            DBCustomer newCustomer = DBEntitySerializer.DeserializeIEntity(updatedEntity) as DBCustomer;

            if (updatedEntity != null)
            {
                using (DBModel ctx = new DBModel())
                {
                    DBCustomer entity = ctx.Customers.Where(e => e.DBCustomerId == this.DBCustomerId).FirstOrDefault();
                    if (newCustomer.CustomerName != null)
                        entity.CustomerName = newCustomer.CustomerName;
                    if (newCustomer.CustomerEmail != null)
                        entity.CustomerEmail = newCustomer.CustomerEmail;

                    ctx.Entry(entity).State = EntityState.Modified;
                    if (ctx.SaveChanges() == 1)
                        returnArray.Add(DBEntitySerializer.SerializeIEntity(entity));
                    else throw new Exception("Unable to update Customer to the database");
                }

                return returnArray;
            }
            else throw new Exception("Update entity is not appropriate entity Type");
        }

        public List<string> DeleteRecord()
        {
            var returnArray = new List<string>();

            using (DBModel ctx = new DBModel())
            {
                ctx.Customers.Remove(this);
                int temp = ctx.SaveChanges();
                returnArray.Add(temp + " record(s) removed");
            }

            return returnArray;
        }
    }
}