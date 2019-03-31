
using UnityEngine;
using UnityEngine.UI;

public class PlayerListing : MonoBehaviour
{
    public Photon.Realtime.Player PhotonPlayer;

    [SerializeField]
    private Text _PlayerName;
    private Text _Name
    {
        get { return _PlayerName; }
    }


    public void ApplyPhotonPlayer(Photon.Realtime.Player photonPlayer)
    {
        _PlayerName.text = photonPlayer.NickName;
    }
}
