using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompleted : MonoBehaviour
{
    public Text wavesText;

    private void OnEnable()
    {
        wavesText.text = PlayerStats.wavesFinishedCount.ToString();
    }
    public void Menu()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
