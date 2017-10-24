using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;

/// <summary>
/// Summary description for GarbageCollector
/// </summary>
public class GarbageCollector
{
    private bool isRunning()
    {
        lock (SessionObjects.DUserTables._Lock) return _running;
    }
    private volatile bool _running;

    public GarbageCollector()
	{
		//
		// TODO: Add constructor logic here
		//
        _running = true;
        ThreadStart start = new ThreadStart(ExpiringDictionaryTimer);
        new Thread(start).Start();
	}

    private void ExpiringDictionaryTimer()
    {
        while (isRunning())
        {
            Thread.Sleep(3000);
            System.Diagnostics.Debug.WriteLine("FGS ServerAPI @ GC started.");
            try
            {
                foreach (var pair in SessionObjects.DUserTables.Keys.ToList())
                {
                    if (pair != Utils.SERVER_NAME + "_fillUsersToCache")
                    {
                        ExpiringDictionary<string, object> _DictionaryTable = ((UserInformation)SessionObjects.wincache_ucache_get(pair)).OnlineClients;
                        if (!isRunning())
                        {
                            foreach (var onlineuserpair in _DictionaryTable.Keys.ToList())
                            {
                                _DictionaryTable.DestoryExpiredItems(onlineuserpair);
                            }
                        }
                    }
                }
            }
            finally
            {
                System.Diagnostics.Debug.WriteLine("FGS ServerAPI @ GC finished.");
            }
        }
    }
}