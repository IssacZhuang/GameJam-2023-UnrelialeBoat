using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a script for camera shake by sin/cosin sample strategy.
/// Attach this script to target object, then call "Shake()" function at any position to apply shake.
/// </summary>
public class CameraShakeSin : MonoBehaviour
{
    public float angle = 45f;
    public float volume = 1f;
    public float interval = 0.1f;

    private Vector3 originalPosition;
    private float shakeEndTime;

    public void Shake(float time)
    {
        // 
        shakeEndTime = Time.time + time;
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        if (Time.time < shakeEndTime)
        {
            float deltaTime = Time.deltaTime;
            float progress = 1f - (shakeEndTime - Time.time) / interval;

            Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0f);

            // Calculation sin 
            float offsetX = Mathf.Sin(progress * Mathf.PI * 2f) * volume * direction.x;
            float offsetY = Mathf.Sin(progress * Mathf.PI * 2f) * volume * direction.y;

            // Apply sin position to target object
            transform.localPosition = originalPosition + new Vector3(offsetX, offsetY, 0f);
        }
    }
}
