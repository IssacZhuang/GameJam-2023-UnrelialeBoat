using System.Collections.Generic;
using UnityEngine;

public struct BrightnessAnimate
{
    public string animateName;
    public int outline;
    public int isBrighten;
    public int shouldLightUp;
    public float thickness;
    public float brightnessStart; 
    public float brightnessEnd;
    public float duration;
    public Vector4 curve;
}
public struct BrightnessAnimateLoop
{
    public string animateName;
    public int outline;
    public int isBrighten;
    public int shouldLightUp;
    public float thickness;
    public float brightnessStart;
    public float brightnessMiddle;
    public float brightnessEnd;
    public float durationStart;
    public float durationEnd;
    public Vector4 curveStart;
    public Vector4 curveEnd;
}

public class ObjectBrightnessAllConfig : BaseThingConfig
{
    public BrightnessAnimate DiscoverAnimates;
    //public BrightnessAnimate UndiscoverAnimates; 
    //public BrightnessAnimateLoop DiscoverAnimatesLoop;
}