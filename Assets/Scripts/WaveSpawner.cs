using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System;

public class WaveSpawner : MonoBehaviour
{

    private Wave[] waves;
    [SerializeField]
    private GameManager gameManager;

    [Header("Map Properties")]
    [SerializeField]
    private Transform spawnPoint;

    [Header("General Properties")]
    [SerializeField]
    private float timeBetweenWaves = 5f;
    [SerializeField]
    private float countDown = 2f;
    [SerializeField]
    GameObject standardEnemyPrefab;
    [SerializeField]
    GameObject fastEnemyPrefab;
    [SerializeField]
    GameObject slowEnemyPrefab;

    [Header("UI")]
    [SerializeField]
    private Text WaveCountdownTimer;

    public static int waveIndex = 0;
    private bool waveFinished;
    private bool waveFinishedToSpawn = true;

    public TextAsset wavesPropertiesFile;


    private void Start()
    {
        waveIndex = 0;
        waves = new Wave[GameManager.maxWaves];
        InitWavesPrefab();
    }

    void Update()
    {

        bool noMoreEnemiesOnMap = (GameObject.FindWithTag("Enemy") == null);

        bool waveJustFinished = (!waveFinished && noMoreEnemiesOnMap && waveFinishedToSpawn);

        if (waveJustFinished && waveFinishedToSpawn)
        {
            if (waveIndex != 0)
                PlayerStats.money += 150;

            waveIndex++;
            gameManager.UpdateWave(waveIndex);

            if (IsLevelCompleted())
            {
                waveIndex--;
                gameManager.UpdateWave(waveIndex);
                gameManager.WinLevel();

                this.enabled = false;
                return;
            }

        }

        waveFinished = (noMoreEnemiesOnMap && waveFinishedToSpawn);
        WaveCountdownTimer.enabled = waveFinished;

        if (waveFinished && countDown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countDown = timeBetweenWaves;
        }
        else if (waveFinished)
        {
            countDown -= Time.deltaTime;
            this.DisplayTimer();
        }
    }


    private void InitWavesPrefab()
    {
        Waves wavesDeserialized = JsonUtility.FromJson<Waves>(wavesPropertiesFile.text);

        for (int i = 0; i < wavesDeserialized.wave.Length; i++)
        {
            waves[i] = wavesDeserialized.wave[i];
            switch (waves[i].enemyName)
            {
                case "standard":
                    waves[i].enemyType = standardEnemyPrefab;
                    break;

                case "slow":
                    waves[i].enemyType = slowEnemyPrefab;
                    break;

                case "fast":
                    waves[i].enemyType = fastEnemyPrefab;
                    break;

                default:
                    waves[i].enemyType = standardEnemyPrefab;
                    break;

            }
        }
    }


    private bool IsLevelCompleted()
    {
        return (waveIndex > waves.Length);
    }


    private void DisplayTimer()
    {
        float timeRemaining = (float)Math.Round(countDown, 1);

        string stringToDisplay = Mathf.Clamp(timeRemaining, 0, Mathf.Infinity).ToString();

        if (!stringToDisplay.Contains(","))
            WaveCountdownTimer.text = stringToDisplay + ",0";
        else
            WaveCountdownTimer.text = stringToDisplay;
    }


    IEnumerator SpawnWave()
    {
        this.waveFinishedToSpawn = false;

        Wave wave = waves[waveIndex - 1];

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemyType);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        this.waveFinishedToSpawn = true;
    }


    void SpawnEnemy(GameObject enemyToSpawn)
    {
        Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation);
    }
}
