using Photon.Pun;
using UnityEngine;

public class CurrentRoomCanvas : MonoBehaviour
{
   public void OnClickStartSync()
    {
        PhotonNetwork.LoadLevel(1);

    }
    public void OnClickStartDelayed()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
        PhotonNetwork.LoadLevel(1);
    }
}
