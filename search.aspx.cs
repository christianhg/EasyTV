using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class search : System.Web.UI.Page
{
    // Instanciate Data-Access Components
    TvProviderDB tvProviderDB = new TvProviderDB();
    PackageDB packageDB = new PackageDB();
    ChannelDB channelDB = new ChannelDB();
    PackageChannelDB packageChannelDB = new PackageChannelDB();
    PackageStreamingServiceDB packageStreamingServiceDB = new PackageStreamingServiceDB();
    InternetDB internetDB = new InternetDB();
    StreamingServiceDB streamingServiceDB = new StreamingServiceDB();

    protected void Page_Load(object sender, EventArgs e)
    {
        // If page is not post back
        if (!Page.IsPostBack)
        {
            // Get all packages from database
            List<Package> packages = packageDB.GetPackages();
            // Get all channels from database
            List<Channel> channels = channelDB.GetChannels();
            // Order channels by name (ascending)
            IEnumerable<Channel> channelOrder = channels.OrderBy(channel => channel.Name);

            // For each channel in ordered list
            foreach (Channel channel in channelOrder)
            {
                // Add channel to multi select box
                lbFavouriteChannels.Items.Add(new ListItem(channel.Name, channel.Id.ToString()));
            }

            // If zip exists in querystring and the length of the value is 4
            if (Request.QueryString["zip"] != null && Request.QueryString["zip"].Length == 4)
            {
                int zip = int.Parse(Request.QueryString["zip"].ToString());
                // Filter packages by zip code
                packages = filterPackagesByZip(zip, packages);
            }

            // If price exists in querysting
            if (Request.QueryString["price"] != null)
            {
                int price = int.Parse(Request.QueryString["price"].ToString());
                // Filter packages by price
                packages = filterPackagesByPrice(price, packages);
                // Add price to price form field 
                txtPrice.Text = price.ToString();
            }

            // If channels exists in querystring
            if (Request.QueryString["channels"] != null)
            {
                // Create an empty list with favourite channels
                List<Channel> favouriteChannels = new List<Channel>();

                // Split serialized channel ids from query string into an array
                string[] channelIds = Request.QueryString["channels"].Split(',');

                // For each channelId 
                foreach (string channelId in channelIds)
                {
                    // If channelId matches a channel id from database, add channel to list of favourite channels
                    favouriteChannels.Add(channels.Find(channel => channel.Id == Convert.ToInt32(channelId)));
                }

                // For eachh channel in list of wanted channels
                foreach (Channel channel in favouriteChannels)
                {
                    // Mark channel as selected in multi select box
                    lbFavouriteChannels.Items.FindByValue(channel.Id.ToString()).Selected = true;
                }

                // Filter packages by channels
                packages = filterPackagesByChannels(favouriteChannels, packages);
            }

            // If internet exists in querystring
            if (Request.QueryString["internet"] != null)
            {
                // Mark internet checkbox as checked
                internetCheck.Checked = true;

                // Filter packages by internet
                packages = filterPackagesByInternet(packages);
            }

            // Show packages
            showPackages(packages);
        }
    }

    // Filter packages by zip code
    List<Package> filterPackagesByZip(int zip, List<Package> packages)
    {
        // Get tvProviders based on zip code
        List<TvProvider> tvProviders = tvProviderDB.SearchTvProviders(zip);
        // Create empty list with filtered packages
        List<Package> filteredPackages = new List<Package>();

        // For each tvProvider
        foreach (TvProvider tvProvider in tvProviders)
        {
            // For each package in original list of packages
            foreach (Package package in packages)
            {
                // If package belongs to tvProvider 
                if (package.TvProviderId == tvProvider.Id)
                {
                    // Add package to filtered packages
                    filteredPackages.Add(package);
                }
            }
        }

        // Return filtered packages
        return filteredPackages;
    }

    // Filter packages by price
    List<Package> filterPackagesByPrice(int price, List<Package> packages)
    {
        // Create empty list with filtered packages
        List<Package> filteredPackages = new List<Package>();

        // For each package original list of packages 
        foreach (Package package in packages)
        {
            // If package has a price per month less than or equal to the desired price
            if (package.PricePerMonth <= price)
            {
                // Add package to filtered packages
                filteredPackages.Add(package);
            }

        }

        // Return filtered packages
        return filteredPackages;
    }

    // Filter packages by channels
    List<Package> filterPackagesByChannels(List<Channel> favouriteChannels, List<Package> packages)
    {
        // Create empty list with filtered packages
        List<Package> filteredPackages = new List<Package>();

        // For each package original list of packages
        foreach (Package package in packages)
        {
            // Get channels from this package
            List<Channel> packageChannels = packageChannelDB.GetPackageChannels((int)package.Id);

            // For each channel in favourite channels
            foreach (Channel channel in favouriteChannels)
            {
                // If package doesn't contain channel
                if (!packageDB.HasChannel((int)package.Id, (int)channel.Id))
                {
                    // Break loop
                    break;
                }
                
                // If we have reached the end of the loop without a break
                if (favouriteChannels[favouriteChannels.Count - 1].Id == channel.Id)
                {
                    // Add package to filtered packages
                    filteredPackages.Add(package);
                }
            }
        }

        // Return filtered packages
        return filteredPackages;
    }

    // Filter packages by internet
    List<Package> filterPackagesByInternet(List<Package> packages)
    {
        // Create empty list with filtered packages
        List<Package> filteredPackages = new List<Package>();

        // For each package original list of packages
        foreach (Package package in packages)
        {
            // If package has internet
            if (packageDB.HasInternet((int)package.Id))
            {
                // Add package to filtered packages list
                filteredPackages.Add(package);
            }
        }

        // Return filtered packages
        return filteredPackages;
    }

    // Show packages
    void showPackages(List<Package> packages)
    { 
        // Order packages by price per month
        IEnumerable<Package> orderedPackages = packages.OrderBy(package => package.PricePerMonth);

        // For each package in ordered list
        foreach (Package package in orderedPackages)
        {
            // Get tvProvider
            TvProvider tvProvider = tvProviderDB.GetTvProvider(package.TvProviderId);
            // Get channels
            List<Channel> channels = packageChannelDB.GetPackageChannels((int)package.Id);

            // Order channels by name (ascending)
            IEnumerable<Channel> orderedChannels = channels.OrderBy(channel => channel.Name);

            // Create empty string of channels
            string showChannels = "";
            // For each channel in ordered list
            foreach (Channel channel in orderedChannels)
            {
                // Add channel to string of channels
                showChannels += "<img src='assets/img/logos/channels/" + channel.Logo + "' width='70' /> ";
            }

            // Get streamingServices from database
            List<StreamingService> streamingServices = packageStreamingServiceDB.GetPackageStreamingServices((int)package.Id);
            // Order streamingServices by name
            IEnumerable<StreamingService> orderedStreamingServices = streamingServices.OrderBy(stream => stream.Name);

            // Create an empty string for streamingServices
            string showSteamingServices = "";
            // If package contains no streamingServices
            if (streamingServices.Count == 0)
            {
                // 
                showSteamingServices = "Ingen streamingtjenester inkluderet";
            }
            else
            {
                // For each streamingService
                foreach (StreamingService streamingService in orderedStreamingServices)
                {
                    // Add streamingService to string of streamingServices
                    showSteamingServices += streamingService.Name + " ";
                }
            }

            // Internet string
            string showInternet = "";

            // If package has internet
            if (packageDB.HasInternet((int)package.Id))
            {
                // Get internet from database
                Internet internet = internetDB.GetInternet((int)package.Id);
                // Add internet to internet string
                showInternet = internet.Download + "/" + internet.Upload + " MB/s";
            }
            else
            {
                showInternet = "Internet ikke inkluderet";
            }
            
            // Create new table of package
            Table tblPackage = new Table();
            tblPackage.CssClass = "table table-bordered";

            // Create string arrow of header cells
            string[] headerCells = {
                "<span class='logo'><a target='_blank' href='"+ tvProvider.Url +"'><img src='assets/img/logos/tvproviders/" + tvProvider.Logo + "' /></a></span> " +
                "<span>" + package.Name + "</span> " +
                "<span>" + channels.Count + " Kanaler" + "</span> " +
                "<span class='streamingCell'>" + streamingServices.Count + " Streamingtjeneste(r)" + "</span> " +
                "<span class='internetCell'>" + showInternet + "</span>",
            };

            // Get row
            TableRow tblHeader = getTableRow(headerCells, 4);
            tblHeader.CssClass = "tblHeader";
            tblPackage.Rows.Add(tblHeader);

            string[] channelRow = {
                showChannels
            };

            TableRow tblRow = getTableRow(channelRow, 4);
            tblPackage.Rows.Add(tblRow);

            string[] streamRow = {
                showSteamingServices
            };
            tblPackage.Rows.Add(getTableRow(streamRow, 4));
            string[] bottomRow = {
                "<span>" + (int)package.PricePerMonth + " kr/md</span> " +
                "<span>" + (int)package.StartUpFee + " kr i oprettelse</span>" +
                "<span>Mindstepris i 6 mdr. " + ((int)package.StartUpFee + ((int)package.PricePerMonth * 6)) + " kr.</span>" +
                "<a href='"+ tvProvider.Url +"' target='blank' class='pull-right btn btn-success' >Gå til "+ tvProvider.Name +"</a>",
            };
            TableRow tblFooter = getTableRow(bottomRow, 4);
            tblFooter.CssClass = "tblFooter";
            tblPackage.Rows.Add(tblFooter);

            pnlPackages.Controls.Add(tblPackage);
        }
    }

    // Get table row
    TableRow getTableRow(string[] cells, int colSpan)
    {
        TableRow tr = new TableRow();
        foreach (string cell in cells)
        {
            TableCell tc = new TableCell();
            tc.Text = cell;
            tc.ColumnSpan = colSpan;
            if (colSpan == 1)
            {
                tc.CssClass = "tblPackageCell";
            }
            tr.Cells.Add(tc);
        }
        return tr;
    }

    // On btnSearch click
    protected void btnSearch_Click(object Source, EventArgs e)
    {
        List<string> selectedChannels = new List<string>();

        foreach (ListItem li in lbFavouriteChannels.Items)
        {
            if (li.Selected)
            {
                selectedChannels.Add(li.Value);
            }
        }
        string selected = string.Join(",", selectedChannels);

        string queryString = "";

        queryString += "search.aspx?zip=" + Request.QueryString["zip"];
        queryString += "&price=" + Server.UrlEncode(txtPrice.Text);

        if (internetCheck.Checked)
        {
            queryString += "&internet=1";
        }
        if (selectedChannels.Count > 0)
        {
            queryString += "&channels=" + selected;
        }

        Response.Redirect(queryString, true);
    }
}