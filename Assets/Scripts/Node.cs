using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    private Color startColor;
    public Color notEnoughMoneyColor;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBluePrint turretBluePrint;
    [HideInInspector]
    public bool isUpgraded = false;

    public Vector3 positionOffset;

    private Renderer rend;
    private BuildManager buildManager;
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private void BuildTurret(TurretBluePrint blueprint)
    {
        if (buildManager.HasEnoughMoney)
        {
            PlayerStats.money -= blueprint.cost;

            turretBluePrint = blueprint;

            GameObject buildedTurret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
            turret = buildedTurret;

            buildManager.SelectTurretToBuild(null);
        }
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.money >= turretBluePrint.upgradeCost)
        {
            Destroy(turret);

            PlayerStats.money -= turretBluePrint.upgradeCost;

            GameObject buildedTurret = (GameObject)Instantiate(turretBluePrint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
            turret = buildedTurret;

            isUpgraded = true;

            buildManager.SelectTurretToBuild(null);
        }

    }

    public void SellTurret()
    {
        if (isUpgraded)
        {
            PlayerStats.money += turretBluePrint.upgradedSellPrice;
        }
        else
        {
            PlayerStats.money += turretBluePrint.sellPrice;
        }

        Destroy(turret);
        turretBluePrint = null;
        isUpgraded = false;
    }


    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }

        BuildTurret(buildManager.GetTurretToBuild());
    }

    private void OnMouseEnter()
    {
        if (buildManager.CanBuild == false || EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (!buildManager.HasEnoughMoney || turret != null)
        {
            rend.material.color = notEnoughMoneyColor;
        }
        else
        {
            rend.material.color = hoverColor;
        }
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    public GameObject GetTurret()
    {
        return turret;
    }
}
