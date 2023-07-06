using System;
using UnityEngine;
using Vocore;


public static class CommandMap
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

    [RegisterCommand(Name = "spawn-bullet", Help = "Spawn a bullet", MaxArgCount = 1)]
    static void CommandSpawnBullet(CommandArg[] args)
    {
        if (args.Length <= 0)
        {
            Debug.Log("只接收一个参数");
            return;
        }

        var bulletName = args[0].String;
        Debug.Log("bulletName: " + bulletName);

        var bulletConfig = Content.GetConfig<BulletConfig>(bulletName, false);

        if (bulletConfig == null)
        {
            Debug.Log("找不到子弹配置: " + bulletName);
            return;
        }
        
        ThingBullet bullet = ThingGenerator.CreateBullet(bulletConfig);
        Current.Map.AddEntity(bullet);
    }
}

