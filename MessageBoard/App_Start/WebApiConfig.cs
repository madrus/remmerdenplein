using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace MessageBoard
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
      jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

      config.Routes.MapHttpRoute(
          name: "RepliesRoutetApi",
          routeTemplate: "api/v1/topics/{topicid}/replies/{id}", /* we introduce topicid to avoid duplicate ids */
          defaults: new { controller = "replies", id = RouteParameter.Optional }
      );

      config.Routes.MapHttpRoute(
          name: "DefaultApi",
        //routeTemplate: "api/v1/{controller}/{id}",
          routeTemplate: "api/v1/topics/{id}", /* we make topics here because we are not going to use any other controllers */
        //defaults: new { id = RouteParameter.Optional }
          defaults: new { controller = "topics", id = RouteParameter.Optional }
      );

      // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
      // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
      // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
      //config.EnableQuerySupport();
    }
  }
}