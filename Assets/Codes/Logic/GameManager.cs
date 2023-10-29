using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static readonly string BestRecordKey = "Best01";


    public string gameSceneName = "Main"; // The name of the game scene to load
    public Player player;
    public Text MenuScore;
    public Text MenuBestScore;

    // Function to save the best record
    public void SaveBestRecord(int score)
    {
        if (score > PlayerPrefs.GetInt(BestRecordKey, 0))
        {
            PlayerPrefs.SetInt(BestRecordKey, score);
            PlayerPrefs.Save();
        }
    }

    // Function to load the best record
    public int LoadBestRecord()
    {
        MenuScore.text = player.PointCalc().ToString();
        MenuBestScore.text = PlayerPrefs.GetInt(BestRecordKey, 0).ToString();

        return PlayerPrefs.GetInt(BestRecordKey, 0);
    }


    public void LoadGameScene()
    {
        SceneManager.LoadScene(gameSceneName); // Loading the game scene
    }

    public void ResetGame()
    {
        Scene currentScene = SceneManager.GetActiveScene(); // Geting the currently active scene
        SceneManager.LoadScene(currentScene.name); // Reload the current scene
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
