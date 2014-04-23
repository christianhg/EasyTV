using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

/// <summary>
/// Summary description for StreamingServiceDB
/// </summary>
public class StreamingServiceDB
{
	
    // streamingserviceDB constructor
    public StreamingServiceDB()
	{
        
	}

    // insert streamingservice method
    public void InsertStreamingService(StreamingService streamingService)
    {
        SqlParameter[] parameters = {
            new SqlParameter("name", streamingService.Name)
        };
        UtilityDB.InsertProcedures("sp_InsertStreamingService", parameters);
    }

    // read streamingservice method
    public StreamingService GetStreamingService(int streamingServiceId)
    {
        DataTable dt = UtilityDB.GetProcedures("sp_GetStreamingService", new SqlParameter("pk_id", streamingServiceId));
        DataRow dr = dt.Rows[0];

        StreamingService streamingService = new StreamingService(
            (int)dr["pk_id"],
            dr["name"].ToString()
        );

        return streamingService;
    }

    // read streamingservices method
    public List<StreamingService> GetStreamingServices()
    {
        DataTable dt = UtilityDB.GetProcedures("sp_GetStreamingServices");
        List<StreamingService> StreamingServices = new List<StreamingService>();

        foreach (DataRow row in dt.Rows)
        {
            StreamingService streamingService = new StreamingService(
                (int)row["pk_id"],
                row["name"].ToString()
            );
            StreamingServices.Add(streamingService);
        }

        return StreamingServices;
    }

    // update streamingservice method
    public void UpdateStreamingService(StreamingService streamingService)
    {
        SqlParameter[] parameters = {
            new SqlParameter("pk_id", (int)streamingService.Id),
            new SqlParameter("name", streamingService.Name)
        };

        UtilityDB.UpdateProcedures("sp_UpdateStreamingService", parameters);
    }

    // delete streamingservice method
    public void DeleteStreamingService(int streamingServiceId)
    {
        SqlParameter[] parameters = {
            new SqlParameter("pk_id", streamingServiceId)
        };
        UtilityDB.DeleteProcedures("sp_DeleteStreamingService", parameters);
    }
}