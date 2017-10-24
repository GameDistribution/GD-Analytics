using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.ComponentModel;

/// <summary>
/// Summary description for ExpiringDictionary
/// </summary>
/// 


public class ExpiringDictionary<TKey, TValue> : IDictionary<TKey, TValue>
{
    public delegate void ExpiringDictionaryEventHandler(object e);
    public static event ExpiringDictionaryEventHandler onItemExpired;

    protected void EventInvokingCode(object innerDictionary)
    {
        if (onItemExpired != null)
        {
            onItemExpired(((ExpiringValueHolder<TValue>)innerDictionary).Value);
        }
    }

    public readonly Object _Lock = new Object();

    private class ExpiringValueHolder<T>
    {
        public T Value { get; set; }
        public DateTime Expiry { get; private set; }
        public bool NoExpire { get; private set; }
        public ExpiringValueHolder(T value, TimeSpan expiresAfter)
        {
            Value = value;
            Expiry = DateTime.Now.Add(expiresAfter);
            NoExpire = false;
        }
        public ExpiringValueHolder(T value)
        {
            Value = value;
            NoExpire = true;
        }

        public override string ToString() { return Value.ToString(); }

        public override int GetHashCode() { return Value.GetHashCode(); }
    };
    private IDictionary<TKey, ExpiringValueHolder<TValue>> innerDictionary;
    private TimeSpan expiryTimeSpan;

    public void DestoryExpiredItems(TKey key)
    {
        lock (_Lock)
        {
            if (innerDictionary.ContainsKey(key))
            {
                var value = innerDictionary[key];

                if (value.Expiry < System.DateTime.Now && !value.NoExpire)
                {
                    EventInvokingCode(value);
                    innerDictionary.Remove(key);
                    System.Diagnostics.Debug.WriteLine("FGS ServerAPI @ Key Removed: " + key.ToString());
                }
            }
        }
    }

    /*
    private bool isRunning()
    {
        lock (_Lock) return _running;
    }
    private volatile bool _running;

    private void ExpiringDictionaryTimer()
    {
        while (isRunning())
        {
            Thread.Sleep(3000);
            System.Diagnostics.Debug.WriteLine("FGS ServerAPI @ GC started.");
            try
            {
                foreach (var pair in innerDictionary.Keys.ToList())
                {
                    if (innerDictionary.ContainsKey(pair))
                    {
                        var value = innerDictionary[pair];

                        if (value.Expiry < System.DateTime.Now && !value.NoExpire)
                        {
                            innerDictionary.Remove(pair);
                            System.Diagnostics.Debug.WriteLine("FGS ServerAPI @ GC Key Removed: " + pair.ToString());
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
    */
    public ExpiringDictionary(TimeSpan expiresAfter)
    {
        expiryTimeSpan = expiresAfter;
        innerDictionary = new Dictionary<TKey, ExpiringValueHolder<TValue>>();
        //Timer t = new Timer(new TimerCallback(ExpiringDictionaryTimer), null, 1000, 3000);
        /*
        _running = true;
        ThreadStart start = new ThreadStart(ExpiringDictionaryTimer);
        new Thread(start).Start();
        */ 
    }

    public void Add(TKey key, TValue value, TimeSpan TTL)
    {
        DestoryExpiredItems(key);
        lock (_Lock)
        {
            innerDictionary.Add(key, new ExpiringValueHolder<TValue>(value, (TTL == null ? expiryTimeSpan : TTL)));
        }
    }

    public void Add(TKey key, TValue value)
    {
        DestoryExpiredItems(key);
        lock (_Lock)
        {
            innerDictionary.Add(key, new ExpiringValueHolder<TValue>(value));
        }
    }

    public void Set(TKey key, TValue value, TimeSpan TTL)
    {
        lock (_Lock)
        {
            if (innerDictionary.ContainsKey(key))
            {
                innerDictionary[key] = new ExpiringValueHolder<TValue>(value, (TTL == null ? expiryTimeSpan : TTL));
            }
            else
            {
                innerDictionary.Add(key, new ExpiringValueHolder<TValue>(value, (TTL == null ? expiryTimeSpan : TTL)));
            }
        }
        DestoryExpiredItems(key);
    }

