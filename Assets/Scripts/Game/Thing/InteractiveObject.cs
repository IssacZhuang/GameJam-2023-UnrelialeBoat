using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Vocore;

public class InteractiveObject : BaseThing<BaseThingConfig>
{
    public bool isDone = false; // 是否已交互过 True为已交互，False为未交互
    public int status; // 1：关键物品交互-演出 2.非关键物品-显形
    public int showStatus; // 1：高亮展示 2.显示 3.不显示 4.被探测未显示

    public override void OnSpawn()
    {
        base.OnSpawn();
        this.BindEvent<int>(EventHoverObject.eventHoverObject, OnHover);
    }

    public void OnHover(int damage)
    {
        //do something
    }

    public override void OnUpdate()
    {
        if (!isDone)
        {
            if (status == 1 && showStatus == 2)
            {
                // 演出
                Debug.Log("演出");
            }
        }

        base.OnUpdate();
    }
}
