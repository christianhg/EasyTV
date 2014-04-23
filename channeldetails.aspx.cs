using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class channeldetails : System.Web.UI.Page
{
    // Instanciate data access component    
    ChannelDB channelDB = new ChannelDB();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // For each logo file in image dir
            foreach (string filename in Directory.GetFiles(Server.MapPath("assets/img/logos/channels")))
            {
                // Fill drop down list with logo filenames
                ddlChannelLogo.Items.Add(new ListItem(Path.GetFileName(filename), Path.GetFileName(filename)));
            }

            // If an id exists in querystring
            if (Request.QueryString["id"] != null)
            {
                int id;
                // If variable is an int
                if (int.TryParse(Request.QueryString["id"].ToString(), out id))
                {
                    /*
                     * Further development: Insert check if id exists in database
                     */

                    // Hide the insert button
                    btnInsertChannel.Visible = false;
                    // Show the update button
                    btnUpdateChannel.Visible = true;
                    // Fill the form fields with data from database
                    GetFormDataFromDB(id);
                }
            }
        }
    }

    // Fill the form fields with data from database
    private void GetFormDataFromDB(int id)
    {
        // Create channel from database
        Channel channel = channelDB.GetChannel(id);
        // Fill the form fields
        txtChannelName.Text = channel.Name;
        txtChannelInfo.Text = channel.Info;
        ddlChannelLogo.SelectedValue = channel.Logo;
    }

    // On btnInsertChannel click
    protected void btnInsertChannel_Click(object sender, EventArgs e)
    {
        // If form fields validate
        if (Page.IsValid) 
        {
            // Create channel
            Channel channel = new Channel(
                txtChannelName.Text, 
                txtChannelInfo.Text, 
                ddlChannelLogo.SelectedValue
            );

            // Insert channel
            channelDB.InsertChannel(channel);

            // Reset fields
            txtChannelName.Text = "";
            txtChannelInfo.Text = "";

            // Show success message
            pnlAlert.CssClass = "alert alert-success";
            pnlAlert.Visible = true;
            ltrAlert.Text = "<span class='glyphicon glyphicon-ok'></span> " + channel.Name + " er nu oprettet";       
        }
    }

    // On btnUpdateChannel click
    protected void btnUpdateChannel_Click(object sender, EventArgs e)
    {
        // If form fields validate
        if (Page.IsValid)
        {
            // Get channel id from query string
            int channelId = Convert.ToInt32(Request.QueryString["id"]);

            // Create channel
            Channel channel = new Channel(
                channelId, 
                txtChannelName.Text, 
                txtChannelInfo.Text, 
                ddlChannelLogo.SelectedValue
            );

            // Update channel
            channelDB.UpdateChannel(channel);

            // Show success message
            pnlAlert.CssClass = "alert alert-success";
            pnlAlert.Visible = true;
            ltrAlert.Text = "<span class='glyphicon glyphicon-ok'></span> " + channel.Name + " er nu opdateret";
        }
    }

    // Validate the entered channel name
    protected void cvTxtChannelName_ServerValidate(object source, ServerValidateEventArgs e)
    {
        // Initially set updating to false
        Boolean updating = false;

        // If an id exists in querystring
        if (Request.QueryString["id"] != null)
        {
            // We are updating a channel
            updating = true;
        }

        try
        {
            // Initially pass validation
            e.IsValid = true;
            // For each channel in database
            foreach (Channel channel in channelDB.GetChannels())
            {
                // If we are updating a channel
                if (updating)
                {
                    int channelId;
                    // If variable is an int
                    if (int.TryParse(Request.QueryString["id"].ToString(), out channelId))
                    {
                        // If channel name already exists in database and the channel is not the one being updated
                        if (String.Compare(channel.Name, txtChannelName.Text, true) == 0 && channel.Id != channelId)
                        {
                            // Fail validation
                            e.IsValid = false;
                            // Break loop
                            break;
                        }
                    }
                }
                // We are inserting new channel
                else
                {
                    // If channel name already exists in database
                    if (String.Compare(channel.Name, txtChannelName.Text, true) == 0)
                    {
                        // Fail validation
                        e.IsValid = false;
                        // Break loop
                        break;
                    }
                }
            }
        }
        catch
        {
            // Fail validation
            e.IsValid = false;
        }
    }
}