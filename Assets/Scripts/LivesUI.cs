using UnityEngine.UI;
using UnityEngine;

public class LivesUI : MonoBehaviour
{
    public Text livesRemainingText;

    void Update()
    {
        livesRemainingText.text = PlayerStats.lives.ToString();
    }
}
