using UnityEngine.UI;
using UnityEngine;

public class WaveCountingUI : MonoBehaviour
{
    public Text waveCoutingText;
    void Update()
    {
        waveCoutingText.text = (WaveSpawner.waveIndex) + " / " + GameManager.maxWaves;
    }
}
