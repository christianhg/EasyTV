using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StreamingService
/// </summary>
public class StreamingService
{
    int? id;
    public int? Id
    {
        get { return id; }
    }
    string name;
    public string Name
    {
        get { return name; }
    }

    // StreamingService constructor 1
	public StreamingService(int id, string name)
	{
        this.id = id;
        this.name = name;
	}
    // StreamingService constructor 2
    public StreamingService(string name)
    {
        this.name = name;
    }
}