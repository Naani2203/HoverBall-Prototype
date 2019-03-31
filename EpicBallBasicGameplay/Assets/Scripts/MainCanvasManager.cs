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

    private void Awake()
    {
        Instance = this;
    }
}
