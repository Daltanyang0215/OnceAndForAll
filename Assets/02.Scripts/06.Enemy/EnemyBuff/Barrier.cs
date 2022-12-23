using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : EnemyBuffBase
{
    private int _barrierCount = 10;
    private GameObject _prefab;
    public Barrier(Enemy owner) : base(owner)
    {
    }


    public override void BuffActive(BuffStatus status)
    {
        switch (status)
        {
            case BuffStatus.Enable:
                _prefab= ObjectPool.Instance.Spawn("Barrier", owner.transform.position+ owner.GetComponent<SphereCollider>().center, Quaternion.identity, owner.transform);
                _prefab.transform.localScale = Vector3.one * owner.GetComponent<SphereCollider>().radius * 2.6f;
                break;
            case BuffStatus.Update:
                break;
            case BuffStatus.Hit:
                if (_barrierCount > 0)
                {
                    _barrierCount--;
                    if (_barrierCount == 0)
                    {
                        ObjectPool.Instance.Return(_prefab);
                        owner.RemoveBuff(this);
                    }
                }
                break;
            case BuffStatus.Disable:
                break;
            default:
                break;
        }
    }

    public override float HitActive(float damage)
    {
        return 0;
    }
}
