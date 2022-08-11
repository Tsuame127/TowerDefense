using UnityEngine;

[System.Serializable]
public class Wave
{
    public string enemyName;
    public int count;
    public float rate;
    public GameObject enemyType;
}


public class Waves
{
    public Wave[] wave;
}