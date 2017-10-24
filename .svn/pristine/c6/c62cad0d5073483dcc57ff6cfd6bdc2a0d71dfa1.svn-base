using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class vdata1_0_Monetize : System.Web.UI.Page
{
    private const int DefaultRowLimit = 20;
    private static int TotalUsers = 0;

    enum SQL_RESULT { ERROR = 0, INSERTED, UPDATED, DELETED, EXIST };

    struct structCountPercent
    {
        public string c1;
        public int c2;
        public decimal c3;
    }

    struct structCountFour
    {
        public string c1;
        public int c2;
        public decimal c3;
        public int c4;
    }

    struct structCountSeven
    {
        public int c1;
        public int c2;
        public decimal c3;
        public decimal c4;
        public decimal c5;
        public string c6;
        public decimal c7;
    }

    struct structCountEight
    {
        public int c1;
        public int c2;
        public decimal c3;
        public decimal c4;
        public decimal c5;
        public string c6;
        public decimal c7;
        public string c8;
    }

    struct structCount
    {
        public int c1;
        public int c2;
    }

    struct structCountFloat
    {
        public float c1;
        public float c2;
    }

    struct structString
    {
        public string c1;
    }

    struct structInt
    {
        public int c1;
    }

    private static classJSON _classJSON;
    public class classJSON
    {
        public Dictionary<string, object> metrics = new Dictionary<string, object>();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionObjects.checkUserSession())
        {
            string GUId = SessionObjects.SessionUserRegId.ToLower();
            string GameGUId = (Utils._GET("gid") ?? "").ToLower(); //f457c545a9ded88f18ecee47145a72c0
            string[] Action = (Utils._GET("act") ?? "").Split(',');
            string[] DateInterval = (Utils._GET("dat") ?? "").Split(',');
            int page = Utils.safeInt((Utils._GET("pag") ?? ""));

            if ((GameGUId == "all" || GameGUId.Length == 32) && Action.Count() > 0 && Utils.safeInt(DateInterval[0]) > 0 && Utils.safeInt(DateInterval[1]) > 0)
            {
                _classJSON = new classJSON();

                for (int key = 0; key < Action.Count(); key++)
                {
                    switch (Action[key])
                    {
                        case "GameIncomesTotal":
                            _classJSON.metrics.Add("GameIncomesTotal", jsonMonetizeGameIncomesTotal(GameGUId, Utils.safeInt(DateInterval[0]), Utils.safeInt(DateInterval[1])));
                            break;
                        case "GameIncomes":
                            _classJSON.metrics.Add("GameIncomes", jsonMonetizeGameIncomes(GameGUId, Utils.safeInt(DateInterval[0]), Utils.safeInt(DateInterval[1]), page));
                            break;
                    }
                }

                Response.Write(Utils.json_encode(_classJSON));
            }
            else
            {
                Response.Write("false");
            }
        }
        else
        {
            Response.Write("false");
        }
    }

    /*
        JSON Functions      
    */
    private static structCountSeven[] jsonMonetizeGameIncomesTotal(string GameGUId, int startDate, int endDate)
    {
        return MonetizeGameIncomesTotal(sqlMonetizeGameIncomesTotal(GameGUId, startDate, endDate).Tables[0]);
    }

    private static structCountEight[] jsonMonetizeGameIncomes(string GameGUId, int startDate, int endDate, int page)
    {
        return MonetizeGameIncomes(sqlMonetizeGameIncomes(GameGUId, startDate, endDate, page).Tables[0]);
    }

    /*
        Struct Functions      
    */
    private static structCountSeven[] MonetizeGameIncomesTotal(DataTable _DataTable)
    {
        int i = 0;
        // Set MetricsCount Size
        structCountSeven[] _MetricsCount = new structCountSeven[(_DataTable.Rows.Count < DefaultRowLimit ? _DataTable.Rows.Count : DefaultRowLimit)];
        foreach (DataRow dr in _DataTable.Rows)
        {
            _MetricsCount[i].c1 = Convert.ToInt32(dr["SumBannerView"]);
            _MetricsCount[i].c2 = Convert.ToInt32(dr["SumBannerClick"]);
            _MetricsCount[i].c3 = Math.Round(Convert.ToDecimal(dr["AvgBannerClickThroughRate"]), 2);
            _MetricsCount[i].c4 = Math.Round(Convert.ToDecimal(dr["AvgBannerCostPerClick"]), 2);
            _MetricsCount[i].c5 = Math.Round(Convert.ToDecimal(dr["AvgBannerRatePerImp"]), 2);
            _MetricsCount[i].c6 = Convert.ToString(dr["Currency"]);
            _MetricsCount[i].c7 = Math.Round(Convert.ToDecimal(dr["SumEarnings"]), 2);
            i++;
        }
        return _MetricsCount;
    }

    private static structCountEight[] MonetizeGameIncomes(DataTable _DataTable)
    {
        int i = 0;
        // Set MetricsCount Size
        structCountEight[] _MetricsCount = new structCountEight[(_DataTable.Rows.Count < DefaultRowLimit ? _DataTable.Rows.Count : DefaultRowLimit)];
        foreach (DataRow dr in _DataTable.Rows)
        {
            _MetricsCount[i].c1 = Convert.ToInt32(dr["BannerView"]);
            _MetricsCount[i].c2 = Convert.ToInt32(dr["BannerClick"]);
            _MetricsCount[i].c3 = Math.Round(Convert.ToDecimal(dr["BannerClickThroughRate"]), 2);
            _MetricsCount[i].c4 = Math.Round(Convert.ToDecimal(dr["BannerCostPerClick"]), 2);
            _MetricsCount[i].c5 = Math.Round(Convert.ToDecimal(dr["BannerRatePerImp"]), 2);
            _MetricsCount[i].c6 = Convert.ToString(dr["Currency"]);
            _MetricsCount[i].c7 = Math.Round(Convert.ToDecimal(dr["Earnings"]), 2);
            _MetricsCount[i].c8 = Convert.ToString(Utils.DateTimeToUnixTime((DateTime)dr["EarnDate"]));
            i++;
        }
        return _MetricsCount;
    }
    /*
        SQL Functions      
    */
    public static DataSet sqlMonetizeGameIncomesTotal(string GameGUId, int startDate, int endDate)
    {
        string strKey = String.Concat("MonetizeGameIncomesTotal_", GameGUId, "_", startDate.ToString(), "_", endDate.ToString());

        if (HttpRuntime.Cache[strKey] == null)
        {
            using (SqlConnection con = Utils.GetNewConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(GameGUId == "all" ? "W_MonetizeGameIncomesAll" : "W_MonetizeGameIncomesTotal", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", SessionObjects.SessionUserId);
                    if (GameGUId != "all") cmd.Parameters.AddWithValue("@GameId", Utils.safeStr(GameGUId));
                    cmd.Parameters.AddWithValue("@startDate", Utils.UnixTimeToDateTime(startDate));
                    cmd.Parameters.AddWithValue("@endDate", Utils.UnixTimeToDateTime(endDate));
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        HttpRuntime.Cache.Insert(strKey, ds, null, DateTime.Now.AddMinutes(Utils.C_TIMEOUT_FORMS), System.Web.Caching.Cache.NoSlidingExpiration);
                        return ds;
                    }

                }
            }
        }
        else
        {
            return (DataSet)HttpRuntime.Cache[strKey];
        }
    }


    public static DataSet sqlMonetizeGameIncomes(string GameGUId, int startDate, int endDate, int page)
    {
        string strKey = String.Concat("MonetizeGameIncomes_", GameGUId, "_", startDate.ToString(), "_", endDate.ToString(), "_", page.ToString());

        if (HttpRuntime.Cache[strKey] == null)
        {
            using (SqlConnection con = Utils.GetNewConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(GameGUId == "all" ? "W_MonetizeGameIncomesAllWithPager" : "W_MonetizeGameIncomesWithPager", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", SessionObjects.SessionUserId);
                    if (GameGUId != "all") cmd.Parameters.AddWithValue("@GameId", Utils.safeStr(GameGUId));
                    cmd.Parameters.AddWithValue("@startDate", Utils.UnixTimeToDateTime(startDate));
                    cmd.Parameters.AddWithValue("@endDate", Utils.UnixTimeToDateTime(endDate));
                    cmd.Parameters.AddWithValue("@page", page);
                    cmd.Parameters.AddWithValue("@pageLimit", DefaultRowLimit);
                    SqlParameter totalRecord = cmd.Parameters.Add("@total", SqlDbType.Int);
                    totalRecord.Direction = ParameterDirection.Output;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        HttpRuntime.Cache.Insert(strKey, ds, null, DateTime.Now.AddMinutes(Utils.C_TIMEOUT_FORMS), System.Web.Caching.Cache.NoSlidingExpiration);
                        HttpRuntime.Cache.Insert(String.Concat("TotalPage_", strKey), Convert.ToInt32(totalRecord.Value), null, DateTime.Now.AddMinutes(Utils.C_TIMEOUT_FORMS), System.Web.Caching.Cache.NoSlidingExpiration);
                        _classJSON.metrics.Add("TotalPage", Convert.ToInt32(totalRecord.Value));
                        return ds;
                    }

                }
            }
        }
        else
        {
            _classJSON.metrics.Add("TotalPage", (int)HttpRuntime.Cache[String.Concat("TotalPage_", strKey)]);
            return (DataSet)HttpRuntime.Cache[strKey];
        }
    }


}