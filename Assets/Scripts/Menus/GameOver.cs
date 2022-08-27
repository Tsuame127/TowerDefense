using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private Text wavesText;

    private void OnEnable() { wavesText.text = PlayerStats.wavesFinishedCount.ToString(); }

    public void Retry() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }

    public void GoToMenu() { SceneManager.LoadScene("LevelSelect"); }
}
