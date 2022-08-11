using UnityEngine.UI;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    public Text moneyRemainingText;

    void Update()
    {
        moneyRemainingText.text = "$" + PlayerStats.money.ToString();
    }
}
