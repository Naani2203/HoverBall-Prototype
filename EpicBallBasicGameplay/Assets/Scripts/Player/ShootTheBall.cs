using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootTheBall : MonoBehaviourPun
{
    [SerializeField]
    private Transform _ShootPosition;
    [SerializeField]
    private GameObject _EpicBallHolder;
    [SerializeField]
    private EpicBall _EpicBall;
    [SerializeField]
    private float _BallForce;
    
    private GameObject _Ball;
    public bool _HaveBall;
   
    void Update()
    {
        
            if(_HaveBall==true && photonView.IsMine)
            {
                _EpicBallHolder.SetActive(true);
            }
        

        if(_HaveBall==true && Input.GetButtonDown("Fire3") && photonView.IsMine)
        {
            
           _Ball= PhotonNetwork.Instantiate(_EpicBall.gameObject.name, _ShootPosition.position, Quaternion.identity);
            _Ball.GetComponent<Rigidbody>().AddForce(transform.forward * _BallForce,ForceMode.Impulse);
            _HaveBall = false;
            _EpicBallHolder.SetActive(false);
        }   
        if(_HaveBall==false)
        {
            _EpicBallHolder.SetActive(false);
        }
    }
}
