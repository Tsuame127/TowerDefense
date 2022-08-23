using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameIsOver;

    [Header("Map Properties")]
    [SerializeField]
    private int levelToUnlock = 2;
    public static int maxWaves = 50;
    private static int wave;

    [Header("UI")]
    [SerializeField]
    private GameObject gameOverUI;
    [SerializeField]
    public GameObject levelCompletedUI;


    private void Start()
    {
        gameIsOver = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            LoseLevel();
        }

        if (gameIsOver)
        {
            return;
        }

        if (PlayerStats.lives <= 0)
        {
            this.LoseLevel();
        }
    }

    public void UpdateWave(int waveIndex) { wave = waveIndex; }

    public void WinLevel()
    {
        if (!gameIsOver)
        {
            PlayerStats.wavesFinishedCount = wave;

            if (PlayerPrefs.GetInt("levelReached", 1) < levelToUnlock)
            {
                PlayerPrefs.SetInt("levelReached", levelToUnlock);
            }

            levelCompletedUI.SetActive(true);
            gameIsOver = true;
        }
    }

    private void LoseLevel()
    {
        PlayerStats.wavesFinishedCount = wave - 1;
        gameOverUI.SetActive(true);
        gameIsOver = true;
    }
}
