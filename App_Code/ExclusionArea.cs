using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[Serializable]
public class ExclusionArea
{
    private int? id;
    public int? Id
    {
        get { return id; }
    }
    private int highZip;
    public int HighZip
    {
        get { return highZip; }
    }
    private int lowZip;
    public int LowZip
    {
        get { return lowZip; }
    }
    private int tvProviderId;
    public int TvProviderId
    {
        get { return tvProviderId; }
    }

    public ExclusionArea(int lowZip, int highZip, int tvProviderId)
    {
        this.lowZip = lowZip;
        this.highZip = highZip;
        this.tvProviderId = tvProviderId;
    }

    public ExclusionArea(int id, int lowZip, int highZip, int tvProviderId)
    {
        this.id = id;
        this.lowZip = lowZip;
        this.highZip = highZip;
        this.tvProviderId = tvProviderId;
    }
}