    public void Set(TKey key, TValue value)
    {
        lock (_Lock)
        {
            if (innerDictionary.ContainsKey(key))
            {
                innerDictionary[key] = new ExpiringValueHolder<TValue>(value);
            }
            else
            {
                innerDictionary.Add(key, new ExpiringValueHolder<TValue>(value));
            }
        }
        DestoryExpiredItems(key);
    }

    public bool ContainsKey(TKey key)
    {
        DestoryExpiredItems(key);

        lock (_Lock)
        {
            return innerDictionary.ContainsKey(key);
        }
    }

    public bool Remove(TKey key)
    {
        DestoryExpiredItems(key);

        lock (_Lock)
        {
            return innerDictionary.Remove(key);
        }
    }

    public ICollection<TKey> Keys
    {
        get { return innerDictionary.Keys; }
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        bool returnval = false;
        DestoryExpiredItems(key);

        lock (_Lock)
        {
            if (innerDictionary.ContainsKey(key))
            {
                value = innerDictionary[key].Value;
                returnval = true;
            }
            else { value = default(TValue); }

            return returnval;
        }
    }

    public ICollection<TValue> Values
    {
        get { return innerDictionary.Values.Select(vals => vals.Value).ToList(); }
    }

    public TValue this[TKey key]
    {
        get
        {
            TValue dicvalue = innerDictionary[key].Value;
            DestoryExpiredItems(key);
            return dicvalue;
        }
        set
        {
            DestoryExpiredItems(key);
            innerDictionary[key] = new ExpiringValueHolder<TValue>(value, expiryTimeSpan);
        }
    }

    public void Add(KeyValuePair<TKey, TValue> item)
    {
        DestoryExpiredItems(item.Key);

        lock (_Lock)
        {
            innerDictionary.Add(item.Key, new ExpiringValueHolder<TValue>(item.Value));
        }
    }

    public void Add(KeyValuePair<TKey, TValue> item, TimeSpan TTL)
    {
        DestoryExpiredItems(item.Key);

        lock (_Lock)
        {
            innerDictionary.Add(item.Key, new ExpiringValueHolder<TValue>(item.Value, (TTL == null ? expiryTimeSpan : TTL)));
        }
    }

    public void Set(KeyValuePair<TKey, TValue> item)
    {
        lock (_Lock)
        {
            if (innerDictionary.ContainsKey(item.Key))
            {
                innerDictionary[item.Key] = new ExpiringValueHolder<TValue>(item.Value);
            }
            else
            {
                innerDictionary.Add(item.Key, new ExpiringValueHolder<TValue>(item.Value));
            }
        }
        DestoryExpiredItems(item.Key);
    }

    public void Set(KeyValuePair<TKey, TValue> item, TimeSpan TTL)
    {
        lock (_Lock)
        {
            if (innerDictionary.ContainsKey(item.Key))
            {
                innerDictionary[item.Key] = new ExpiringValueHolder<TValue>(item.Value, (TTL == null ? expiryTimeSpan : TTL));
            }
            else
            {
                innerDictionary.Add(item.Key, new ExpiringValueHolder<TValue>(item.Value, (TTL == null ? expiryTimeSpan : TTL)));
            }
        }
        DestoryExpiredItems(item.Key);
    }

    public void Clear()
    {
        lock (_Lock)
        {
            innerDictionary.Clear();
        }
    }

    public int Count
    {
        get { return innerDictionary.Count; }
    }

    public bool IsReadOnly
    {
        get { return false; }
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        throw new NotImplementedException();
        //innerDictionary.CopyTo(new ExpiringValueHolder<TValue>(array), arrayIndex);
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        //throw new NotImplementedException();
        lock (_Lock)
        {
            return innerDictionary.Select(kvp => new KeyValuePair<TKey, TValue>(kvp.Key, kvp.Value.Value)).GetEnumerator();
        }
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }

}