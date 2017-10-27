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
        public List<IDBEntity> CreateRecord()
        {
            var returnArray = new List<IDBEntity>();

            using (DBModel ctx = new DBModel())
            {
                ctx.Users.Add(this);
                if (ctx.SaveChanges() == 1)
                    returnArray.Add(this);
                else throw new Exception("Unable to add User to the database");
            }

            return returnArray;
        }

        public List<IDBEntity> ReadRecord()
        {
            var returnArray = new List<IDBEntity>();
            IQueryable<DBUser> entities;

            using (DBModel ctx = new DBModel())
            {
                if (this.DBUserId != 0)
                {
                    entities = ctx.Users.Where<DBUser>(e => e.DBUserId == this.DBUserId);
                }
                else if ((this.UserName != null) && (this.UserEmail != null))
                {
                    entities = ctx.Users
                        .Where<DBUser>(e => (e.UserName == this.UserName) && e.UserEmail == this.UserEmail);
                }
                else if (this.UserName != null)
                {
                    entities = ctx.Users
                        .Where<DBUser>(e => e.UserName == this.UserName);
                }
                else if (this.UserEmail != null)
                {
                    entities = ctx.Users
                        .Where<DBUser>(e => e.UserEmail == this.UserEmail);
                }
                else throw new Exception("read requires a primary key, username, or useremail");

                if (entities.Count() == 0)
                    throw new Exception("unable to find any user records with the search parameters");

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
            var newUser = DBEntitySerializer.DeserializeIEntity(updatedEntity) as DBUser;
            var entities = this.ReadRecord();

            if (updatedEntity != null)
            {
                using (DBModel ctx = new DBModel())
                {
                    foreach (DBUser entity in entities)
                    {
                        ctx.Users.Where(e => e.DBUserId == entity.DBUserId);
                        if (newUser.UserName != null)
                            entity.UserName = newUser.UserName;
                        if (newUser.UserEmail != null)
                            entity.UserEmail = newUser.UserEmail;
                        ctx.Entry(entity).State = EntityState.Modified;
                        returnArray.Add(entity);
                    }

                    if (ctx.SaveChanges() == returnArray.Count)
                        return returnArray;
                    else throw new Exception("Error updating users to the database, request may have partially completed");
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
                var query = ctx.Users.Where<DBUser>(e => e.DBUserId == this.DBUserId);

                foreach (DBUser entity in entities)
                {
                    ctx.Users.Remove(entity);
                    returnArray.Add(entity);
                }

                if (ctx.SaveChanges() == returnArray.Count)
                    return returnArray;
                else throw new Exception("Error deleting users from the database, request may have partially completed");
            }
        }
    }
}