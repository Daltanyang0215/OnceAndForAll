using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectileBase : TowerTargetingBase
{
    [Space]
    [Header("projectile")]
    [SerializeField] private string _bulletName;
    [SerializeField] private int _projectileSpeed;
    private int _projectileAttackCount = 1;
    private EnemyBuffBase giveDebuff = null;
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
                                                      _projectileAttackCount,
                                                      giveDebuff);
    }

    public override bool OnUpgrad(Element addElement)
    {
        if (upgradLevel >= 3) return false;
        _applyelements.Add(addElement);
        switch (upgradLevel)
        {
            case 0:
                attackType = addElement;
                firePoint.GetChild((int)addElement).gameObject.SetActive(true);
                upgradLevel++;
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

                switch (addElement)
                {
                    case Element.Normal:
                        break;
                    case Element.Fire:
                        giveDebuff = new Burning(null, 3, 50);
                        break;
                    case Element.Ice:
                        giveDebuff = new Chilling(null, 10, 50);
                        break;
                    case Element.Electricity:
                        giveDebuff = new Sparking(null, 10f,2f);
                        break;
                    default:
                        break;
                }
                upgradLevel++;
                return true;
            default:
                return false;
        }
    }
}
