using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectileBase : TowerTargetingBase
{
    [Space]
    [Header("projectile")]
    [SerializeField] private string _bulletName;
    [SerializeField] private int _projectileSpeed;

    public override void OnApply()
    {
    }

    protected override void Attack()
    {
        GameObject bullet = ObjectPool.Instance.Spawn(_bulletName, firePoint.position);
        bullet.GetComponent<ProjectileBullet>().SetUp(target,
                                                      _projectileSpeed,
                                                      damage,
                                                      false,
                                                      blockLayer,
                                                      targetLayer);
    }
}
