using UnityEngine.UI;
using UnityEngine;

public class LivesUI : MonoBehaviour
{
    [SerializeField]
    private Text livesRemainingText;

    void Update()
    {
        livesRemainingText.text = PlayerStats.lives.ToString();
    }
}
