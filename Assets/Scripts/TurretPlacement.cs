using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class TurretPlacement : MonoBehaviour
{

    public GameObject turretPrefab;

    [SerializeField]
    private LayerMask layerToHit;

    BuildManager buildManager;
    private GameObject placingTurret;

    public Color cantPlaceColor;
    public Color canPlaceColor;

    void Start()
    {
        buildManager = BuildManager.instance;

        placingTurret = null;
    }

    private void Update()
    {
        if (buildManager.IsBuilding() == false && placingTurret != null)
        {
            Destroy(placingTurret);
            return;
        }

        if (Input.mousePresent && buildManager.IsBuilding())
        {
            Vector3 screenpos;
            Vector3 worldpos;

            screenpos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(screenpos);

            if (Physics.Raycast(ray, out RaycastHit rayData, 1000, layerToHit))
            {
                worldpos = rayData.point;
                worldpos.y = 0;
                if (placingTurret == null || placingTurret.GetComponent<Turret>().blueprint != buildManager.GetTurretToBuild())
                {
                    Destroy(placingTurret);
                    placingTurret = (GameObject)Instantiate(buildManager.GetTurretToBuild().prefab, worldpos, Quaternion.identity);
                    placingTurret.GetComponent<Turret>().SetBlueprint(buildManager.GetTurretToBuild());
                    placingTurret.GetComponent<CapsuleCollider>().enabled = false;
                    placingTurret.GetComponent<Turret>().SetTurretStatus(TurretStatus.Placing);
                }

                placingTurret.transform.position = worldpos;

                if (!CanPlaceTurret(worldpos))
                {
                    placingTurret.transform.Find("RangeCylinder").GetComponent<Renderer>().material.color = cantPlaceColor;
                    return;
                }
                placingTurret.transform.Find("RangeCylinder").GetComponent<Renderer>().material.color = canPlaceColor;

                if (Input.GetMouseButtonDown(0))
                {
                    if (EventSystem.current.IsPointerOverGameObject())
                    {
                        Destroy(placingTurret);
                        buildManager.SelectTurretToBuild(null);
                        return;
                    }
                    PlayerStats.money -= buildManager.GetTurretToBuild().cost;

                    placingTurret.GetComponent<Turret>().HideRange();
                    placingTurret.GetComponent<Turret>().SetTurretStatus(TurretStatus.Active);
                    placingTurret.GetComponent<CapsuleCollider>().enabled = true;

                    buildManager.SelectTurretToBuild(null);
                    placingTurret = null;
                }

            }
        }
    }

    private bool CanPlaceTurret(Vector3 posToPlace)
    {
        if (buildManager.HasEnoughMoney)
        {

            Collider[] collidersInMouseRange = Physics.OverlapSphere(posToPlace, buildManager.GetTurretToBuild().placementRadius);

            foreach (Collider collider in collidersInMouseRange)
            {
                if (!collider.CompareTag("Ground"))
                {
                    return false;
                }
            }

            return true;
        }

        return false;
    }
}
