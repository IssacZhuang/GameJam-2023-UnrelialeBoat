using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimShaderController : MonoBehaviour
{
    public float speed = 1;
    private static readonly int ShaderKey_Frame = Shader.PropertyToID("_Frame");
    private Material _material;

    private float _frame = 0;
    // Start is called before the first frame update
    void Start()
    {
        _material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        _frame += speed * Time.deltaTime;
        _material.SetFloat(ShaderKey_Frame, _frame);
    }
}
