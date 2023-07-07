using System;
using UnityEngine;

public class ThingBehavior<TThing, TConfig> : MonoBehaviour where TThing: BaseThing<TConfig> where TConfig: BaseThingConfig
{
    public string configName;
    void Start()
    {
        TThing thing = Activator.CreateInstance<TThing>();
        thing.Initialize(Content.GetConfig<TConfig>(configName), this.gameObject);
        Current.Map.AddEntity(thing);
    }
}