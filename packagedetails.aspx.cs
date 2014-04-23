using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class packagedetails : System.Web.UI.Page
{
    // Instanciate data access components
    TvProviderDB tvProviderDB = new TvProviderDB();
    PackageDB packageDB = new PackageDB();
    ChannelDB channelDB = new ChannelDB();
    InternetDB internetDB = new InternetDB();
    PackageChannelDB packageChannelDB = new PackageChannelDB();
    StreamingServiceDB streamingServiceDB = new StreamingServiceDB();
    PackageStreamingServiceDB packageStreamingServiceDB = new PackageStreamingServiceDB();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // For each tvProvider in database
            foreach (TvProvider tvProvider in tvProviderDB.GetTvProviders())
            {
                // Add tvProvider to drop down list
                ddlPackageTvProvider.Items.Add(new ListItem(tvProvider.Name, tvProvider.Id.ToString()));
            }

            // get channels and order by name
            List<Channel> channels = channelDB.GetChannels();
            // List channels by name
            IEnumerable<Channel> channelOrder = channels.OrderBy(channel => channel.Name);
            
            // For each channel in database
            foreach (Channel channel in channelOrder)
            {
                // Add channel to multi select box
                lbPackageChannels.Items.Add(new ListItem(channel.Name, channel.Id.ToString()));
            }

            // get streaming and order by name
            List<StreamingService> streamingServices = streamingServiceDB.GetStreamingServices();
            IEnumerable<StreamingService> streamingServiceOrder = streamingServices.OrderBy(streamingService => streamingService.Name); 
            
            // For each streamingService in database
            foreach (StreamingService streamingService in streamingServiceOrder)
            {
                // Add streamingSerbice to multi select box
                lbPackageStreamingServices.Items.Add(new ListItem(streamingService.Name, streamingService.Id.ToString()));
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
                    btnInsertPackage.Visible = false;
                    // Show the update button
                    btnUpdatePackage.Visible = true;
                    // Fill the form fields with data from database
                    GetFormDataFromId(id);
                }
            }
        }
    }

    // Fill the form fields with data from database
    protected void GetFormDataFromId(int id)
    {
        // Create package from database
        Package package = packageDB.GetPackage(id);

        // Fill the form fields
        ddlPackageTvProvider.SelectedValue = package.TvProviderId.ToString();
        txtPackageName.Text = package.Name;
        txtPackageInfo.Text = package.Info;
        txtPackageUrl.Text = package.Url;
        txtPackagePricePerMonth.Text = package.PricePerMonth.ToString();
        txtPackageStartUpFee.Text = package.StartUpFee.ToString();

        // If package has internet
        if (packageDB.HasInternet(id))
        {
            // Get internet from database
            Internet internet = internetDB.GetInternet(id);
            // Fill internet form fields with data from database
            txtPackageDownload.Text = internet.Download.ToString();
            txtPackageUpload.Text = internet.Upload.ToString();
        }
        else
        {
            // Uncheck internet checkbox
            cbInternet.Checked = false;
            // Hide panel with internet input fields
            pnlPackageInternet.Visible = false;
            // Turn off validation on said input fields
            rfvTxtPackageDownload.Enabled = false;
            rfvTxtPackageUpload.Enabled = false;
        }

        // For each channel in database
        foreach (Channel channel in packageChannelDB.GetPackageChannels(id))
        {
            // For each channel in multi select box
            foreach (ListItem liChannel in lbPackageChannels.Items)
            {
                // If a channel is in both the database and the multi select box
                if (channel.Id == Convert.ToInt32(liChannel.Value))
                {
                    // Mark channel as selected
                    liChannel.Selected = true;
                }
            }
        }

        // For each streamingService in database
        foreach (StreamingService streamingService in packageStreamingServiceDB.GetPackageStreamingServices(id))
        {
            // For each streamingService in multi select box
            foreach (ListItem liStreamingService in lbPackageStreamingServices.Items)
            {
                // If a streamingService is in both the database and the multi select box
                if (streamingService.Id == Convert.ToInt32(liStreamingService.Value))
                {
                    // Mark streamingService as selected
                    liStreamingService.Selected = true;
                }
            }
        }
    }

    // On internet checkbox change
    protected void cbInternet_CheckedChanged(Object sender, EventArgs e)
    {
        // If checkbox is checked
        if (cbInternet.Checked)
        {
            // Show panel with internet input fields
            pnlPackageInternet.Visible = true;
            // Enable validation for internet input fields
            rfvTxtPackageDownload.Enabled = true;
            rfvTxtPackageUpload.Enabled = true;
        }
        // If checkbox is not checked
        else
        {
            // Hide panel with internet input fields
            pnlPackageInternet.Visible = false;
            // Disable validation for internet input fields
            rfvTxtPackageDownload.Enabled = false;
            rfvTxtPackageUpload.Enabled = false;
        }
    }

    // On btnInsertPackage click
    protected void btnInsertPackage_Click(object sender, EventArgs e)
    {
        // If form fields validate
        if (Page.IsValid)
        {
            // Create package
            Package package = new Package(
                txtPackageName.Text,
                txtPackageInfo.Text,
                txtPackageUrl.Text,
                Convert.ToDecimal(txtPackagePricePerMonth.Text),
                Convert.ToDecimal(txtPackageStartUpFee.Text),
                Convert.ToInt32(ddlPackageTvProvider.SelectedValue)
            );

            // Insert package and return primary key
            int packageId = packageDB.InsertPackage(package);

            // If internet checkbox is checked
            if (cbInternet.Checked)
            {
                // Create internet
                Internet internet = new Internet(
                    Convert.ToInt32(txtPackageDownload.Text),
                    Convert.ToInt32(txtPackageUpload.Text),
                    packageId
                );

                // Insert internet
                internetDB.InsertInternet(internet);
            }

            // For each channel in multi select box
            foreach (ListItem liPackageChannel in lbPackageChannels.Items)
            {
                // If channel is selected
                if (liPackageChannel.Selected)
                {
                    // Insert package id and channel id in relationship table
                    packageChannelDB.InsertPackageChannel(
                        packageId,
                        Convert.ToInt32(liPackageChannel.Value)
                    );
                }
            }

            // For each streamingService in multi select box
            foreach (ListItem liPackageStreamingServices in lbPackageStreamingServices.Items)
            {
                // If streamingService is selected
                if (liPackageStreamingServices.Selected)
                {
                    // Insert package id and streamingService id in relationsship table
                    packageStreamingServiceDB.InsertPackageStreamingService(
                        packageId,
                        Convert.ToInt32(liPackageStreamingServices.Value)
                    );
                }
            }

            // Show success message
            pnlAlert.CssClass = "alert alert-success";
            pnlAlert.Visible = true;
            ltrAlert.Text = "<span class='glyphicon glyphicon-ok'></span> " + txtPackageName.Text + " er nu oprettet";
        }
    }

    // On btnUpdatePackage click
    protected void btnUpdatePackage_Click(object sender, EventArgs e)
    {
        // If form fields validate
        if (Page.IsValid)
        {
            // Get package id from query string
            int packageId = Convert.ToInt32(Request.QueryString["id"]);

            // Create package
            Package package = new Package(
                packageId,
                txtPackageName.Text,
                txtPackageInfo.Text,
                txtPackageUrl.Text,
                Convert.ToDecimal(txtPackagePricePerMonth.Text),
                Convert.ToDecimal(txtPackageStartUpFee.Text),
                Convert.ToInt32(ddlPackageTvProvider.SelectedValue)
            );

            // Update package
            packageDB.UpdatePackage(package);

            // If internet checkbox is checked
            if (cbInternet.Checked)
            {
                // If package already has internet
                if (packageDB.HasInternet(packageId))
                {
                    // Create internet
                    Internet internet = new Internet(
                        Convert.ToInt32(txtPackageDownload.Text),
                        Convert.ToInt32(txtPackageUpload.Text),
                        packageId
                    );

                    // Update internet
                    internetDB.UpdateInternet(internet);
                }
                // If package dosn't have internet
                else
                {
                    // Create internet
                    Internet internet = new Internet(
                        Convert.ToInt32(txtPackageDownload.Text),
                        Convert.ToInt32(txtPackageUpload.Text),
                        packageId
                    );

                    // Insert internet
                    internetDB.InsertInternet(internet);
                }
            }
            // If internet checkbox is not checked
            else
            {
                // If package already has internet
                if (packageDB.HasInternet(packageId))
                {
                    // Delete internet
                    internetDB.DeleteInternet(packageId);
                }
            }

            // For each channel in multi select box
            foreach (ListItem liChannel in lbPackageChannels.Items)
            {
                // If channel is not selected and package used to contain that channel
                if (!liChannel.Selected && packageDB.HasChannel(packageId, Convert.ToInt32(liChannel.Value)))
                {
                    // Delete channel from package
                    packageChannelDB.DeletePackageChannel(packageId, Convert.ToInt32(liChannel.Value));
                }
                // If channel is selected and package didn't already contain channel
                if (liChannel.Selected && !packageDB.HasChannel(packageId, Convert.ToInt32(liChannel.Value)))
                {
                    // Insert channel
                    packageChannelDB.InsertPackageChannel(packageId, Convert.ToInt32(liChannel.Value));
                }
            }

            // For each streamingService in multi select box
            foreach (ListItem liStreamingService in lbPackageStreamingServices.Items)
            {
                // If streamingService is not selected and package used to contain that streamingService
                if (!liStreamingService.Selected && packageDB.HasStreamingService(packageId, Convert.ToInt32(liStreamingService.Value)))
                {
                    // Delete streamingService from package
                    packageStreamingServiceDB.DeletePackageStreamingService(packageId, Convert.ToInt32(liStreamingService.Value));
                }
                // If streamingService is selected and package didn't already contain that streamingService
                if (liStreamingService.Selected && !packageDB.HasStreamingService(packageId, Convert.ToInt32(liStreamingService.Value)))
                {
                    // Insert streamingService
                    packageStreamingServiceDB.InsertPackageStreamingService(packageId, Convert.ToInt32(liStreamingService.Value));
                }
            }

            // Show success message
            pnlAlert.CssClass = "alert alert-success";
            pnlAlert.Visible = true;
            ltrAlert.Text = "<span class='glyphicon glyphicon-ok'></span> " + txtPackageName.Text + " er nu opdateret";
        }
    }

    // Validate the entered package name
    protected void cvTxtPackageName_ServerValidate(object source, ServerValidateEventArgs e)
    {
        // Initially set updating to false
        Boolean updating = false;

        // If an id exists in querystring
        if (Request.QueryString["id"] != null)
        {
            // We are updating a tvProvider
            updating = true;
        }

        try
        {
            // Initially pass validation
            e.IsValid = true;
            // For each package in database
            foreach (Package package in packageDB.GetPackages())
            {
                // If we are updating a package
                if (updating)
                {
                    int packageId;
                    // If variable is an int
                    if (int.TryParse(Request.QueryString["id"].ToString(), out packageId))
                    {
                        // If package name already exists in database and the package is not the one being updated
                        if (String.Compare(package.Name, txtPackageName.Text, true) == 0 && package.Id != packageId)
                        {
                            // If the two packages belong to the same tvProvider
                            if (package.TvProviderId == Convert.ToInt32(ddlPackageTvProvider.SelectedValue))
                            {
                                // Fail validation
                                e.IsValid = false;
                                // Break loop
                                break;
                            }
                        }
                    }
                }
                // We are inserting new package
                else
                {
                    // If package name already exists in database
                    if (String.Compare(package.Name, txtPackageName.Text, true) == 0)
                    {
                        // If the two packages belong to the same tvProvider
                        if (package.TvProviderId == Convert.ToInt32(ddlPackageTvProvider.SelectedValue))
                        {
                            // Fail validation
                            e.IsValid = false;
                            // Break loop
                            break;
                        }
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