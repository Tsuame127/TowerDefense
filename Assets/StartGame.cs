using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField]
    private WaveSpawner waveSpawner;

    // Start is called before the first frame update
    void Start()
    {
        waveSpawner.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            waveSpawner.enabled = true;

            Destroy(this.gameObject);
        }
    }
}
