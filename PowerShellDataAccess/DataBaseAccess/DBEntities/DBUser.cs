using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Data.Entity;
using Entities;

namespace DBEntities
{
    public class DBUser : User, IDBEntity
    {
        public List<string> CreateRecord()
        {
            var returnArray = new List<string>();

            using (UserModel ctx = new UserModel())
            {
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

            using (UserModel ctx = new UserModel())
            {
                query = ctx.Users.Where<DBUser>(e => e.UserId == this.UserId);
            }

            foreach (var record in query)
            {
                returnArray.Add(DBEntitySerializer.SerializeIEntity(record));
            }

            return returnArray;
        }

        public List<string> UpdateRecord(string updatedEntity)
        {
            var returnArray = new List<string>();
            User newUser = DBEntitySerializer.DeserializeIEntity(updatedEntity) as DBUser;

            if (updatedEntity != null)
            {
                using (UserModel ctx = new UserModel())
                {
                    DBUser entity = ctx.Users.Where(e => e.UserId == this.UserId).FirstOrDefault();
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

            using (UserModel ctx = new UserModel())
            {
                ctx.Users.Remove(this);
                int temp = ctx.SaveChanges();
                returnArray.Add(temp + " record(s) removed");
            }

            return returnArray;
        }
    }
}
