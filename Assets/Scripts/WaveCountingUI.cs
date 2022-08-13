using UnityEngine.UI;
using UnityEngine;

public class WaveCountingUI : MonoBehaviour
{
    [SerializeField]
    private Text waveCoutingText;
    void Update()
    {
        waveCoutingText.text = (WaveSpawner.waveIndex) + " / " + GameManager.maxWaves;
    }
}
