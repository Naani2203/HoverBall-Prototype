﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpicBall : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _RB;
    [SerializeField]
    private float _BallForce;
    // Start is called before the first frame update
    void Start()
    {
        //_RB.AddForce(transform.forward * _BallForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player") && collision.gameObject.GetComponent<ShootTheBall>()._HaveBall == false)
        {
            collision.gameObject.GetComponent<ShootTheBall>()._HaveBall = true;
            PhotonNetwork.Destroy(this.gameObject);
        }
    }
}