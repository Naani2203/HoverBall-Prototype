using Photon.Pun;
using UnityEngine;

public class CurrentRoomCanvas : MonoBehaviour
{
   public void OnClickStartSync()
    {
        PhotonNetwork.LoadLevel(1);
    }
}
