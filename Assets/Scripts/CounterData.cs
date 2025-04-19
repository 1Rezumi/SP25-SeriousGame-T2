using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class CounterData
{
    private static Dictionary<string, int> counters = new Dictionary<string, int>();
    private static Dictionary<string, string> cropLabels = new Dictionary<string, string>();

    private static readonly string[] knownKeys = { "Crop1", "Crop2", "Crop3" };

    private static float lastEventTime = -999f;

    public static void SetLastEventTime(float time)
    {
        lastEventTime = time;
    }

    public static float GetLastEventTime()
    {
        return lastEventTime;
    }
    public static void SetLabel(string key, string label)
    {
        cropLabels[key] = label;
    }

    public static string GetLabel(string key)
    {
        return cropLabels.ContainsKey(key) ? cropLabels[key] : key;
    }

    static CounterData()
    {
        InitializeCounters();
    }

    private static void InitializeCounters()
    {
        foreach (string key in knownKeys)
        {
            if (!counters.ContainsKey(key))
                counters[key] = 0;
        }
    }
    public static void SetCounter(string key, int value)
    {
        EnsureKeyExists(key);
        counters[key] = value;
    }

    public static int GetCounter(string key)
    {
        EnsureKeyExists(key);
        Debug.Log($"[CounterData] Getting {key}: {counters[key]}");
        return counters[key];
    }

    public static void AddToCounter(string key, int value)
    {
        if (!counters.ContainsKey(key))
            counters[key] = 0;

        EnsureKeyExists(key);

        counters[key] += value;
        if (counters[key] < 0)
            counters[key] = 0;
    }

    public static List<string> GetAllKeys()
    {
        return new List<string>(counters.Keys);
    }

    public static void ResetAllCounters()
    {
        counters.Clear();
        InitializeCounters();
    }

    public static class CounterDisplayUpdater
    {
        public static void RefreshAllDisplays()
        {
            foreach (CounterDisplay display in Object.FindObjectsOfType<CounterDisplay>())
            {
                display.UpdateCounter();
            }
        }
    }

    private static void EnsureKeyExists(string key)
    {
        if (!counters.ContainsKey(key))
        {
            Debug.LogWarning($"[CounterData] Initializing missing counter key: {key}");
            counters[key] = 0;
        }
    }
    public static void EnsureCounterExists(string key)
    {
        if (!counters.ContainsKey(key))
            counters[key] = 0;
    }
}
