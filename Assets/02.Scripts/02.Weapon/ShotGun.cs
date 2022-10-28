using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Weapon
{
    private ShotGunInfo _shotGunInfo;

    protected override void Start()
    {
        base.Start();
        _shotGunInfo = weaponInfo as ShotGunInfo;
    }

    protected override void Shot()
    {
        Collider[] hits = Physics.OverlapSphere(Player.Instance.transform.position, _shotGunInfo.ShotRange);
        foreach (var hit in hits)
        {
            // ���� �� ���� Ȯ��
            Vector3 tmp = hit.transform.position+Vector3.up - maincamera.transform.position;
            if(Vector3.Angle(tmp, maincamera.transform.forward) < _shotGunInfo.HitCurcleError)
            {
                // ��ֹ� Ȯ��
                if (Physics.Raycast(maincamera.transform.position, hit.transform.position - maincamera.transform.position, out _hit, 200f))
                {
                    if (_hit.collider.TryGetComponent(out Enemy enemy))
                    {
                        enemy.Hit(damage);
                    }
                }
            }
        }
    }

    public override void OnApply()
    {
        base.OnApply();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        MainUIManager.instance.ShowWeaponCircle(weaponInfo.Type, true);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        MainUIManager.instance.ShowWeaponCircle(weaponInfo.Type, false);
    }
}
