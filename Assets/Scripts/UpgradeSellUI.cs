using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeSellUI : MonoBehaviour
{
    private Turret target;
    private BuildManager buildManager;
    public GameObject ui;

    public Text upgradeCostText;
    public Text sellPriceText;
    public Button upgradeButton;
    public Button sellButton;


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
            if (GameManager.gameIsOver)
            {
                this.Hide();
                return;
            }

            buildManager.SelectTurretToBuild(null);


            if (Input.GetMouseButtonDown(0) && justSelectedTurret == false && !EventSystem.current.IsPointerOverGameObject())
            {
                Hide();
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                this.upgradeButton.onClick.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                this.sellButton.onClick.Invoke();
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

        ui.SetActive(true);

        target.ShowRange();

        if (target.isUpgraded)
        {
            upgradeButton.interactable = false;
            upgradeCostText.text = "X";
            sellPriceText.text = "+ $" + target.blueprint.upgradedSellPrice;
        }
        else
        {
            upgradeButton.interactable = true;
            upgradeCostText.text = "- $" + target.blueprint.upgradeCost;
            sellPriceText.text = "+ $" + target.blueprint.sellPrice;
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
}
