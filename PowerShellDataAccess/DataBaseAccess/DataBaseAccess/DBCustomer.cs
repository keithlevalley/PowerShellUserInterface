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
        public List<IDBEntity> CreateRecord()
        {
            var returnArray = new List<IDBEntity>();

            using (DBModel ctx = new DBModel())
            {
                ctx.Customers.Add(this);
                if (ctx.SaveChanges() == 1)
                    returnArray.Add(this);
                else throw new Exception("Unable to add Customer to the database");
            }

            return returnArray;
        }

        public List<IDBEntity> ReadRecord()
        {
            var returnArray = new List<IDBEntity>();
            IQueryable<DBCustomer> entities;

            using (DBModel ctx = new DBModel())
            {
                if (this.DBCustomerId != 0)
                {
                    entities = ctx.Customers.Where<DBCustomer>(e => e.DBCustomerId == this.DBCustomerId);
                }
                else if ((this.CustomerName != null) && (this.CustomerEmail != null))
                {
                    entities = ctx.Customers
                        .Where<DBCustomer>(e => (e.CustomerName == this.CustomerName) && e.CustomerEmail == this.CustomerEmail);
                }
                else if (this.CustomerName != null)
                {
                    entities = ctx.Customers
                        .Where<DBCustomer>(e => e.CustomerName == this.CustomerName);
                }
                else if (this.CustomerEmail != null)
                {
                    entities = ctx.Customers
                        .Where<DBCustomer>(e => e.CustomerEmail == this.CustomerEmail);
                }
                else throw new Exception("read requires a primary key, Customername, or Customeremail");

                if (entities.Count() == 0)
                    throw new Exception("unable to find any Customer records with the search parameters");


                foreach (var entity in entities)
                {
                    returnArray.Add(entity);
                }
            }

            return returnArray;
        }

        public List<IDBEntity> UpdateRecord(string updatedEntity)
        {
            var returnArray = new List<IDBEntity>();
            var newCustomer = DBEntitySerializer.DeserializeIEntity(updatedEntity) as DBCustomer;
            var entities = this.ReadRecord();

            if (updatedEntity != null)
            {
                using (DBModel ctx = new DBModel())
                {
                    foreach (DBCustomer entity in entities)
                    {
                        ctx.Customers.Where(e => e.DBCustomerId == entity.DBCustomerId);
                        if (newCustomer.CustomerName != null)
                            entity.CustomerName = newCustomer.CustomerName;
                        if (newCustomer.CustomerEmail != null)
                            entity.CustomerEmail = newCustomer.CustomerEmail;
                        ctx.Entry(entity).State = EntityState.Modified;
                        returnArray.Add(entity);
                    }

                    if (ctx.SaveChanges() == returnArray.Count)
                        return returnArray;
                    else throw new Exception("Error updating Customers to the database, request may have partially completed");
                }
            }
            else throw new Exception("Update entity is not appropriate entity Type");
        }

        public List<IDBEntity> DeleteRecord()
        {
            var returnArray = new List<IDBEntity>();
            var entities = this.ReadRecord();

            using (DBModel ctx = new DBModel())
            {
                foreach (DBCustomer entity in entities)
                {
                    ctx.Customers.Attach(entity);
                    ctx.Customers.Remove(entity);
                    returnArray.Add(entity);
                }

                if (ctx.SaveChanges() == returnArray.Count)
                    return returnArray;
                else throw new Exception("Error deleting Customers from the database, request may have partially completed");
            }
        }
    }
}