using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 _Temp;
   public void FollowCameraPoint(Transform FollowPoint, Transform LookAt)
    {
        transform.position = Vector3.Lerp(transform.position, FollowPoint.position, 0.2f);
        transform.LookAt(LookAt);
    }
    public void FollowLookAtPoint(Transform followPoint)
    {
        _Temp.x = Mathf.Lerp(transform.position.x, followPoint.position.x, 0.2f);
        _Temp.y = Mathf.Lerp(transform.position.y, followPoint.position.y, 0.5f);
        _Temp.z = Mathf.Lerp(transform.position.z, followPoint.position.z, 0.2f);
        transform.position = _Temp;
    }
}
