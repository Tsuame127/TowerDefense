using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBluePrint
{
    public GameObject prefab;
    public int cost;
    public int sellPrice;

    public GameObject upgradedPrefab;
    public int upgradeCost;
    public int upgradedSellPrice;
}
