using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UpgradeSellUI : MonoBehaviour
{
    private Turret target;
    private BuildManager buildManager;
    public GameObject ui;

    public Text upgradeCostText;
    public Text sellPriceText;
    public Button upgradeButton;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    private void Update()
    {
        if (ui.activeSelf)
        {
            buildManager.SelectTurretToBuild(null);
        }

    }

    public void SetTarget(Turret _target)
    {

        if (ui.activeSelf && target == _target)
        {
            Hide();
            return;
        }

        if (target != null)
        {
            target.HideRange();
        }

        target = _target;
        transform.position = target.transform.position;

        ui.SetActive(true);

        target.ShowRange();

        if (target.isUpgraded)
        {
            upgradeButton.interactable = false;
            upgradeCostText.text = "X";
            sellPriceText.text = "- $" + target.blueprint.upgradedSellPrice;
        }
        else
        {
            upgradeButton.interactable = true;
            upgradeCostText.text = "- $" + target.blueprint.upgradeCost;
            sellPriceText.text = "- $" + target.blueprint.sellPrice;
        }
    }


    public void Hide()
    {
        if (target != null)
        {
            target.HideRange();
        }
        ui.SetActive(false);
    }


    public void Upgrade()
    {
        buildManager.UpgradeTurret(target);

        if (target.isUpgraded)
        {

            target.GetComponent<Turret>().SetTurretStatus(TurretStatus.Active);
            DeselectTurret();
        }
    }

    public void Sell()
    {
        buildManager.SellTurret(target);
    }

    public Turret GetTarget()
    {
        return target;
    }

    public void DeselectTurret()
    {
        Hide();
    }
}
