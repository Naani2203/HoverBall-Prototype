﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFollowCam : MonoBehaviour
{
    [SerializeField]
    private GameObject _FollowCamera;
    [SerializeField]
    private GameObject _FollowLerp;
    [SerializeField]
    private Transform _FollowPoint;
    [SerializeField]
    private Transform _LookAtPoint;
    private GameObject _CameraInScene;
    private GameObject _FollowLerpInScene;

    private void Awake()
    {
        Instantiate(_FollowCamera, transform.position, Quaternion.identity);
        Instantiate(_FollowLerp, transform.position, Quaternion.identity);
        _CameraInScene = GameObject.FindGameObjectWithTag("MainCamera");
        _FollowLerpInScene = GameObject.FindGameObjectWithTag("FollowVector");
    }

    // Update is called once per frame
    void Update()
    {

        _FollowLerpInScene.GetComponent<CameraFollow>().FollowLookAtPoint(_LookAtPoint);
            _CameraInScene.GetComponent<CameraFollow>().FollowCameraPoint(_FollowPoint,_FollowLerpInScene.transform);
    }
}