using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MessageBoard.Data
{
  public class MessageBoardContext : DbContext
  {
    public MessageBoardContext()
      : base("DefaultConnection")
    {
      /* don't try to send unloaded data over the line 
       * first it has to be explicitly obtained and serialized */
      this.Configuration.LazyLoadingEnabled = false;
      /*
         proxy generation can cause problems with serialization later */
      this.Configuration.ProxyCreationEnabled = false;

      /* necessary for code first migrations (to the latest version) */
      Database.SetInitializer(
        new MigrateDatabaseToLatestVersion<MessageBoardContext, MessageBoardMigrationsConfiguration>()
        );
    }

    public DbSet<Topic> Topics { get; set; }
    public DbSet<Reply> Replies { get; set; }
  }
}