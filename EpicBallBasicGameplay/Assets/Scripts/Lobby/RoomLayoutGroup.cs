using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLayoutGroup : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject _RoomListingPrefab;
    private GameObject _RoomListing
    {
        get { return _RoomListingPrefab; }
    }

    private List<RoomListing> _RoomListingButtons = new List<RoomListing>();
    private List<RoomListing> _RoomListingB
    {
        get { return _RoomListingButtons; }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);

        foreach(RoomInfo room in roomList)
        {
            RoomReceived(room);
        }
    }
    private void RoomReceived(RoomInfo room)
    {
        int index = _RoomListingButtons.FindIndex(x => x.RoomName == room.Name);

        if(index == -1)
        {
            if(room.IsVisible && room.PlayerCount<room.MaxPlayers)
            {
                GameObject roomListingObj = Instantiate(_RoomListingPrefab);
                roomListingObj.transform.SetParent(transform, false);

                RoomListing roomlisting = roomListingObj.GetComponent<RoomListing>();
                _RoomListingButtons.Add(roomlisting);

                index = (_RoomListingButtons.Count - 1);
            }
        }
        if(index != -1)
        {
            RoomListing roomListing = _RoomListingButtons[index];
            roomListing.SetRoomNameText(room.Name);
            roomListing.Updated = true;
        }
    }

    private void RemoveOldRooms()
    {
        List<RoomListing> removeRooms = new List<RoomListing>();

        foreach(RoomListing roomListing in _RoomListingButtons)
        {
            if (!roomListing.Updated)
                removeRooms.Add(roomListing);
            else
                roomListing.Updated = false;
        }

        foreach (RoomListing roomListing in removeRooms)
        {
            GameObject roomListingObj = roomListing.gameObject;
            _RoomListingButtons.Remove(roomListing);
            Destroy(roomListingObj);
        }
    }
}
