using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompleted : MonoBehaviour
{
    [SerializeField]
    private Text wavesText;

    private void OnEnable()
    {
        wavesText.text = PlayerStats.wavesFinishedCount.ToString();
    }
    public void GoToLevelSelector() { SceneManager.LoadScene("LevelSelect"); }
}
