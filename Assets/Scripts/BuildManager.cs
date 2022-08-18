using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;

public class BuildManager : MonoBehaviour
{
    #region Singleton
    public static BuildManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("BuildManager instace already created");
            return;
        }
        instance = this;
    }
    #endregion

    private TurretBluePrint turretToBuild;

    [SerializeField]
    public UpgradeSellUI upgradeSellUI;
    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasEnoughMoney { get { return PlayerStats.money >= turretToBuild.cost; } }


    private void Start()
    {
        turretToBuild = null;
    }


    public bool IsBuilding()
    {
        return (turretToBuild != null);
    }


    public void SelectTurretToBuild(TurretBluePrint turret)
    {
        turretToBuild = turret;
        if (turretToBuild == null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void SellTurret(Turret turretToSell)
    {
        if (turretToSell.isUpgraded)
            PlayerStats.money += turretToSell.blueprint.upgradedSellPrice;
        else
            PlayerStats.money += turretToSell.blueprint.sellPrice;

        Destroy(turretToSell.gameObject);
        upgradeSellUI.DeselectTurret();
    }

    public void UpgradeTurret(Turret turretToUpgrade)
    {
        if (turretToUpgrade.blueprint.upgradeCost < PlayerStats.money)
        {
            GameObject upgradedTurret = Instantiate(turretToUpgrade.blueprint.upgradedPrefab, turretToUpgrade.transform.position, Quaternion.identity);
            upgradedTurret.GetComponent<Turret>().isUpgraded = true;
            upgradedTurret.GetComponent<Turret>().SetBlueprint(turretToUpgrade.blueprint);

            Destroy(turretToUpgrade.gameObject);

            upgradeSellUI.SetTarget(upgradedTurret.GetComponent<Turret>());

            PlayerStats.money -= turretToUpgrade.blueprint.upgradeCost;
        }
    }

    //Accessors
    public TurretBluePrint GetTurretToBuild() { return turretToBuild; }
}
