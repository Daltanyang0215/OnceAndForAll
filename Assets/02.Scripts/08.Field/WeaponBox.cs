using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBox : MonoBehaviour, IInteraction
{
    private WeaponBoxManager _manager;
    private Transform _renderer;
    private int _weaponIndex;

    private void Awake()
    {
        _renderer = transform.GetChild(0);
        _manager = transform.parent.GetComponent<WeaponBoxManager>();
        _manager.ChildBoxAdd(this);
    }

    public void Interaction()
    {
        _renderer.gameObject.SetActive(false);
        _manager.BoxIntaraction();
    }

}
