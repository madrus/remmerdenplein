using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using Microsoft.AspNet.FriendlyUrls.ModelBinding;
using WebApplication4.Models;

namespace WebApplication4.Controllers.People
{
    public partial class Details : System.Web.UI.Page
    {
		protected WebApplication4.Models.WebApplication4Context _db = new WebApplication4.Models.WebApplication4Context();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // This is the Select methd to selects a single Person item with the id
        // USAGE: <asp:FormView SelectMethod="GetItem">
        public WebApplication4.Models.Person GetItem([FriendlyUrlSegmentsAttribute(0)]int? ID)
        {
            if (ID == null)
            {
                return null;
            }

            using (_db)
            {
	            return _db.People.Where(m => m.ID == ID).FirstOrDefault();
            }
        }

        protected void ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Cancel", StringComparison.OrdinalIgnoreCase))
            {
                Response.Redirect("../Default");
            }
        }
    }
}

