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
        // ÁÜ ÀÌ¿´À»¶§ ÇØÁ¦ ÇÔ
        if (_isZoom)
        {
            _isZoom = false;
        }
        // ÁÜ ¾Æ´Ò¶§ ÁÜ ½ÃÅ´
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
            animator.SetBool("Zoom", true);
            maincamera.fieldOfView = _originFOV / _sniperInfo.ZoomGain;
            MainUIManager.instance.ShowWeaponCircle(weaponInfo.Type, true);
            // ÀÚ½Ä0 -> ·»´õ·¯
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            animator.SetBool("Zoom", false);
            maincamera.fieldOfView = _originFOV;
            MainUIManager.instance.ShowWeaponCircle(weaponInfo.Type, false);
            // ÀÚ½Ä0 -> ·»´õ·¯
            transform.GetChild(0).gameObject.SetActive(true);
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
