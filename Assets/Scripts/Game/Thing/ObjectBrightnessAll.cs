using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Vocore;
using System.Linq;
using UnityEngine.UI;

public class ObjectBrightnessAll : BaseThing<ObjectBrightnessAllConfig>
{
    private Material[] collectedMaterials; // 所有子物体的材质

    private int isBrightenAll;
    private int shouldLightUpAll;
    private int outlineAll;
    private float thicknessAll;
    private float currentBrightnessAll;
    private float brightnessStartAll;
    //private float brightnessMiddleAll;
    private float brightnessEndAll;
    private Vector4 CurveAll;
    //private Vector4 CurveEndAll;


    private Func<float, float> speedCurve;
    //private Func<float, float> speedCurveEnd;
    private float animateStartTime;
    private float duration;
    //private float durationEnd;
    private bool isAnimateAll= false;
    //private bool isAnimateLoopAll = false;


    public override void OnSpawn()
    {
        base.OnSpawn();
        this.BindEvent(EventObjectBrightnessAll.eventDiscoverObjectAll, DiscoverObjectAll);
        //this.BindEvent(EventObjectBrightnessAll.eventUndiscoverObjectAll, UndiscoverObjectAll);
        //this.BindEvent(EventObjectBrightnessAll.eventDiscoverObjectLoopAll, DiscoverObjectLoopAll);
        //// 获取当前物体的Renderer组件，并获取其中的第一个材质
        //Renderer renderer = this.Instance.gameObject.GetComponent<Renderer>();
        //if (renderer != null)
        //{
        //    material = renderer.material;
        //}
        CollectMaterials();
    }

    public override void OnTick()
    {
        base.OnTick();
        if (isAnimateAll)
        {
            float _t = Mathf.Clamp01((Time.time - animateStartTime) / duration);
            //currentBrightnessAll = Mathf.Lerp(brightnessStartAll, brightnessEndAll, _t);
            currentBrightnessAll = brightnessStartAll + speedCurve(_t) * (brightnessEndAll - brightnessStartAll);
            ChangeAllPropertiesFloat(collectedMaterials, "_brightness", currentBrightnessAll);
            ChangeAllPropertiesFloat(collectedMaterials, "_thickness", thicknessAll);
            ChangeAllPropertiesInt(collectedMaterials, "_outline", outlineAll);
            ChangeAllPropertiesInt(collectedMaterials, "_isBrighten", isBrightenAll); 
            ChangeAllPropertiesInt(collectedMaterials, "_shouldLightUp", shouldLightUpAll);
            if (_t >= 1)
            {
                isAnimateAll = false;
            }
        }

        //if (isAnimateLoopAll)
        //{
        //    float _t = Mathf.Clamp01((Time.time - animateStartTime) / duration);
        //    float _tEnd = Mathf.Clamp01((Time.time - (animateStartTime+duration)) / (durationEnd + duration));
        //    float _tTotal = Mathf.Clamp01((Time.time - animateStartTime) / (durationEnd + duration));
        //    if (_t < 1)
        //    {
        //        currentBrightnessAll = brightnessStartAll + speedCurve(_t) * (brightnessMiddleAll - brightnessStartAll);
        //        ChangeAllPropertiesFloat(collectedMaterials, "_brightness", currentBrightnessAll);
        //        ChangeAllPropertiesFloat(collectedMaterials, "_thickness", thicknessAll);
        //        ChangeAllPropertiesInt(collectedMaterials, "_outline", outlineAll);
        //        ChangeAllPropertiesInt(collectedMaterials, "_isBrighten", isBrightenAll);
        //        ChangeAllPropertiesInt(collectedMaterials, "_shouldLightUp", shouldLightUpAll);
        //    }
        //    else if (_t >= 1 && _tTotal <= 1)
        //    {
        //        currentBrightnessAll = brightnessMiddleAll + speedCurveEnd(_tEnd) * (brightnessEndAll - brightnessMiddleAll);
        //        ChangeAllPropertiesFloat(collectedMaterials, "_brightness", currentBrightnessAll);
        //        ChangeAllPropertiesFloat(collectedMaterials, "_thickness", thicknessAll);
        //        ChangeAllPropertiesInt(collectedMaterials, "_outline", outlineAll);
        //        ChangeAllPropertiesInt(collectedMaterials, "_isBrighten", isBrightenAll);
        //        ChangeAllPropertiesInt(collectedMaterials, "_shouldLightUp", shouldLightUpAll);
        //    }
        //    else
        //    {
        //        isAnimateLoopAll = false;
        //    }
        //}
    }


