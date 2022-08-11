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

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetTarget(Node _target)
    {
        target = _target;
        transform.position = target.GetBuildPosition();

        ui.SetActive(true);

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

    private void drawTurretRange()
    {
        //Draw turret range when clicked
    }
}
