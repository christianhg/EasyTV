using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class Internet
{
    private int download;
    public int Download
    {
        get { return download; }
    }
    private int upload;
    public int Upload
    {
        get { return upload; }
    }
    private int packageId;
    public int PackageId
    {
        get { return packageId; }
    }
    public Internet(int download, int upload, int packageId)
	{
        this.download = download;
        this.upload = upload;
        this.packageId = packageId;
	}
}