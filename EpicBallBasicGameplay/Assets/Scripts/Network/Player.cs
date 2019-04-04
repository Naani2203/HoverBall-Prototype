using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NetworkPrototype
{

    public class Player : MonoBehaviourPun
    {
        private void Awake()
        {
            if(photonView!=null)
            {
                if (photonView.IsMine == false && GetComponent<PlayerMovement>() != null )
                {
                    Destroy(GetComponent<PlayerMovement>());
                    if(GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>()!=null)
                    {
                        Destroy(GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>());
                    }
                        if (GetComponentInChildren<ShootTheBall>() != null)
                        {
                            Destroy(GetComponentInChildren<ShootTheBall>());
                        }
                }

            }
           
        }
          public static void RefreshInstance(ref Player player, Player prefab)
          {
                var position = Vector3.zero;
                var rotation = Quaternion.identity;
           
                if(player!=null)
                {
                    position = player.transform.position;
                    rotation = player.transform.rotation;
                
                    PhotonNetwork.Destroy(player.gameObject);
                }

                player = PhotonNetwork.Instantiate(prefab.gameObject.name, position, rotation).GetComponent<Player>();
                
          }
    }
}
