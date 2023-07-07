using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ViewConfig : BaseConfig
{
    public string prefab;


}

public class View : BaseThing<ViewConfig>
{

    public override void OnSpawn()
    {
        Transform transform = this.Instance.transform;
        base.OnSpawn();
        Button buttonStart = transform.Find("ButtonStart").GetComponent<Button>();
        buttonStart.onClick.AddListener(()=>
        {
            // ÇÐ»»³¡¾°
        }
        );



        
    }
}