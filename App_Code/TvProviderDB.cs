using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

public class TvProviderDB
{
    public TvProviderDB()
    {

    }

    // Insert a tvProvider in databasé
    public int InsertTvProvider(TvProvider tvProvider)
    {
        SqlParameter[] parameters = {
            new SqlParameter("name", tvProvider.Name),
            new SqlParameter("info", tvProvider.Info),
            new SqlParameter("logo", tvProvider.Logo),
            new SqlParameter("address", tvProvider.Address),
            new SqlParameter("phone", tvProvider.Phone),
            new SqlParameter("url", tvProvider.Url),
        };

        return UtilityDB.InsertProceduresWithOutput("sp_InsertTvProvider", parameters, "pk_id");
    }

    // Get a specific tvProvider from database
    public TvProvider GetTvProvider(int tvProviderId)
    {
        DataTable dt = UtilityDB.GetProcedures("sp_GetTvProvider", new SqlParameter("pk_id", tvProviderId));
        DataRow dr = dt.Rows[0];

        TvProvider tvProvider = new TvProvider(
            (int)dr["pk_id"],
            dr["name"].ToString(),
            dr["info"].ToString(),
            dr["logo"].ToString(),
            dr["address"].ToString(),
            Convert.ToInt32(dr["phone"]),
            dr["url"].ToString()
        );

        return tvProvider;
    }

    // Get all tvProviders from database
    public List<TvProvider> GetTvProviders()
    {
        DataTable dt = UtilityDB.GetProcedures("sp_GetTvProviders");
        List<TvProvider> providers = new List<TvProvider>();

        foreach (DataRow row in dt.Rows)
        {
            TvProvider tvProvider = new TvProvider(
                (int)row["pk_id"],
                row["name"].ToString(),
                row["info"].ToString(),
                row["logo"].ToString(),
                row["address"].ToString(),
                 Convert.ToInt32(row["phone"]),
                row["url"].ToString()
            );
            providers.Add(tvProvider);
        }

        return providers;
    }

    // Update a specific tvProvider in database
    public void UpdateTvProvider(TvProvider tvProvider)
    {
        SqlParameter[] parameters = {
            new SqlParameter("pk_id", (int)tvProvider.Id),
            new SqlParameter("name", tvProvider.Name),
            new SqlParameter("info", tvProvider.Info),
            new SqlParameter("logo", tvProvider.Logo),
            new SqlParameter("address", tvProvider.Address),
            new SqlParameter("phone", tvProvider.Phone),
            new SqlParameter("url", tvProvider.Url),
        };

        UtilityDB.UpdateProcedures("sp_UpdateTvProvider", parameters);
    }

    // Delete a single tvProvider from database
    public void DeleteTvProvider(int tvProviderId)
    {
        SqlParameter[] parameters = {
            new SqlParameter("pk_id", tvProviderId)
        };

        UtilityDB.DeleteProcedures("sp_DeleteTvProvider", parameters);
    }

    // Get tvProviders based on a zip code
    public List<TvProvider> SearchTvProviders(int zip)
    {
        List<TvProvider> tvProviders = new List<TvProvider>();

        DataTable dt = UtilityDB.GetProcedures("sp_ZipSearch", new SqlParameter("zip", zip));

        foreach (DataRow row in dt.Rows)
        {
            TvProvider tvProvider = new TvProvider(
                (int)row["pk_id"],
                row["name"].ToString(),
                row["info"].ToString(),
                row["logo"].ToString(),
                row["address"].ToString(),
                Convert.ToInt32(row["phone"]),
                row["url"].ToString()
            );

            tvProviders.Add(tvProvider);
        }

        return tvProviders;
    }
}