using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCanvas : MonoBehaviour
{
    [SerializeField]
    private RoomLayoutGroup _RoomLayoutGroup;
    private RoomLayoutGroup _RoomLayout
        {
            get { return _RoomLayoutGroup; }
        }
    
    public void OnClickJoinRoom(string RoomName)
    {

    }
}
