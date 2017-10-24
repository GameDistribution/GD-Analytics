using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class analytcs_MonetizePaymentSettings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext.Current.Cache.Remove("MonetizePayments_" + SessionObjects.SessionUserId.ToString());

        rptSharedGames.DataSource = GetSharedGames();
        rptSharedGames.DataBind();
    }

    public static DataSet GetSharedGames()
    {
        string strKey = "MonetizePayments_" + SessionObjects.SessionUserId.ToString();

        if (HttpContext.Current.Cache[strKey] == null)
        {
            using (SqlConnection con = Utils.GetNewConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("W_GetMonetizePaymentAccounts", con))
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


    public void rptMonetizePayments_Item_Created(Object sender, RepeaterItemEventArgs e)
    {

    }
}