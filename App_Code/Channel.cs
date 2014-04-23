using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Channel
{
    private int? id;
    public int? Id {
        get { return id; }
    }
    private string name;
    public string Name
    {
        get { return name; }
    }
    private string info;
    public string Info { 
        get { return info; } 
    }
    private string logo;
    public string Logo 
    {
        get { return logo; } 
    }

    public Channel(string name, string info, string logo)
    {
        this.name = name;
        this.info = info;
        this.logo = logo;
    }

    public Channel(int id, string name, string info, string logo)
    {
        this.id = id;
        this.name = name;
        this.info = info;
        this.logo = logo;
    }
}