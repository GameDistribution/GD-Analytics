using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class vdata_Tools : System.Web.UI.Page
{
	private const int DefaultRowLimit = 20;

	struct structBlockSites
	{
		public string c1;
		public string c2;
		public string c3;
		public string c4;
	}

	struct structBannerConfig
	{
		public string c1;
		public string c2;
		public string c3;
		public string c4;
		public string c5;
		public string c6;
		public string c7;
		public string c8;
		public string c9;
	}

    struct structBannerFilterCountry
    {
        public int c1;
        public string c2;
        public bool c3;
    }

    struct structBannerFilterDomain
    {
        public int c1;
        public string c2;
    }

    struct structBannerFilterState
    {
        public int c1;
        public bool c2;
        public bool c3;
        public bool c4;
        public bool c5;
        public int c6;
    }

    struct structBannerFilter
    {
        public structBannerFilterCountry[] c1;
        public structBannerFilterDomain[] c2;
        public structBannerFilterState c3;
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

			if (GameGUId.Length == 32 && Action.Count() > 0)
			{
				_classJSON = new classJSON();

				for (int key = 0; key < Action.Count(); key++)
				{
					switch (Action[key])
					{
						case "BlockSites":
							_classJSON.metrics.Add("BlockSites", jsonBlockSites(SessionObjects.SessionUserId, GameGUId ));
							break;
						case "BlockBannerSites":
							_classJSON.metrics.Add("BlockSites", jsonBlockBannerSites(SessionObjects.SessionUserId, GameGUId));
							break;
						case "BannerConfig":
							_classJSON.metrics.Add("BannerConfig", jsonBannerConfig(SessionObjects.SessionUserId, GameGUId));
							break;
                        case "BannerFilter":
                            _classJSON.metrics.Add("BannerFilter", jsonBannerFilter(SessionObjects.SessionUserId, GameGUId));
                            break;
                        case "SharedGames":
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


	private static structBlockSites[] jsonBlockSites(int UserId, string GameGUId)
	{
		return BlockedSites(sqlBlockedGames(UserId, GameGUId).Tables[0]);
	}

	private static structBlockSites[] BlockedSites(DataTable _DataTable)
	{
		int i = 0;
		// Set MetricsCount Size
		structBlockSites[] _MetricsCount = new structBlockSites[(_DataTable.Rows.Count < DefaultRowLimit ? _DataTable.Rows.Count : DefaultRowLimit)];
		foreach (DataRow dr in _DataTable.Rows)
		{
			_MetricsCount[i].c1 = Convert.ToString(dr["GameMD5Id"]);
			_MetricsCount[i].c2 = Convert.ToString(dr["Title"]);
			_MetricsCount[i].c3 = Convert.ToString(dr["BlockWebSite"]);
			_MetricsCount[i].c4 = Convert.ToString(dr["BlockedDate"]);
			i++;
		}
		return _MetricsCount;
	}

	public static DataSet sqlBlockedGames(int UserId, string GameGUId)
	{
		//string strKey = String.Concat("BlockedGames_", UserId.ToString(), "_" , GameGUId);

		//if (HttpRuntime.Cache[strKey] == null)
		//{
			using (SqlConnection con = Utils.GetNewConnection())
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("W_GetYourBlockedGames", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@UserId", SessionObjects.SessionUserId);
					cmd.Parameters.AddWithValue("@GameId", GameGUId);
					using (SqlDataAdapter da = new SqlDataAdapter(cmd))
					{
						DataSet ds = new DataSet();
						da.Fill(ds);
						//HttpRuntime.Cache.Insert(strKey, ds, null, DateTime.Now.AddMinutes(Utils.C_TIMEOUT_FORMS), System.Web.Caching.Cache.NoSlidingExpiration);
						return ds;
					}

				}
			}
		//}
		//else
		//{
		//    return (DataSet)HttpRuntime.Cache[strKey];
		//}
	}

	private static structBlockSites[] jsonBlockBannerSites(int UserId, string GameGUId)
	{
		return BlockedBannerSites(sqlBlockedBannerGames(UserId, GameGUId).Tables[0]);
	}

	private static structBlockSites[] BlockedBannerSites(DataTable _DataTable)
	{
		int i = 0;
		// Set MetricsCount Size
		structBlockSites[] _MetricsCount = new structBlockSites[(_DataTable.Rows.Count < DefaultRowLimit ? _DataTable.Rows.Count : DefaultRowLimit)];
		foreach (DataRow dr in _DataTable.Rows)
		{
			_MetricsCount[i].c1 = Convert.ToString(dr["GameMD5Id"]);
			_MetricsCount[i].c2 = Convert.ToString(dr["Title"]);
			_MetricsCount[i].c3 = Convert.ToString(dr["BlockWebSite"]);
			_MetricsCount[i].c4 = Convert.ToString(dr["BlockedDate"]);
			i++;
		}
		return _MetricsCount;
	}

	public static DataSet sqlBlockedBannerGames(int UserId, string GameGUId)
	{
		//string strKey = String.Concat("BlockedGames_", UserId.ToString(), "_" , GameGUId);

		//if (HttpRuntime.Cache[strKey] == null)
		//{
		using (SqlConnection con = Utils.GetNewConnection())
		{
			con.Open();
			using (SqlCommand cmd = new SqlCommand("W_GetYourBlockedBannerGames", con))
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@UserId", SessionObjects.SessionUserId);
				cmd.Parameters.AddWithValue("@GameId", GameGUId);
				using (SqlDataAdapter da = new SqlDataAdapter(cmd))
				{
					DataSet ds = new DataSet();
					da.Fill(ds);
					//HttpRuntime.Cache.Insert(strKey, ds, null, DateTime.Now.AddMinutes(Utils.C_TIMEOUT_FORMS), System.Web.Caching.Cache.NoSlidingExpiration);
					return ds;
				}

			}
		}
		//}
		//else
		//{
		//    return (DataSet)HttpRuntime.Cache[strKey];
		//}
	}

	private static structBannerConfig jsonBannerConfig(int UserId, string GameGUId)
	{
		return BannerConfig(sqlBannerConfig(UserId, GameGUId).Tables[0]);
	}

	private static structBannerConfig BannerConfig(DataTable _DataTable)
	{
		int i = 0;
		// Set MetricsCount Size
		/*
	@BannerText varchar(32)='', 
	@BGColor varchar(6)='000000', 
	@Width int = 500, 
	@Height int = 350, 
	@Timeout int = 17, 
	@Autosize bit = 1, 
	@Active bit = 1, 
	@Url varchar(500)=''         
		 */
		structBannerConfig _MetricsCount = new structBannerConfig();
		foreach (DataRow dr in _DataTable.Rows)
		{
			_MetricsCount.c1 = Convert.ToString(dr["BannerText"]);
			_MetricsCount.c2 = Convert.ToString(dr["BGColor"]);
			_MetricsCount.c3 = Convert.ToString(dr["Width"]);
			_MetricsCount.c4 = Convert.ToString(dr["Height"]);
			_MetricsCount.c5 = Convert.ToString(dr["Timeout"]);
			_MetricsCount.c6 = Convert.ToString(dr["Autosize"]);
			_MetricsCount.c7 = Convert.ToString(dr["Active"]);
			_MetricsCount.c8 = Convert.ToString(dr["Url"]);
			_MetricsCount.c9 = Convert.ToString(dr["ConfigDate"]);
			i++;
		}
		return _MetricsCount;
	}

	public static DataSet sqlBannerConfig(int UserId, string GameGUId)
	{
		//string strKey = String.Concat("BlockedGames_", UserId.ToString(), "_" , GameGUId);

		//if (HttpRuntime.Cache[strKey] == null)
		//{
		using (SqlConnection con = Utils.GetNewConnection())
		{
			con.Open();
			using (SqlCommand cmd = new SqlCommand("W_GetYourBannerConfig", con))
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@UserId", SessionObjects.SessionUserId);
				cmd.Parameters.AddWithValue("@GameId", GameGUId);
				using (SqlDataAdapter da = new SqlDataAdapter(cmd))
				{
					DataSet ds = new DataSet();
					da.Fill(ds);
					//HttpRuntime.Cache.Insert(strKey, ds, null, DateTime.Now.AddMinutes(Utils.C_TIMEOUT_FORMS), System.Web.Caching.Cache.NoSlidingExpiration);
					return ds;
				}

			}
		}
		//}
		//else
		//{
		//    return (DataSet)HttpRuntime.Cache[strKey];
		//}
	}

    private static structBannerFilter jsonBannerFilter(int UserId, string GameGUId)
    {
        DataSet ds = sqlBannerFilter(UserId, GameGUId);
        structBannerFilter _BannerFilter = new structBannerFilter();
        _BannerFilter.c1 = BannerFilterCountry(ds.Tables[0]);
        _BannerFilter.c2 = BannerFilterDomain(ds.Tables[1]);
        _BannerFilter.c3 = BannerFilterState(ds.Tables[2]);
        return _BannerFilter;
    }

    private static structBannerFilterCountry[] BannerFilterCountry(DataTable _DataTable)
    {
        int i = 0;
        structBannerFilterCountry[] _MetricsCount = new structBannerFilterCountry[_DataTable.Rows.Count];
        foreach (DataRow dr in _DataTable.Rows)
        {
            _MetricsCount[i].c1 = Utils.safeInt(dr["Id"]);
            _MetricsCount[i].c2 = Utils.safeStr(dr["Country"]);
            _MetricsCount[i].c3 = Utils.safeInt(dr["CountryId"]) == Utils.safeInt(dr["Id"]);
            i++;
        }
        return _MetricsCount;
    }

    private static structBannerFilterDomain[] BannerFilterDomain(DataTable _DataTable)
    {
        int i = 0;
        structBannerFilterDomain[] _MetricsCount = new structBannerFilterDomain[_DataTable.Rows.Count];
        foreach (DataRow dr in _DataTable.Rows)
        {
            _MetricsCount[i].c1 = Utils.safeInt(dr["Id"]);
            _MetricsCount[i].c2 = Utils.safeStr(dr["Domain"]);
            i++;
        }
        return _MetricsCount;
    }

    private static structBannerFilterState BannerFilterState(DataTable _DataTable)
    {
        structBannerFilterState _MetricsCount = new structBannerFilterState();
        foreach (DataRow dr in _DataTable.Rows)
        {
            _MetricsCount.c1 = Utils.safeInt(dr["Id"]);
            _MetricsCount.c2 = Utils.safeBool(dr["CountryState"]);
            _MetricsCount.c3 = Utils.safeBool(dr["DomainState"]);
            _MetricsCount.c4 = Utils.safeBool(dr["BannerEnable"]);
            _MetricsCount.c5 = Utils.safeBool(dr["PreRoll"]);
            _MetricsCount.c6 = Utils.safeInt(dr["ShowAfterTime"]);
            break;
        }
        return _MetricsCount;
    }

    public static DataSet sqlBannerFilter(int UserId, string GameGUId)
    {
        using (SqlConnection con = Utils.GetNewConnection())
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("W_GetBannerFilter", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", SessionObjects.SessionUserId);
                cmd.Parameters.AddWithValue("@GameId", GameGUId);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds;
                }

            }
        }
    }

}