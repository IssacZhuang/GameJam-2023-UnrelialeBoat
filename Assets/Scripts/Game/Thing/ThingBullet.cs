using System;
using UnityEngine;


public class ThingBullet : BaseThing<BulletConfig>
{
    public override void OnTick()
    {
        this.Instance.transform.Translate(Vector3.forward * this.Config.speed * Time.deltaTime);
    }
    public override void OnSpawn()
    {
        this.Instance.transform.SetParent(this.Map.Scene.transform);
    }
}
