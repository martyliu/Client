using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEventArg
{
    public void OnAfterFetch()
    {

    }
    public void OnAfterRelease()
    {
    }
}

public class GenericArg<T> : BaseEventArg
{
    public T value;
}

public class GenericArg<T1, T2> : BaseEventArg
{
    public T1 value1;
    public T2 value2;
}


