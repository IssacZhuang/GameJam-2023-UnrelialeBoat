using System;
using UnityEngine;

using Vocore;

public static class ThingGenerator
{
    public static ThingBullet CreateBullet(BulletConfig config)
    {
        ThingBullet thing = new ThingBullet();
        GameObject instance = Content.GetPrefabInstance(config.prefab);
        thing.Initialize(config, instance);
        return thing;
    }
}
