using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class analitcs_Default : System.Web.UI.MasterPage
{
    protected void Page_PreRender(object sender, EventArgs e)
    {
        
        if (!SessionObjects.checkUserSession())
        {
            Session.Abandon();
            Response.Redirect("/?timeout");
            Response.End();
        }
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!SessionObjects.checkUserSession())
        {
            Session.Abandon();
            Response.Redirect("/?timeout1");
            Response.End();
        }        

        if (Utils._GET("gid")!=null && Utils._GET("gid").Length!=32)
        {
            Response.Redirect("/RealTimeDashboard.aspx");
            Response.End();        
        }
    }
}
