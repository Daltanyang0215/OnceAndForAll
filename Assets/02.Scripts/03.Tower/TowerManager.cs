using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType
{
    Projectile,
    Range,
    Lift,
    Destroy
}
public class TowerManager : MonoBehaviour
{
    public static TowerManager instance;
    private void Awake()
    {
        instance = this; 
    }
    public GameObject towerBuildPoint;
    public List<TowerInfo> towerlist = new List<TowerInfo>();

    public void OnStatesEnforce()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<TowerBase>().OnApply();
        }
    }
}
