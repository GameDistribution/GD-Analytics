using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.ComponentModel;
using System.Net.Mail;
using System.Globalization;

/// <summary>
/// Summary description for Utils
/// </summary>
public class Utils
{
	private static string C_CONSTR = "constr";

	public const int C_TIMEOUT_LOOKUPS_DAY = 365;
	public const int C_TIMEOUT_ONLINEUSERS = 50;
	public const int C_TIMEOUT_FORMS = 30;

	public const string SERVER_NAME = "s1";
	public const string SQL_USERS = "_SQL_Users_";
	public const string GEO_DATA = "_GEO_Data";
	public const string ONLINE_USERS = "_ONL_Users_";

	public static SqlConnection GetNewConnection()
	{
		return new SqlConnection(ConfigurationManager.ConnectionStrings[C_CONSTR].ToString());
	}

	public static string json_encode(Object _object)
	{
		var jss = new JavaScriptSerializer();
		//var dict = jss.Deserialize<Dictionary<string,dynamic>>(jsonText);

		string dict = jss.Serialize(_object);
		return dict;
	}

	public static dynamic json_decode<T>(String _object)
	{
		try
		{
			if (_object != null)
			{
				var jss = new JavaScriptSerializer();
				var dict = jss.Deserialize<T>(_object);
				return dict;
			}
			else
			{
				return null;
			}
		}
		catch {
			return null;
		}
	}

	public static int safeInt(object val, int def)
	{
		try
		{
			return Convert.ToInt32(val);

		}
		catch
		{
			return def;
		}
	}

	public static int safeInt(object val)
	{
		return safeInt(val, -1);
	}

	public static string safeStr(object val, string def)
	{
		try
		{
			return Convert.ToString(val);

		}
		catch
		{
			return def;
		}
	}

	public static string safeStr(object val)
	{
		return safeStr(val, "");
	}

	public static string convertDate(string tDate)
	{
		string[] ttDate = tDate.Split('.');
		return ttDate[2] + "." + ttDate[1] + "." + ttDate[0] + " 00:00:00";
	}

	public static string _POST(string key)
	{
		return HttpContext.Current.Request.Form[key];
	}

	public static string _GET(string key)
	{
		return HttpContext.Current.Request.QueryString[key];
	}

	public static string _SERVER(string key)
	{
		return HttpContext.Current.Request.ServerVariables[key];
	}

	public static string md5(string value)
	{
		MD5CryptoServiceProvider _md5 = new MD5CryptoServiceProvider();

		byte[] btr = Encoding.UTF8.GetBytes(value);
		btr = _md5.ComputeHash(btr);

		StringBuilder sb = new StringBuilder();

		foreach (byte ba in btr)
		{
			sb.Append(ba.ToString("x2").ToLower());
		}

		return sb.ToString();
	}

	/*
	#
	# Find User Dictionary Key
	#
	*/
	public static string FindUserDictionaryKey()
	{
		string[] GUIDInfo = Utils._SERVER("SERVER_NAME").Split('.');
		return String.Concat( Utils.SERVER_NAME , Utils.SQL_USERS , GUIDInfo[0]);
	}

	/*
	#
	# Check User FGSAPI Key Correct
	#
	*/
	public static bool CheckUserFGSAPIKeyCorrect()
	{
		string[] GUIDInfo = Utils._SERVER("SERVER_NAME").Split('.');
		return GUIDInfo[0].Length == 36 && GUIDInfo[1].ToLower() == Utils.SERVER_NAME;
	}

	/*
	#
	# Check User Exists
	#
	*/

	public static int checkUserExists()
	{
		string[] GUIDInfo = Utils._SERVER("SERVER_NAME").Split('.');

		if (CheckUserFGSAPIKeyCorrect())
		{
			if (SessionObjects.wincache_ucache_exists(FindUserDictionaryKey()))
			{
				return ((UserInformation)SessionObjects.wincache_ucache_get(FindUserDictionaryKey())).UserId;
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

	public static string buildQueryString(string changedKey = "", string changedKeyValue = "")
	{
		string tempQuery = "?";
		int i = 0;
		var keyFound = false;

		if (HttpContext.Current.Request.QueryString.Count > 0)
		{

			foreach (string key in HttpContext.Current.Request.QueryString.Keys)
			{
				tempQuery = tempQuery + key + "=" + (key == changedKey ? changedKeyValue : HttpContext.Current.Request.QueryString[key]) + (i < HttpContext.Current.Request.QueryString.Count - 1 ? "&" : "");
				if (key == changedKey)
				{
					keyFound = true;
				}
				i++;
			}
			if (!keyFound)
			{
				tempQuery += "&" + changedKey + "=" + changedKeyValue;
			}

		}
		else
		{
			tempQuery = tempQuery + changedKey + "=" + changedKeyValue;
		}

		return tempQuery;
	}

	public static string buildQueryString()
	{
		return (HttpContext.Current.Request.QueryString.Count > 0 ? "?" + HttpContext.Current.Request.QueryString : "");
	}

	public static DateTime UnixTimeToDateTime(double unixTimeStamp)
	{
		// Unix timestamp is seconds past epoch
		System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
		dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
		return dtDateTime;
	}

	public static double DateTimeToUnixTime(DateTime dateTime)
	{
		return (dateTime - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds;
	}

	public static int UnixTimeStamp()
	{
		TimeSpan unix_time = (System.DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
		return (int)unix_time.TotalSeconds;
	}

	public static bool validateEmail(string email)
	{
		try
		{
			MailAddress m = new MailAddress(email);
			return true;
		}
		catch (FormatException)
		{
			return false;
		}
	}

	public static string safeNumber(object val)
	{
		return safeNumber(val, "0.00");
	}

	public static string safeNumber(object val, string def)
	{
		try
		{
			return String.Format(CultureInfo.InvariantCulture, "{0:0.00}", val);
		}
		catch
		{
			return def;
		}
	}

	public static bool safeBool(object val)
	{
		return safeBool(val, false);
	}

	public static bool safeBool(object val, bool def)
	{
		try
		{
			return (Convert.ToBoolean(val) ? true : false);

		}
		catch
		{
			return def;
		}
	}

	public static DateTime safeTime(object val, string def)
	{
		try
		{
			return Convert.ToDateTime(val);

		}
		catch
		{
			return Convert.ToDateTime(def);
		}
	}

	public static string safeStrTime(object val, string def)
	{
		try
		{
			return Convert.ToString(val);
		}
		catch
		{
			return def;
		}
	}

	public static string safeStrTime(object val)
	{
		return safeStrTime(val, "00:00");
	}

	public static string safeStrDateTime(object val)
	{
		return safeStrDateTime(val, "00.00.0000 00:00:00");
	}

	public static string safeStrDateTime(object val, string def)
	{
		try
		{
			return Convert.ToDateTime(val).ToString("dd.MM.yyyy HH:mm:ss");
		}
		catch
		{
			return def;
		}
	}

	public static string safeStrDate(object val)
	{
		return safeStrDate(val, "00.00.0000");
	}

	public static string safeStrDate(object val, string def)
	{
		try
		{
			return Convert.ToDateTime(val).ToString("dd.MM.yyyy");
		}
		catch
		{
			return def;
		}
	}

	public static string getDate()
	{
		return DateTime.Today.ToString("dd.MM.yyyy");
	}

	public static string getDateTime()
	{
		return DateTime.Today.ToString("dd.MM.yyyy HH:mm:ss");
	}

	public Utils()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}

