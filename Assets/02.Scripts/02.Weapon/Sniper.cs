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
        // �� �̿����� ���� ��
        if (_isZoom)
        {
            _isZoom = false;
        }
        // �� �ƴҶ� �� ��Ŵ
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

    // �ִϸ��̼ǿ��� ������ �ٶ󺼶� ȭ���� ��ȯ�ǰ�, public ���� ���
    public void ZoomIN()
    {
        maincamera.fieldOfView = _originFOV / _sniperInfo.ZoomGain;
        MainUIManager.instance.ShowWeaponCircle(weaponInfo.Type, true);
        // �ڽ�0 -> ������
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
            // �ڽ�0 -> ������
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
