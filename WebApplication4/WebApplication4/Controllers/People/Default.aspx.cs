using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using WebApplication4.Models;

namespace WebApplication4.Controllers.People
{
    public partial class Default : System.Web.UI.Page
    {
		protected WebApplication4.Models.WebApplication4Context _db = new WebApplication4.Models.WebApplication4Context();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // Model binding method to get List of Person entries
        // USAGE: <asp:ListView SelectMethod="GetData">
        public IQueryable<WebApplication4.Models.Person> GetData()
        {
            return _db.People;
        }
    }
}

