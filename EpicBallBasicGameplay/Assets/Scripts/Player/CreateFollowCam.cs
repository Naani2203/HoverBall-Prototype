using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFollowCam : MonoBehaviour
{
    [SerializeField]
    private GameObject _FollowCamera;
    [SerializeField]
    private Transform _FollowPoint;
    [SerializeField]
    private Transform _LookAtPoint;
    private GameObject _CameraInScene;

    private void Awake()
    {
        Instantiate(_FollowCamera, transform.position, Quaternion.identity);
        _CameraInScene = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
                           

            _CameraInScene.GetComponent<CameraFollow>().FollowCameraPoint(_FollowPoint,_LookAtPoint);
    }
}
