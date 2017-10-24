using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;

public partial class vdata1_0_Default : System.Web.UI.Page
{
    private const int DefaultRowLimit = 20;
    struct structCountPercent
    {
        public string c1;
        public int c2;
        public decimal c3;
    }

    public class classJSON
    {
        public Dictionary<string, object> metrics = new Dictionary<string, object>();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionObjects.checkUserSession())
        {
            string GUId = SessionObjects.SessionUserRegId.ToLower();
            if (Utils.safeStr(Utils._GET("rid")).Length == 36)
            {
                GUId = Utils._GET("rid");
            }

            string GameGUId = (Utils._GET("gid") ?? "").ToLower(); //f457c545a9ded88f18ecee47145a72c0
            string[] Action = (Utils._GET("act") ?? "").Split(',');

            if ((GameGUId.Length == 32 || GameGUId == "whole") && Action.Count() > 0 && GameGUId != "")
            {
                classJSON _classJSON = new classJSON();

                if (GameGUId == "whole")
                {
                    for (int key = 0; key < Action.Count(); key++)
                    {
                        switch (Action[key])
                        {
                            case "Whole":
                                _classJSON.metrics.Add("Games", jsonWholeGames(GUId));
                                break;
                            case "Country":
                                _classJSON.metrics.Add("Country", jsonWholeCountries(GUId));
                                break;
                            case "WebRefer":
                                _classJSON.metrics.Add("WebRefer", jsonWholeWebRefer(GUId));
                                break;
                            case "VisitState":
                                _classJSON.metrics.Add("VisitState", jsonWholeVisitState(GUId));
                                break;
                            case "TotalOnline":
                                _classJSON.metrics.Add("TotalOnline", jsonTotalOnlineCount(GUId));
                                break;
                        }
                    }
                } //Whole
                else
                {
                    for (int key = 0; key < Action.Count(); key++)
                    {
                        switch (Action[key])
                        {
                            case "City":
                                _classJSON.metrics.Add("City", jsonCities(GUId, GameGUId));
                                break;
                            case "Country":
                                _classJSON.metrics.Add("Country", jsonCountries(GUId, GameGUId));
                                break;
                            case "WebRefer":
                                _classJSON.metrics.Add("WebRefer", jsonWebRefer(GUId, GameGUId));
                                break;
                            case "VisitState":
                                _classJSON.metrics.Add("VisitState", jsonVisitState(GUId, GameGUId));
                                break;
                            case "GameOnline":
                                _classJSON.metrics.Add("GameOnline", jsonGameOnlineCount(GUId, GameGUId));
                                break;
                            case "TotalOnline":
                                _classJSON.metrics.Add("TotalOnline", jsonTotalOnlineCount(GUId));
                                break;
                        }
                    }
                }

                Response.Write(Utils.json_encode(_classJSON));
            }
            else
            {
                Response.Redirect("/analytics/");
            }
        }
        else
        {
            Response.Redirect("/analytics/");
        }
    }



    private int jsonTotalOnlineCount(string UserKey)
    {
        int count = 0;

        try
        {
            ExpiringDictionary<string, object> _DictionaryTable = ((UserInformation)SessionObjects.wincache_ucache_get(String.Concat(Utils.SERVER_NAME, Utils.SQL_USERS, UserKey))).OnlineClients;
            count = _DictionaryTable.Keys.Count;
        }
        catch (Exception e)
        {
            count = 0;
        }
        return count;
    }

    private int jsonGameOnlineCount(string UserKey, string GameId)
    {
        return GameOnlineCount(UserKey, GameId);
    }

    private int jsonCountryOnlineCount(string UserKey, string GameId)
    {
        return GameOnlineCount(UserKey, GameId);
    }

    private int GameOnlineCount(string UserKey, string GameId)
    {
        int TotalCount = 0;
        var _UserInformation = (UserInformation)SessionObjects.wincache_ucache_get(String.Concat(Utils.SERVER_NAME, Utils.SQL_USERS, UserKey));
        if (_UserInformation != null)
        {
            ExpiringDictionary<string, object> _DictionaryTable = _UserInformation.OnlineClients;
            foreach (var onlineuserpair in _DictionaryTable.Keys.ToList())
            {
                Dictionary<string, string> OnlineUserValue = (Dictionary<string, string>)SessionObjects.wincache_ucache_get(_DictionaryTable, onlineuserpair);
                if (OnlineUserValue != null)
                {
                    if (OnlineUserValue["GameId"] == GameId)
                    {
                        TotalCount++;
                    }
                }
            }
        }
        return TotalCount;
    }

    private structCountPercent[] jsonCountries(string UserKey, string GameId)
    {
        return countByValueGivenKey(UserKey, GameId, "Country", DefaultRowLimit, true);

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
        if (onlineuserpair.Substring(0, 12) == "s1_ONL_Users")
        {
            ONLUsers++;
        }
            * */
        /*
        var items = from pair in CountriesCount
                    orderby pair.Value descending
                    select pair;
        */
    }

    private structCountPercent[] jsonCities(string UserKey, string GameId)
    {
        return countByValueGivenKey(UserKey, GameId, "City", DefaultRowLimit, true);
    }

    private structCountPercent[] jsonWebRefer(string UserKey, string GameId)
    {
        return countByValueGivenKey(UserKey, GameId, "WebRefer", DefaultRowLimit, true);
    }

    private structCountPercent[] jsonVisitState(string UserKey, string GameId)
    {
        return countByValueGivenKey(UserKey, GameId, "VisitState", 10, true);
    }

    private structCountPercent[] countByValueGivenKey(string UserKey, string GameId, string SortKey, int Limit = 20, bool orderByDesc = false)
    {
        Dictionary<string, structCountPercent> MetricsCount = new Dictionary<string, structCountPercent>();
        try
        {
            var _DictionaryTable = ((UserInformation)SessionObjects.wincache_ucache_get(String.Concat(Utils.SERVER_NAME, Utils.SQL_USERS, UserKey))).OnlineClients;

            if (_DictionaryTable != null)
            {
                int TotalCount = GameOnlineCount(UserKey, GameId);

                foreach (var onlineuserpair in _DictionaryTable.Keys.ToList())
                {
                    Dictionary<string, string> OnlineUserValue = (Dictionary<string, string>)SessionObjects.wincache_ucache_get(_DictionaryTable, onlineuserpair);
                    if (OnlineUserValue != null)
                    {
                        if (OnlineUserValue["GameId"] == GameId)
                        {
                            string MetricKey = OnlineUserValue[SortKey] == "" || OnlineUserValue[SortKey] == null ? "Other" : OnlineUserValue[SortKey];
                            if (MetricsCount.ContainsKey(MetricKey))
                            {
                                structCountPercent _structCountPercent = (structCountPercent)MetricsCount[MetricKey];
                                _structCountPercent.c2++;
                                _structCountPercent.c3 = Math.Round((decimal)_structCountPercent.c2 / TotalCount * 100, 2);
                                MetricsCount[MetricKey] = _structCountPercent;
                            }
                            else
                            {
                                structCountPercent _structCountPercent = new structCountPercent();
                                _structCountPercent.c2 = 1;
                                _structCountPercent.c3 = Math.Round((decimal)_structCountPercent.c2 / TotalCount * 100, 2);
                                MetricsCount[MetricKey] = _structCountPercent;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            System.Diagnostics.Debug.WriteLine("FGS Server Online : " + e.StackTrace);
        }

        // Set MetricsCount Size
        structCountPercent[] _MetricsCount = new structCountPercent[(MetricsCount.Keys.Count < Limit ? MetricsCount.Keys.Count : Limit)];
        int i = 0;

        if (orderByDesc)
        {
            //foreach (var pair in MetricsCount.OrderByDescending(p => ((structCountPercent)p.Value).c2).Skip((TotalSize > Pager * Limit ? Pager * Limit : 0)).Take(Limit))
            foreach (var pair in MetricsCount.OrderByDescending(p => ((structCountPercent)p.Value).c2).Take(Limit))
            {
                _MetricsCount[i].c1 = pair.Key;
                _MetricsCount[i].c2 = pair.Value.c2;
                _MetricsCount[i].c3 = pair.Value.c3;
                i++;
            }
            return _MetricsCount;
        }
        else
        {
            //foreach (var pair in MetricsCount.OrderBy(p => ((structCountPercent)p.Value).c2).Skip((TotalSize > Pager * Limit ? Pager * Limit : 0)).Take(Limit))
            foreach (var pair in MetricsCount.OrderBy(p => ((structCountPercent)p.Value).c2).Take(Limit))
            {
                _MetricsCount[i].c1 = pair.Key;
                _MetricsCount[i].c2 = pair.Value.c2;
                _MetricsCount[i].c3 = pair.Value.c3;
                i++;
            }
            return _MetricsCount;
        }

    }

    /*
     * 
     * Whole Games 
     * 
     */
    private structCountPercent[] jsonWholeGames(string UserKey)
    {
        return countWholeByValueGivenKey(UserKey, "GameId", DefaultRowLimit, true);
    }

    private structCountPercent[] jsonWholeCountries(string UserKey)
    {
        return countWholeByValueGivenKey(UserKey, "Country", DefaultRowLimit, true);
    }

    private structCountPercent[] jsonWholeWebRefer(string UserKey)
    {
        return countWholeByValueGivenKey(UserKey, "WebRefer", DefaultRowLimit, true);
    }

    private structCountPercent[] jsonWholeVisitState(string UserKey)
    {
        return countWholeByValueGivenKey(UserKey, "VisitState", 10, true);
    }

    private structCountPercent[] countWholeByValueGivenKey(string UserKey, string SortKey, int Limit = 20, bool orderByDesc = false)
    {
        Dictionary<string, structCountPercent> MetricsCount = new Dictionary<string, structCountPercent>();
        try
        {
            var _DictionaryTable = ((UserInformation)SessionObjects.wincache_ucache_get(String.Concat(Utils.SERVER_NAME, Utils.SQL_USERS, UserKey))).OnlineClients;

            if (_DictionaryTable != null)
            {
                int TotalCount = jsonTotalOnlineCount(UserKey);

                foreach (var onlineuserpair in _DictionaryTable.Keys.ToList())
                {
                    Dictionary<string, string> OnlineUserValue = (Dictionary<string, string>)SessionObjects.wincache_ucache_get(_DictionaryTable, onlineuserpair);
                    if (OnlineUserValue != null)
                    {
                        string MetricKey = Regex.Split((OnlineUserValue[SortKey] == "" || OnlineUserValue[SortKey] == null ? "Other" : OnlineUserValue[SortKey]).Replace("http://", "").Replace("https://", ""), @"[/?#]")[0];
                        if (MetricsCount.ContainsKey(MetricKey))
                        {
                            structCountPercent _structCountPercent = (structCountPercent)MetricsCount[MetricKey];
                            _structCountPercent.c2++;
                            _structCountPercent.c3 = Math.Round((decimal)_structCountPercent.c2 / TotalCount * 100, 2);
                            MetricsCount[MetricKey] = _structCountPercent;
                        }
                        else
                        {
                            structCountPercent _structCountPercent = new structCountPercent();
                            _structCountPercent.c2 = 1;
                            _structCountPercent.c3 = Math.Round((decimal)_structCountPercent.c2 / TotalCount * 100, 2);
                            MetricsCount[MetricKey] = _structCountPercent;
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            System.Diagnostics.Debug.WriteLine("FGS Server Online : " + e.StackTrace);
        }

        // Set MetricsCount Size
        structCountPercent[] _MetricsCount = new structCountPercent[(MetricsCount.Keys.Count < Limit ? MetricsCount.Keys.Count : Limit)];
        int i = 0;

        if (orderByDesc)
        {
            //foreach (var pair in MetricsCount.OrderByDescending(p => ((structCountPercent)p.Value).c2).Skip((TotalSize > Pager * Limit ? Pager * Limit : 0)).Take(Limit))
            foreach (var pair in MetricsCount.OrderByDescending(p => ((structCountPercent)p.Value).c2).Take(Limit))
            {
                _MetricsCount[i].c1 = pair.Key;
                _MetricsCount[i].c2 = pair.Value.c2;
                _MetricsCount[i].c3 = pair.Value.c3;
                i++;
            }
            return _MetricsCount;
        }
        else
        {
            //foreach (var pair in MetricsCount.OrderBy(p => ((structCountPercent)p.Value).c2).Skip((TotalSize > Pager * Limit ? Pager * Limit : 0)).Take(Limit))
            foreach (var pair in MetricsCount.OrderBy(p => ((structCountPercent)p.Value).c2).Take(Limit))
            {
                _MetricsCount[i].c1 = pair.Key;
                _MetricsCount[i].c2 = pair.Value.c2;
                _MetricsCount[i].c3 = pair.Value.c3;
                i++;
            }
            return _MetricsCount;
        }

    }

}