using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserInformation
/// </summary>
/*     
#
# User Dictionary Class Type  
#
*/
public class UserInformation
{
    public int UserId = 0;
    public ExpiringDictionary<string, object> OnlineClients = new ExpiringDictionary<string, object>(new TimeSpan(0, 0, Utils.C_TIMEOUT_ONLINEUSERS));

    public UserInformation(int UserId)
    {
        this.UserId = UserId;
    }
}
