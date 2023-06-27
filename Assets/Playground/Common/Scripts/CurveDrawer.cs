using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Vocore;

public class CurveDrawer3D<T> where T : ICurve3D
{
    private ICurve3D _curve;
    private float _step = 0.01f;

    public CurveDrawer3D(ICurve3D curve, float step = 0.01f)
    {
        _curve = curve;
        _step = step;
    }

    public void Draw()
    {
        float tStart = _curve.Points[0].t;
        float tEnd = _curve.Points[_curve.Points.Count - 1].t;

        for (float t = tStart; t < tEnd; t += _step)
        {
            Vector3 p = _curve.Evaluate(t);
            Vector3 pNext = _curve.Evaluate(t + _step);
            Debug.DrawLine(p, pNext, Color.red);
        }

        //draw a cross for each point
        foreach (CurvePoint<float3> point in _curve.Points)
        {
            Vector3 p = point.value;
            Debug.DrawLine(p + Vector3.up, p - Vector3.up, Color.blue);
            Debug.DrawLine(p + Vector3.left, p - Vector3.left, Color.blue);
            Debug.DrawLine(p + Vector3.forward, p - Vector3.forward, Color.blue);
        }
    }
}
