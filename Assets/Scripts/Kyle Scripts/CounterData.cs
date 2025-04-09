using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CounterData
{
    private static Dictionary<string, int> counters = new Dictionary<string, int>();

    public static void IncreaseCounter(string key)
    {
        if (!counters.ContainsKey(key))
            counters[key] = 0;

        counters[key]++;
        Debug.Log($"[CounterData] {key} = {counters[key]}");
    }

    public static int GetCounter(string key)
    {
        Debug.Log($"[CounterData] Getting {key}: {(counters.ContainsKey(key) ? counters[key] : 0)}");
        return counters.ContainsKey(key) ? counters[key] : 0;
    }

    public static void SetCounter(string key, int value)
    {
        counters[key] = value;
    }

    public static void ResetAllCounters()
    {
        counters.Clear();
    }
}
