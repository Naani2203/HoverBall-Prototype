using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerNetwork : MonoBehaviour
{
    public static PlayerNetwork Instance;
    public string PlayerName { get; private set; }
    private PhotonView _PhotonView;
    private int _PlayersInGame = 0;

    private void Awake()
    {
        Instance = this;
        PlayerName = "RandomPlayer#" + Random.Range(1, 9999);
        _PhotonView = GetComponent<PhotonView>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "GamePlay")
        {
            if(PhotonNetwork.IsMasterClient)
            {
                MasterLoadedGame();
            }
            else
            {
                NonMasterLoadedGame();
            }
        }
    }
    private void MasterLoadedGame()
    {
        _PlayersInGame = 1;
        _PhotonView.RPC("RPC_LoadGameOthers", RpcTarget.Others);
    }
    private void NonMasterLoadedGame()
    {
        _PhotonView.RPC("RPC_LoadedGameScene", RpcTarget.Others);
    }

    [PunRPC]
    private void RPC_LoadGameOthers()
    {
        PhotonNetwork.LoadLevel(1);
    }
    [PunRPC]
    private void RPC_LoadedGameScene()
    {
        _PlayersInGame++;
        if (_PlayersInGame == PhotonNetwork.PlayerList.Length)
        {
            print("ALL PLAYERS ARE IN GAMESCENE");
        }
    }
}
