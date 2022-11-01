using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static TowerManager instance;
    private void Awake()
    {
        instance = this; 
    }

    public void OnStatesEnforce()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<TowerBase>().OnApply();
        }
    }
}
