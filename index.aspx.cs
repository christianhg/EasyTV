using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnStartSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid) 
        {
            string zip = Server.UrlEncode(txtStartSearch.Text);
            Response.Redirect("search.aspx?zip=" + zip + "&price=700");
        }     
    }
}