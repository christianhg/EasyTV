using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class backend : System.Web.UI.Page
{
    /* Instanciate Data-Access Components */
    public TvProviderDB tvProviderDB = new TvProviderDB();
    public PackageDB packageDB = new PackageDB();
    public PackageChannelDB packageChannelDB = new PackageChannelDB();
    public PackageStreamingServiceDB packageStreamingServiceDB = new PackageStreamingServiceDB();
    public ChannelDB channelDB = new ChannelDB();
    public InternetDB internetDB = new InternetDB();
    public StreamingServiceDB streamingServiceDB = new StreamingServiceDB();

    protected void Page_Load(object sender, EventArgs e)
    {
        loginCheck();

        // show elements in tables
        startUp();
    }

    void startUp() 
    {
        List<TvProvider> tvProviders = tvProviderDB.GetTvProviders();
        showTvProviders(tvProviders);

        List<Channel> channels = channelDB.GetChannels();
        showChannels(channels);

        List<Package> packages = packageDB.GetPackages();
        showPackages(packages);

        List<StreamingService> streamingServices = streamingServiceDB.GetStreamingServices();
        showStreamingServices(streamingServices);
    }
    private void showTvProviders(List<TvProvider> tvProviders)
    {
        // List tvp by name
        IEnumerable<TvProvider> queryOrder = tvProviders.OrderBy(tvProvider => tvProvider.Name);
        // TVP table
        foreach (TvProvider tvProvider in queryOrder)
        {
            TableRow tr = new TableRow();

            TableCell tc = new TableCell();
            tc.Text = tvProvider.Name;
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = tvProvider.Info;
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "<img src='assets/img/logos/tvproviders/" + tvProvider.Logo + "' height='40' />";
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = tvProvider.Address;
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = tvProvider.Phone.ToString();
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = tvProvider.Url;
            tr.Cells.Add(tc);

            tc = new TableCell();
            LinkButton lb = new LinkButton();
            lb.PostBackUrl = "tvproviderdetails.aspx?id=" + tvProvider.Id.ToString();
            lb.Text = "Redigér";
            lb.CssClass = "btn btn-primary";
            tc.Controls.Add(lb);
            tr.Cells.Add(tc);

            tc = new TableCell();
            lb = new LinkButton();
            lb.Text = "Slet";
            lb.Attributes["data-tvp-id"] = tvProvider.Id.ToString();
            lb.CssClass = "btn btn-danger";
            lb.Click += new EventHandler(DeleteTvProvider);
            lb.OnClientClick = ("return confirm('Er du sikker på du vil slette " + tvProvider.Name + "?');");
            tc.Controls.Add(lb);
            tr.Cells.Add(tc);

            tblTvProviders.Rows.Add(tr);
        }
    }

    void showChannels(List<Channel> channels) 
    {
        // List channels by name
        IEnumerable<Channel> queryOrder = channels.OrderBy(channel => channel.Name);
        // Channel table
        foreach (Channel channel in queryOrder)
        {
            Table tbl = new Table();

            TableRow tr = new TableRow();

            TableCell tc = new TableCell();
            tc.Text = channel.Name;
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = channel.Info;
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "<img src='assets/img/logos/channels/" + channel.Logo + "' width='70' /> ";
            tr.Cells.Add(tc);

            tc = new TableCell();
            LinkButton lb = new LinkButton();
            lb.PostBackUrl = "channeldetails.aspx?id=" + channel.Id.ToString();
            lb.Text = "Redigér";
            lb.CssClass = "btn btn-primary";
            tc.Controls.Add(lb);
            tr.Cells.Add(tc);

            tc = new TableCell();
            lb = new LinkButton();
            lb.Text = "Slet";
            lb.Attributes["data-channel-id"] = channel.Id.ToString();
            lb.CssClass = "btn btn-danger";
            lb.Click += new EventHandler(DeleteChannel);
            lb.OnClientClick = ("return confirm('Er du sikker på du vil slette " + channel.Name + "?');");
            tc.Controls.Add(lb);
            tr.Cells.Add(tc);

            tblChannels.Rows.Add(tr);

        }  
    }

    void showStreamingServices(List<StreamingService> streamingServices) 
    {
        // List streaming by name
        IEnumerable<StreamingService> queryOrder = streamingServices.OrderBy(streamingService => streamingService.Name);
        
        // streamingservice table
        foreach (StreamingService streamingService in queryOrder)
        {
            Table tbl = new Table();

            TableRow tr = new TableRow();

            TableCell tc = new TableCell();
            tc.Text = streamingService.Name;
            tr.Cells.Add(tc);

            tc = new TableCell();
            LinkButton lb = new LinkButton();
            lb.PostBackUrl = "streamingservicedetails.aspx?id=" + streamingService.Id.ToString();
            lb.Text = "Redigér";
            lb.CssClass = "btn btn-primary";
            tc.Controls.Add(lb);
            tr.Cells.Add(tc);

            tc = new TableCell();
            lb = new LinkButton();
            lb.Text = "Slet";
            lb.Attributes["data-streamingservice-id"] = streamingService.Id.ToString();
            lb.CssClass = "btn btn-danger";
            lb.Click += new EventHandler(DeleteStreamingService);
            lb.OnClientClick = ("return confirm('Er du sikker på du vil slette " + streamingService.Name + "?');");
            tc.Controls.Add(lb);
            tr.Cells.Add(tc);

            tblStreamingServices.Rows.Add(tr);
        }    
    }

    void showPackages(List<Package> packages) 
    {
        // oprder by tvprovider, then by package name
        IEnumerable<Package> queryOrder = packages.OrderBy(package => package.TvProviderId).ThenBy(package => package.Name);

        foreach (Package package in queryOrder)
        {
            TvProvider tvp = tvProviderDB.GetTvProvider(package.TvProviderId);
            TableRow tr = new TableRow();

            TableCell tc = new TableCell();
            tc.Text = "<a target='_blank' href='" + tvp.Url + "'><img src='assets/img/logos/tvproviders/" + tvp.Logo + "' height='30'" + tvp.Name + "/></a>";
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = package.Name;
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = package.Info;
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = package.Url;
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = package.PricePerMonth.ToString();
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = package.StartUpFee.ToString();
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = ((package.PricePerMonth * 6) + package.StartUpFee).ToString();
            tr.Cells.Add(tc);

            tc = new TableCell();
            if (packageDB.HasInternet((int)package.Id))
            {
                Internet internet = internetDB.GetInternet((int)package.Id);
                tc.Text = internet.Download + "/" + internet.Upload + " MB/s";
            }
            else
            {
                tc.Text = "Nej";
            }
            tr.Cells.Add(tc);

            tc = new TableCell();
            int numOfChannels = packageChannelDB.GetPackageChannels((int)package.Id).Count();
            tc.Text = numOfChannels + " kanal";
            if (numOfChannels != 1) tc.Text += "er";
            tr.Cells.Add(tc);

            tc = new TableCell();
            int numOfStreamingServicess = packageStreamingServiceDB.GetPackageStreamingServices((int)package.Id).Count();
            tc.Text = numOfStreamingServicess + " streaming-tjeneste";
            if (numOfStreamingServicess != 1) tc.Text += "r";
            tr.Cells.Add(tc);

            tc = new TableCell();
            LinkButton lb = new LinkButton();
            lb.PostBackUrl = "packagedetails.aspx?id=" + package.Id.ToString();
            lb.Text = "Redigér";
            lb.CssClass = "btn btn-primary";
            tc.Controls.Add(lb);
            tr.Cells.Add(tc);

            tc = new TableCell();
            lb = new LinkButton();
            lb.Text = "Slet";
            lb.Attributes["data-package-id"] = package.Id.ToString();
            lb.CssClass = "btn btn-danger";
            lb.Click += new EventHandler(DeletePackage);
            lb.OnClientClick = ("return confirm('Er du sikker på du vil slette " + package.Name + "?');");
            tc.Controls.Add(lb);
            tr.Cells.Add(tc);

            tblPackages.Rows.Add(tr);
        }    
    }

    // show channel details
    protected void btnInsertChannel_Click(object sender, EventArgs e)
    {
        Response.Redirect("channeldetails.aspx");
    }

    // show tvp details
    protected void btnInsertTvProvider_Click(object sender, EventArgs e)
    {
        Response.Redirect("tvproviderdetails.aspx");
    }

    // show streamingservice details
    protected void btnInsertStreamingService_Click(object sender, EventArgs e)
    {
        Response.Redirect("streamingservicedetails.aspx");
    }

    // show streamingservice details
    protected void btnInsertPackage_Click(object sender, EventArgs e)
    {
        Response.Redirect("packagedetails.aspx");
    }

    // Delete tvp event
    public void DeleteTvProvider(object sender, EventArgs e)
    {
        // caster til en linkbutton
        LinkButton btnDel = sender as LinkButton;
        int tvProviderId;
        if (int.TryParse(btnDel.Attributes["data-tvp-id"].ToString(), out tvProviderId))
        {
            SqlParameter[] parameters = {
                new SqlParameter("pk_id", tvProviderId)
            };
            UtilityDB.DeleteProcedures("sp_DeleteTvProvider", parameters);
        }
    }

    public void DeletePackage(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        int packageId;
        if (int.TryParse(lb.Attributes["data-package-id"].ToString(), out packageId))
        {
            SqlParameter[] parameters = {
                new SqlParameter("pk_id", packageId)
            };
            UtilityDB.DeleteProcedures("sp_DeletePackage", parameters);
        }
    }

    // Delete channel event
    private void DeleteChannel(object sender, EventArgs e)
    {
        // caster til en linkbutton
        LinkButton btnDel = sender as LinkButton;
        int channelId;
        if (int.TryParse(btnDel.Attributes["data-channel-id"].ToString(), out channelId))
        {
            SqlParameter[] parameters = {
                new SqlParameter("pk_id", channelId)
            };
            UtilityDB.DeleteProcedures("sp_DeleteChannel", parameters);
        }
    }
    // Delete streamingservice event
    private void DeleteStreamingService(object sender, EventArgs e)
    {
        // caster til en linkbutton
        LinkButton btnDel = sender as LinkButton;
        int streamingServiceId;
        if (int.TryParse(btnDel.Attributes["data-streamingservice-id"].ToString(), out streamingServiceId))
        {
            SqlParameter[] parameters = {
                new SqlParameter("pk_id", streamingServiceId)
            };
            UtilityDB.DeleteProcedures("sp_DeleteStreamingService", parameters);
        }
    }

    // check if user is logged in
    void loginCheck()
    {
        // Read a session variable
        string Valid_user = (string)Session["valid_user"];
        //if empty redirect to login
        if (Valid_user == null || Valid_user == "")
        {
            Response.Redirect("login.aspx");
        }
    }
}