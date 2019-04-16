using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   public void FollowCameraPoint(Transform FollowPoint, Transform LookAt)
    {
        transform.position = Vector3.Lerp(transform.position, FollowPoint.position, 0.2f);
        transform.LookAt(LookAt);
    }
}
