using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class analytics_ctrlPager : System.Web.UI.UserControl
{
    private string _PagerName;
    public string PagerName
    {
        get
        {
            return _PagerName;
        }

        set
        {
            _PagerName = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}