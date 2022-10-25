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
        // 줌 이였을때 해제 함
        if (_isZoom)
        {
            _isZoom = false;
        }
        // 줌 아닐때 줌 시킴
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

    // 애니메이션에서 스코프 바라볼때 화면이 전환되게, public 으로 사용
    public void ZoomIN()
    {
        maincamera.fieldOfView = _originFOV / _sniperInfo.ZoomGain;
        MainUIManager.instance.ShowWeaponCircle(weaponInfo.Type, true);
        // 자식0 -> 렌더러
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void Zoom(bool show)
    {
        if (show)
        {
            animator.SetBool("Zoom", true);
        }
        else
        {
            animator.SetBool("Zoom", false);
            maincamera.fieldOfView = _originFOV;
            MainUIManager.instance.ShowWeaponCircle(weaponInfo.Type, false);
            // 자식0 -> 렌더러
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
