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
    private int _projectileAttackCount = 1;

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
                                                      attackType,
                                                      _projectileAttackCount);
    }

    public override bool OnUpgrad(Element addElement)
    {
        if (upgradLevel >= 3) return false;
        _applyelements.Add(addElement);
        switch (upgradLevel)
        {
            case 0:
                attackType = addElement;
                upgradLevel++;
                firePoint.GetChild((int)addElement).gameObject.SetActive(true);
                return true;
            case 1:
                switch (addElement)
                {
                    case Element.Normal:
                        break;
                    case Element.Fire:
                        _bulletName = "BoomBullet";
                        break;
                    case Element.Ice:
                        reloadTime *= 0.5f;
                        break;
                    case Element.Electricity:
                        _projectileAttackCount = 2;
                        break;
                    default:
                        break;
                }
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
