using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using UnityEngine;

using Vocore;

public class WaveShaderController : MonoBehaviour
{
    private struct ComparePosition : IComparer<Vector3>
    {
        public Vector3 mainCharacterPos;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Compare(Vector3 a, Vector3 b)
        {
            //use sqrMagnitude to avoid sqrt
            float disA = (a - mainCharacterPos).sqrMagnitude;
            float disB = (b - mainCharacterPos).sqrMagnitude;
            return disA.CompareTo(disB);
        }
    }

    private int shaderId_t = Shader.PropertyToID("_t");
    private int shaderId_waveStrength = Shader.PropertyToID("_wave_strength");
    private int shaderId_waveScale = Shader.PropertyToID("_wave_scale");


    private int shaderId_background = Shader.PropertyToID("_background");
    private int shaderId_waveTex = Shader.PropertyToID("_waveTex");
    private int shaderId_Color = Shader.PropertyToID("_Color");
    private int shaderId_v_scale = Shader.PropertyToID("_v_scale");
    private int shaderId_itemPos1 = Shader.PropertyToID("_itemPos1");
    private int shaderId_itemPost2 = Shader.PropertyToID("_itemPost2");
    private int shaderId_itemPost3 = Shader.PropertyToID("_itemPost3");
    private int shaderId_waive_radius1 = Shader.PropertyToID("_waive_radius1");
    private int shaderId_waive_radius2 = Shader.PropertyToID("_waive_radius2");
    private int shaderId_waive_radius3 = Shader.PropertyToID("_waive_radius3");

    private Material _matWave;
    private Material _matScene;

    public SpriteRenderer waveRenderer;
    public SpriteRenderer sceneRenderer;
    public RenderTexture sceneTexture;
    public Camera sceneCamera;

    public float waveStrength = 0.5f;
    public float waveScale = 0.5f;
    public float playSpeed = 1f;
    private float _t = 0;
    private Vector2 _screenSize = Vector2.zero;
    private NativeArrayList<Vector3> _positionBuffer = new NativeArrayList<Vector3>();

    public void AddBuffer(Vector3 pos)
    {
        _positionBuffer.Add(pos);
    }

    void Awake()
    {
        if (waveRenderer != null)
        {
            _matWave = waveRenderer.material;
        }
        else
        {
            Debug.LogError("waveRenderer is null");
        }

        if (sceneRenderer != null)
        {
            _matScene = sceneRenderer.material;
        }
        else
        {
            Debug.LogError("sceneRenderer is null");
        }

        if (sceneTexture == null)
        {
            Debug.LogError("sceneTexture is null");
        }

        if (sceneCamera == null)
        {
            Debug.LogError("sceneCamera is null");
        }

        _screenSize = new Vector2(Screen.width, Screen.height);

        _matWave.SetFloat(shaderId_waveStrength, waveStrength);
        _matWave.SetFloat(shaderId_waveScale, waveScale);

        Current.WaveShaderController = this;
    }

    void Update()
    {
        if (_screenSize.x != Screen.width || _screenSize.y != Screen.height)
        {
            _screenSize = new Vector2(Screen.width, Screen.height);
        }
        _t += Time.deltaTime * playSpeed;
        _matWave.SetFloat(shaderId_t, _t);

    }

    void LateUpdate()
    {
        Vector3 mainCharacterPos = Vector3.zero;
        if(Current.MainCharacter != null)
        {
            mainCharacterPos = Current.MainCharacter.Instance.transform.position;
        }

        ComparePosition comparePosition = new ComparePosition();
        comparePosition.mainCharacterPos = mainCharacterPos;
        _positionBuffer.Sort(comparePosition);

        Vector4[] nearstPos = new Vector4[3];
        for (int i = 0; i < 3; i++)
        {
            if (i < _positionBuffer.Count)
            {
                Vector2 pos = sceneCamera.WorldToScreenPoint(_positionBuffer[i]);
                //normalize
                pos.x /= Screen.width;
                pos.y /= Screen.height;
                nearstPos[i] = new Vector4(pos.x, pos.y, 0, 0);
            }
            else
            {
                nearstPos[i] = new Vector4(2, 2, 2 ,2);
            }
        }

        _matScene.SetVector(shaderId_itemPos1, nearstPos[0]);
        _matScene.SetVector(shaderId_itemPost2, nearstPos[1]);
        _matScene.SetVector(shaderId_itemPost3, nearstPos[2]);
        
        _positionBuffer.Clear();
    }

    void OnScreenResize()
    {
        sceneTexture.width = Screen.width;
        sceneTexture.height = Screen.height;
    }

    void OnDestroy()
    {
        _positionBuffer.Dispose();
    }




}
