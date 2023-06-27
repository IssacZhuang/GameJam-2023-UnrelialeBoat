using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Mathematics;
using Vocore;

public class CubeHermiteMove : MonoBehaviour
{
    public List<Transform> transforms;

    public bool inverse = false;
    public float duration = 2f;
    private Vector3 _startPosition;
    private CurveHermite3D _curveHermite;
    private CurveDrawer3D<CurveHermite3D> _curveDrawer;

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _curveHermite = new CurveHermite3D();

        IList<CurvePoint<float3>> points = new List<CurvePoint<float3>>();
        foreach (Transform transform in transforms)
        {
            points.Add(new CurvePoint<float3>(transform.position.x, transform.position));
        }

        _curveHermite.SetPoints(points);

        _curveDrawer = new CurveDrawer3D<CurveHermite3D>(_curveHermite);
        _curveDrawer.Draw();

        float tStart = _curveHermite.Points[0].t;
        float tEnd = _curveHermite.Points[_curveHermite.Points.Count - 1].t;

        float t = Mathf.Cos(Time.time / duration * 2 * Mathf.PI) / 2 + 0.5f;
        if (inverse)
        {
            t = 1 - t;
        }

        //consider tStart and tEnd
        float tNormalized = tStart + (tEnd - tStart) * t;

        Vector3 p = _curveHermite.Evaluate(tNormalized);
        transform.position = p;
    }
}
