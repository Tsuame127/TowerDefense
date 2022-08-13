using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    private Node target;
    private BuildManager buildManager;
    public GameObject ui;

    public Text upgradeCostText;
    public Text sellPriceText;
    public Button upgradeButton;

    private Turret selectedTurret = null;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }


    public void SetTarget(Node _target)
    {
        if (selectedTurret != null)
        {
            selectedTurret.HideRange();
        }

        target = _target;
        transform.position = target.GetBuildPosition();
        selectedTurret = target.GetTurret().GetComponent<Turret>();

        ui.SetActive(true);
        selectedTurret.ShowRange();

        if (target.isUpgraded)
        {
            upgradeButton.interactable = false;
            upgradeCostText.text = "X";
            sellPriceText.text = "- $" + target.turretBluePrint.upgradedSellPrice;
        }
        else
        {
            upgradeButton.interactable = true;
            upgradeCostText.text = "- $" + target.turretBluePrint.upgradeCost;
            sellPriceText.text = "- $" + target.turretBluePrint.sellPrice;
        }
    }


    public void Hide()
    {
        if (selectedTurret != null)
        {
            selectedTurret.HideRange();
        }
        ui.SetActive(false);
    }


    public void Upgrade()
    {
        target.UpgradeTurret();

        if (!target.isUpgraded)
        {
            buildManager.DeselectNode();
        }
    }

    public void Sell()
    {
        target.SellTurret();
        buildManager.DeselectNode();
    }
}
