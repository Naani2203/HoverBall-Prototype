
using UnityEngine;
using UnityEngine.UI;

public class RoomListing : MonoBehaviour
{
    [SerializeField]
    private Text _RoomNameText;

    private Text _RoomName
    {
        get { return _RoomNameText; }
    }
    public string RoomName;

    public bool Updated;
    private void Start()
    {
        GameObject lobbyCanvasObj = MainCanvasManager.Instance._LCanvas.gameObject;
        if (lobbyCanvasObj == null)
            return;

        LobbyCanvas lobbycanvas = lobbyCanvasObj.GetComponent<LobbyCanvas>();

        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => lobbycanvas.OnClickJoinRoom(_RoomNameText.text));

    }
    private void OnDestroy()
    {
        Button button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
    }

    public void SetRoomNameText(string text)
    {
        RoomName = text;
        _RoomNameText.text = RoomName;
    }
}
