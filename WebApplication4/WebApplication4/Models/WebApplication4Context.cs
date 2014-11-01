using System.Data.Entity;

namespace WebApplication4.Models
{
    public class WebApplication4Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<WebApplication4.Models.WebApplication4Context>());

        public WebApplication4Context() : base("name=WebApplication4Context")
        {
        }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Person> People { get; set; }
    }
}
