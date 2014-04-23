using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
public class ExclusionAreaDB
{
    public ExclusionAreaDB()
    {

    }

    public void InsertExclusionArea(ExclusionArea exclusionArea)
    {
        SqlParameter[] parameters = {
            new SqlParameter("lowZip", exclusionArea.LowZip),
            new SqlParameter("highZip", exclusionArea.LowZip),
            new SqlParameter("fk_tvProvider_id", exclusionArea.TvProviderId),
        };

        UtilityDB.InsertProcedures("sp_InsertExclusionArea", parameters);
    }

    public List<ExclusionArea> GetExclusionAreas(int tvProviderId)
    {
        DataTable dt = UtilityDB.GetProcedures("sp_GetExclusionAreas", new SqlParameter("fk_tvProvider_id", tvProviderId));
        List<ExclusionArea> exclusionAreas = new List<ExclusionArea>();

        foreach (DataRow row in dt.Rows)
        {
            exclusionAreas.Add(new ExclusionArea(
                (int)row["pk_id"],
                (int)row["lowZip"],
                (int)row["highZip"],
                (int)row["fk_tvProvider_id"]
            ));
        }

        return exclusionAreas;
    }

    public void UpdateExclusionArea(ExclusionArea exclusionArea)
    {
        SqlParameter[] parameters = {
            new SqlParameter("pk_id", (int)exclusionArea.Id),
            new SqlParameter("lowZip", exclusionArea.LowZip),
            new SqlParameter("highZip", exclusionArea.HighZip),
            new SqlParameter("fk_tvProvider_id", exclusionArea.TvProviderId),
        };

        UtilityDB.UpdateProcedures("sp_UpdateExclusionArea", parameters);
    }

    public void DeleteExclusionArea(int exclusionAreaId)
    {
        SqlParameter[] parameters = {
            new SqlParameter("pk_id", exclusionAreaId)
        };
        UtilityDB.DeleteProcedures("sp_DeleteExclusionArea", parameters);
    }

    public void DeleteExclusionAreas(int tvProviderId)
    {
        SqlParameter[] parameters = {
            new SqlParameter("fk_tvProvider_id", tvProviderId)
        };
        UtilityDB.DeleteProcedures("sp_DeleteExclusionAreas", parameters);
    }
}