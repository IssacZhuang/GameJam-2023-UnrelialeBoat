using System;
using UnityEngine;

using Vocore;

public static class ThingGenerator
{
    public static ThingBullet CreateBullet(BulletConfig config)
    {
        ThingBullet thing = new ThingBullet();
        thing.Initialize(config);
        return thing;
    }
}
