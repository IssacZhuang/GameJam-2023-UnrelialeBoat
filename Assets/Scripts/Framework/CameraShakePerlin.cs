using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakePerlin : MonoBehaviour
{
    public float volume = 1f;
    public float interval = 0.1f;

    private Vector3 originalPosition;
    private float shakeEndTime;

    void Start()
    {
    }

    public void Shake(float time)
    {
        shakeEndTime = Time.time + time;
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        if (Time.time < shakeEndTime)
        {
            float deltaTime = Time.deltaTime;
            float progress = 1f - (shakeEndTime - Time.time) / interval;

            // Perlin Noise
            float offsetX = Mathf.PerlinNoise(Time.time * 10f, 0f) * 2f - 1f;
            float offsetY = Mathf.PerlinNoise(0f, Time.time * 10f) * 2f - 1f;

            // apply noise
            transform.localPosition = originalPosition + new Vector3(offsetX, offsetY, 0f) * volume;
        }
    }
}
