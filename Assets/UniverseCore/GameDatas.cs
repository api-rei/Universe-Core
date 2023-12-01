using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDatas
{
    private static Dictionary<string, float> FloatValues = new Dictionary<string, float>();
    private static Dictionary<string, int> IntValues = new Dictionary<string, int>();
    private static Dictionary<string, bool> BoolValues = new Dictionary<string, bool>();
    private static Dictionary<string, string> StringValues = new Dictionary<string, string>();
    public static void ResetFloatValues()
    {
        FloatValues = new Dictionary<string, float>();
    }
    public static void ResetIntValues()
    {
        IntValues = new Dictionary<string, int>();
    }
    public static void ResetBoolValues()
    {
        BoolValues = new Dictionary<string, bool>();
    }
    public static void ResetStringValues()
    {
        StringValues = new Dictionary<string, string>();
    }
    public static void SetFloatValue(string key, float value)
    {
        if (!FloatValues.ContainsKey(key))
        {
            FloatValues.Add(key, value);
        }
        {
            FloatValues[key] = value;
        }
    }
    public static void SetIntValue(string key, int value)
    {
        if (!IntValues.ContainsKey(key))
        {
            IntValues.Add(key, value);
        }
        {
            IntValues[key] = value;
        }
    }
    public static void SetBoolValue(string key, bool value)
    {
        if (!BoolValues.ContainsKey(key))
        {
            BoolValues.Add(key, value);
        }
        {
            BoolValues[key] = value;
        }
    }
    public static void SetStringValue(string key, string value)
    {
        if (!StringValues.ContainsKey(key))
        {
            StringValues.Add(key, value);
        }
        {
            StringValues[key] = value;
        }
    }
    public static float GetFloatValue(string key)
    {
        if (!FloatValues.ContainsKey(key))
            return 0f;
        return FloatValues[key];
    }
    public static int GetIntValue(string key)
    {
        if (!FloatValues.ContainsKey(key))
            return 0;
        return IntValues[key];
    }
    public static bool GetBoolValue(string key)
    {
        if (!FloatValues.ContainsKey(key))
            return false;
        return BoolValues[key];
    }
    public static string GetStringValue(string key)
    {
        if (!FloatValues.ContainsKey(key))
            return "";
        return StringValues[key];
    }
}
