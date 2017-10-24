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
        //HttpContext.Current.Cache.Remove("MonetizePaymentHistory_" + SessionObjects.SessionUserId.ToString());

        rptSharedGames.DataSource = GetSharedGames().Tables[0];
        rptSharedGames.DataBind();

        DataRow dr;

        if (GetSharedGames().Tables[1].Rows.Count > 0)
        {
            dr = GetSharedGames().Tables[1].Rows[0];

            accdetail.Text = String.Concat(Utils.safeStr(dr["AccountType"]), " ", Utils.safeStr(dr["AccountDetail"]));
            accname.Text = Utils.safeStr(dr["AccountName"]);
        }
        else
        {
            accdetail.Text = "<a href=\"MonetizePaymentAddAccount.aspx\" class=\"white\">Add payment method</a>";
            accname.Text = "Not defined";
        }

        if (GetSharedGames().Tables[2].Rows.Count > 0)
        {
            dr = GetSharedGames().Tables[2].Rows[0];

            earnings.Text = String.Concat(dr["Earnings"], " ", dr["Currency"]);
            lastpaymentdate.Text = String.Concat(Utils.safeStrDate(dr["StartPeriod"]), " - ", Utils.safeStrDate(dr["EndPeriod"]));

        }
        else
        {
            earnings.Text = "0.00";
            lastpaymentdate.Text = "No interval";
        }
    }

    public static DataSet GetSharedGames()
    {
        string strKey = "MonetizePaymentHistory_" + SessionObjects.SessionUserId.ToString();

        if (HttpContext.Current.Cache[strKey] == null)
        {
            using (SqlConnection con = Utils.GetNewConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("W_MonetizePaymentHistory", con))
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