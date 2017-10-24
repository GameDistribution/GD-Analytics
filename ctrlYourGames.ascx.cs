using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class analitcs_ctrlYourGames : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        rptYourGames.DataSource = GetYourGames();
        rptYourGames.DataBind();

        rptSharedGames.DataSource = GetSharedGames();
        rptSharedGames.DataBind();

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

    public static DataSet GetSharedGames()
    {
        string strKey = "GetYourSharedGames_" + SessionObjects.SessionUserId.ToString();

        if (HttpContext.Current.Cache[strKey] == null)
        {
            using (SqlConnection con = Utils.GetNewConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("W_GetYourSharedGames", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OwnerUserId", SessionObjects.SessionUserId);
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