using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBox : MonoBehaviour, IInteraction
{
    private WeaponBoxManager _manager;
    private Transform _renderer;
    private Transform _shadowRenderer;
    [SerializeField] private int _weaponIndex;

    [SerializeField]private bool _waeponUsing;

    private void Awake()
    {
        _renderer = transform.GetChild(0);
        _shadowRenderer = transform.GetChild(1);
        _manager = transform.parent.GetComponent<WeaponBoxManager>();
        //_manager.ChildBoxAdd(this);
    }

    public void Interaction()
    {
        if (_waeponUsing) return;

        _waeponUsing = true;
        _renderer.gameObject.SetActive(false);
        _shadowRenderer.gameObject.SetActive(true);
        _manager.BoxIntaraction(_weaponIndex);
    }

    public void RendererReset()
    {
        _waeponUsing = false;   
        _renderer.gameObject.SetActive(true);
        _shadowRenderer.gameObject.SetActive(false);
    }
}
