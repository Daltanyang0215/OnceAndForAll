using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectileBase : TowerTargetingBase
{
    [Space]
    [Header("projectile")]
    [SerializeField] private string _bulletName;
    [SerializeField] private int _projectileSpeed;
    [SerializeField] private string _afterEffect;

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
                                                      targetLayer,
                                                      attackType);
    }

    public override bool OnUpgrad(Element addElement)
    {
        if (upgradLevel >= 3) return false;
        switch (upgradLevel)
        {
            case 0:
                attackType = addElement;
                upgradLevel++;
                firePoint.GetChild((int)addElement).gameObject.SetActive(true);
                return true;
            case 1:
                _bulletName = "ProjectileBullet_" + addElement.ToString();
                upgradLevel++;

                return true;
           case 2:
                upgradLevel++;
                _afterEffect = addElement.ToString();
                return true;
            default:
                return false;

        }
    }
}
