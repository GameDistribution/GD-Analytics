using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class vdata1_0_Audience : System.Web.UI.Page
{
    private const int DefaultRowLimit = 20;
    private static int TotalUsers = 0;

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
        public string c1;
        public int c2;
        public int c3;
        public int c4;
        public float c5;
        public float c6;
        public string c7;
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

            if (GameGUId.Length == 32 && Action.Count() > 0 && Utils.safeInt(DateInterval[0]) > 0 && Utils.safeInt(DateInterval[1]) > 0)
            {
                _classJSON = new classJSON();

                for (int key = 0; key < Action.Count(); key++)
                {
                    switch (Action[key])
                    {
                        case "Summary":
                            jsonSummary(Utils.safeInt(DateInterval[0]), Utils.safeInt(DateInterval[1]));
                            break;
                        case "Overview":
                            jsonOverview(GameGUId, Utils.safeInt(DateInterval[0]), Utils.safeInt(DateInterval[1]));
                            break;
                        case "Total":
                            jsonTotalUsers(GameGUId, Utils.safeInt(DateInterval[0]), Utils.safeInt(DateInterval[1]));
                            break;
                        case "TotalWithDomain":
                            jsonTotalUsersWithDomain(GameGUId, Utils.safeInt(DateInterval[0]), Utils.safeInt(DateInterval[1]), Utils.safeStr((Utils._GET("dom") ?? "")));
                            break;
                        case "TotalWithBrowser":
                            jsonTotalUsersWithBrowser(GameGUId, Utils.safeInt(DateInterval[0]), Utils.safeInt(DateInterval[1]), Utils.safeStr((Utils._GET("bro") ?? "")));
                            break;
                        case "TotalWithOS":
                            jsonTotalUsersWithOS(GameGUId, Utils.safeInt(DateInterval[0]), Utils.safeInt(DateInterval[1]), Utils.safeStr((Utils._GET("os") ?? "")));
                            break;
                        case "City":
                            _classJSON.metrics.Add("City", jsonCities(GameGUId, Utils.safeInt(DateInterval[0]), Utils.safeInt(DateInterval[1])));
                            break;
                        case "Country":
                            _classJSON.metrics.Add("Country", jsonCountries(GameGUId, Utils.safeInt(DateInterval[0]), Utils.safeInt(DateInterval[1]), page));
                            break;
                        case "WebRefer":
                            _classJSON.metrics.Add("WebRefer", jsonWebRefer(GameGUId, Utils.safeInt(DateInterval[0]), Utils.safeInt(DateInterval[1]), Utils.safeStr((Utils._GET("dom") ?? "")), page));
                            break;
                        case "WebReferDomain":
                            _classJSON.metrics.Add("WebReferDomain", jsonWebReferDomain(GameGUId, Utils.safeInt(DateInterval[0]), Utils.safeInt(DateInterval[1]), page));
                            break;
                        case "BrowserName":
                            _classJSON.metrics.Add("BrowserName", jsonBrowserName(GameGUId, Utils.safeInt(DateInterval[0]), Utils.safeInt(DateInterval[1]), page));
                            break;
                        case "Browser":
                            _classJSON.metrics.Add("Browser", jsonBrowser(GameGUId, Utils.safeInt(DateInterval[0]), Utils.safeInt(DateInterval[1]), Utils.safeStr((Utils._GET("bro") ?? "")), page));
                            break;
                        case "Device":
                            _classJSON.metrics.Add("Device", jsonDevice(GameGUId, Utils.safeInt(DateInterval[0]), Utils.safeInt(DateInterval[1]), page));
                            break;
                        case "OSName":
                            _classJSON.metrics.Add("OSName", jsonOSName(GameGUId, Utils.safeInt(DateInterval[0]), Utils.safeInt(DateInterval[1]), page));
                            break;
                        case "OS":
                            _classJSON.metrics.Add("OS", jsonOS(GameGUId, Utils.safeInt(DateInterval[0]), Utils.safeInt(DateInterval[1]), Utils.safeStr((Utils._GET("os") ?? "")), page));
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

    private static void jsonSummary(int startDate, int endDate)
    {
        string OrderColumn = "Title";
        switch ((Utils._GET("col") ?? ""))
        {
            case "1": OrderColumn = "Title";
                break;
            case "2": OrderColumn = "TotalUsers";
                break;
            case "3": OrderColumn = "WebSite";
                break;
            case "4": OrderColumn = "TimePlayed";
                break;
            case "5": OrderColumn = "Bounce";
                break;
        }

        int OrderDirection = (Utils._GET("dir") ?? "0") == "1" ? 1 : 0;

        DataSet _SummaryDataset = sqlAudienceSummary(OrderColumn, OrderDirection, startDate, endDate);

        // Summary
        _classJSON.metrics.Add("Summary", AudienceSummary(_SummaryDataset.Tables[0]));
    }


    private static void jsonOverview(string GameGUId, int startDate, int endDate)
    {
        structCount _structCount = new structCount();
        DataSet _OverviewDataset = sqlAudienceOverview(GameGUId, startDate, endDate);
        // Total and Unique Users
        _structCount.c1 = Convert.ToInt32(_OverviewDataset.Tables[0].Rows[0]["TotalUsers"] ?? 0);
        TotalUsers = _structCount.c1;
        _structCount.c2 = Convert.ToInt32(_OverviewDataset.Tables[0].Rows[0]["UniqueUsers"] ?? 0);
        _classJSON.metrics.Add("Total", _structCount);

        // Total List
        _classJSON.metrics.Add("TotalUsers", AudienceTotalUsers(_OverviewDataset.Tables[1]));
        // Country
        _classJSON.metrics.Add("Country", AudienceCountry(_OverviewDataset.Tables[2]));
        // City
        _classJSON.metrics.Add("City", AudienceCity(_OverviewDataset.Tables[3]));
        // WebRef
        _classJSON.metrics.Add("WebRefer", AudienceWebRef(_OverviewDataset.Tables[4]));
        // First Seen Date
        _classJSON.metrics.Add("Seen", AudienceFirstSeenDate(_OverviewDataset.Tables[5]));
        // Site Played
        _classJSON.metrics.Add("SitePlayed", AudienceSitePlayed(_OverviewDataset.Tables[6]));
        // Average Time Played
        _classJSON.metrics.Add("AvgTimePlayed", AudienceAvgTimePlayed(_OverviewDataset.Tables[7]));
        // Bounce
        _classJSON.metrics.Add("Bounce", AudienceBounce(_OverviewDataset.Tables[8]));
    }

    private static void jsonTotalUsers(string GameGUId, int startDate, int endDate)
    {
        structCount _structCount = new structCount();
        DataSet _OverviewDataset = sqlAudienceTotal(GameGUId, startDate, endDate);
        // Total and Unique Users
        _structCount.c1 = Convert.ToInt32(_OverviewDataset.Tables[0].Rows[0]["TotalUsers"] ?? 0);
        TotalUsers = _structCount.c1;
        _structCount.c2 = Convert.ToInt32(_OverviewDataset.Tables[0].Rows[0]["UniqueUsers"] ?? 0);
        _classJSON.metrics.Add("Total", _structCount);
        // Total List
        _classJSON.metrics.Add("TotalUsers", AudienceTotalUsers(_OverviewDataset.Tables[1]));

    }

    private static void jsonTotalUsersWithDomain(string GameGUId, int startDate, int endDate, string domain)
    {
        structCount _structCount = new structCount();
        DataSet _OverviewDataset = sqlAudienceTotalWithDomain(GameGUId, startDate, endDate, domain);
        // Total and Unique Users
        _structCount.c1 = Convert.ToInt32(_OverviewDataset.Tables[0].Rows[0]["TotalUsers"] ?? 0);
        TotalUsers = _structCount.c1;
        _structCount.c2 = Convert.ToInt32(_OverviewDataset.Tables[0].Rows[0]["UniqueUsers"] ?? 0);        
        _classJSON.metrics.Add("Total", _structCount);
        // Total List
        _classJSON.metrics.Add("TotalUsers", AudienceTotalUsers(_OverviewDataset.Tables[1]));

    }

    private static void jsonTotalUsersWithBrowser(string GameGUId, int startDate, int endDate, string browser)
    {
        structCount _structCount = new structCount();
        DataSet _OverviewDataset = sqlTechnologyTotalWithBrowser(GameGUId, startDate, endDate, browser);
        // Total and Unique Users
        _structCount.c1 = Convert.ToInt32(_OverviewDataset.Tables[0].Rows[0]["TotalUsers"] ?? 0);
        TotalUsers = _structCount.c1;
        _structCount.c2 = Convert.ToInt32(_OverviewDataset.Tables[0].Rows[0]["UniqueUsers"] ?? 0);
        _classJSON.metrics.Add("Total", _structCount);
        // Total List
        _classJSON.metrics.Add("TotalUsers", AudienceTotalUsers(_OverviewDataset.Tables[1]));

    }

    private static void jsonTotalUsersWithOS(string GameGUId, int startDate, int endDate, string os)
    {
        structCount _structCount = new structCount();
        DataSet _OverviewDataset = sqlTechnologyTotalWithOS(GameGUId, startDate, endDate, os);
        // Total and Unique Users
        _structCount.c1 = Convert.ToInt32(_OverviewDataset.Tables[0].Rows[0]["TotalUsers"] ?? 0);
        TotalUsers = _structCount.c1;
        _structCount.c2 = Convert.ToInt32(_OverviewDataset.Tables[0].Rows[0]["UniqueUsers"] ?? 0);
        _classJSON.metrics.Add("Total", _structCount);
        // Total List
        _classJSON.metrics.Add("TotalUsers", AudienceTotalUsers(_OverviewDataset.Tables[1]));

    }

    private static structCountPercent[] jsonCountries(string GameGUId, int startDate, int endDate, int page)
    {
        return AudienceCountry(sqlCountries(GameGUId, startDate, endDate, page).Tables[0]);
    }

    private static structCountPercent[] jsonCities(string GameGUId, int startDate, int endDate)
    {
        return AudienceCity(sqlCities(GameGUId, startDate, endDate).Tables[0]);
    }

    private static structCountPercent[] jsonWebRefer(string GameGUId, int startDate, int endDate, string domain, int page)
    {
        return AudienceWebRef(sqlWebRefer(GameGUId, startDate, endDate, domain, page).Tables[0]);
    }

    private static structCountPercent[] jsonWebReferDomain(string GameGUId, int startDate, int endDate, int page)
    {
        return AudienceWebRefDomain(sqlWebReferDomain(GameGUId, startDate, endDate, page).Tables[0]);
    }

    private static structCountPercent[] jsonBrowserName(string GameGUId, int startDate, int endDate, int page)
    {
        return TechnologyBrowserName(sqlBrowserName(GameGUId, startDate, endDate, page).Tables[0]);
    }

    private static structCountPercent[] jsonBrowser(string GameGUId, int startDate, int endDate, string browser, int page)
    {
        return TechnologyBrowser(sqlBrowser(GameGUId, startDate, endDate, browser, page).Tables[0]);
    }

    private static structCountFour[] jsonDevice(string GameGUId, int startDate, int endDate, int page)
    {
        return TechnologyDevice(sqlDevice(GameGUId, startDate, endDate, page).Tables[0]);
    }

    private static structCountFour[] jsonOSName(string GameGUId, int startDate, int endDate, int page)
    {
        return TechnologyOSName(sqlOSName(GameGUId, startDate, endDate, page).Tables[0]);
    }
    private static structCountFour[] jsonOS(string GameGUId, int startDate, int endDate, string os, int page)
    {
        return TechnologyOS(sqlOS(GameGUId, startDate, endDate, os, page).Tables[0]);
    }

    private static structCountSeven[] AudienceSummary(DataTable _DataTable)
    {
        int i = 0;
        // Set MetricsCount Size
        structCountSeven[] _MetricsCount = new structCountSeven[_DataTable.Rows.Count];
        foreach (DataRow dr in _DataTable.Rows)
        {
            //_MetricsCount[i].c1 = String.Format("{0:dd.MM.yyyy}", dr["ReportDate"]);
            //_MetricsCount[i].c1 = Convert.ToString(Utils.DateTimeToUnixTime((DateTime)dr["ReportDate"]));
            _MetricsCount[i].c1 = Convert.ToString(dr["Title"]);
            _MetricsCount[i].c2 = Convert.ToInt32(dr["TotalUsers"]);
            _MetricsCount[i].c3 = Convert.ToInt32(dr["UniqueUsers"]);
            _MetricsCount[i].c4 = Convert.ToInt32(dr["WebSitePlayedCount"]);
            _MetricsCount[i].c5 = Convert.ToInt32(dr["AvgTimePlayed"]);
            _MetricsCount[i].c6 = Convert.ToSingle(dr["Bounce"]);
            _MetricsCount[i].c7 = Convert.ToString(dr["GameId"]);
            _MetricsCount[i].c8 = Convert.ToString(dr["FirstSeenDate"]);
            i++;
        }
        return _MetricsCount;
    }

    private static structCountFour[] AudienceTotalUsers(DataTable _DataTable)
    {
        int i = 0;
        // Set MetricsCount Size
        structCountFour[] _MetricsCount = new structCountFour[_DataTable.Rows.Count];
        foreach (DataRow dr in _DataTable.Rows)
        {
            //_MetricsCount[i].c1 = String.Format("{0:dd.MM.yyyy}", dr["ReportDate"]);
            _MetricsCount[i].c1 = Convert.ToString(Utils.DateTimeToUnixTime((DateTime)dr["ReportDate"]));
            _MetricsCount[i].c2 = Convert.ToInt32(dr["TotalUsers"]);
            _MetricsCount[i].c3 = Convert.ToInt32(dr["UniqueUsers"]);
            _MetricsCount[i].c4 = Convert.ToInt32(dr["NewVisitors"]);
            i++;
        }
        return _MetricsCount;
    }

    private static structString[] AudienceFirstSeenDate(DataTable _DataTable)
    {
        int i = 0;
        // Set _MetricsString Size
        structString[] _MetricsString = new structString[_DataTable.Rows.Count];
        foreach (DataRow dr in _DataTable.Rows)
        {
            _MetricsString[i].c1 = Convert.ToString(dr["FirstSeenDate"]);
            i++;
        }
        return _MetricsString;
    }

    private static structCount[] AudienceSitePlayed(DataTable _DataTable)
    {
        int i = 0;
        int TotalSiteplayed = 0;
        // Set _MetricsCount Size
        structCount[] _MetricsCount = new structCount[_DataTable.Rows.Count];
        foreach (DataRow dr in _DataTable.Rows)
        {
            _MetricsCount[i].c1 = Convert.ToInt32(dr["WebSitePlayedCount"]);
            TotalSiteplayed += _MetricsCount[i].c1;
            _MetricsCount[i].c2 = TotalSiteplayed;
            i++;
        }
        return _MetricsCount;
    }

    private static structCount[] AudienceAvgTimePlayed(DataTable _DataTable)
    {
        int i = 0;
        int TotalTimeplayed = 0;
        // Set _MetricsCount Size
        structCount[] _MetricsCount = new structCount[_DataTable.Rows.Count];
        foreach (DataRow dr in _DataTable.Rows)
        {
            _MetricsCount[i].c1 = Convert.ToInt32(dr["AvgTimePlayed"]);
            TotalTimeplayed += _MetricsCount[i].c1;
            _MetricsCount[i].c2 = (int)TotalTimeplayed/(i+1);
            i++;
        }
        return _MetricsCount;
    }

    private static structCountFloat[] AudienceBounce(DataTable _DataTable)
    {
        int i = 0;
        float TotalBounce = 0;
        // Set _MetricsCount Size
        structCountFloat[] _MetricsCount = new structCountFloat[_DataTable.Rows.Count];
        foreach (DataRow dr in _DataTable.Rows)
        {
            _MetricsCount[i].c1 = Convert.ToInt32(dr["Bounce"]);
            TotalBounce += _MetricsCount[i].c1;
            _MetricsCount[i].c2 = TotalBounce / (i + 1);
            i++;
        }
        return _MetricsCount;
    }


    private static structCountPercent[] AudienceCountry(DataTable _DataTable)
    {
        int i = 0;
        // Set MetricsCount Size
        structCountPercent[] _MetricsCount = new structCountPercent[(_DataTable.Rows.Count < DefaultRowLimit ? _DataTable.Rows.Count : DefaultRowLimit)];
        foreach (DataRow dr in _DataTable.Rows)
        {
            _MetricsCount[i].c1 = Convert.ToString(dr["Country"]) == "" ? "Other" : Convert.ToString(dr["Country"]);
            _MetricsCount[i].c2 = Convert.ToInt32(dr["CountryCount"]);
            _MetricsCount[i].c3 = Math.Round((decimal)_MetricsCount[i].c2 / TotalUsers * 100, 2);
            i++;
        }
        return _MetricsCount;
    }

    private static structCountPercent[] AudienceCity(DataTable _DataTable)
    {
        int i = 0;
        // Set MetricsCount Size
        structCountPercent[] _MetricsCount = new structCountPercent[(_DataTable.Rows.Count < DefaultRowLimit ? _DataTable.Rows.Count : DefaultRowLimit)];
        foreach (DataRow dr in _DataTable.Rows)
        {
            _MetricsCount[i].c1 = Convert.ToString(dr["City"]) == "" ? "Other" : Convert.ToString(dr["City"]);
            _MetricsCount[i].c2 = Convert.ToInt32(dr["CityCount"]);
            _MetricsCount[i].c3 = Math.Round((decimal)_MetricsCount[i].c2 / TotalUsers * 100, 2);
            i++;
        }
        return _MetricsCount;
    }

    private static structCountPercent[] AudienceWebRef(DataTable _DataTable)
    {
        int i = 0;
        // Set MetricsCount Size
        structCountPercent[] _MetricsCount = new structCountPercent[(_DataTable.Rows.Count < DefaultRowLimit ? _DataTable.Rows.Count : DefaultRowLimit)];
        foreach (DataRow dr in _DataTable.Rows)
        {
            _MetricsCount[i].c1 = Convert.ToString(dr["WebRef"]) == "" ? "Other" : Convert.ToString(dr["WebRef"]);
            _MetricsCount[i].c2 = Convert.ToInt32(dr["WebRefCount"]);
            _MetricsCount[i].c3 = Math.Round((decimal)_MetricsCount[i].c2 / TotalUsers * 100, 2);
            i++;
        }
        return _MetricsCount;
    }

    private static structCountPercent[] AudienceWebRefDomain(DataTable _DataTable)
    {
        int i = 0;
        // Set MetricsCount Size
        structCountPercent[] _MetricsCount = new structCountPercent[(_DataTable.Rows.Count < DefaultRowLimit ? _DataTable.Rows.Count : DefaultRowLimit)];
        foreach (DataRow dr in _DataTable.Rows)
        {
            _MetricsCount[i].c1 = Convert.ToString(dr["Domain"]) == "" ? "Other" : Convert.ToString(dr["Domain"]);
            _MetricsCount[i].c2 = Convert.ToInt32(dr["WebRefCount"]);
            _MetricsCount[i].c3 = Math.Round((decimal)_MetricsCount[i].c2 / TotalUsers * 100, 2);
            i++;
        }
        return _MetricsCount;
    }

    private static structCountPercent[] TechnologyBrowserName(DataTable _DataTable)
    {
        int i = 0;
        // Set MetricsCount Size
        structCountPercent[] _MetricsCount = new structCountPercent[(_DataTable.Rows.Count < DefaultRowLimit ? _DataTable.Rows.Count : DefaultRowLimit)];
        foreach (DataRow dr in _DataTable.Rows)
        {
            _MetricsCount[i].c1 = Convert.ToString(dr["BrowserName"]) == "" ? "Other" : Convert.ToString(dr["BrowserName"]);
            _MetricsCount[i].c2 = Convert.ToInt32(dr["BrowserCount"]);
            _MetricsCount[i].c3 = Math.Round((decimal)_MetricsCount[i].c2 / TotalUsers * 100, 2);
            i++;
        }
        return _MetricsCount;
    }

    private static structCountPercent[] TechnologyBrowser(DataTable _DataTable)
    {
        int i = 0;
        // Set MetricsCount Size
        structCountPercent[] _MetricsCount = new structCountPercent[(_DataTable.Rows.Count < DefaultRowLimit ? _DataTable.Rows.Count : DefaultRowLimit)];
        foreach (DataRow dr in _DataTable.Rows)
        {
            _MetricsCount[i].c1 = Convert.ToString(dr["Browser"]) == "" ? "Other" : Convert.ToString(dr["Browser"]);
            _MetricsCount[i].c2 = Convert.ToInt32(dr["BrowserCount"]);
            _MetricsCount[i].c3 = Math.Round((decimal)_MetricsCount[i].c2 / TotalUsers * 100, 2);
            i++;
        }
        return _MetricsCount;
    }

    private static structCountFour[] TechnologyDevice(DataTable _DataTable)
    {
        int i = 0;
        // Set MetricsCount Size
        structCountFour[] _MetricsCount = new structCountFour[(_DataTable.Rows.Count < DefaultRowLimit ? _DataTable.Rows.Count : DefaultRowLimit)];
        foreach (DataRow dr in _DataTable.Rows)
        {
            _MetricsCount[i].c1 = Convert.ToString(dr["Device"]) == "" ? "Other" : Convert.ToString(dr["Device"]);
            _MetricsCount[i].c2 = Convert.ToInt32(dr["DeviceCount"]);
            _MetricsCount[i].c4 = Convert.ToInt32(dr["DeviceUniqueCount"]);
            _MetricsCount[i].c3 = Math.Round((decimal)_MetricsCount[i].c4 / TotalUsers * 100, 2);
            i++;
        }
        return _MetricsCount;
    }

    private static structCountFour[] TechnologyOSName(DataTable _DataTable)
    {
        int i = 0;
        // Set MetricsCount Size
        structCountFour[] _MetricsCount = new structCountFour[(_DataTable.Rows.Count < DefaultRowLimit ? _DataTable.Rows.Count : DefaultRowLimit)];
        foreach (DataRow dr in _DataTable.Rows)
        {
            _MetricsCount[i].c1 = Convert.ToString(dr["OSName"]) == "" ? "Other" : Convert.ToString(dr["OSName"]);
            _MetricsCount[i].c2 = Convert.ToInt32(dr["OSCount"]);
            _MetricsCount[i].c4 = Convert.ToInt32(dr["OSUniqueCount"]);
            _MetricsCount[i].c3 = Math.Round((decimal)_MetricsCount[i].c4 / TotalUsers * 100, 2);
            i++;
        }
        return _MetricsCount;
    }

    private static structCountFour[] TechnologyOS(DataTable _DataTable)
    {
        int i = 0;
        // Set MetricsCount Size
        structCountFour[] _MetricsCount = new structCountFour[(_DataTable.Rows.Count < DefaultRowLimit ? _DataTable.Rows.Count : DefaultRowLimit)];
        foreach (DataRow dr in _DataTable.Rows)
        {
            _MetricsCount[i].c1 = Convert.ToString(dr["OS"]) == "" ? "Other" : Convert.ToString(dr["OS"]);
            _MetricsCount[i].c2 = Convert.ToInt32(dr["OSCount"]);
            _MetricsCount[i].c4 = Convert.ToInt32(dr["OSUniqueCount"]);
            _MetricsCount[i].c3 = Math.Round((decimal)_MetricsCount[i].c4 / TotalUsers * 100, 2);
            i++;
        }
        return _MetricsCount;
    }

    public static DataSet sqlAudienceSummary(string OrderColumn, int OrderDirection, int startDate, int endDate)
    {
        string strKey = String.Concat("GetAudienceSummary_", SessionObjects.SessionUserId.ToString(), "_", OrderColumn, "_", OrderDirection.ToString(), "_", startDate.ToString(), "_", endDate.ToString());

        if (HttpRuntime.Cache[strKey] == null)
        {
            using (SqlConnection con = Utils.GetNewConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("W_AudienceSummary", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", SessionObjects.SessionUserId);
                    cmd.Parameters.AddWithValue("@OrderColumn", OrderColumn);
                    cmd.Parameters.AddWithValue("@OrderDirection", OrderDirection);
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

    public static DataSet sqlAudienceOverview(string GameGUId, int startDate, int endDate)
    {
        string strKey = String.Concat("AudienceOverview_", GameGUId, "_", startDate.ToString(), "_", endDate.ToString());

        if (HttpRuntime.Cache[strKey] == null)
        {
            using (SqlConnection con = Utils.GetNewConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("W_AudienceOverview", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@GameId", Utils.safeStr(GameGUId));
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

    public static DataSet sqlAudienceTotal(string GameGUId, int startDate, int endDate)
    {
        string strKey = String.Concat("AudienceTotal_", GameGUId, "_", startDate.ToString(), "_", endDate.ToString());

        if (HttpRuntime.Cache[strKey] == null)
        {
            using (SqlConnection con = Utils.GetNewConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("W_AudienceTotal", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@GameId", Utils.safeStr(GameGUId));
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

    public static DataSet sqlAudienceTotalWithDomain(string GameGUId, int startDate, int endDate, string domain)
    {
        string strKey = String.Concat("AudienceTotalWithDomain_", GameGUId, "_", startDate.ToString(), "_", endDate.ToString(), "_", Utils.md5(domain));

        if (HttpRuntime.Cache[strKey] == null)
        {
            using (SqlConnection con = Utils.GetNewConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("W_AudienceTotalWithDomain", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@GameId", Utils.safeStr(GameGUId));
                    cmd.Parameters.AddWithValue("@startDate", Utils.UnixTimeToDateTime(startDate));
                    cmd.Parameters.AddWithValue("@endDate", Utils.UnixTimeToDateTime(endDate));
                    cmd.Parameters.AddWithValue("@domain", domain);
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

    public static DataSet sqlTechnologyTotalWithBrowser(string GameGUId, int startDate, int endDate, string browser)
    {
        string strKey = String.Concat("TechnologyTotalWithBrowser_", GameGUId, "_", startDate.ToString(), "_", endDate.ToString(), "_", Utils.md5(browser));

        if (HttpRuntime.Cache[strKey] == null)
        {
            using (SqlConnection con = Utils.GetNewConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("W_TechnologyTotalWithBrowser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@GameId", Utils.safeStr(GameGUId));
                    cmd.Parameters.AddWithValue("@startDate", Utils.UnixTimeToDateTime(startDate));
                    cmd.Parameters.AddWithValue("@endDate", Utils.UnixTimeToDateTime(endDate));
                    cmd.Parameters.AddWithValue("@browser", browser);
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

    public static DataSet sqlTechnologyTotalWithOS(string GameGUId, int startDate, int endDate, string os)
    {
        string strKey = String.Concat("TechnologyTotalWithOS_", GameGUId, "_", startDate.ToString(), "_", endDate.ToString(), "_", Utils.md5(os));

        if (HttpRuntime.Cache[strKey] == null)
        {
            using (SqlConnection con = Utils.GetNewConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("W_TechnologyTotalWithOS", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@GameId", Utils.safeStr(GameGUId));
                    cmd.Parameters.AddWithValue("@startDate", Utils.UnixTimeToDateTime(startDate));
                    cmd.Parameters.AddWithValue("@endDate", Utils.UnixTimeToDateTime(endDate));
                    cmd.Parameters.AddWithValue("@os", os);
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

    public static DataSet sqlCountries(string GameGUId, int startDate, int endDate, int page)
    {
        string strKey = String.Concat("AudienceCountry_", GameGUId, "_", startDate.ToString(), "_", endDate.ToString(), "_", page.ToString());

        if (HttpRuntime.Cache[strKey] == null)
        {
            using (SqlConnection con = Utils.GetNewConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("W_AudienceCountryWithPager", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@GameId", Utils.safeStr(GameGUId));
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
                        HttpRuntime.Cache.Insert(String.Concat("TotalPage_",strKey), Convert.ToInt32(totalRecord.Value), null, DateTime.Now.AddMinutes(Utils.C_TIMEOUT_FORMS), System.Web.Caching.Cache.NoSlidingExpiration);
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

    public static DataSet sqlCities(string GameGUId, int startDate, int endDate)
    {
        string strKey = String.Concat("AudienceCity_", GameGUId, "_", startDate.ToString(), "_", endDate.ToString());

        if (HttpRuntime.Cache[strKey] == null)
        {
            using (SqlConnection con = Utils.GetNewConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("W_AudienceCity", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@GameId", Utils.safeStr(GameGUId));
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

    public static DataSet sqlWebRefer(string GameGUId, int startDate, int endDate, string domain, int page)
    {
        string strKey = String.Concat("AudienceWebRef_", GameGUId, "_", startDate.ToString(), "_", endDate.ToString(), "_", page.ToString(), "_", Utils.md5(domain));

        if (HttpRuntime.Cache[strKey] == null)
        {
            using (SqlConnection con = Utils.GetNewConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("W_AudienceWebReferWithPager", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@GameId", Utils.safeStr(GameGUId));
                    cmd.Parameters.AddWithValue("@startDate", Utils.UnixTimeToDateTime(startDate));
                    cmd.Parameters.AddWithValue("@endDate", Utils.UnixTimeToDateTime(endDate));
                    cmd.Parameters.AddWithValue("@domain", domain);
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

    public static DataSet sqlWebReferDomain(string GameGUId, int startDate, int endDate, int page)
    {
        string strKey = String.Concat("AudienceWebRefDomain_", GameGUId, "_", startDate.ToString(), "_", endDate.ToString(), "_", page.ToString());

        if (HttpRuntime.Cache[strKey] == null)
        {
            using (SqlConnection con = Utils.GetNewConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("W_AudienceWebReferDomainWithPager", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@GameId", Utils.safeStr(GameGUId));
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

    public static DataSet sqlBrowserName(string GameGUId, int startDate, int endDate, int page)
    {
        string strKey = String.Concat("TechnologyBrowserName_", GameGUId, "_", startDate.ToString(), "_", endDate.ToString(), "_", page.ToString());

        if (HttpRuntime.Cache[strKey] == null)
        {
            using (SqlConnection con = Utils.GetNewConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("W_TechnologyBrowserNameWithPager", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@GameId", Utils.safeStr(GameGUId));
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

    public static DataSet sqlBrowser(string GameGUId, int startDate, int endDate, string browser, int page)
    {
        string strKey = String.Concat("TechnologyBrowser_", GameGUId, "_", startDate.ToString(), "_", endDate.ToString(), "_", page.ToString(), "_", Utils.md5(browser));

        if (HttpRuntime.Cache[strKey] == null)
        {
            using (SqlConnection con = Utils.GetNewConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("W_TechnologyBrowserWithPager", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@GameId", Utils.safeStr(GameGUId));
                    cmd.Parameters.AddWithValue("@startDate", Utils.UnixTimeToDateTime(startDate));
                    cmd.Parameters.AddWithValue("@endDate", Utils.UnixTimeToDateTime(endDate));
                    cmd.Parameters.AddWithValue("@browser", browser);
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
    public static DataSet sqlDevice(string GameGUId, int startDate, int endDate, int page)
    {
        string strKey = String.Concat("TechnologyDevice_", GameGUId, "_", startDate.ToString(), "_", endDate.ToString(), "_", page.ToString());

        if (HttpRuntime.Cache[strKey] == null)
        {
            using (SqlConnection con = Utils.GetNewConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("W_TechnologyDeviceWithPager", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@GameId", Utils.safeStr(GameGUId));
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

    public static DataSet sqlOSName(string GameGUId, int startDate, int endDate, int page)
    {
        string strKey = String.Concat("TechnologyOSName_", GameGUId, "_", startDate.ToString(), "_", endDate.ToString(), "_", page.ToString());

        if (HttpRuntime.Cache[strKey] == null)
        {
            using (SqlConnection con = Utils.GetNewConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("W_TechnologyOSNameWithPager", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@GameId", Utils.safeStr(GameGUId));
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
    public static DataSet sqlOS(string GameGUId, int startDate, int endDate, string os, int page)
    {
        string strKey = String.Concat("TechnologyOS_", GameGUId, "_", startDate.ToString(), "_", endDate.ToString(), "_", page.ToString(), "_", Utils.md5(os));

        if (HttpRuntime.Cache[strKey] == null)
        {
            using (SqlConnection con = Utils.GetNewConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("W_TechnologyOSWithPager", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@GameId", Utils.safeStr(GameGUId));
                    cmd.Parameters.AddWithValue("@startDate", Utils.UnixTimeToDateTime(startDate));
                    cmd.Parameters.AddWithValue("@endDate", Utils.UnixTimeToDateTime(endDate));
                    cmd.Parameters.AddWithValue("@os", os);
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