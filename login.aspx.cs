using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class login : System.Web.UI.Page
{
    /* Instanciate Data-Access Components */
    UserDB userDB = new UserDB();

    protected void Page_Load(object sender, EventArgs e)
    {
        // hide logout btn, when on login.aspx
        Master.FindControl("btnLogout").Visible = false;
        
        if (Page.IsPostBack)
        {
            string userid = txtUsername.Value;
            string password = txtPassword.Value;

                // if user exists
                if (userDB.HasUser(userid, password))
                {
                    // Create a session variable
                    Session["valid_user"] = userid;
                }

                // if not
                if (Session["valid_user"] == null)
                {
                    Response.Redirect("login.aspx");
                }
                else
                {
                    Response.Redirect("backend.aspx");
                }


        }
    }
    protected void cvTxtUsername_ServerValidate(object source, ServerValidateEventArgs e) 
    {
        string userid = txtUsername.Value;
        string password = txtPassword.Value;

        // if hasUser returns false, set custaomvalidator to false
        if (userDB.HasUser(userid, password) == false)
        {
            e.IsValid = false;
        }
        else {
            e.IsValid = true;
        }
    }
}