
using System.Text;
using UnityEngine;

public class LogManager
{
    public static void Log(params object[] str)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < str.Length; i++)
        {
            sb.Append(str[i].ToString());
        }
        string result = sb.ToString();
        Debug.Log(result);
    }

    public static void Warn(params object[] str)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<color=#ffff00>");
        for (int i = 0; i < str.Length; i++)
        {
            sb.Append(str[i].ToString());
        }
        sb.Append("</color>");
        string result = sb.ToString();
        Debug.Log(result);
    }

    public static void Error(params object[] str)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<color=#ff0000>");
        for (int i = 0; i < str.Length; i++)
        {
            sb.Append(str[i].ToString());
        }
        // string stackString = Utils.GetStackTraceModelName();
        // sb.Append(stackString);
        sb.Append("</color>");
        string result = sb.ToString();
        Debug.Log(result);
    }
}
