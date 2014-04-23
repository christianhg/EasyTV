using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TVProvider
/// </summary>
public class TvProvider
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

    private string logo;
    public string Logo
    {
        get { return logo; }
    }

    private string address;
    public string Address
    {
        get { return address; }
    }

    private int phone;
    public int Phone
    {
        get { return phone; }
    }

    private string url;
    public string Url
    {
        get { return url; }
    }

    // constructor 1 - without id 
    public TvProvider(string name, string info, string logo, string address, int phone, string url)
    {
        this.name = name;
        this.info = info;
        this.logo = logo;
        this.address = address;
        this.phone = phone;
        this.url = url;
    }
    // constructor 2 - with id 
    public TvProvider(int id, string name, string info, string logo, string address, int phone, string url)
    {
        this.id = id;
        this.name = name;
        this.info = info;
        this.logo = logo;
        this.address = address;
        this.phone = phone;
        this.url = url;
    }
}