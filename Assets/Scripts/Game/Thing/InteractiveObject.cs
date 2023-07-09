using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Vocore;

public class InteractiveObject : BaseThing<InteractiveObjectConfig>
{
    private bool _isDone = false;  // 是否已交互过 True为已交互，False为未交互
    private bool _isKeyItem; // 是否为关键物品交互-演出
    private int _showStatus; // 1：高亮展示 2.显示 3.不显示 4.被探测未显示

    private int _hoverCounter = 0; // count the frames that the mouse is on the object

    private float _interactiveRange;  // the interactive range of the object

    public override void OnSpawn()
    {
        base.OnSpawn();
        this.BindEvent(EventHoverObject.eventHoverObject, OnDiscover);
        _isKeyItem = Config.isKeyItem;
        _interactiveRange = Config.detectionRadius;
    }

    public void OnDiscover()
    {
        //do something
        if (_isDone){
            _showStatus = 2;
        }else{
            _showStatus = 4;
        }
        
    }


    public override void OnTick()
    {
      if (!_isDone){ // this event is not interacteved yet

        if (_hoverCounter >= 120){
                //   Debug.Log("触发");
            // Interacte with the object
            // TODO send message to event manager
            if (Config.isDoor){ // if its a door
                if (_isKeyItem){ // if its a key door
                    if (Current.MainCharacter.GetHasKey()){ // has key
                        WindowDialog.PopDialog(Config.dialogConfig);
                        this.Instance.GetComponent<BoxCollider2D>().enabled = false;
                        _isDone = true;
                    }else{ // no key
                        Debug.Log("no key");
                        FloatTip.Pop(Config.description);
                    }
                }else{  // if its a normal door
                    FloatTip.Pop(Config.description);
                    this.Instance.GetComponent<BoxCollider2D>().enabled = false;
                    Current.AudioManager.PlayAsync("doorlock",0.5f);
                    _isDone = true;
                }

            }else if (Config.isKey){
                    WindowDialog.PopDialog(Config.dialogConfig);
                    Current.MainCharacter.SetHasKey(true);
                    _isDone = true;
            }else{ // if its not a door
                if (_isKeyItem){ // if its a key item
                    WindowDialog.PopDialog(Config.dialogConfig);
                    _isDone = true;
                }else{ // if its not a key item
                    FloatTip.Pop(Config.description);
                    _isDone = true;
                }
            }
            _hoverCounter = 0;  // reset the counter
        }
        Vector3 mousePosition = GetMousePosition();
        // if the interactive object is in the range of the character and the mouse position is in the range of the object
        if (CalculateDistance(this.Instance.transform.position,Current.MainCharacter.Instance.transform.position,_interactiveRange) && CalculateDistance(this.Instance.transform.position,mousePosition,_interactiveRange)){  
            if (_hoverCounter ++ == 0){
                Debug.Log("Bright");
                this.Instance.GetComponent<EventBridge>().SendEvent(EventObjectBrightness.eventDiscoverLineObjectLoop);
            }
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
        // if (!_isDone)
        // {
        //     if (_isKeyItem == 1 && _showStatus == 2)
        //     {
        //         // �ݳ�
        //         Debug.Log("�ݳ�");
        //     }
        // }

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

        public bool IsDone => _isDone;

        public bool IsKeyItem => _isKeyItem;
}
