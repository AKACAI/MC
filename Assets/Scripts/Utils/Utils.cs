using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    private static DateTime _startTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);

    /**获取当前时间戳*/
    public static int GetTimeStamp()
    {
        TimeSpan ts = DateTime.Now - Utils._startTime;
        return ts.Milliseconds;
    }
}
