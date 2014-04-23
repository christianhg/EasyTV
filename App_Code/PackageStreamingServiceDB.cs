using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

public class PackageStreamingServiceDB
{
    // PackageStreamingServiceD constructor
    public PackageStreamingServiceDB()
    {

    }

    // insert
    public void InsertPackageStreamingService(int packageId, int streamingServiceId)
    {
        SqlParameter[] parameters = {
            new SqlParameter("fk_package_id", packageId),
            new SqlParameter("fk_streamingService_id", streamingServiceId),
        };

        UtilityDB.InsertProcedures("sp_InsertPackageStreamingService", parameters);
    }

    public List<StreamingService> GetPackageStreamingServices(int packageId)
    {
        DataTable dt = UtilityDB.GetProcedures("sp_GetPackageStreamingServices", new SqlParameter("fk_package_id", packageId));
        List<StreamingService> streamingServices = new List<StreamingService>();

        foreach (DataRow row in dt.Rows)
        {
            streamingServices.Add(new StreamingService((int)row["pk_id"], row["name"].ToString()));
        }

        return streamingServices;
    }

    // delete streamingservice method
    public void DeletePackageStreamingService(int packageId, int streamingServiceId)
    {
        SqlParameter[] parameters = {
            new SqlParameter("fk_package_id", packageId),
            new SqlParameter("fk_streamingService_id", streamingServiceId),
        };

        UtilityDB.DeleteProcedures("sp_DeletePackageStreamingService", parameters);
    }
}