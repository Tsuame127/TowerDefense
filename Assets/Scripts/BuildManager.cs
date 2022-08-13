using UnityEngine;

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
    private Node selectedNode;

    [SerializeField]
    private NodeUI nodeUI;
    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasEnoughMoney { get { return PlayerStats.money >= turretToBuild.cost; } }


    private void Start()
    {
        turretToBuild = null;
        selectedNode = null;
    }


    public bool IsBuilding()
    {
        return ((turretToBuild != null) || (selectedNode != null));
    }


    public void SelectTurretToBuild(TurretBluePrint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }


    public void SelectNode(Node node)
    {
        if (node == selectedNode)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }


    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }


    //Accessors
    public TurretBluePrint GetTurretToBuild() { return turretToBuild; }
}
