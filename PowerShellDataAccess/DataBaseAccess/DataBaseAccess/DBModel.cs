namespace DataBaseAccess
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DBModel : DbContext
    {
        // Your context has been configured to use a 'DBModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'DataBaseAccess.DBModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DBModel' 
        // connection string in the application configuration file.
        public DBModel()
            : base("name=DBModel")
        {
            //DbModelBuilder modelBuilder = new DbModelBuilder();
            //modelBuilder.Entity<DBUser>().HasKey(t => t.UserId);
            //modelBuilder.Entity<DBCustomer>().HasKey(t => t.CustomerId);
        }
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.
        

        public virtual DbSet<DBCustomer> Customers { get; set; }
        public virtual DbSet<DBUser> Users { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}