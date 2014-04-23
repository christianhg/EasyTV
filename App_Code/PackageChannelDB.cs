using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

public class PackageChannelDB
{
    public PackageChannelDB()
    {

    }

    public void InsertPackageChannel(int packageId, int channelId)
    {
        SqlParameter[] parameters = {
            new SqlParameter("fk_package_id", packageId),
            new SqlParameter("fk_channel_id", channelId),
        };

        UtilityDB.InsertProcedures("sp_InsertPackageChannel", parameters);
    }

    public List<Channel> GetPackageChannels(int id)
    {
        DataTable dt = UtilityDB.GetProcedures("sp_GetPackageChannels", new SqlParameter("fk_package_id", id));
        List<Channel> channels = new List<Channel>();

        foreach (DataRow row in dt.Rows)
        {
            channels.Add(new Channel((int)row["pk_id"], row["name"].ToString(), row["info"].ToString(), row["logo"].ToString()));
        }

        return channels;
    }

    public void DeletePackageChannel(int packageId, int channelId)
    {
        SqlParameter[] parameters = {
            new SqlParameter("fk_package_id", packageId),
            new SqlParameter("fk_channel_id", channelId),
        };

        UtilityDB.DeleteProcedures("sp_DeletePackageChannel", parameters);
    }
}