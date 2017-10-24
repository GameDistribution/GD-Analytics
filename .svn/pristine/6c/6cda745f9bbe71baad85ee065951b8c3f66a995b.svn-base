using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public partial class _Default : System.Web.UI.Page
{
    private static int TimeToLive = 30;

    /*     
	#
	# Responser Types
	#
    */
    struct CustomResponder
    {
        public string act;
        public string res;
        public string custom;
    }

    struct ErrorResponder
    {
        public string act;
        public string res;
        public string error;
    }

    struct Responder
    {
        public string act;
        public string res;
    }

    struct DataResponder
    {
        public string act;
        public string res;
        public object dat;
    }

    struct structCALLJS
    {
        public string jsdata;
    }

    struct structOPENURL
    {
        public bool reopen;
        public string url;
        public string target;
    }

    /*     
	#
	# FGSAPI Json Type
	#
    */
    public class FGSAPIAction
    {
        public dynamic value { get; set; }
        public string action { get; set; }
        public string result { get; set; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (SessionObjects.wincache_ucache_get(Utils.SERVER_NAME + "_fillUsersToCache") == null)
        {
            fillUsersToCache();
        }

        DoAction();
    }

    /*     
	#
	# Do Actions
	#
    */
    public static void DoAction()
    {
        FGSAPIAction FGSAction = Utils.json_decode<FGSAPIAction>(Utils._POST("act"));
        // {"value":[{"key":"Level_1","value":1}],"action":"custom"}

        if (FGSAction != null)
        {
            //			HttpContext.Current.Response.Write("ok");

            try
            {
                switch (FGSAction.action)
                {
                    case "ping":
                        if (FGSAction.value == "ping")
                        {
                            //$FGSCache->RequestOpenURL("");
                            //$FGSCache->RequestCallJS("");
                            ping();
                        }
                        break;
                    case "visit":
                        if (Utils.safeInt(FGSAction.value) > -1)
                        {
                            visit(Utils.safeInt(FGSAction.value));
                        }
                        break;
                    case "play":
                        if (Utils.safeInt(FGSAction.value) > 0)
                        {
                            play(Utils.safeInt(FGSAction.value));
                        }
                        break;
                    case "cbp":
                        if (Utils.safeInt(FGSAction.value) > 0)
                        {
                            setCallBackParam(Utils.safeInt(FGSAction.value), FGSAction.result);
                        }
                        break;
                    case "custom":
                        var customValue = (Dictionary<string, object>)(FGSAction.value[0]);
                        if (Utils.safeInt(customValue["value"]) > 0)
                        {
                            custom(Utils.safeStr(customValue["key"]), Utils.safeInt(customValue["value"]));
                        }
                        break;
                    default:
                        RequestVisit();
                        break;
                }
            }
            catch (Exception e)
            {
                //HttpContext.Current.Response.Write("ok"+e);
                RequestVisit();
            }
        }
        else
        {
            RequestVisit();
        }
    }

    /*     
	#
	# Responser
	#
    */
    public static void Responser(String _act, String _res, object _dat)
    {
        DataResponder _Responser = new DataResponder();
        _Responser.act = _act;
        _Responser.res = _res;
        _Responser.dat = _dat;
        HttpContext.Current.Response.Write(Utils.json_encode(_Responser));
    }

    public static void Responser(String _act, String _res, String _custom)
    {
        CustomResponder _Responser = new CustomResponder();
        _Responser.act = _act;
        _Responser.res = _res;
        _Responser.custom = _custom;
        HttpContext.Current.Response.Write(Utils.json_encode(_Responser));
    }

    public static void Responser(String _act, String _res)
    {
        Responder _Responser = new Responder();
        _Responser.act = _act;
        _Responser.res = _res;
        HttpContext.Current.Response.Write(Utils.json_encode(_Responser));
    }

    public static void Responser(String _act, String _res, int _error)
    {
        ErrorResponder _Responser = new ErrorResponder();
        _Responser.act = _act;
        _Responser.res = _res;
        _Responser.error = getCallBackMessage(_error);
        HttpContext.Current.Response.Write(Utils.json_encode(_Responser));
    }

    /*     
    #
    # Callback Param Messages
    #
    */
    private static string getCallBackMessage(int value)
    {
        if (HttpRuntime.Cache["getCallBackMessage"] == null)
        {
            Dictionary<int, string> Msg = new Dictionary<int, string>();
            Msg.Add(1000, "ping");
            Msg.Add(1100, "visit");
            Msg.Add(1200, "play");
            Msg.Add(1300, "custom");
            Msg.Add(1400, "req url");
            Msg.Add(1500, "idle open");
            Msg.Add(1501, "clear url");
            Msg.Add(1502, "openned url");
            Msg.Add(1600, "idle js");
            Msg.Add(1601, "called js");
            Msg.Add(1602, "error js");
            Msg.Add(5000, "Only alpnum chars and underscore allowed!");
            HttpRuntime.Cache.Insert("getCallBackMessage", Msg, null, DateTime.Now.AddSeconds(Utils.C_TIMEOUT_LOOKUPS_DAY), System.Web.Caching.Cache.NoSlidingExpiration);
            return Msg[value];
        }
        else
        {
            return ((Dictionary<int, string>)HttpRuntime.Cache["getCallBackMessage"])[value];
        }
    }

    /*
	#
	# setCallBackParam
	#
    */

    public static void setCallBackParam(int cbp, String result)
    {
        int UserId = checkUserExists();

        if ((Utils._POST("gid").Length == 32) && UserId > 0)
        {

            string OnlineUserKey = Utils.SERVER_NAME + Utils.ONLINE_USERS + Utils.md5(Utils._POST("sid") + Utils._SERVER("REMOTE_ADDR") + Utils._POST("gid"));
            if (SessionObjects.wincache_ucache_exists(OnlineUserKey))
            {
                Dictionary<string, string> OnlineUserValue = (Dictionary<string, string>)SessionObjects.wincache_ucache_get(OnlineUserKey);
                OnlineUserValue["CallBack"] = getCallBackMessage(cbp) + ":" + result;
                SessionObjects.wincache_ucache_set(OnlineUserKey, OnlineUserValue, TimeToLive);

                Responser("ping", "pong");
            }
            else
            {
                RequestVisit();
            }
        }
        else
        {
            RequestVisit();
        }
    }

    /*
	#
	# Request Visit 
	#
    */
    public static void RequestVisit()
    {
        Responser("cmd", "visit");
    }

    /*
	#
	# Check User Exists
	#
    */

    public static int checkUserExists()
    {
        string[] GUIDInfo = Utils._SERVER("SERVER_NAME").Split('.');


        if (GUIDInfo[0].Length == 36 && GUIDInfo[1].ToLower() == Utils.SERVER_NAME)
        {
            if (SessionObjects.wincache_ucache_exists(Utils.SERVER_NAME + Utils.SQL_USERS + GUIDInfo[0]))
            {
                return (int)SessionObjects.wincache_ucache_get(Utils.SERVER_NAME + Utils.SQL_USERS + GUIDInfo[0]);
            }
            else
            {
                return 0;
            }
        }
        else
        {
            return 0;
        }
    }

    /*
    #
    # Fill Hashtable Users
    # 
    */
    public static void fillUsersToCache()
    {
        SessionObjects.wincache_ucache_set(Utils.SERVER_NAME + "_fillUsersToCache", true, 0);

        using (SqlConnection con = Utils.GetNewConnection())
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("STAT_getUsersList", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ServerName", Utils.SERVER_NAME);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        SessionObjects.wincache_ucache_set(Utils.SERVER_NAME + Utils.SQL_USERS + dr["RegId"].ToString().ToLower(), Convert.ToInt32(dr["Id"]), 0);
                    }
                }

            }
        }

    }

    /*
	#
	# Call JS
	#
    */
    public static void RequestCallJS(string JS)
    {
        int UserId = checkUserExists();

        if ((Utils._POST("gid").Length == 32) && UserId > 0)
        {

            string OnlineUserKey = Utils.SERVER_NAME + Utils.ONLINE_USERS + Utils.md5(Utils._POST("sid") + Utils._SERVER("REMOTE_ADDR") + Utils._POST("gid"));
            if (SessionObjects.wincache_ucache_exists(OnlineUserKey))
            {
                Dictionary<string, string> OnlineUserValue = (Dictionary<string, string>)SessionObjects.wincache_ucache_get(OnlineUserKey);
                OnlineUserValue["CallBack"] = "req JS";
                SessionObjects.wincache_ucache_set(OnlineUserKey, OnlineUserValue, TimeToLive);

                structCALLJS _dat = new structCALLJS();
                _dat.jsdata = "navigator.appName.toString";
                Responser("cmd", "js", _dat);

            }
            else
            {
                RequestVisit();
            }
        }
        else
        {
            RequestVisit();
        }
    }

    /*
	#
	# Open URL
	#
    */
    public static void RequestOpenURL(string url)
    {
        int UserId = checkUserExists();

        if ((Utils._POST("gid").Length == 32) && UserId > 0)
        {

            string OnlineUserKey = Utils.SERVER_NAME + Utils.ONLINE_USERS + Utils.md5(Utils._POST("sid") + Utils._SERVER("REMOTE_ADDR") + Utils._POST("gid"));
            if (SessionObjects.wincache_ucache_exists(OnlineUserKey))
            {
                Dictionary<string, string> OnlineUserValue = (Dictionary<string, string>)SessionObjects.wincache_ucache_get(OnlineUserKey);
                OnlineUserValue["CallBack"] = "req url";
                SessionObjects.wincache_ucache_set(OnlineUserKey, OnlineUserValue, TimeToLive);

                structOPENURL _dat = new structOPENURL();
                _dat.reopen = false;
                _dat.url = "http://www.y8.fm";
                _dat.target = "_blank";
                Responser("cmd", "url", _dat);

            }
            else
            {
                RequestVisit();
            }
        }
        else
        {
            RequestVisit();
        }
    }

    /*
	#
	# Custom Count
	#
    */
    public static void custom(string customKey, int customTimes)
    {
        int UserId = checkUserExists();
        if (Regex.Match(customKey, @"^[A-Za-z0-9_]+$").Success)
        {
            if ((Utils._POST("gid").Length == 32) && UserId > 0)
            {

                string OnlineUserKey = Utils.SERVER_NAME + Utils.ONLINE_USERS + Utils.md5(Utils._POST("sid") + Utils._SERVER("REMOTE_ADDR") + Utils._POST("gid"));
                if (SessionObjects.wincache_ucache_exists(OnlineUserKey))
                {
                    Dictionary<string, string> OnlineUserValue = (Dictionary<string, string>)SessionObjects.wincache_ucache_get(OnlineUserKey);
                    OnlineUserValue["CallBack"] = "custom";
                    SessionObjects.wincache_ucache_set(OnlineUserKey, OnlineUserValue, TimeToLive);

                    /*
                        #
                        # Save Played Value
                        #
                        @ServerName varchar(10),
                        @UserId int,
                        @OnlineGameStatsId bigint,
                        @GameId varchar(32),
                        @CustomKey varchar(10),
                        @CustomCount int
                    */
                    using (SqlConnection con = Utils.GetNewConnection())
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("STAT_setCustomCount", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ServerName", Utils.SERVER_NAME);
                            cmd.Parameters.AddWithValue("@UserId", OnlineUserValue["UserId"]);
                            cmd.Parameters.AddWithValue("@OnlineGameStatsId", OnlineUserValue["Id"]);
                            cmd.Parameters.AddWithValue("@GameId", OnlineUserValue["GameId"]);
                            cmd.Parameters.AddWithValue("@CustomKey", customKey);
                            cmd.Parameters.AddWithValue("@CustomCount", customTimes);

                            SqlParameter prid = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
                            prid.Direction = ParameterDirection.ReturnValue;

                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                DataSet ds = new DataSet();
                                da.Fill(ds);

                                //if (Convert.ToInt32(prid.Value) != 0)

                            }
                        }
                    }

                    /*
					#
					# Send Response
					#
                    */
                    Responser("custom", Utils._POST("sid"), customKey);

                }
                else
                {
                    RequestVisit();
                }
            }
            else
            {
                RequestVisit();
            }
        }
        else
        { // Check for alpha number chars
            /*
			#
			# Send Response
			#
            */
            Responser("custom", Utils._POST("sid"), 5000);
        }
    }

    /*
	#
	# Play
	#
    */
    public static void play(int playTimes)
    {
        int UserId = checkUserExists();

        if ((Utils._POST("gid").Length == 32) && UserId > 0)
        {

            string OnlineUserKey = Utils.SERVER_NAME + Utils.ONLINE_USERS + Utils.md5(Utils._POST("sid") + Utils._SERVER("REMOTE_ADDR") + Utils._POST("gid"));
            if (SessionObjects.wincache_ucache_exists(OnlineUserKey))
            {
                Dictionary<string, string> OnlineUserValue = (Dictionary<string, string>)SessionObjects.wincache_ucache_get(OnlineUserKey);
                OnlineUserValue["CallBack"] = "play";
                SessionObjects.wincache_ucache_set(OnlineUserKey, OnlineUserValue, TimeToLive);

                /*
                #
                # Save Played Value
                #
                @ServerName varchar(10),
                @UserId int,
                @OnlineGameStatsId bigint,
                @GameId varchar(32),
                @Play int				
                */
                using (SqlConnection con = Utils.GetNewConnection())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("STAT_setPlayGame", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ServerName", Utils.SERVER_NAME);
                        cmd.Parameters.AddWithValue("@UserId", OnlineUserValue["UserId"]);
                        cmd.Parameters.AddWithValue("@OnlineGameStatsId", OnlineUserValue["Id"]);
                        cmd.Parameters.AddWithValue("@GameId", OnlineUserValue["GameId"]);
                        cmd.Parameters.AddWithValue("@Play", playTimes);

                        SqlParameter prid = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
                        prid.Direction = ParameterDirection.ReturnValue;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            //if (Convert.ToInt32(prid.Value) != 0)

                        }
                    }
                }

                /*
				#
				# Send Response
				#
                */
                Responser("play", Utils._POST("sid"));

            }
            else
            {
                RequestVisit();
            }
        }
        else
        {
            RequestVisit();
        }
    }

    /*
	#
	# Visit
	#
    */
    public static void visit(int visitTimes)
    {
        int UserId = checkUserExists();
        if (visitTimes < 50)
        {
            if ((Utils._POST("gid").Length == 32) && UserId > 0)
            {
                string OnlineUserKey = Utils.SERVER_NAME + Utils.ONLINE_USERS + Utils.md5(Utils._POST("sid") + Utils._SERVER("REMOTE_ADDR") + Utils._POST("gid"));
                Dictionary<string, string> OnlineUserValue = new Dictionary<string, string>();
                OnlineUserValue["UserId"] = UserId.ToString();
                OnlineUserValue["GameId"] = Utils._POST("gid");
                OnlineUserValue["Action"] = Utils._POST("act");
                OnlineUserValue["APIVer"] = "v1";
                OnlineUserValue["WebRefer"] = Utils._POST("ref");
                OnlineUserValue["Refer"] = Utils._SERVER("HTTP_REFERER");
                OnlineUserValue["ClientIP"] = Utils._SERVER("REMOTE_ADDR");
                OnlineUserValue["Visit"] = visitTimes.ToString();
                OnlineUserValue["WebAgent"] = Utils._SERVER("HTTP_USER_AGENT");
                OnlineUserValue["CallBack"] = "visit";

                LookupService ls = new LookupService("C:/VStudioC#/Fgs/geo/GeoIPCity.dat", LookupService.GEOIP_STANDARD);
                Location geo = ls.getLocation("88.247.103.59");
                OnlineUserValue["City"] = geo.city;
                OnlineUserValue["Country"] = geo.countryName;
                /*
                OnlineUserValue["City"] = "";
                OnlineUserValue["Country"] = "";
                */
                OnlineUserValue["Id"] = InsertRefererTable(OnlineUserValue).ToString();
                SessionObjects.wincache_ucache_set(OnlineUserKey, OnlineUserValue, TimeToLive);
                /*
                #
                # Send Response
                #
                */
                Responser("visit", Utils._POST("sid"));
            }
            else
            {
                RequestVisit();
            }
        }
        else
        {
            Responser("ping", "pong");
        }
    }

    private static int InsertRefererTable(Dictionary<string, string> OnlineUserValue)
    {
        /*
	        @ServerName varchar(10),
	        @UserId int,
	        @GameId varchar(32),
	        @ClientIP varchar(20),
	        @Country varchar(20),
	        @City varchar(20),
	        @Refer varchar(500),
	        @WebRefer varchar(500),
	        @Visit int,
	        @WebAgent varchar(500)         
         */
        using (SqlConnection con = Utils.GetNewConnection())
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("STAT_setOnlineGames", con))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ServerName", Utils.SERVER_NAME);
                    cmd.Parameters.AddWithValue("@UserId", Utils.safeInt(OnlineUserValue["UserId"]));
                    cmd.Parameters.AddWithValue("@GameId", Utils.safeStr(OnlineUserValue["GameId"]));
                    cmd.Parameters.AddWithValue("@ClientIP", Utils.safeStr(OnlineUserValue["ClientIP"]));
                    cmd.Parameters.AddWithValue("@Country", Utils.safeStr(OnlineUserValue["Country"]));
                    cmd.Parameters.AddWithValue("@City", Utils.safeStr(OnlineUserValue["City"]));
                    cmd.Parameters.AddWithValue("@Refer", Utils.safeStr(OnlineUserValue["Refer"]));
                    cmd.Parameters.AddWithValue("@WebRefer", Utils.safeStr(OnlineUserValue["WebRefer"]));
                    cmd.Parameters.AddWithValue("@Visit", Utils.safeInt(OnlineUserValue["Visit"]));
                    cmd.Parameters.AddWithValue("@WebAgent", Utils.safeStr(OnlineUserValue["WebAgent"]));
                    SqlParameter prid = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
                    prid.Direction = ParameterDirection.ReturnValue;
                    cmd.ExecuteNonQuery();
                    return Convert.ToInt32(prid.Value);
                }
                catch
                {
                    return 0;
                }
            }
        }

    }

    /*
	#
	# Ping
	#
    */
    public static void ping()
    {
        int UserId = checkUserExists();

        if ((Utils._POST("gid").Length == 32) && UserId > 0)
        {

            string OnlineUserKey = Utils.SERVER_NAME + Utils.ONLINE_USERS + Utils.md5(Utils._POST("sid") + Utils._SERVER("REMOTE_ADDR") + Utils._POST("gid"));
            if (SessionObjects.wincache_ucache_exists(OnlineUserKey))
            {
                Dictionary<string, string> OnlineUserValue = (Dictionary<string, string>)SessionObjects.wincache_ucache_get(OnlineUserKey);
                OnlineUserValue["CallBack"] = "ping";
                SessionObjects.wincache_ucache_set(OnlineUserKey, OnlineUserValue, TimeToLive);
                /*
				#
				# Send Response
				#
                */
                Responser("ping", "pong");

            }
            else
            {
                //HttpContext.Current.Response.Write(OnlineUserKey);
                //Responser("ping", "pong");
                RequestVisit();
            }
        }
        else
        {
            RequestVisit();
        }
    }
}