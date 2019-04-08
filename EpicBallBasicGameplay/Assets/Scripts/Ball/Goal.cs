using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NetworkPrototype
{

    public class Goal : MonoBehaviourPun
    {
        [SerializeField]
        private Score _Score;
    
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
            
               if(other.GetComponent<PlayerMovement>()._TeamNumber==1)
                {
                    BlueScore();
                    if(PhotonNetwork.IsMasterClient)
                    {
                        PlayerNetwork.Instance.SpawnBall();
                    }
                    other.gameObject.GetComponentInChildren<ShootTheBall>()._HaveBall=false;

                }
                if (other.GetComponent<PlayerMovement>()._TeamNumber == 2)
                {
                    RedScore();
                    other.gameObject.GetComponentInChildren<ShootTheBall>()._HaveBall=false;
                    if (PhotonNetwork.IsMasterClient)
                    {
                        PlayerNetwork.Instance.SpawnBall();
                    }
                }
            }
        }

      
        private void BlueScore()
        {
            _Score.BlueScore++;
            _Score.photonView.RPC("RPC_UpdateScore", RpcTarget.AllBuffered, _Score.BlueScore, _Score.RedScore);
        }

     
        private void RedScore()
        {
            _Score.RedScore++;
            _Score.photonView.RPC("RPC_UpdateScore", RpcTarget.AllBuffered, _Score.BlueScore, _Score.RedScore);
        }
    }
}
