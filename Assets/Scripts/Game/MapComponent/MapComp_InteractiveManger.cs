using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity;
using System.Collections;

public class MapComp_InteractiveManger : BaseMapComponent
{
    private const int _tickInterval = 1;
    private int _tick = 0;
    private bool _shouldCount = false;
    // create a timer
    private float c = 0.0f;

    public override void OnCreate()
    {
        base.OnCreate();
        Debug.Log("InteractiveManger OnCreate");
    }

    public override void OnTick()
    {
        _tick++;
        if (_tick % _tickInterval == 0)
        {
            CheckCondition();
        }
        //Debug.Log("Tick");
        c += Time.deltaTime;
    }

    public override void OnUpdate()
    {
        // update timer
    }

    public void CheckCondition()
    {
        var entities = Map.Entities;
        bool isAllCondition = true;
        int count = 0;
        int remainCount = 0;
        for (int i = 0; i < entities.Count(); i++)
        {
            if (entities.ElementAt(i) is InteractiveObject interactive)
            {
                if (interactive.IsKeyItem){
                    count++;
                    if (!interactive.IsDone){
                        remainCount++;
                    }
                }
                if (interactive.IsKeyItem && !interactive.IsDone){
                    isAllCondition = false;
                }
            }
        }

        
        if (isAllCondition){
            // TODO 游戏结束 GameOver
            // WindowDialog.PopDialog("GameOverDialogTest");
            Current.Mask.RevealScene();
            Current.AudioManager.ChangeBGM();
            Debug.Log("This area is done!");

            // call WindowEndGame.Pop() after 5 seconds
            _shouldCount = true;

        }
        if(_shouldCount){
            Debug.Log("timer: " + c);

            if(c >= 40.0f){
                WindowEndGame.Pop();
                Debug.Log("windowEndGame!");
                _shouldCount = false;
            }
        }

    }
}
