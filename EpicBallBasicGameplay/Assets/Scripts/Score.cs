using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviourPun
{
    [SerializeField]
    private Text _BlueScoreText;
    [SerializeField]
    private Text _RedScoreText;

    public  int BlueScore;
    public  int RedScore;

    private void Awake()
    {
       
    }

    // Start is called before the first frame update
    void Start()
    {
        BlueScore = 0;
        RedScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
       
        _BlueScoreText.text = BlueScore.ToString();
        _RedScoreText.text = RedScore.ToString();
    }

    [PunRPC]
    public void RPC_UpdateScore(int bscore,int rscore)
    {
        BlueScore = bscore;
        RedScore = rscore;
    }
}
