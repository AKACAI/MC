using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    private Dictionary<string, List<Action<EventArgs>>> eventsDict = new Dictionary<string, List<Action<EventArgs>>>();

    public void AddEvent(string eventName, Action<EventArgs> func)
    {
        if (eventsDict.ContainsKey(eventName) && eventsDict[eventName].Contains(func))
        {
            // Debug.WriteLine("event aready exist eventName=" + eventName + ", func=" + func.Method.Name);
            return;
        }
        if (!eventsDict.ContainsKey(eventName))
        {
            eventsDict.Add(eventName, new List<Action<EventArgs>>());
        }
        List<Action<EventArgs>> list = eventsDict[eventName];
        list.Add(func);
    }

    public void RemoveEvent(string eventName, Action<EventArgs> func)
    {
        if (!eventsDict.ContainsKey(eventName))
        {
            return;
        }
        eventsDict[eventName].Remove(func);
    }

    /**触发事件*/
    public void Dispatch(string eventName, EventArgs arg)
    {
        if (!eventsDict.ContainsKey(eventName))
        {
            // Debug.WriteLine("event doesn't add yet! eventName=" + eventName);
            return;
        }
        List<Action<EventArgs>> list = eventsDict[eventName];
        for (int i = 0; i < list.Count; i++)
        {
            list[i]?.Invoke(arg);
        }
    }
}