using System;
using UnityEngine;
using Vocore;


public static class CommandLoad
{
    [RegisterCommand(Name = "load-map", Help = "Load amp", MaxArgCount = 1)]
    static void CommandReloadScene(CommandArg[] args)
    {
        if (args.Length <= 0)
        {
            Debug.Log("只接收一个参数");
            return;
        }



        var sceneName = args[0].String;

        MapConfig config = Content.GetConfig<MapConfig>(sceneName, false);

        if (config == null)
        {
            Debug.Log("找不到地图配置: " + sceneName);
            return;
        }


        Current.Game.LoadMap(config);
    }

    [RegisterCommand(Name = "reload-config", Help = "Hot update config", MaxArgCount = 0)]
    static void CommandReloadConfig(CommandArg[] args)
    {
        DatabaseLoader.HotUpdate();
    }
}

