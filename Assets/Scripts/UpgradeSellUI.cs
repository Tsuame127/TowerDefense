using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
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


    private bool justSelectedTurret;

    private void Start()
    {
        buildManager = BuildManager.instance;
        justSelectedTurret = false;
    }

    private void Update()
    {
        if (ui.activeSelf)
        {
            buildManager.SelectTurretToBuild(null);

            if (Input.GetMouseButtonDown(0) && justSelectedTurret == false && !EventSystem.current.IsPointerOverGameObject())
            {
                Hide();
            }
        }

        justSelectedTurret = false;
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

        justSelectedTurret = true;
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

    private GameObject GetClickedGameObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f))
        {
            return hit.transform.gameObject;
        }

        return null;
    }
}
