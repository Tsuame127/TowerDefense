using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        turretToBuild = null;
        selectedNode = null;
    }

    private TurretBluePrint turretToBuild;
    public Node selectedNode;
    public NodeUI nodeUI;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasEnoughMoney { get { return PlayerStats.money >= turretToBuild.cost; } }


    public bool IsBuilding()
    {
        return ((turretToBuild != null) || (selectedNode != null));
    }

    public TurretBluePrint GetTurretToBuild()
    {
        return turretToBuild;
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

    private void Update()
    {
    }
}