    public void DiscoverObjectAll()
    {
        // 物体揭露状态，逐渐变化亮度
        animateStartTime = Time.time;
        CurveAll = Config.DiscoverAnimates.curve;
        speedCurve = UtilsCurve.GenerateBizerLerpCurve(CurveAll.x, CurveAll.y, CurveAll.z, CurveAll.w);
        // 赋值
        isBrightenAll = Config.DiscoverAnimates.isBrighten;
        shouldLightUpAll = Config.DiscoverAnimates.shouldLightUp;
        outlineAll = Config.DiscoverAnimates.outline;
        thicknessAll = Config.DiscoverAnimates.thickness;
        brightnessStartAll = Config.DiscoverAnimates.brightnessStart;
        brightnessEndAll = Config.DiscoverAnimates.brightnessEnd;
        duration = Config.DiscoverAnimates.duration;
        // 标记开始动画
        isAnimateAll = true;
        //isAnimateLoopAll = false;
    }

    //public void DiscoverObjectLoopAll()
    //{
    //    // 物体揭露状态，逐渐变化亮度
    //    animateStartTime = Time.time;
    //    CurveAll = Config.DiscoverAnimatesLoop.curveStart;
    //    speedCurve = UtilsCurve.GenerateBizerLerpCurve(CurveAll.x, CurveAll.y, CurveAll.z, CurveAll.w);
    //    CurveEndAll = Config.DiscoverAnimatesLoop.curveStart;
    //    speedCurveEnd = UtilsCurve.GenerateBizerLerpCurve(CurveEndAll.x, CurveEndAll.y, CurveEndAll.z, CurveEndAll.w);
    //    // 赋值
    //    isBrightenAll = Config.DiscoverAnimatesLoop.isBrighten;
    //    shouldLightUpAll = Config.DiscoverAnimatesLoop.shouldLightUp;
    //    outlineAll = Config.DiscoverAnimatesLoop.outline;
    //    thicknessAll = Config.DiscoverAnimatesLoop.thickness;
    //    brightnessStartAll = Config.DiscoverAnimatesLoop.brightnessStart;
    //    brightnessMiddleAll = Config.DiscoverAnimatesLoop.brightnessMiddle;
    //    brightnessEndAll = Config.DiscoverAnimatesLoop.brightnessEnd;
    //    duration = Config.DiscoverAnimatesLoop.durationStart;
    //    durationEnd = Config.DiscoverAnimatesLoop.durationEnd;
    //    // 标记开始动画
    //    isAnimateAll = false; 
    //    isAnimateLoopAll = true;
    //}


    //public void UndiscoverObjectAll()
    //{
    //    // 物体返回状态，逐渐变化亮度
    //    animateStartTime=Time.time;
    //    CurveAll = Config.UndiscoverAnimates.curve;
    //    speedCurve = UtilsCurve.GenerateBizerLerpCurve(CurveAll.x, CurveAll.y, CurveAll.z, CurveAll.w);
    //    // 赋值
    //    isBrightenAll = Config.UndiscoverAnimates.isBrighten;
    //    shouldLightUpAll = Config.UndiscoverAnimates.shouldLightUp;
    //    outlineAll = Config.UndiscoverAnimates.outline;
    //    thicknessAll = Config.UndiscoverAnimates.thickness;
    //    brightnessStartAll = Config.UndiscoverAnimates.brightnessStart;
    //    brightnessEndAll = Config.UndiscoverAnimates.brightnessEnd;
    //    duration = Config.DiscoverAnimates.duration;
    //    // 标记开始动画
    //    isAnimateAll = true;
    //    isAnimateLoopAll = false;
    //}

    public void ChangeAllPropertiesFloat(Material[] materials, string propertyName, float propertyValue)
    {
        foreach (Material material in materials)
        {
            // 检查材质是否包含指定的属性
            if (material.HasProperty(propertyName))
            {
                // 根据属性名称设置属性值
                material.SetFloat(propertyName, propertyValue);
            }
        }
    }

    public void ChangeAllPropertiesInt(Material[] materials, string propertyName, int propertyValue)
    {
        foreach (Material material in materials)
        {
            // 检查材质是否包含指定的属性
            if (material.HasProperty(propertyName))
            {
                // 根据属性名称设置属性值
                material.SetInt(propertyName, propertyValue);
            }
        }
    }

    public Material[] CollectMaterials()
    {
        // 初始化存储材质的数组
        collectedMaterials = new Material[0];

        // 调用递归函数，收集材质
        CollectMaterialsRecursive(this.Instance.gameObject.transform);

        return collectedMaterials;
    }

    private void CollectMaterialsRecursive(Transform parent)
    {
        // 获取当前对象的渲染器组件
        Renderer renderer = parent.GetComponent<Renderer>();

        // 如果渲染器存在，收集其材质
        if (renderer != null)
        {
            Material[] materials = renderer.materials;

            // 将当前对象的材质添加到存储数组中
            collectedMaterials = collectedMaterials.Concat(materials).ToArray();
        }

        // 递归处理子对象
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            CollectMaterialsRecursive(child);
        }
    }
}
