using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public delegate void EventAction<T>(T arg) where T : BaseEventArg ;

public abstract class AbstractHandlerInfoBase
{
    public abstract EventAction<Arg> GetEventAction<Arg>() where Arg : BaseEventArg;
}

public class HandlerInfo<T> : AbstractHandlerInfoBase where T : BaseEventArg
{
    public EventAction<T> action;

    public HandlerInfo(EventAction<T> a)
    {
        action = a;
    }

    public override EventAction<Arg> GetEventAction<Arg>() 
    {
        Debug.Assert(typeof(Arg) == typeof(T));
        return action as EventAction<Arg>;
    }
}

public class EventDispatcher
{
    Dictionary<int, List<AbstractHandlerInfoBase>> eventHandlerDict = new Dictionary<int, List<AbstractHandlerInfoBase>>();

    /// <summary>
    /// 注册事件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="e"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public bool RegisterEvent<T>(int e, EventAction<T> action) where T : BaseEventArg
    {
        if(!eventHandlerDict.ContainsKey((int)e))
        {
            var infoLst = new List<AbstractHandlerInfoBase>();
            eventHandlerDict.Add((int)e, infoLst);
        }

        var lst = eventHandlerDict[e];
        foreach(var info in lst)
        {
            if(info.GetEventAction<T>() == action)
            {
                Debug.LogWarning("event handler is already exist! ");
                return false;
            }
        }

        var newInfo = new HandlerInfo<T>(action);
        eventHandlerDict[e].Add(newInfo);
        return true;
    }

    public void UnRegisterEvent<T>(int e, EventAction<T> action) where T : BaseEventArg
    {
        List<AbstractHandlerInfoBase> han = null;
        if (eventHandlerDict.TryGetValue(e, out han))
        {
            for(int i = 0, count = han.Count; i < count; i++)
            {
                var info = han[i];
                if(info.GetEventAction<T>() == action)
                {
                    han.RemoveAt(i);
                    return;
                }
            }
        }
    }
    public void DispatchEvent(int e, BaseEventArg arg)
    {
        List<AbstractHandlerInfoBase> han = null;
        if(eventHandlerDict.TryGetValue((int)e, out han))
        {
            foreach(var h in han)
            {
                var act = h.GetEventAction<BaseEventArg>();
                act.Invoke(arg);
            }
        }
    }

    public void DispatchEvent<T>(int e, T arg)  where T : BaseEventArg
    {
        List<AbstractHandlerInfoBase> han = null;
        if (eventHandlerDict.TryGetValue(e, out han))
        {
            foreach (var h in han)
            {
                var act = h.GetEventAction<T>();
                act.Invoke(arg);
            }
        }
    }
}
