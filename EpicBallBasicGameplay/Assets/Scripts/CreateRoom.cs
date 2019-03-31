using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoom : MonoBehaviour
{
    [SerializeField]
    private Text _RoomName;
    private Text _RoomText
    {
        get { return _RoomName; }
    }
   
    public void OnClickCreateRoom()
    {
        if(PhotonNetwork.CreateRoom(_RoomText.text))
        {
            print("Create Room successfully sent: ");
        }
        else
        {
            print("create room failed");
        }

    }

    private void OnPhotonCreateRoomFailed(Object[] codeAndMessage)
    {
        print("Create room failed" + codeAndMessage[1]);
    }

    private void OnCreatedRoom()
    {
        print("Room Created Successfully");
    }
}
