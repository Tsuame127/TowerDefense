using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    
    [Header("Default Stats")]
    public int startingMoney = 400;
    public int startingLives = 50;

    public static int money;
    public static int lives;
    public static int wavesFinishedCount;

    void Start()
    {
        money = startingMoney;
        lives = startingLives;
        wavesFinishedCount = 0;
    }
}
