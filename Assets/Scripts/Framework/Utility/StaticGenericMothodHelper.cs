using System;
using System.Collections.Generic;
using System.Reflection;


public class StaticGenericMothodHelper
{
    private MethodInfo _method;
    private Dictionary<Type, MethodInfo> _cache = new Dictionary<Type, MethodInfo>();
    public StaticGenericMothodHelper(Type type, string methodName)
    {
        _method = type.GetMethod(methodName, BindingFlags.NonPublic| BindingFlags.Static );
    }

    public MethodInfo GetMethod(Type type)
    {
        MethodInfo method;
        if (_cache.TryGetValue(type, out method))
        {
            return method;
        }
        method = _method.MakeGenericMethod(type);
        _cache.Add(type, method);
        return method;
    }
}
