using UnityEngine.UI;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField]
    private Text moneyRemainingText;

    void Update()
    {
        moneyRemainingText.text = "$" + PlayerStats.money.ToString();
    }
}
