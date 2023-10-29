using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
   
    public Text Score;
    public Text BestScore;

 
    public Player player;
    public GameManager gameManager;
 

    private void Start()
    {
        gameManager.LoadBestRecord();
        BestScore.text = gameManager.LoadBestRecord().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Score.text = player.PointCalc().ToString();
    }

}
