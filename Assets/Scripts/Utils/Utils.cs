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

    /**<summary> 获取当前调用堆栈 </summary>*/
    public static string GetStackTraceModelName()
    {
        //当前堆栈信息
        System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
        System.Diagnostics.StackFrame[] sfs = st.GetFrames();
        //过虑的方法名称,以下方法将不会出现在返回的方法调用列表中
        string _filterdName = "ResponseWrite,ResponseWriteError,";
        string _fullName = string.Empty, _methodName = string.Empty;
        for (int i = 1; i < sfs.Length; ++i)
        {
            //非用户代码,系统方法及后面的都是系统调用，不获取用户代码调用结束
            if (System.Diagnostics.StackFrame.OFFSET_UNKNOWN == sfs[i].GetILOffset()) break;
            _methodName = sfs[i].GetMethod().Name;//方法名称
            //sfs[i].GetFileLineNumber();//没有PDB文件的情况下将始终返回0
            if (_filterdName.Contains(_methodName)) continue;
            _fullName = _methodName + "()->" + _fullName;
        }
        st = null;
        sfs = null;
        _filterdName = _methodName = null;
        return _fullName.TrimEnd('-', '>');
    }
}
