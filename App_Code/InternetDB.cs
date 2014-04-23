using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
public class InternetDB
{
    public InternetDB()
    {

    }

    public void InsertInternet(Internet internet)
    {
        SqlParameter[] parameters = {
            new SqlParameter("download", internet.Download),
            new SqlParameter("upload", internet.Upload),
            new SqlParameter("fk_package_id", internet.PackageId)
        };

        UtilityDB.InsertProcedures("sp_InsertInternet", parameters);
    }

    public Internet GetInternet(int packageId)
    {
        Internet internet;
        DataTable dt = UtilityDB.GetProcedures("sp_GetInternet", new SqlParameter("fk_package_id", packageId));

        DataRow row = dt.Rows[0];

        internet = new Internet(
            (int)row["download"],
            (int)row["upload"],
            (int)row["fk_package_id"]
        );

        return internet;
    }

    public void UpdateInternet(Internet internet)
    {
        SqlParameter[] parameters = {
            new SqlParameter("download", internet.Download),
            new SqlParameter("upload", internet.Upload),
            new SqlParameter("fk_package_id", internet.PackageId),
        };

        UtilityDB.UpdateProcedures("sp_UpdateInternet", parameters);
    }

    public void DeleteInternet(int packageId)
    {
        SqlParameter[] parameters = {
            new SqlParameter("fk_package_id", packageId)
        };
        UtilityDB.DeleteProcedures("sp_DeleteInternet", parameters);
    }
}