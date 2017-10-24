using System;
using System.Web;
using System.Data;
using System.Collections;
using System.Xml;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data.SqlClient;

/// <summary>
/// Summary description for SessionObjects
/// </summary>
/// 
public class SessionObjects
{
    public static ExpiringDictionary<string, object> DUserTables = new ExpiringDictionary<string, object>(new TimeSpan(0, 20, 0));    

    public static void wincache_ucache_clear()
    {
        DUserTables.Clear();
    }

    public static void wincache_ucache_clear(ExpiringDictionary<string, object> _DictionaryTable)
    {
        _DictionaryTable.Clear();
    }

    public static bool wincache_ucache_exists(String key)
    {
        return DUserTables.ContainsKey(key);
    }

    public static bool wincache_ucache_exists(ExpiringDictionary<string, object> _DictionaryTable, String key)
    {
        return _DictionaryTable.ContainsKey(key);
    }

    public static object wincache_ucache_get(String key)
    {
        object vValue=null;
        if (DUserTables.TryGetValue(key, out vValue))
        {
            return vValue;
        }
        return vValue;
    }

    public static object wincache_ucache_get(ExpiringDictionary<string, object> _DictionaryTable, String key)
    {
        object vValue = null;
        if (_DictionaryTable.TryGetValue(key, out vValue))
        {
            return vValue;
        }
        return vValue;
    }

    public static bool wincache_ucache_set(String key, object value, int ttl)
    {
        try
        {
            if (ttl==0)
                DUserTables.Set(key, value);
            else
                DUserTables.Set(key, value, new TimeSpan(0, 0, ttl));
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool wincache_ucache_set(ExpiringDictionary<string, object> _DictionaryTable, String key, object value, int ttl)
    {
        try
        {
            if (ttl == 0)
                _DictionaryTable.Set(key, value);
            else
                _DictionaryTable.Set(key, value, new TimeSpan(0, 0, ttl));
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool wincache_ucache_add(String key, object value, int ttl)
    {
        try
        {
            if (ttl == 0)
                DUserTables.Add(key, value);
            else
                DUserTables.Add(key, value, new TimeSpan(0, 0, ttl));
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool wincache_ucache_add(ExpiringDictionary<string, object> _DictionaryTable, String key, object value, int ttl)
    {
        try
        {
            if (ttl == 0)
                _DictionaryTable.Add(key, value);
            else
                _DictionaryTable.Add(key, value, new TimeSpan(0, 0, ttl));
            return true;
        }
        catch
        {
            return false;
        }
    }

    /*
        User Sessions     
     */
    private const string C_UserId = "session_userid";
    private const string C_UserRegId = "session_userregid";
    private const string C_UserInfo = "session_userinfo";

    public static int SessionUserId
    {
        get
        {
            try
            {
                return Convert.ToInt32(HttpContext.Current.Session[C_UserId]);
            }
            catch
            {
                return 0;
            }
        }
        set { HttpContext.Current.Session[C_UserId] = value; }
    }

    public static string SessionUserRegId
    {
        get
        {
            try
            {
                return Convert.ToString(HttpContext.Current.Session[C_UserRegId]);
            }
            catch
            {
                return "";
            }
        }
        set { HttpContext.Current.Session[C_UserRegId] = value; }
    }

    public static DataRow SessionUserInfo
    {
        get
        {
            try
            {
                return (DataRow)HttpContext.Current.Session[C_UserInfo];
            }
            catch
            {
                return null;
            }
        }
        set { HttpContext.Current.Session[C_UserInfo] = value; }
    }

    public static bool checkUserSession()
    {
        return SessionUserId > 0;
    }

    public SessionObjects()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void Start()
    {
        ExpiringDictionary<string, object>.onItemExpired += wincache_ucache_expired;
        System.Diagnostics.Debug.WriteLine("FGS Session is created.");
    }

    public static void wincache_ucache_expired(object e)
    {
        Dictionary<string, string> OnlineUserValue = (Dictionary<string, string>)e;

        using (SqlConnection con = Utils.GetNewConnection())
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("STAT_setTimePlay", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ServerName", Utils.SERVER_NAME);
                cmd.Parameters.AddWithValue("@UserId", OnlineUserValue["UserId"]);
                cmd.Parameters.AddWithValue("@OnlineGameStatsId", OnlineUserValue["Id"]);
                cmd.Parameters.AddWithValue("@GameId", OnlineUserValue["GameId"]);
                cmd.Parameters.AddWithValue("@TimePlayed", (Utils.UnixTimeStamp()-Convert.ToInt32(OnlineUserValue["Time"])));
                cmd.ExecuteNonQuery();
            }
        }

        System.Diagnostics.Debug.WriteLine("wincache_ucache_expired : " + e.ToString());
    }

}
