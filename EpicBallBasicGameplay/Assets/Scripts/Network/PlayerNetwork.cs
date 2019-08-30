using Photon.Pun;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerNetwork : MonoBehaviour
{
    public static PlayerNetwork Instance;
    public string PlayerName { get; private set; }
    private PhotonView _PhotonView;
    private int _PlayersInGame = 0;
    [SerializeField]
    private GameObject _PlayertoSpawn;
    [SerializeField]
    private EpicBall _EpicBall;
    public int TeamNumber=1;

    private void Awake()
    {
        Instance = this;
        PlayerName = "RandomPlayer#" + Random.Range(1, 9999);
        _PhotonView = GetComponent<PhotonView>();
        SceneManager.sceneLoaded += OnSceneLoaded;
        PhotonNetwork.SendRate = 25;
        PhotonNetwork.SerializationRate = 15;
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
        _PhotonView.RPC("RPC_LoadedGameScene", RpcTarget.All);
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
        print(PhotonNetwork.PlayerList.Length);
        if (_PlayersInGame == PhotonNetwork.PlayerList.Length)
        {
            _PhotonView.RPC("RPC_SpawnThePlayer", RpcTarget.All);
            print("ALL PLAYERS ARE IN GAMESCENE");
        }
    }

    [PunRPC]
    private void RPC_SpawnThePlayer()
    {
        PhotonNetwork.Instantiate(_PlayertoSpawn.name, Vector3.zero, Quaternion.identity);       
    }
   
    public void UpdateTeam()
    {
        if (TeamNumber == 1)
            TeamNumber = 2;
        else
            TeamNumber = 1;
    }
    public void SpawnBall()
    {
        PhotonNetwork.Instantiate(_EpicBall.name, Vector3.zero, Quaternion.identity);
    }
}
