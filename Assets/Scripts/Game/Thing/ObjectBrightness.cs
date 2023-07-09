using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Vocore;
using System.Linq;
using UnityEngine.UI;

public class ObjectBrightness : BaseThing<ObjectBrightnessConfig>
{
    private Material material; // 子物体的材质

    private int isBrighten;
    private int shouldLightUp;
    private int outline;
    private float thickness;
    private float currentBrightness;
    private float brightnessStart;
    private float brightnessMiddle;
    private float brightnessEnd;
    private Vector4 Curve;
    private Vector4 CurveEnd;



    private Func<float, float> speedCurve;
    private Func<float, float> speedCurveEnd;
    private float animateStartTime;
    private float duration;
    private float durationEnd;
    private bool isAnimate = false;
    private bool isAnimateLoop = false;


    public override void OnSpawn()
    {
        base.OnSpawn();
        this.BindEvent(EventObjectBrightness.eventDiscoverObject, DiscoverObject);
        this.BindEvent(EventObjectBrightness.eventDiscoverLineObjectLoop, DiscoverLineObjectLoop);
        //this.BindEvent(EventObjectBrightness.eventUndiscoverObject, UndiscoverObject);
        //this.BindEvent(EventObjectBrightness.eventDiscoverObjectLoop, DiscoverObjectLoop);
        //this.BindEvent(EventObjectBrightness.eventDiscoverLineObject, DiscoverLineObject);
        //this.BindEvent(EventObjectBrightness.eventUndiscoveLinerObject, UndiscoveLinerObject);
        //// 获取当前物体的Renderer组件，并获取其中的第一个材质
        Renderer renderer = this.Instance.gameObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            material = renderer.material;
        }
    }

    public override void OnTick()
    {
        base.OnTick();
        if (isAnimate)
        {
            float _t = Mathf.Clamp01((Time.time - animateStartTime) / duration);
            //currentBrightnessAll = Mathf.Lerp(brightnessStartAll, brightnessEndAll, _t);
            currentBrightness = brightnessStart + speedCurve(_t) * (brightnessEnd - brightnessStart);
            material.SetFloat("_brightness", currentBrightness);
            material.SetFloat("_thickness", thickness);
            material.SetInt("_outline", outline);
            material.SetInt("_isBrighten", isBrighten);
            material.SetInt("_shouldLightUp", shouldLightUp);
            if (_t >= 1)
            {
                isAnimate = false;
            }
        }
        if (isAnimateLoop)
        {
            float _t = Mathf.Clamp01((Time.time - animateStartTime) / duration);
            float _tEnd = Mathf.Clamp01((Time.time - (animateStartTime + duration)) / (durationEnd + duration));
            float _tTotal = Mathf.Clamp01((Time.time - animateStartTime) / (durationEnd + duration));
            if (_t < 1)
            {
                currentBrightness = brightnessStart + speedCurve(_t) * (brightnessMiddle - brightnessStart);
                material.SetFloat("_brightness", currentBrightness);
                material.SetFloat("_thickness", thickness);
                material.SetInt("_outline", outline);
                material.SetInt("_isBrighten", isBrighten);
                material.SetInt("_shouldLightUp", shouldLightUp);
            }
            else if (_t >= 1 && _tTotal <= 1)
            {
                currentBrightness = brightnessMiddle + speedCurveEnd(_tEnd) * (brightnessEnd - brightnessMiddle);
                material.SetFloat("_brightness", currentBrightness);
                material.SetFloat("_thickness", thickness);
                material.SetInt("_outline", outline);
                material.SetInt("_isBrighten", isBrighten);
                material.SetInt("_shouldLightUp", shouldLightUp);
            }
            else
            {
                isAnimateLoop = false;
            }
        }
    }

    public void DiscoverObject()
    {
        // 物体揭露状态，逐渐变化亮度
        animateStartTime= Time.time;
        Curve = Config.DiscoverAnimates.curve;
        speedCurve = UtilsCurve.GenerateBizerLerpCurve(Curve.x, Curve.y, Curve.z, Curve.w);
        // 赋值
        isBrighten = Config.DiscoverAnimates.isBrighten;
        shouldLightUp = Config.DiscoverAnimates.shouldLightUp;
        outline = Config.DiscoverAnimates.outline;
        thickness = Config.DiscoverAnimates.thickness;
        brightnessStart = Config.DiscoverAnimates.brightnessStart;
        brightnessEnd = Config.DiscoverAnimates.brightnessEnd;
        duration = Config.DiscoverAnimates.duration;
        // 标记开始动画
        isAnimate = true;
        isAnimateLoop = false;
    }


    public void DiscoverLineObjectLoop()
    {
        // 物体揭露状态，逐渐变化亮度
        animateStartTime = Time.time;
        Curve = Config.DiscoverLineAnimatesLoop.curveStart;
        speedCurve = UtilsCurve.GenerateBizerLerpCurve(Curve.x, Curve.y, Curve.z, Curve.w);
        CurveEnd = Config.DiscoverLineAnimatesLoop.curveStart;
        speedCurveEnd = UtilsCurve.GenerateBizerLerpCurve(CurveEnd.x, CurveEnd.y, CurveEnd.z, CurveEnd.w);
        // 赋值
        isBrighten = Config.DiscoverLineAnimatesLoop.isBrighten;
        shouldLightUp = Config.DiscoverLineAnimatesLoop.shouldLightUp;
        outline = Config.DiscoverLineAnimatesLoop.outline;
        thickness = Config.DiscoverLineAnimatesLoop.thickness;
        brightnessStart = Config.DiscoverLineAnimatesLoop.brightnessStart;
        brightnessMiddle = Config.DiscoverLineAnimatesLoop.brightnessMiddle;
        brightnessEnd = Config.DiscoverLineAnimatesLoop.brightnessEnd;
        duration = Config.DiscoverLineAnimatesLoop.durationStart;
        durationEnd = Config.DiscoverLineAnimatesLoop.durationEnd;
        // 标记开始动画
        isAnimate = false;
        isAnimateLoop = true;
    }

    //public void UndiscoverObject()
    //{
    //    // 物体返回状态，逐渐变化亮度
    //    animateStartTime=Time.time;
    //    Curve = Config.UndiscoverAnimates.curve;
    //    speedCurve = UtilsCurve.GenerateBizerLerpCurve(Curve.x, Curve.y, Curve.z, Curve.w);
    //    // 赋值
    //    isBrighten = Config.UndiscoverAnimates.isBrighten;
    //    shouldLightUp = Config.UndiscoverAnimates.shouldLightUp;
    //    outline = Config.UndiscoverAnimates.outline;
    //    thickness = Config.UndiscoverAnimates.thickness;
    //    brightnessStart = Config.UndiscoverAnimates.brightnessStart;
    //    brightnessEnd = Config.UndiscoverAnimates.brightnessEnd;
    //    duration = Config.DiscoverAnimates.duration;
    //    // 标记开始动画
    //    isAnimate = true;
    //    isAnimateLoop = false;
    //}


    //public void DiscoverObjectLoop()
    //{
    //    // 物体揭露状态，逐渐变化亮度
    //    animateStartTime = Time.time;
    //    Curve = Config.DiscoverAnimatesLoop.curveStart;
    //    speedCurve = UtilsCurve.GenerateBizerLerpCurve(Curve.x, Curve.y, Curve.z, Curve.w);
    //    CurveEnd = Config.DiscoverAnimatesLoop.curveStart;
    //    speedCurveEnd = UtilsCurve.GenerateBizerLerpCurve(CurveEnd.x, CurveEnd.y, CurveEnd.z, CurveEnd.w);
    //    // 赋值
    //    isBrighten = Config.DiscoverAnimatesLoop.isBrighten;
    //    shouldLightUp = Config.DiscoverAnimatesLoop.shouldLightUp;
    //    outline = Config.DiscoverAnimatesLoop.outline;
    //    thickness = Config.DiscoverAnimatesLoop.thickness;
    //    brightnessStart = Config.DiscoverAnimatesLoop.brightnessStart;
    //    brightnessMiddle = Config.DiscoverAnimatesLoop.brightnessMiddle;
    //    brightnessEnd = Config.DiscoverAnimatesLoop.brightnessEnd;
    //    duration = Config.DiscoverAnimatesLoop.durationStart;
    //    durationEnd = Config.DiscoverAnimatesLoop.durationEnd;
    //    // 标记开始动画
    //    isAnimate = false;
    //    isAnimateLoop = true;
    //    loopCounter = 0;
    //    targetLoop= Config.DiscoverAnimatesLoop.loopTime;
    //}


    //public void DiscoverLineObject()
    //{
    //    // 物体揭露状态，逐渐变化亮度
    //    animateStartTime = Time.time;
    //    Curve = Config.DiscoverLineAnimates.curve;
    //    speedCurve = UtilsCurve.GenerateBizerLerpCurve(Curve.x, Curve.y, Curve.z, Curve.w);
    //    // 赋值
    //    isBrighten = Config.DiscoverLineAnimates.isBrighten;
    //    shouldLightUp = Config.DiscoverLineAnimates.shouldLightUp;
    //    outline = Config.DiscoverLineAnimates.outline;
    //    thickness = Config.DiscoverLineAnimates.thickness;
    //    brightnessStart = Config.DiscoverLineAnimates.brightnessStart;
    //    brightnessEnd = Config.DiscoverLineAnimates.brightnessEnd;
    //    duration = Config.DiscoverLineAnimates.duration;
    //    // 标记开始动画
    //    isAnimate = true;
    //    isAnimateLoop = false;
    //}

    //public void UndiscoveLinerObject()
    //{
    //    // 物体返回状态，逐渐变化亮度
    //    animateStartTime = Time.time;
    //    Curve = Config.UndiscoverLineAnimates.curve;
    //    speedCurve = UtilsCurve.GenerateBizerLerpCurve(Curve.x, Curve.y, Curve.z, Curve.w);
    //    // 赋值
    //    isBrighten = Config.UndiscoverLineAnimates.isBrighten;
    //    shouldLightUp = Config.UndiscoverLineAnimates.shouldLightUp;
    //    outline = Config.UndiscoverLineAnimates.outline;
    //    thickness = Config.UndiscoverLineAnimates.thickness;
    //    brightnessStart = Config.UndiscoverLineAnimates.brightnessStart;
    //    brightnessEnd = Config.UndiscoverLineAnimates.brightnessEnd;
    //    duration = Config.UndiscoverLineAnimates.duration;
    //    // 标记开始动画
    //    isAnimate = true;
    //    isAnimateLoop = false;
    //}



}
