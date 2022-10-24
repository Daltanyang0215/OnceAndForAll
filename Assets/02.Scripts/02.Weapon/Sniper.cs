using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Weapon
{
    private SniperInfo _sniperInfo;

    private bool _isZoom;
    private float _originFOV;
    public override void WaeponAction()
    {
        // ¡‹ ¿Ãø¥¿ª∂ß «ÿ¡¶ «‘
        if (_isZoom)
        {
            _isZoom = false;
        }
        // ¡‹ æ∆¥“∂ß ¡‹ Ω√≈¥
        else
        {
            _isZoom = true;
        }

        Zoom(_isZoom);

    }

    public override void Reload()
    {
        base.Reload();
        _isZoom = false;
        Zoom(false);
    }

    public override void EnforceApply()
    {
        base.EnforceApply();
    }

    private void Zoom(bool show)
    {
        if (show)
        {
            maincamera.fieldOfView = _originFOV / _sniperInfo.ZoomGain;
            MainUIManager.instance.ShowWeaponCircle(weaponInfo.Type, true);
        }
        else
        {
            maincamera.fieldOfView = _originFOV;
            MainUIManager.instance.ShowWeaponCircle(weaponInfo.Type, false);
        }
    }

    protected override void Start()
    {
        base.Start();
        _sniperInfo = weaponInfo as SniperInfo;
        _originFOV = maincamera.fieldOfView;
    }
    
    protected override void OnDisable()
    {
        base.OnDisable();
        Zoom(false);
    }
}
