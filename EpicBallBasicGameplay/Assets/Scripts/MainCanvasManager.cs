using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvasManager : MonoBehaviour
{
    public static MainCanvasManager Instance;

    [SerializeField]
    private LobbyCanvas _LobbyCanvas;
    public LobbyCanvas _LCanvas
    {
        get { return _LobbyCanvas; }
    }
    [SerializeField]
    private CurrentRoomCanvas _CurrentRoomCanvas;
    public CurrentRoomCanvas CurrentRoomCanvas
    {
        get { return _CurrentRoomCanvas; }
    }

    private void Awake()
    {
        Instance = this;
    }
}
