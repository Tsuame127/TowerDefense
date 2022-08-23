using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    private BuildManager buildManager;

    [Header("Blueprints")]
    [SerializeField]
    private TurretBluePrint standardTurret;
    [SerializeField]
    private TurretBluePrint missileLauncherTurret;
    [SerializeField]
    private TurretBluePrint laserBeamerTurret;

    [Header("Buttons")]
    [SerializeField]
    private Button standTurretButton;
    [SerializeField]
    private Button missileTurretButton;
    [SerializeField]
    private Button laserTurretButton;

    [Header("Price Texts")]
    private Text standardTurretText;
    private Text missileTurretText;
    private Text laserTurretText;


    private void Start()
    {
        buildManager = BuildManager.instance;

        standardTurretText = standTurretButton.GetComponentInChildren<Text>();
        missileTurretText = missileTurretButton.GetComponentInChildren<Text>();
        laserTurretText = laserTurretButton.GetComponentInChildren<Text>();

        standardTurretText.text = "$" + standardTurret.cost;
        missileTurretText.text = "$" + missileLauncherTurret.cost;
        laserTurretText.text = "$" + laserBeamerTurret.cost;

        InitTurretPlacementRadius();
    }


    public void SelectStandardTurret()
    {
        buildManager.DeselectTurret();
        buildManager.SelectTurretToBuild(standardTurret);
    }

    public void SelectMissileLauncher()
    {
        buildManager.DeselectTurret();
        buildManager.SelectTurretToBuild(missileLauncherTurret);
    }

    public void SelectLaserBeamer()
    {
        buildManager.DeselectTurret();
        buildManager.SelectTurretToBuild(laserBeamerTurret);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SelectStandardTurret();

        if (Input.GetKeyDown(KeyCode.Alpha2))
            SelectMissileLauncher();

        if (Input.GetKeyDown(KeyCode.Alpha3))
            SelectLaserBeamer();

        if (buildManager.GetTurretToBuild() == standardTurret)
            standTurretButton.Select();

        if (buildManager.GetTurretToBuild() == missileLauncherTurret)
            missileTurretButton.Select();

        if (buildManager.GetTurretToBuild() == laserBeamerTurret)
            laserTurretButton.Select();
    }

    private void InitTurretPlacementRadius()
    {
        standardTurret.placementRadius = standardTurret.prefab.GetComponent<CapsuleCollider>().radius;
        missileLauncherTurret.placementRadius = missileLauncherTurret.prefab.GetComponent<CapsuleCollider>().radius;
        laserBeamerTurret.placementRadius = laserBeamerTurret.prefab.GetComponent<CapsuleCollider>().radius;
    }
}
