using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

public class PackageDB
{

    public PackageDB()
    {

    }

    // Insert a package in database
    public int InsertPackage(Package package)
    {
        SqlParameter[] parameters = {
            new SqlParameter("name", package.Name),
            new SqlParameter("info", package.Info),
            new SqlParameter("url", package.Url),
            new SqlParameter("pricePerMonth", package.PricePerMonth),
            new SqlParameter("startUpFee", package.StartUpFee),
            new SqlParameter("fk_tvProvider_id", package.TvProviderId),
        };

        return UtilityDB.InsertProceduresWithOutput("sp_InsertPackage", parameters, "pk_id");
    }

    // Return a specific package from database
    public Package GetPackage(int packageId)
    {
        DataTable dt = UtilityDB.GetProcedures("sp_GetPackage", new SqlParameter("pk_id", packageId));
        DataRow dr = dt.Rows[0];

        Package package = new Package(
            (int)dr["pk_id"],
            dr["name"].ToString(),
            dr["info"].ToString(),
            dr["url"].ToString(),
            (decimal)dr["pricePerMonth"],
            (decimal)dr["startUpFee"],
            (int)dr["fk_tvProvider_id"]
        );

        return package;
    }

    // Check if a package has internet
    public Boolean HasInternet(int packageId)
    {
        DataTable dt = UtilityDB.GetProcedures("sp_GetInternet", new SqlParameter("fk_package_id", packageId));

        if (dt != null && dt.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Check if a package has a specific channell
    public Boolean HasChannel(int packageId, int channelId)
    {
        SqlParameter[] parameters =
        {
            new SqlParameter("fk_package_id", packageId),
            new SqlParameter("fk_channel_id", channelId)
        };

        DataTable dt = UtilityDB.GetProcedures("sp_GetPackageChannel", parameters);

        if (dt != null && dt.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Check if a package has a specific streamingService
    public Boolean HasStreamingService(int packageId, int streamingServiceId)
    {
        SqlParameter[] parameters =
        {
            new SqlParameter("fk_package_id", packageId),
            new SqlParameter("fk_streamingService_id", streamingServiceId)
        };

        DataTable dt = UtilityDB.GetProcedures("sp_GetPackageStreamingService", parameters);

        if (dt != null && dt.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Return all packages in database
    public List<Package> GetPackages()
    {
        // Create empty list of packages
        List<Package> packages = new List<Package>();
        // Get packages from database
        DataTable dt = UtilityDB.GetProcedures("sp_GetPackages");

        // For each row in datatable
        foreach (DataRow row in dt.Rows)
        {
            // Create package
            Package package = new Package(
                (int)row["pk_id"],
                row["name"].ToString(),
                row["info"].ToString(),
                row["url"].ToString(),
                (decimal)row["pricePerMonth"],
                (decimal)row["startUpFee"],
                (int)row["fk_tvProvider_id"]
            );

            // Add to list
            packages.Add(package);
        }

        // Return list of packages
        return packages;
    }

    // Update a single package in database
    public void UpdatePackage(Package package)
    {
        SqlParameter[] parameters = {
            new SqlParameter("pk_id", package.Id),
            new SqlParameter("name", package.Name),
            new SqlParameter("info", package.Info),
            new SqlParameter("url", package.Url),
            new SqlParameter("pricePerMonth", package.PricePerMonth),
            new SqlParameter("startUpFee", package.StartUpFee),
            new SqlParameter("fk_tvProvider_id", package.TvProviderId),
        };

        UtilityDB.UpdateProcedures("sp_UpdatePackage", parameters);
    }

    // Delete a single package form database
    public void DeletePackage(int packageId)
    {
        SqlParameter[] parameters = {
            new SqlParameter("pk_id", packageId)
        };

        UtilityDB.DeleteProcedures("sp_DeletePackage", parameters);
    }
}