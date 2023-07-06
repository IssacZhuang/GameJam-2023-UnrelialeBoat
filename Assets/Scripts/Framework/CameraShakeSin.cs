using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeSin : MonoBehaviour
{
    public float angle = 45f;
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

            Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0f);

            // sin calculation
            float offsetX = Mathf.Sin(progress * Mathf.PI * 2f) * volume * direction.x;
            float offsetY = Mathf.Sin(progress * Mathf.PI * 2f) * volume * direction.y;

            // apply
            transform.localPosition = originalPosition + new Vector3(offsetX, offsetY, 0f);
        }
    }
}
