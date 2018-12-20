using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractArgFactory
{
}

public class EventArgFactory<T> : AbstractArgFactory where T : BaseEventArg, new()
{
    Stack<T> stack = new Stack<T>();

    public T Fetch()
    {
        T result;
        if(stack.Count > 0)
        {
            result = stack.Pop();
            result.OnAfterFetch();
        }else
        {
            result = new T();
        }
        return result;
    }

    public void Recycle(T arg)
    {
        stack.Push(arg);
        arg.OnAfterRelease();
    }
}

