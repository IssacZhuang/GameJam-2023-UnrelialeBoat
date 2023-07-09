using System.Collections.Generic;
using UnityEngine;

//public struct BrightnessAnimateLoopWithTime
//{
//    public string animateName;
//    public int outline;
//    public int isBrighten;
//    public int shouldLightUp;
//    public float thickness;
//    public float brightnessStart;
//    public float brightnessMiddle;
//    public float brightnessEnd;
//    public float durationStart;
//    public float durationEnd;
//    public int loopTime;
//    public Vector4 curveStart;
//    public Vector4 curveEnd;
//}
public class ObjectBrightnessConfig : BaseThingConfig
{
    public BrightnessAnimate DiscoverAnimates;
    //public BrightnessAnimate UndiscoverAnimates;
    //public BrightnessAnimateLoopWithTime DiscoverAnimatesLoop;
    //public BrightnessAnimate DiscoverLineAnimates;
    //public BrightnessAnimate UndiscoverLineAnimates;
    public BrightnessAnimateLoop DiscoverLineAnimatesLoop;
}