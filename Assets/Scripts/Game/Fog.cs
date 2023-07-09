using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    public float transparency = 0.5f;
    public float playSpeed = 2;
    public SpriteRenderer buffer1;
    public SpriteRenderer buffer2;
    public List<Sprite> textures = new List<Sprite>();
    private float _t = 0;

    void Awake()
    {

    }


    void Update()
    {
        _t += Time.deltaTime * playSpeed;
        buffer1.sprite = GetTextureBuffer1();
        buffer2.sprite = GetTextureBuffer2();
        float subT = _t % 1;
        buffer1.color = new Color(1, 1, 1, (1 - subT) * transparency);
        buffer2.color = new Color(1, 1, 1, subT * transparency);
    }

    private Sprite GetTextureBuffer1()
    {
        return textures[GetIndexBuffer1()];
    }

    private Sprite GetTextureBuffer2()
    {
        return textures[GetIndexBuffer2()];
    }

    private int GetIndexBuffer1()
    {
        return (int)(_t) % textures.Count;
    }

    private int GetIndexBuffer2()
    {
        return (int)(_t + 1) % textures.Count;
    }
}
