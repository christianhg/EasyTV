using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
public class ChannelDB
{
    public ChannelDB()
    {

    }

    // Insert a channel in database
    public void InsertChannel(Channel channel)
    {
        SqlParameter[] parameters = {
            new SqlParameter("name", channel.Name),
            new SqlParameter("info", channel.Info),
            new SqlParameter("logo", channel.Logo),
        };

        UtilityDB.InsertProcedures("sp_InsertChannel", parameters);
    }

    // Get a specific channel from database
    public Channel GetChannel(int channelId)
    {
        DataTable dt = UtilityDB.GetProcedures("sp_GetChannel", new SqlParameter("pk_id", channelId));
        DataRow dr = dt.Rows[0];

        Channel channel = new Channel(
            (int)dr["pk_id"],
            dr["name"].ToString(),
            dr["info"].ToString(),
            dr["logo"].ToString()
        );

        return channel;
    }

    // Get all channels from database
    public List<Channel> GetChannels()
    {
        DataTable dt = UtilityDB.GetProcedures("sp_GetChannels");
        List<Channel> Channels = new List<Channel>();

        foreach (DataRow row in dt.Rows)
        {
            Channel channel = new Channel(
                (int)row["pk_id"],
                row["name"].ToString(),
                row["info"].ToString(),
                row["logo"].ToString()
            );
            Channels.Add(channel);
        }

        return Channels;
    }

    // Update a specific channel in database
    public void UpdateChannel(Channel channel)
    {
        SqlParameter[] parameters = {
            new SqlParameter("pk_id", (int)channel.Id),
            new SqlParameter("name", channel.Name),
            new SqlParameter("info", channel.Info),
            new SqlParameter("logo", channel.Logo)
        };

        UtilityDB.UpdateProcedures("sp_UpdateChannel", parameters);
    }

    // Delete a single channel from database
    public void DeleteChannel(int channelId)
    {
        SqlParameter[] parameters = {
            new SqlParameter("pk_id", channelId)
        };

        UtilityDB.DeleteProcedures("sp_DeleteChannel", parameters);
    }


}