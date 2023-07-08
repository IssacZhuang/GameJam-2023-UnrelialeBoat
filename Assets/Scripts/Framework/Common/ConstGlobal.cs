using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用于存放全局常量
public static class ConstGlobal
{
    //配置文件目录
    public static readonly string PathConfig = "Config/";

    public static string GetConfigDirectory()
    {
        if (Application.isEditor)
        {
            return GetEditorConfigDirectory();
        }
        else
        {
            return GetRuntimeConfigDirectory();
        }
    }

    public static string GetEditorConfigDirectory()
    {
        return Path.Combine(Application.dataPath, ConstGlobal.PathConfig);
    }

    public static string GetRuntimeConfigDirectory()
    {
        return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConstGlobal.PathConfig);
    }
}
