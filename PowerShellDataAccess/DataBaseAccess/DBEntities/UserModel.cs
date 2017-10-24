using DBEntities;
using System;
using System.Data.Entity;
using System.Linq;

namespace DBEntities
{
    public class UserModel : DbContext
    {
        // Your context has been configured to use a 'UserModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'DataBaseAccess.UserModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'UserModel' 
        // connection string in the application configuration file.
        public UserModel()
            : base("name=UserModel")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public DbSet<DBUser> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }

    //public class MyEntity{}
}
