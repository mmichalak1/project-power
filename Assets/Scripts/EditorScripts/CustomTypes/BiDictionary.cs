using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class BiDictionary<T1, T2>
{
    private Dictionary<T1, T2> dict1 = new Dictionary<T1, T2>();
    private Dictionary<T2, T1> dict2 = new Dictionary<T2, T1>();

    public T1 GetValue(T2 key)
    {
        return dict2[key];
    }

    public T2 GetValue(T1 key)
    {
        return dict1[key];
    }

    public void Add(T1 key, T2 value)
    {
        dict1[key] = value;
        dict2[value] = key;
    }

    public void Add(T2 key, T1 value)
    {
        dict1[value] = key;
        dict2[key] = value;
    }

    public void Remove(T1 key)
    {
        var val = dict1[key];
        dict1.Remove(key);
        dict2.Remove(val);
    }

    public void Remove(T2 key)
    {
        var val = dict2[key];
        dict2.Remove(key);
        dict1.Remove(val);
    }

    public T2 this[T1 val]
    {
        get { return GetValue(val); }
        set { Add(val, value); }
    }

    public T1 this[T2 val]
    {
        get { return GetValue(val); }
        set { Add(val, value); }
    }

    public bool ContainsKey(T1 key)
    {
        return dict1.ContainsKey(key);
    }

    public bool ContainsKey(T2 key)
    {
        return dict2.ContainsKey(key);
    }

    public int Count { get { return dict1.Count; } }

    public override string ToString()
    {
        return "Elements: " + Count;
    }

}

