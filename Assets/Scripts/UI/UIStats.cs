using UnityEngine.UI;
using UnityEngine;

public class UIStats : MonoBehaviour
{
    [SerializeField]
    private Text moneyRemainingText;
    [SerializeField]
    private Text livesRemainingText;
    [SerializeField]
    private Text waveCoutingText;

    void Update()
    {
        moneyRemainingText.text = "$" + PlayerStats.money.ToString();
        livesRemainingText.text = PlayerStats.lives.ToString();
        waveCoutingText.text = (WaveSpawner.waveIndex) + " / " + GameManager.maxWaves;
    }
}
