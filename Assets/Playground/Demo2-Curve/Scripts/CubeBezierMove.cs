using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vocore;

public class CubeSmoothMove : MonoBehaviour
{
    public Transform target;
    public bool inverse = false;
    public float duration = 2f;
    private Vector3 _startPosition;
    private static Func<float, float> bezierCurve = UtilsCurve.GenerateBizerLerpCurve(0.4f, 0, 0.4f, 1);

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float t = Mathf.Cos(Time.time / duration * 2 * Mathf.PI) / 2 + 0.5f;
        if (inverse)
        {
            t = 1 - t;
        }
        float x = bezierCurve(t);
        transform.position = Vector3.Lerp(_startPosition, target.position, x);
        Debug.DrawLine(_startPosition, target.position, Color.green);
    }
}
