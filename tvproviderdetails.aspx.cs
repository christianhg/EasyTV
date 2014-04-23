using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class tvproviderdetails : System.Web.UI.Page
{
    // Instanciate data access components
    TvProviderDB tvProviderDB = new TvProviderDB();
    ExclusionAreaDB exclusionAreaDB = new ExclusionAreaDB();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // For each logo file in image dir
            foreach (string filename in Directory.GetFiles(Server.MapPath("assets/img/logos/tvproviders")))
            {
                // Fill drop down list with logo filenames
                ddlTvProviderLogo.Items.Add(new ListItem(Path.GetFileName(filename), Path.GetFileName(filename)));
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
                    btnInsertTvProvider.Visible = false;
                    // Show the update button
                    btnUpdateTvProvider.Visible = true;
                    // Fill the form fields with data from database
                    GetFormDataFromDB(id);
                }
            }
            else
            {
                // Create an empty list of exclusionAreas
                List<ExclusionArea> exclusionAreas = new List<ExclusionArea>();
                // Save it in a ViewState variable
                ViewState["ExclusionAreas"] = exclusionAreas;
            }
        }
    }

    // Fill the form fields with data from database
    private void GetFormDataFromDB(int id)
    {
        TvProvider tvProvider = tvProviderDB.GetTvProvider(id);
        txtTvProviderName.Text = tvProvider.Name;
        txtTvProviderInfo.Text = tvProvider.Info;
        ddlTvProviderLogo.SelectedValue = tvProvider.Logo;
        txtTvProviderAddress.Text = tvProvider.Address;
        txtTvProviderPhone.Text = tvProvider.Phone.ToString();
        txtTvProviderUrl.Text = tvProvider.Url;

        // Create a list of exclusonAreas from database
        List<ExclusionArea> exclusionAreas = exclusionAreaDB.GetExclusionAreas(id);
        // Save it in a ViewState variable
        ViewState["ExclusionAreas"] = exclusionAreas;
        // Build table of exclusionAreas
        BuildExclusionAreaTable(exclusionAreas);
    }

    // On btnInsertTvProvider click
    protected void btnInsertTvProvider_Click(object sender, EventArgs e)
    {
        // If form fields validate
        if (Page.IsValid)
        {
            // Create tvProvider
            TvProvider tvProvider = new TvProvider(
                txtTvProviderName.Text,
                txtTvProviderInfo.Text,
                ddlTvProviderLogo.SelectedValue,
                txtTvProviderAddress.Text,
                Convert.ToInt32(txtTvProviderPhone.Text),
                txtTvProviderUrl.Text
            );

            // Insert tvProvider
            int id = tvProviderDB.InsertTvProvider(tvProvider);

            // Create an empty list of exclusionAreas
            List<ExclusionArea> exclusionAreas = new List<ExclusionArea>();
            // Fill it with data from ViewState variable
            exclusionAreas = (List<ExclusionArea>)ViewState["ExclusionAreas"];
            // For each exclusionArea in list
            foreach (ExclusionArea exclusionArea in exclusionAreas)
            {
                // Insert exclusionArea in database
                exclusionAreaDB.InsertExclusionArea(new ExclusionArea(exclusionArea.LowZip, exclusionArea.HighZip, id));
            }

            // Reset form fields
            txtTvProviderName.Text = "";
            txtTvProviderInfo.Text = "";
            txtTvProviderAddress.Text = "";
            txtTvProviderPhone.Text = "";
            txtTvProviderUrl.Text = "";

            // Reset exclusionAreas ViewState variable
            ViewState["ExclusionAreas"] = new List<ExclusionArea>();

            // Show success message
            pnlAlert.CssClass = "alert alert-success";
            pnlAlert.Visible = true;
            ltrAlert.Text = "<span class='glyphicon glyphicon-ok'></span> " + tvProvider.Name + " er nu oprettet";
        }
    }

    protected void btnUpdateTvProvider_Click(object sender, EventArgs e)
    {
        // If form fields validate
        if (Page.IsValid)
        {
            // Get id from query string
            int tvProviderId = Convert.ToInt32(Request.QueryString["id"]);

            // Create tvProvider
            TvProvider tvProvider = new TvProvider(
                tvProviderId,
                txtTvProviderName.Text,
                txtTvProviderInfo.Text,
                ddlTvProviderLogo.SelectedValue,
                txtTvProviderAddress.Text,
                 Convert.ToInt32(txtTvProviderPhone.Text),
                txtTvProviderUrl.Text
            );

            // Update tvprovider
            tvProviderDB.UpdateTvProvider(tvProvider);

            // Delete all existing exclusionAreas
            exclusionAreaDB.DeleteExclusionAreas(tvProviderId);

            // Create an empty list of exclusionAreas
            List<ExclusionArea> exclusionAreas = new List<ExclusionArea>();
            // Fill it with data from ViewState variable
            exclusionAreas = (List<ExclusionArea>)ViewState["ExclusionAreas"];
            // For each exclusionArea in list
            foreach (ExclusionArea exclusionArea in exclusionAreas)
            {
                // Insert exclusionArea in database
                exclusionAreaDB.InsertExclusionArea(exclusionArea);
            }

            // Build table of exclusionAreas
            BuildExclusionAreaTable(exclusionAreas);

            // Show success message
            pnlAlert.CssClass = "alert alert-success";
            pnlAlert.Visible = true;
            ltrAlert.Text = "<span class='glyphicon glyphicon-ok'></span> " + tvProvider.Name + " er nu opdateret";
        }
    }

    protected void cvAddExclusionArea_ServerValidate(object source, ServerValidateEventArgs e)
    {
        try
        {
            // Initially pass validation
            e.IsValid = true;

            // Create an empty list of exclusionAreas
            List<ExclusionArea> exclusionAreas = new List<ExclusionArea>();
            // Fill it with data from ViewState variable
            exclusionAreas = (List<ExclusionArea>)ViewState["ExclusionAreas"];

            // For each exclusionArea in list
            foreach (ExclusionArea exclusionArea in exclusionAreas)
            {
                // If tvProvider name already exists in database
                if (exclusionArea.LowZip.ToString() == txtLowZip.Text && exclusionArea.HighZip.ToString() == txtHighZip.Text)
                {
                    // Fail validation
                    e.IsValid = false;
                    // Break loop
                    break;
                }
            }
        }
        catch
        {
            // Fail validation
            e.IsValid = false;
        }
    }

    // Validate the entered tvProvider name
    protected void cvTxtTvProviderName_ServerValidate(object source, ServerValidateEventArgs e)
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
            // For each tvProvider in database
            foreach (TvProvider tvProvider in tvProviderDB.GetTvProviders())
            {
                // If we are updating a tvProvider
                if (updating)
                {
                    int tvProviderId;
                    // If variable is an int
                    if (int.TryParse(Request.QueryString["id"].ToString(), out tvProviderId))
                    {
                        // If tvProvider name already exists in database and the tvProvider is not the one being updated
                        if (String.Compare(tvProvider.Name, txtTvProviderName.Text, true) == 0 && tvProvider.Id != tvProviderId)
                        {
                            // Fail validation
                            e.IsValid = false;
                            // Break loop
                            break;
                        }
                    }
                }
                // We are inserting new tvProvider
                else
                {
                    // If tvProvider name already exists in database
                    if (String.Compare(tvProvider.Name, txtTvProviderName.Text, true) == 0)
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

    // Add exclusionArea to list
    protected void btnAddExclusionArea_Click(object source, EventArgs e)
    {
        int id = Convert.ToInt32(Request.QueryString["id"]);

        // Create an empty list of exclusionAreas
        List<ExclusionArea> exclusionAreas = new List<ExclusionArea>();
        // Fill it with data from ViewState variable
        exclusionAreas = (List<ExclusionArea>)ViewState["ExclusionAreas"];

        // Add exclusionArea to list
        exclusionAreas.Add(new ExclusionArea(Convert.ToInt32(txtLowZip.Text), Convert.ToInt32(txtHighZip.Text), id));

        // Save it in a ViewState variable
        ViewState["ExclusionAreas"] = exclusionAreas;

        // Build table of exclusionAreas
        BuildExclusionAreaTable(exclusionAreas);

        // Clear form fields
        txtLowZip.Text = "";
        txtHighZip.Text = "";
    }

    // Remove exclusionAreas from list
    protected void btnRemoveExclusionAreas_Click(object sender, EventArgs e)
    {
        // Create an empty list of exclusionAreas
        List<ExclusionArea> exclusionAreas = new List<ExclusionArea>();
        // Save it in a ViewState variable
        ViewState["ExclusionAreas"] = exclusionAreas;

        // Build (empty) table of exclusionAreas
        BuildExclusionAreaTable(exclusionAreas);
    }

    // Build table of exclusionAreas
    protected void BuildExclusionAreaTable(List<ExclusionArea> exclusionAreas)
    {
        // For each exclusionArea in list
        foreach (ExclusionArea exclusionArea in exclusionAreas)
        {
            // Add new row
            TableRow tr = new TableRow();

            // Add cell with lowZip
            TableCell tc = new TableCell();
            tc.Text = exclusionArea.LowZip.ToString();
            tr.Cells.Add(tc);

            // Add cell with highZip
            tc = new TableCell();
            tc.Text = exclusionArea.HighZip.ToString();
            tr.Cells.Add(tc);

            // Add row to table
            tblExclusionAreas.Rows.Add(tr);
        }
    }
}