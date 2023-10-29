using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class SnakeBite : MonoBehaviour
{
    public Player player;
    public GameObject playerGameObject;
    public GameManager gameManager;
    public GameObject GameOver;
    public GameObject cam;
    public GameObject joystick;
    public GameObject InGameScorePanel;

    void Start()
    {
        GameOver.SetActive(false);
        InGameScorePanel.SetActive(true);
        joystick.SetActive(true);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Body")
        {

            gameManager.SaveBestRecord(player.PointCalc());
            gameManager.LoadBestRecord();
            Destroy(playerGameObject);
            GameOver.SetActive(true);
            cam.SetActive(true);
            joystick.SetActive(false);
            InGameScorePanel.SetActive(false);
        }
    }
}
