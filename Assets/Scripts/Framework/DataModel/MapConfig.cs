using System;
using System.Collections.Generic;
using UnityEngine;

public class MapConfig: BaseConfig
{
    public string scene;//地图prefab的addressable id
    public string description;
    public List<Type> components;
}