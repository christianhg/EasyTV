using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Package
{
    private int? id;
    public int? Id
    {
        get { return id; }
    }
    private string name;
    public string Name
    {
        get { return name; }
    }
    private string info;
    public string Info
    {
        get { return info; }
    }
    private string url;
    public string Url
    {
        get { return url; }
    }
    private decimal pricePerMonth;
    public decimal PricePerMonth
    {
        get { return pricePerMonth; }
    }
    private decimal startUpFee;
    public decimal StartUpFee
    {
        get { return startUpFee; }
    }
    private int tvProviderId;
    public int TvProviderId
    {
        get { return tvProviderId; }
    }

    public Package(string name, string info, string url, decimal pricePerMonth, decimal startUpFee, int tvProviderId)
    {
        this.name = name;
        this.info = info;
        this.url = url;
        this.pricePerMonth = pricePerMonth;
        this.startUpFee = startUpFee;
        this.tvProviderId = tvProviderId;
    }

    public Package(int id, string name, string info, string url, decimal pricePerMonth, decimal startUpFee, int tvProviderId)
    {
        this.id = id;
        this.name = name;
        this.info = info;
        this.url = url;
        this.pricePerMonth = pricePerMonth;
        this.startUpFee = startUpFee;
        this.tvProviderId = tvProviderId;
    }
}