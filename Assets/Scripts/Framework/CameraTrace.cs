using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrace: MonoBehaviour
{

    public Transform target;
    public float smoothTime = 0.3f;
    public bool isBlending = false;
    public bool isLockZ = false;

    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        if (target != null)
        {
            if (isLockZ)
            {
            }
            if (!isBlending)
            {
                transform.position = target.position;
            }
            else
            {
                Vector3 desiredPosition = target.position;
                Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
                transform.position = smoothedPosition;
            }
        }
    }

    public void SetTarget(Transform newTarget, bool blend = false, bool flagLockZ = false)
    {
        target = newTarget;
        isBlending = blend;
        isLockZ = flagLockZ;
    }
}

