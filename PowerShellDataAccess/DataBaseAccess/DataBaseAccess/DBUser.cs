using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;
using System.Data.Entity;

namespace DataBaseAccess
{
    public class DBUser : User, IDBEntity
    {
        public List<string> CreateRecord()
        {
            var returnArray = new List<string>();

            using (DBModel ctx = new DBModel())
            {
                //modelBuilder.Entity<OfficeAssignment>().HasKey(t => t.InstructorID);
                ctx.Users.Add(this);
                if (ctx.SaveChanges() == 1)
                    returnArray.Add(DBEntitySerializer.SerializeIEntity(this));
                else throw new Exception("Unable to add User to the database");
            }

            return returnArray;
        }

        public List<string> ReadRecord()
        {
            var returnArray = new List<string>();
            IQueryable<DBUser> query;

            using (DBModel ctx = new DBModel())
            {
                query = ctx.Users.Where<DBUser>(e => e.DBUserId == this.DBUserId);

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
            DBUser newUser = DBEntitySerializer.DeserializeIEntity(updatedEntity) as DBUser;

            if (updatedEntity != null)
            {
                using (DBModel ctx = new DBModel())
                {
                    DBUser entity = ctx.Users.Where(e => e.DBUserId == this.DBUserId).FirstOrDefault();
                    if (newUser.UserName != null)
                        entity.UserName = newUser.UserName;
                    if (newUser.UserEmail != null)
                        entity.UserEmail = newUser.UserEmail;

                    ctx.Entry(entity).State = EntityState.Modified;
                    if (ctx.SaveChanges() == 1)
                        returnArray.Add(DBEntitySerializer.SerializeIEntity(entity));
                    else throw new Exception("Unable to update User to the database");
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
                ctx.Users.Remove(this);
                int temp = ctx.SaveChanges();
                returnArray.Add(temp + " record(s) removed");
            }

            return returnArray;
        }
    }
}