using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Config;

using Unity.Mathematics;

using Vocore;

public class LoadCurveFromConfig : MonoBehaviour
{
    public TextAsset config;
    private CurveHermite3D _curve;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_curve == null)
        {
            LoadCurve();
        }

        CurveDrawer3D<CurveHermite3D> drawer = new CurveDrawer3D<CurveHermite3D>(_curve);
        drawer.Draw();
    }

    private void LoadCurve()
    {
        DemoConfig demoConfig = ConfigLoader.LoadDemoConfig(config, "config1");
        List<CurvePoint<float3>> points = new List<CurvePoint<float3>>();
        foreach (float3 p in demoConfig.points)
        {
            points.Add(new CurvePoint<float3>(p.x, p));
        }

        _curve = new CurveHermite3D();
        _curve.SetPoints(points);
    }

    [ContextMenu("Reload Curve")]
    private void ReloadCurve()
    {
        LoadCurve();
    }
}
