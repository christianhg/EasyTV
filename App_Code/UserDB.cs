using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

/// <summary>
/// Summary description for UserDB
/// </summary>
public class UserDB
{
	public UserDB()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    // Check if a user exists
    public Boolean HasUser(string username, string password)
    {
        SqlParameter[] parameters =
        {
            new SqlParameter("username", username),
            new SqlParameter("password", password)
        };

        DataTable dt = UtilityDB.GetProcedures("sp_GetUser", parameters);

        if (dt != null && dt.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}