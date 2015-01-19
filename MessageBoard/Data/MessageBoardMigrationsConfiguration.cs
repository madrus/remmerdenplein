using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;

namespace MessageBoard.Data
{
  public class MessageBoardMigrationsConfiguration
    : DbMigrationsConfiguration<MessageBoardContext>
  {
    public MessageBoardMigrationsConfiguration()
    {
      /* allow deletion of fields from a database table */
      this.AutomaticMigrationDataLossAllowed = true;
      this.AutomaticMigrationsEnabled = true;
    }

    /// <summary>
    /// This method is called just once during the start of your web application.
    /// It allows to insert = seed data in the database
    /// Extra code is used only in DEBUG mode so that it is easy to devolop the database
    /// </summary>
    /// <param name="context"></param>
    protected override void Seed(MessageBoardContext context)
    {
      base.Seed(context);

#if DEBUG
      if (!context.Topics.Any())
      {
        var topic = new Topic()
        {
          Title = "I love MVC",
          Body = "I love ASP.NET MVC and I want everyone to know about it!",
          Created = DateTime.Now,
          Replies = new List<Reply>()
          {
            new Reply()
            {
              Body = "I love it too!",
              Created = DateTime.Now
            },
            new Reply()
            {
              Body = "Me too",
              Created = DateTime.Now
            },
            new Reply()
            {
              Body = "Aw shucks",
              Created = DateTime.Now
            },
          }
        };

        context.Topics.Add(topic);

        var anotherTopic = new Topic()
        {
          Title = "I like Ruby too!",
          Body = "Ruby on Rails is popular",
          Created = DateTime.Now
        };

        context.Topics.Add(anotherTopic);

        try
        {
          context.SaveChanges();
        }
        catch (Exception ex)
        {

          var msg = ex.Message;
        }
      }
#endif
    }
  }
}

