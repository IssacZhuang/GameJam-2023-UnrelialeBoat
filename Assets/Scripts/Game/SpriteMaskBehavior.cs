using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMaskBehavior : MonoBehaviour
{

    public GameObject parent;
    // Start is called before the first frame update
    void Awake()
    {
        Current.Mask = this;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // update object position to match parent
        transform.position = parent.transform.position;
    }

    public void RevealScene(){
        // set the scale of the mask to 100
        transform.localScale = new Vector3(100, 100, 100);

    }
}
