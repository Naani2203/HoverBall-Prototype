using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLayoutGroup : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject _PlayerListingPrefab;
    private GameObject _PlayerListing
    {
        get { return _PlayerListingPrefab; }
    }
    private List<PlayerListing> _PLayerListings = new List<PlayerListing>();
    private List<PlayerListing> _playerList
    {
        get { return _PLayerListings; }
    }

   

   public override void OnJoinedRoom()
    {
        MainCanvasManager.Instance.CurrentRoomCanvas.transform.SetAsLastSibling();
        Photon.Realtime.Player[] photonPlayers = PhotonNetwork.PlayerList;
        for(int i=0; i<photonPlayers.Length;i++)
        {
            PlayerJoinedRoom(photonPlayers[i]);
        }

    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player photonPlayer)
    {
        PlayerLeftRoom(photonPlayer);
    }



    private void PlayerJoinedRoom(Photon.Realtime.Player photonPlayer)
    {
        if (photonPlayer == null)
            return;
        PlayerLeftRoom(photonPlayer);

        GameObject playerlistingObj = Instantiate(_PlayerListingPrefab);
        playerlistingObj.transform.SetParent(transform, false);

        PlayerListing playerListing = playerlistingObj.GetComponent<PlayerListing>();
        playerListing.ApplyPhotonPlayer(photonPlayer);

        _PLayerListings.Add(playerListing);

    }

    private void PlayerLeftRoom(Photon.Realtime.Player photonPlayer)
    {
        int index = _PLayerListings.FindIndex(x => x.PhotonPlayer == photonPlayer);
        if(index!=-1)
        {
            Destroy(_PLayerListings[index].gameObject);
            _PLayerListings.RemoveAt(index);
        }
    }
}
