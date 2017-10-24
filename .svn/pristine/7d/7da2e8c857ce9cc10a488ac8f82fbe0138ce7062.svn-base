<%@ WebHandler Language="C#" Class="DoAction" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public class DoAction : IHttpHandler, System.Web.SessionState.IRequiresSessionState {

	private const int _SessionTimeOut = 0;
	private const int _Success = 200;
	private const int _UnSuccess = 404;
	
	struct structJSon
	{
		public int code;
		public string message;
	}    
	
	public void ProcessRequest (HttpContext context) {        
		context.Response.ContentType = "application/json";

		structJSon _structJSon = new structJSon();
		_structJSon.code = _SessionTimeOut;
		_structJSon.message = "Session Timeout!";
		
		if (SessionObjects.checkUserSession())
		{
			switch (Utils._GET("act"))
			{
				case "DelSharedGame":
					_structJSon.code = DeleteSharedGame(Utils.safeInt(Utils._GET("gid")), Utils.safeInt(Utils._GET("suid")));
					_structJSon.message = "Selected game is deleted from shared list.";
					break;
				case "AddSharedGame":
					_structJSon.code = AddSharedGame(Utils.safeStr(Utils._GET("gid")), Utils.safeStr(Utils._GET("suemail")));
					switch (_structJSon.code)
					{
						case 401: _structJSon.message = "User is not found!";
							break;
						case 402: _structJSon.message = "Game is not found!";
							break;
						case 403: _structJSon.message = "Game is already shared with this user.";
							break;
						case 200: _structJSon.message = "Game is shared.";
							break;
					}                    
					break;
				case "DelBlockedGame":
					_structJSon.code = DeleteBlockedGame(Utils.safeStr(Utils._GET("gid")), Utils.safeStr(Utils._GET("website")));
					_structJSon.message = "Selected game is deleted from block list.";
					break;
				case "AddBlockGame":
					_structJSon.code = AddBlockedGame(Utils.safeStr(Utils._GET("gid")), Utils.safeStr(Utils._GET("website")));
					switch (_structJSon.code)
					{
						case 402: _structJSon.message = "This is not your game!";
							break;
						case 403: _structJSon.message = "Game is already added to block list.";
							break;
						case 200: _structJSon.message = "Game is added to block list.";
							break;
					}
					break;
				case "ApplyBlockGame":
					_structJSon.code = ApplyBlockedGame(Utils.safeStr(Utils._GET("gid")));
					switch (_structJSon.code)
					{
						case 402: _structJSon.message = "This is not your game!";
							break;
						case 403: _structJSon.message = "Applied blocking for the game.";
							break;
						case 200: _structJSon.message = "Applied blocking for the game.";
							break;
					}
					break;
				case "DelBlockedGameBanner":
					_structJSon.code = DeleteBlockedGameBanner(Utils.safeStr(Utils._GET("gid")), Utils.safeStr(Utils._GET("website")));
					_structJSon.message = "Selected game is deleted from block banner web site list.";
					break;
				case "AddBlockGameBanner":
					_structJSon.code = AddBlockedGameBanner(Utils.safeStr(Utils._GET("gid")), Utils.safeStr(Utils._GET("website")));
					switch (_structJSon.code)
					{
						case 402: _structJSon.message = "This is not your game!";
							break;
						case 403: _structJSon.message = "Game is already added to block banner list.";
							break;
						case 200: _structJSon.message = "Game is added to block banner list.";
							break;
					}
					break;
				case "ApplyBlockGameBanner":
					_structJSon.code = ApplyBlockedGameBanner(Utils.safeStr(Utils._GET("gid")));
					switch (_structJSon.code)
					{
						case 402: _structJSon.message = "This is not your game!";
							break;
						case 403: _structJSon.message = "Applied banner blocking for the game.";
							break;
						case 200: _structJSon.message = "Applied banner blocking for the game.";
							break;
					}
					break;
				case "SaveBannerconfig":
					_structJSon.code = SaveBannerconfig(Utils.safeStr(Utils._GET("gid")));
					switch (_structJSon.code)
					{
						case 402: _structJSon.message = "This is not your game!";
							break;
						case 403:
						case 404: _structJSon.message = "Not saved!";
							break;
						case 200: _structJSon.message = "Saved banner config for the game.";
							break;
					}
					break;
				case "AddPaymentAccount":
					_structJSon.code = MonetizeAddPaymentAccount();
					switch (_structJSon.code)
					{
						case 404: _structJSon.message = "Not saved!";
							break;
						case 200: _structJSon.message = "Saved.";
							break;
					}
					break;
				case "DelPaymentAccount":
					_structJSon.code = MonetizeDeletePaymentAccount();
					switch (_structJSon.code)
					{
						case 404: _structJSon.message = "Not saved!";
							break;
						case 200: _structJSon.message = "Saved.";
							break;
					}
					break;
                case "SetPaymentAccount":
                    _structJSon.code = MonetizeSetPaymentAccount();
                    switch (_structJSon.code)
                    {
                        case 404: _structJSon.message = "Not saved!";
                            break;
                        case 200: _structJSon.message = "Saved.";
                            break;
                    }
                    break;
                case "SaveBannerFilter":
                    _structJSon.code = BannerSaveFilter(Utils.safeStr(Utils._GET("gid")));
                    switch (_structJSon.code)
                    {
                        case 404: _structJSon.message = "Not saved!";
                            break;
                        case 200: _structJSon.message = "Saved.";
                            break;
                    }
                    break;                                    
            }

		}
		context.Response.Write(Utils.json_encode(_structJSon));
	}

	private int DeleteSharedGame(int GId, int SuId)
	{
		using (SqlConnection con = Utils.GetNewConnection())
		{
			con.Open();
			using (SqlCommand cmd = new SqlCommand("W_DeleteSharedUser", con))
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@OwnerUserId", SessionObjects.SessionUserId);
				cmd.Parameters.AddWithValue("@SharedUserId", SuId);
				cmd.Parameters.AddWithValue("@GameId", GId);
				int numberOfRecords = cmd.ExecuteNonQuery();
				if (numberOfRecords == 1)
				{
					return _Success;
				}
			}
		}

		return _UnSuccess;
	}

	private int AddSharedGame(string GId, string SuEmail)
	{
		using (SqlConnection con = Utils.GetNewConnection())
		{
			con.Open();
			using (SqlCommand cmd = new SqlCommand("W_AddGameUserShare", con))
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@OwnerUserId", SessionObjects.SessionUserId);
				cmd.Parameters.AddWithValue("@SharedUserEmail", SuEmail);
				cmd.Parameters.AddWithValue("@GameMd5", GId);
				SqlParameter returnValue = new SqlParameter();
				returnValue.Direction = ParameterDirection.ReturnValue;
				cmd.Parameters.Add(returnValue);
								
				int numberOfRecords = cmd.ExecuteNonQuery();
				return (int)returnValue.Value;
				/*
				if (numberOfRecords == 1)
				{
					return _Success;
				}
				*/ 
			}
		}

		return _UnSuccess;
	}

	private int DeleteBlockedGame(string GId, string WebSite)
	{
		using (SqlConnection con = Utils.GetNewConnection())
		{
			con.Open();
			using (SqlCommand cmd = new SqlCommand("W_DeleteGameBlockedWebSite", con))
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@UserId", SessionObjects.SessionUserId);
				cmd.Parameters.AddWithValue("@GameMd5", GId);
				cmd.Parameters.AddWithValue("@WebSite", WebSite.Trim());
				int numberOfRecords = cmd.ExecuteNonQuery();
				if (numberOfRecords == 1)
				{
					return _Success;
				}
			}
		}

		return _UnSuccess;
	}

	private int AddBlockedGame(string GId, string WebSite)
	{
		using (SqlConnection con = Utils.GetNewConnection())
		{
			con.Open();
			using (SqlCommand cmd = new SqlCommand("W_AddGameBlockedWebSite", con))
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@UserId", SessionObjects.SessionUserId);
				cmd.Parameters.AddWithValue("@WebSite", WebSite.Trim());
				cmd.Parameters.AddWithValue("@GameMd5", GId);
				SqlParameter returnValue = new SqlParameter();
				returnValue.Direction = ParameterDirection.ReturnValue;
				cmd.Parameters.Add(returnValue);

				int numberOfRecords = cmd.ExecuteNonQuery();
				return (int)returnValue.Value;
				/*
				if (numberOfRecords == 1)
				{
					return _Success;
				}
				*/
			}
		}

		return _UnSuccess;
	}

	private int ApplyBlockedGame(string GId)
	{
		using (SqlConnection con = Utils.GetNewConnection())
		{
			con.Open();
			using (SqlCommand cmd = new SqlCommand("W_ApplyGameBlockedWebSite", con))
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@UserId", SessionObjects.SessionUserId);
				cmd.Parameters.AddWithValue("@GameMd5", GId);
				SqlParameter returnValue = new SqlParameter();
				returnValue.Direction = ParameterDirection.ReturnValue;
				cmd.Parameters.Add(returnValue);

				int numberOfRecords = cmd.ExecuteNonQuery();
				return (int)returnValue.Value;
				/*
				if (numberOfRecords == 1)
				{
					return _Success;
				}
				*/
			}
		}

		return _UnSuccess;
	}


	private int DeleteBlockedGameBanner(string GId, string WebSite)
	{
		using (SqlConnection con = Utils.GetNewConnection())
		{
			con.Open();
			using (SqlCommand cmd = new SqlCommand("W_DeleteGameBlockedBanner", con))
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@UserId", SessionObjects.SessionUserId);
				cmd.Parameters.AddWithValue("@GameMd5", GId);
				cmd.Parameters.AddWithValue("@WebSite", WebSite.Trim());
				int numberOfRecords = cmd.ExecuteNonQuery();
				if (numberOfRecords == 1)
				{
					return _Success;
				}
			}
		}

		return _UnSuccess;
	}

	private int AddBlockedGameBanner(string GId, string WebSite)
	{
		using (SqlConnection con = Utils.GetNewConnection())
		{
			con.Open();
			using (SqlCommand cmd = new SqlCommand("W_AddGameBlockedBanner", con))
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@UserId", SessionObjects.SessionUserId);
				cmd.Parameters.AddWithValue("@WebSite", WebSite.Trim() );
				cmd.Parameters.AddWithValue("@GameMd5", GId);
				SqlParameter returnValue = new SqlParameter();
				returnValue.Direction = ParameterDirection.ReturnValue;
				cmd.Parameters.Add(returnValue);

				int numberOfRecords = cmd.ExecuteNonQuery();
				return (int)returnValue.Value;
				/*
				if (numberOfRecords == 1)
				{
					return _Success;
				}
				*/
			}
		}

		return _UnSuccess;
	}

	private int ApplyBlockedGameBanner(string GId)
	{
		using (SqlConnection con = Utils.GetNewConnection())
		{
			con.Open();
			using (SqlCommand cmd = new SqlCommand("W_ApplyGameBlockedBanner", con))
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@UserId", SessionObjects.SessionUserId);
				cmd.Parameters.AddWithValue("@GameMd5", GId);
				SqlParameter returnValue = new SqlParameter();
				returnValue.Direction = ParameterDirection.ReturnValue;
				cmd.Parameters.Add(returnValue);

				int numberOfRecords = cmd.ExecuteNonQuery();
				return (int)returnValue.Value;
				/*
				if (numberOfRecords == 1)
				{
					return _Success;
				}
				*/
			}
		}

		return _UnSuccess;
	}

	private int SaveBannerconfig(string GId)
	{
		/*
		@UserId int,
		@GameMd5 varchar(32),	
		@BGColor varchar(6), 
		@Width int, 
		@Height int, 
		@Timeout int, 
		@Autosize bit, 
		@Active bit      
		  */

		if (Utils.safeInt(Utils._GET("width")) > 200 && Utils.safeInt(Utils._GET("height")) > 200 &&
			(Utils.safeInt(Utils._GET("autosize")) == 1 || Utils.safeInt(Utils._GET("autosize")) == 0) &&
			(Utils.safeInt(Utils._GET("active")) == 1 || Utils.safeInt(Utils._GET("active")) == 0) &&
			(Utils.safeInt(Utils._GET("timeout")) > 9 && Utils.safeInt(Utils._GET("timeout")) < 101) &&
			Utils.safeStr(Utils._GET("color")).Length==6
			)
		{

			using (SqlConnection con = Utils.GetNewConnection())
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("W_UpdateGameBannerConfig", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@UserId", SessionObjects.SessionUserId);
					cmd.Parameters.AddWithValue("@GameMd5", GId);
					cmd.Parameters.AddWithValue("@BGColor", Utils.safeStr(Utils._GET("color")));
					cmd.Parameters.AddWithValue("@Width", Utils.safeInt(Utils._GET("width")));
					cmd.Parameters.AddWithValue("@Height", Utils.safeInt(Utils._GET("height")));
					cmd.Parameters.AddWithValue("@Timeout", Utils.safeInt(Utils._GET("timeout")));
					cmd.Parameters.AddWithValue("@Autosize", Utils.safeInt(Utils._GET("autosize")));
					cmd.Parameters.AddWithValue("@Active", Utils.safeInt(Utils._GET("active")));
					int numberOfRecords = cmd.ExecuteNonQuery();
					if (numberOfRecords == 1)
					{
						return _Success;
					}
				}
			}
		}
		else
		{
			return _UnSuccess;
		}

		return _UnSuccess;
	}


	private int MonetizeAddPaymentAccount()
	{
		try
		{
			if (Utils._GET("holdername").Length > 5 && ((Utils._GET("acctype") == "1" && Utils._GET("accdetail").Length > 15) || (Utils._GET("accdetail").Length > 15 && Utils._GET("acctype") == "2" && Utils.validateEmail(Utils._GET("accdetail")))))
			{
				using (SqlConnection con = Utils.GetNewConnection())
				{
					con.Open();
					using (SqlCommand cmd = new SqlCommand("W_MonetizeAddPaymentSettings", con))
					{
						int retVal = 0;

						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Parameters.AddWithValue("@UserId", SessionObjects.SessionUserId);
						cmd.Parameters.AddWithValue("@AccountName", Utils.safeStr(Utils._GET("holdername")));
						cmd.Parameters.AddWithValue("@AccountDetail", Utils.safeStr(Utils._GET("accdetail")));
						cmd.Parameters.AddWithValue("@AccountType", Utils.safeStr(Utils._GET("acctype")));

						retVal = cmd.ExecuteNonQuery();
						switch (retVal)
						{
							case 0: return _UnSuccess;
							case 1: return _Success;
							default: return _UnSuccess;
						}
					}
				}
			}
			else
			{
				return _UnSuccess;
			}
		}
		catch
		{
			return _UnSuccess;
		}
	}

	private int MonetizeDeletePaymentAccount()
	{
		try
		{
			if (Utils.safeInt(Utils._GET("accid")) > 0)
			{
				using (SqlConnection con = Utils.GetNewConnection())
				{
					con.Open();
					using (SqlCommand cmd = new SqlCommand("W_MonetizeDeletePaymentSettings", con))
					{
						int retVal = 0;

						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Parameters.AddWithValue("@UserId", SessionObjects.SessionUserId);
						cmd.Parameters.AddWithValue("@Id", Utils.safeInt(Utils._GET("accid")));

						retVal = cmd.ExecuteNonQuery();
						switch (retVal)
						{
							case 0: return _UnSuccess;
							case 1: return _Success;
							default: return _UnSuccess;
						}
					}
				}
			}
			else
			{
				return _UnSuccess;
			}
		}
		catch
		{
			return _UnSuccess;
		}
	}

    private int MonetizeSetPaymentAccount()
    {
        try
        {
            if (Utils.safeInt(Utils._GET("accid")) > 0)
            {
                using (SqlConnection con = Utils.GetNewConnection())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("W_MonetizeSetPaymentSettings", con))
                    {
                        int retVal = 0;

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", SessionObjects.SessionUserId);
                        cmd.Parameters.AddWithValue("@Id", Utils.safeInt(Utils._GET("accid")));

                        retVal = cmd.ExecuteNonQuery();
                        if (retVal > 0)
                        {
                            return _Success;
                        }
                        else
                        {
                            return _UnSuccess;
                        }
                    }
                }
            }
            else
            {
                return _UnSuccess;
            }
        }
        catch
        {
            return _UnSuccess;
        }
    }

    private int BannerSaveFilter(string GId)
    {
        /*
	        @UserId int,
	        @CountryState bit,
	        @DomainState bit,
	        @Countries xml(XML_BANNERFILTER_COUNTRY),
	        @Domains xml(XML_BANNERFILTER_DOMAIN),
	        @Games xml(XML_BANNERFILTER_GAME)         
         */
        const string countryXML = "<Row><ID>{0}</ID></Row>";
        const string domainXML = "<Row><Domain>{0}</Domain></Row>";
        const string gameXML = "<Row><GameId>{0}</GameId></Row>";

        string countriesXML = "";
        string domainsXML = "";
        string gamesXML = "";
        
        try
        {
            if (GId.Length == 32)
            {
                Array countries = Utils.json_decode<int[]>(Utils._POST("countries"));
                foreach (int country in countries)
                {
                    countriesXML = String.Concat(countriesXML, String.Format(countryXML, country));
                }

                Array domains = Utils.json_decode<string[]>(Utils._POST("domains"));
                foreach (string domain in domains)
                {
                    if (!String.IsNullOrEmpty(domain)) domainsXML = String.Concat(domainsXML, String.Format(domainXML, domain));
                }

                Array games = Utils.json_decode<string[]>(Utils._POST("games"));
                if (games.Length > 0)
                {
                    bool inArray = false;
                    foreach (string game in games)
                    {
                        gamesXML = String.Concat(gamesXML, String.Format(gameXML, game));
                        if (game==GId) inArray = true;
                    }
                    if (!inArray)
                    {
                        gamesXML = String.Concat(gamesXML, String.Format(gameXML, GId));
                    }
                }
                else
                {
                    gamesXML = String.Format(gameXML, GId);
                }
                
                using (SqlConnection con = Utils.GetNewConnection())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("W_AddBannerFilter", con))
                    {
                        int retVal = 0;

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", SessionObjects.SessionUserId);
                        //cmd.Parameters.AddWithValue("@CountryState", Utils.safeBool(Utils._POST("countryState")));
                        //cmd.Parameters.AddWithValue("@DomainState", Utils.safeBool(Utils._POST("domainState")));
                        cmd.Parameters.AddWithValue("@CountryState", false);
                        cmd.Parameters.AddWithValue("@DomainState", false);
                        cmd.Parameters.AddWithValue("@BannerEnable", Utils.safeBool(Utils._POST("bannerEnable")));
                        cmd.Parameters.AddWithValue("@PreRoll", Utils.safeBool(Utils._POST("preRoll")));
                        cmd.Parameters.AddWithValue("@ShowAfterTime", Utils.safeInt(Utils._POST("showAfterTime")));
                        cmd.Parameters.AddWithValue("@Countries", String.Concat("<Countries>", countriesXML, "</Countries>"));
                        cmd.Parameters.AddWithValue("@Domains", String.Concat("<Domains>", domainsXML, "</Domains>"));
                        cmd.Parameters.AddWithValue("@Games", String.Concat("<Games>", gamesXML, "</Games>"));

                        retVal = cmd.ExecuteNonQuery();
                        if (retVal > 0)
                        {
                            return _Success;
                        }
                        else
                        {
                            return _UnSuccess;
                        }
                    }
                }
            }
            else
            {
                return _UnSuccess;
            }
        }
        catch
        {
            return _UnSuccess;
        }
    }
    
    public bool IsReusable
    {
		get {
			return false;
		}
	}
	
	

}