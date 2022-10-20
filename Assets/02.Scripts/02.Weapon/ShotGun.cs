using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Weapon
{
    private ShotGunInfo shotGunInfo;

    protected override void Start()
    {
        base.Start();
        shotGunInfo = weaponInfo as ShotGunInfo;
    }

    protected override void Shot()
    {
        Collider[] hits = Physics.OverlapSphere(Player.Instance.transform.position, shotGunInfo.ShotRange);
        foreach (var hit in hits)
        {
            Vector3 tmp = hit.transform.position+Vector3.up - maincamera.transform.position;
            if(Vector3.Angle(tmp, maincamera.transform.forward) < shotGunInfo.HitCurcleError)
            {
                if(hit.TryGetComponent(out Enemy enemy))
                {
                    enemy.Hit(damage);
                }
            }
        }
    }
}
