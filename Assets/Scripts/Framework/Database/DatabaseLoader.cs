using System;
using System.Reflection;
using System.Collections.Generic;

using UnityEngine;

public static class DatabaseLoader
{
    private static readonly MethodInfo methodAddToDatabase = typeof(DatabaseLoader).GetMethod("AddToDatabseGeneric", BindingFlags.NonPublic | BindingFlags.Static);
    private static readonly Dictionary<Type, MethodInfo> methodAddToDatabaseTyped = new Dictionary<Type, MethodInfo>();
    private static int count = 0;
    public static void Load()
    {
        Dictionary<Type, List<BaseConfig>> content = new Dictionary<Type, List<BaseConfig>>();
        XmlConfigLoader loader = new XmlConfigLoader();
        count = 0;

        loader.Load();

        foreach (BaseConfig config in loader.Content)
        {
            Type type = config.GetType();
            if (!content.TryGetValue(type, out List<BaseConfig> list))
            {
                list = new List<BaseConfig>();
                content.Add(type, list);
            }
            list.Add(config);
        }

        foreach (var pair in content)
        {
            Clear(pair.Key);
            foreach (BaseConfig config in pair.Value)
            {
                try
                {
                    AddToDatabse(pair.Key, config);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }

        Debug.Log(TextColor.Green(string.Format("加载了 {0} 个配置", count)));
    }

    private static void AddToDatabse(BaseConfig config)
    {
        AddToDatabse(config.GetType(), config);
    }

    private static void AddToDatabse(Type type, BaseConfig config)
    {
        if (!methodAddToDatabaseTyped.TryGetValue(type, out MethodInfo method))
        {
            method = methodAddToDatabase.MakeGenericMethod(type);
            methodAddToDatabaseTyped.Add(type, method);
        }
        method.Invoke(null, new object[] { config });
    }

    private static void AddToDatabseGeneric<T>(T config) where T : BaseConfig
    {
        try
        {
            Database<T>.Add(config);
            count++;
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
        }
    }

    public static void Clear(Type type)
    {
        MethodInfo methodClearDatabase = typeof(DatabaseLoader).GetMethod("ClearDatabse", BindingFlags.NonPublic | BindingFlags.Static);
        MethodInfo methodClearDatabaseTyped = methodClearDatabase.MakeGenericMethod(type);
        methodClearDatabaseTyped.Invoke(null, null);
    }

    private static void ClearDatabse<T>() where T : BaseConfig
    {
        Database<T>.Clear();
    }
}
