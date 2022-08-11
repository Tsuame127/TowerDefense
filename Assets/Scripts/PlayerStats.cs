using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int money;
    public static int lives;
    public int startMoney = 400;
    public int startingLives = 50;

    public static int wavesFinishedCount;

    void Start()
    {
        money = startMoney;
        lives = startingLives;
        wavesFinishedCount = 0;
    }
}
