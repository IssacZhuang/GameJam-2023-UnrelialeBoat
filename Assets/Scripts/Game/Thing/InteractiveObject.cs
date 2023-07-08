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

    private int _hoverCounter = 0; // count the frames that the mouse is on the object

    private float _interactiveRange = 1.5f;  // the interactive range of the object

    public override void OnSpawn()
    {
        base.OnSpawn();
        this.BindEvent(EventHoverObject.eventHoverObject, OnDiscover);
    }

    public void OnDiscover()
    {
        //do something
        Debug.Log("OnDiscover");
        if (isDone){
            showStatus = 2;
        }else{
            showStatus = 4;
        }
        
    }


    public override void OnTick()
    {
        
      if (!isDone){ // this event is not interacteved yet
        if (_hoverCounter >=120){
            // TODO send message to event manager
            // Debug.Log("This object has been interacted");
            isDone = false;
        }
        Vector3 mousePosition = GetMousePosition();
        if (CalculateDistance(this.Instance.transform.position,Current.MainCharacter.Instance.transform.position,_interactiveRange) && CalculateDistance(this.Instance.transform.position,mousePosition,_interactiveRange)){
            _hoverCounter ++;
        }else{
            _hoverCounter = 0;
        }
      }
      if (Input.GetMouseButton(0)){
        //
      }
        base.OnTick();
    }

    public override void OnUpdate()
    {
        if (!isDone)
        {
            if (status == 1 && showStatus == 2)
            {
                // �ݳ�
                Debug.Log("�ݳ�");
            }
        }

        base.OnUpdate();
    }

    private bool CalculateDistance(Vector3 target1Position,Vector3 target2Position, float distance){
        return (target1Position-target2Position).sqrMagnitude < distance;
    }

    private Vector3 GetMousePosition(){
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(Camera.main.transform.position.z);
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
