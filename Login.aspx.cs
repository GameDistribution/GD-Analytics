using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class analytcs_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.ContentType = "application/json"; 
        try
        {            
            if (Utils._POST("uemail").Length > 0 && Utils._POST("upassword").Length > 0)
            {
                Response.Write(Utils.json_encode(LoginUser(Utils._POST("uemail"), Utils._POST("upassword"))));
            }
            else
            {
                Session.Abandon();
                Response.Write(Utils.json_encode(false));
            }
        }
        catch
        {
            Session.Abandon();
            Response.Write(Utils.json_encode(false));
        }
    }

    public bool LoginUser(string Email, string Password)
    {
        using (SqlConnection con = Utils.GetNewConnection())
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("W_LoginUser", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", Utils.safeStr(Email));
                cmd.Parameters.AddWithValue("@Password", Utils.safeStr(Password));
                cmd.Parameters.AddWithValue("@LastLoginIP", HttpContext.Current.Request.UserHostAddress);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    using (DataSet ds = new DataSet())
                    {
                        da.Fill(ds);
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            SessionObjects.SessionUserId = Utils.safeInt(ds.Tables[0].Rows[0]["Id"]);
                            SessionObjects.SessionUserRegId = Utils.safeStr(ds.Tables[0].Rows[0]["RegId"]);
                            SessionObjects.SessionUserInfo = ds.Tables[0].Rows[0];
                            Session.Timeout = 60*24;

                            return true;
                        }
                        else
                        {
                            Session.Abandon();
                            return false;
                        }
                    }
                }

            }
        }
    }

}