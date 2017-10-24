using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class getOnlineUsers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        /*
        string[] mykey = new string[SessionObjects.DUserTables.Keys.Count];
        SessionObjects.DUserTables.Keys.CopyTo(mykey,0);
        string htmlBody = "<table cellspacing=0 cellpadding=0 border=1>";
        int i = 0;
        int SQLUsers = 0;
        int ONLUsers = 0;
        for (i = 0; i < SessionObjects.DUserTables.Count; i++ )
        {
            if (mykey[i].Substring(0, 12) == "s1_SQL_Users")
            {
                SQLUsers++;
            }
            if (mykey[i].Substring(0, 12) == "s1_ONL_Users")
            {
                ONLUsers++;
            }
            htmlBody += ("<tr><td>" + mykey[i] + "</td><td>" + SessionObjects.DUserTables[mykey[i]].ToString() + "</td></tr>");
        }
        Response.Write(htmlBody+"</table><br>");
        htmlBody = "<table cellspacing=0 cellpadding=0 border=1><tr><td>SQL Users</td><td>ONL Users</td></tr><tr><td>" + SQLUsers.ToString() + "</td><td>" + ONLUsers.ToString() + "</td></tr></table>";
        Response.Write(htmlBody);
        */
        /*
        LookupService ls = new LookupService("C:/VStudioC#/Fgs/geo/GeoIPCity.dat", LookupService.GEOIP_MEMORY_CACHE);
        Location l = ls.getLocation("78.183.5.105");
        Response.Write("Country:" + l.countryName+" City:"+l.city);
        */

        int SQLUsers = 0;
        int ONLUsers = 0;
        string htmlBody = "<table cellspacing=0 cellpadding=0 border=1>";
            var tDtable = SessionObjects.DUserTables;
            foreach (var pair in tDtable.Keys.ToList())
            {
                string onlineuserHtmlBody="";

                if (pair != Utils.SERVER_NAME + "_fillUsersToCache")
                {
                    int userId = ((UserInformation)SessionObjects.wincache_ucache_get(pair)).UserId;
                    ExpiringDictionary<string, object> _DictionaryTable = ((UserInformation)SessionObjects.wincache_ucache_get(pair)).OnlineClients;
                    onlineuserHtmlBody = "<table cellspacing=0 cellpadding=0 border=1>";
                    foreach (var onlineuserpair in _DictionaryTable.Keys.ToList())
                    {
                        Dictionary<string, string> OnlineUserValue = (Dictionary<string, string>)SessionObjects.wincache_ucache_get(_DictionaryTable, onlineuserpair);
                        onlineuserHtmlBody = String.Concat(onlineuserHtmlBody, "<tr><td>", OnlineUserValue["Id"], "</td><td>", OnlineUserValue["Time"], "</td><td>", OnlineUserValue["UserId"], "</td><td>", OnlineUserValue["GameId"], "</td><td>", OnlineUserValue["ClientIP"], "</td><td>", OnlineUserValue["Country"], "</td><td>", OnlineUserValue["City"], "</td><td>", OnlineUserValue["Refer"], "</td><td>", OnlineUserValue["WebRefer"], "</td><td>", OnlineUserValue["Visit"], "</td><td>", "</td><td>", OnlineUserValue["VisitState"], "</td><td>", OnlineUserValue["WebAgent"], "</td><td>", OnlineUserValue["APIVer"], "</td><td>", OnlineUserValue["CallBack"], "</td><td>", OnlineUserValue["URL"], "</td><td>", OnlineUserValue["URLTarget"], "</td><td>", OnlineUserValue["URLShowed"], "</td></tr>");
                        /*
                        onlineuserHtmlBody += "<tr>";
                        onlineuserHtmlBody += "<td>" + OnlineUserValue["Id"] + "</td>";
                        onlineuserHtmlBody += "<td>" + OnlineUserValue["UserId"] + "</td>";
                        onlineuserHtmlBody += "<td>" + OnlineUserValue["GameId"] + "</td>";
                        onlineuserHtmlBody += "<td>" + OnlineUserValue["ClientIP"] + "</td>";
                        onlineuserHtmlBody += "<td>" + OnlineUserValue["Country"] + "</td>";
                        onlineuserHtmlBody += "<td>" + OnlineUserValue["City"] + "</td>";
                        onlineuserHtmlBody += "<td>" + OnlineUserValue["Refer"] + "</td>";
                        onlineuserHtmlBody += "<td>" + OnlineUserValue["WebRefer"] + "</td>";
                        onlineuserHtmlBody += "<td>" + OnlineUserValue["Visit"] + "</td>";
                        onlineuserHtmlBody += "<td>" + OnlineUserValue["WebAgent"] + "</td>";
                        onlineuserHtmlBody += "<td>" + OnlineUserValue["APIVer"] + "</td>";
                        onlineuserHtmlBody += "<td>" + OnlineUserValue["CallBack"] + "</td>";
                        onlineuserHtmlBody += "</tr>";
                        */ 
                        if (onlineuserpair.Substring(0, 12) == "s1_ONL_Users")
                        {
                            ONLUsers++;
                        }
                    }
                    onlineuserHtmlBody += "</table>";
                }
                else
                {
                    onlineuserHtmlBody = SessionObjects.wincache_ucache_get(pair).ToString();
                }

                if (pair.Substring(0, 12) == "s1_SQL_Users")
                {
                    SQLUsers++;
                }

                htmlBody += ("<tr><td>" + pair + "</td><td>" + onlineuserHtmlBody + "</td></tr>");
            }
        Response.Write(htmlBody + "</table><br>");
        htmlBody = "<table cellspacing=0 cellpadding=0 border=1><tr><td>SQL Users</td><td>ONL Users</td></tr><tr><td>" + SQLUsers.ToString() + "</td><td>" + ONLUsers.ToString() + "</td></tr></table>";
        Response.Write(htmlBody);
    }
}