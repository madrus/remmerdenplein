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
    public partial class Edit : System.Web.UI.Page
    {
		protected WebApplication4.Models.WebApplication4Context _db = new WebApplication4.Models.WebApplication4Context();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // This is the Update methd to update the selected Person item
        // USAGE: <asp:FormView UpdateMethod="UpdateItem">
        public void UpdateItem(int  ID)
        {
            using (_db)
            {
                var item = _db.People.Find(ID);

                if (item == null)
                {
                    // The item wasn't found
                    ModelState.AddModelError("", String.Format("Item with id {0} was not found", ID));
                    return;
                }

                TryUpdateModel(item);

                if (ModelState.IsValid)
                {
                    // Save changes here
                    _db.SaveChanges();
                    Response.Redirect("../Default");
                }
            }
        }

        // This is the Select method to selects a single Person item with the id
        // USAGE: <asp:FormView SelectMethod="GetItem">
        public WebApplication4.Models.Person GetItem([FriendlyUrlSegmentsAttribute(0)]int? ID)
        {
            if (ID == null)
            {
                return null;
            }

            using (_db)
            {
                return _db.People.Find(ID);
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
