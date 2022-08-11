using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static bool gameIsOver;

    public GameObject gameOverUI;
    public GameObject levelCompletedUI;

    public static int maxWaves = 50;
    public static int wave;
    public int levelToUnlock = 2;

    private void Start()
    {
        gameIsOver = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            EndGame();
        }

        if (gameIsOver)
        {
            return;
        }

        if (PlayerStats.lives <= 0)
        {
            this.EndGame();
        }
    }

    public void UpdateWave(int waveIndex)
    {
        wave = waveIndex;
    }

    private void EndGame()
    {
        PlayerStats.wavesFinishedCount = wave;
        gameOverUI.SetActive(true);
        gameIsOver = true;
    }

    public void WinLevel()
    {
        PlayerStats.wavesFinishedCount = wave;
        Debug.Log("Plus de waves, c'est gagné");

        if (PlayerPrefs.GetInt("levelReached",1) < levelToUnlock)
        {
            PlayerPrefs.SetInt("levelReached", levelToUnlock);
        }

        levelCompletedUI.SetActive(true);
    }
}
