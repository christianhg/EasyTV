using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class streamingservicedetails : System.Web.UI.Page
{
    // Instanciate data access component
    StreamingServiceDB StreamingServiceDB = new StreamingServiceDB();

    protected void Page_Load(object sender, EventArgs e)
    {
        btnUpdateStreamingService.Visible = false;

        if (!Page.IsPostBack)
        {
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
                    btnInsertStreamingService.Visible = false;
                    // Show the update button
                    btnUpdateStreamingService.Visible = true;
                    // Fill the form fields with data from database
                    GetFormDataFromDB(id);
                }
            }
        }
    }
    // get data from db from id
    private void GetFormDataFromDB(int id)
    {
        // Create streamingService from database
        StreamingService streamingService = StreamingServiceDB.GetStreamingService(id);
        // Fill the form field
        txtStreamingServiceName.Text = streamingService.Name;
    }
    protected void InsertStreamingService_Click(object sender, EventArgs e)
    {
        // If form fields validate
        if (Page.IsValid) 
        {
            // Create streamingService
            StreamingService streamingService = new StreamingService(
                txtStreamingServiceName.Text
            );

            // Insert streamingService
            StreamingServiceDB.InsertStreamingService(streamingService);

            // Reset form field
            txtStreamingServiceName.Text = "";

            // Show success message for insert
            pnlAlert.CssClass = "alert alert-success";
            pnlAlert.Visible = true;
            ltrAlert.Text = "<span class='glyphicon glyphicon-ok'></span> " + streamingService.Name + " er nu oprettet";
        }
    }

    protected void UpdateStreamingService_Click(object sender, EventArgs e)
    {
         // If form fields validate
        if (Page.IsValid)
        {
            /// Get streamingService id from query string
            int streamingServiceId = Convert.ToInt32(Request.QueryString["id"]);

            // Create streamingService
            StreamingService streamingService = new StreamingService(
                streamingServiceId,
                txtStreamingServiceName.Text
            );

            // Update streamingService
            StreamingServiceDB.UpdateStreamingService(streamingService);

            // Show success message for update
            pnlAlert.CssClass = "alert alert-success";
            pnlAlert.Visible = true;
            ltrAlert.Text = "<span class='glyphicon glyphicon-ok'></span> " + streamingService.Name + " er nu opdateret";
        }
    }

    // Validate the entered streamingService name
    protected void cvTxtStreamingServiceName_ServerValidate(object source, ServerValidateEventArgs e)
    {
        // Initially set updating to false
        Boolean updating = false;
     
        // If an id exists in querystring
        if (Request.QueryString["id"] != null)
        {
            // We are updating a streamingService
            updating = true;
        }
     
        try
        {
            // Initially pass validation
            e.IsValid = true;

            // For each streamingService in database
            foreach (StreamingService streamingService in StreamingServiceDB.GetStreamingServices())
            {
                // If we are updating a streamingService
                if(updating)
                {
                    int streamingServiceId;
                    // If variable is an int
                    if (int.TryParse(Request.QueryString["id"].ToString(), out streamingServiceId))
                    {
                        // If streamingService name already exists in database and the streamingService is not the one being updated
                        if (String.Compare(streamingService.Name, txtStreamingServiceName.Text, true) == 0 && streamingService.Id != streamingServiceId)
                        {
                            // Fail validation
                            e.IsValid = false;
                            // Break loop
                            break;
                        }
                    }      
                }
                // We are inserting new streamingService
                else
                {
                    // If tvProvider name already exists in database
                    if (String.Compare(streamingService.Name, txtStreamingServiceName.Text, true) == 0)
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


