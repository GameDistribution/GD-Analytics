using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class analytics_BannerFilter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        rptYourGames.DataSource = GetYourGames();
        rptYourGames.DataBind();
    }

    public static DataSet GetYourGames()
    {
        string strKey = "GetYourGames_" + SessionObjects.SessionUserId.ToString();

        if (HttpContext.Current.Cache[strKey] == null)
        {
            using (SqlConnection con = Utils.GetNewConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("W_GetYourGames", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", SessionObjects.SessionUserId);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        HttpContext.Current.Cache.Insert(strKey, ds, null, DateTime.Now.AddMinutes(Utils.C_TIMEOUT_FORMS), System.Web.Caching.Cache.NoSlidingExpiration);
                        return ds;
                    }

                }
            }
        }
        else
        {
            return (DataSet)HttpContext.Current.Cache[strKey];
        }
    }

}