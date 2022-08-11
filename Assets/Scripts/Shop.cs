using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    private BuildManager buildManager;
    public TurretBluePrint standardTurret;
    public TurretBluePrint missileLauncherTurret;
    public TurretBluePrint laserBeamerTurret;

    public Button standTurretButton;
    public Button missileTurretButton;
    public Button laserTurretButton;

    public Text standardTurretText;
    public Text missileTurretText;
    public Text laserTurretText;


    private void Start()
    {
        buildManager = BuildManager.instance;

        standardTurretText.text = "$" + standardTurret.cost;
        missileTurretText.text = "$" + missileLauncherTurret.cost;
        laserTurretText.text = "$" + laserBeamerTurret.cost;
    }
    

    public void SelectStandardTurret()
    {
        buildManager.SelectTurretToBuild(standardTurret);
    }

    public void SelectMissileLauncher()
    {
        buildManager.SelectTurretToBuild(missileLauncherTurret);
    }

    public void SelectLaserBeamer()
    {
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
}
