using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAssist : MonoBehaviour
{
    [SerializeField]
    private float _MaxRayDistance;
    [SerializeField]
    private float _AngleToRotate;
    [SerializeField]
    private int _Layer;
    private int _LayerMask;
    private Vector3 _Forward;
    private Vector3 _Projection;
    

    private void Start()
    {
       
        _LayerMask = 1 << _Layer;
    }

    private void Update()
    {
        _Forward = transform.forward;
       
    }
   
    public void RotateToGround()
    {        
        RaycastHit hit;
        if(Physics.Raycast(transform.position,-Vector3.up,out hit,_MaxRayDistance,_LayerMask))
        {           
           _Projection = _Forward - (Vector3.Dot(_Forward, hit.normal)) * hit.normal;
           transform.rotation= Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_Projection,hit.normal),_AngleToRotate*Time.deltaTime);           
        }
    }

}
