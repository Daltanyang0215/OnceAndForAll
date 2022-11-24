using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tent : EnemyBuffBase
{
    private GameObject _prefab;
    public Tent(Enemy owner) : base(owner)
    {
    }

    public override void BuffActive(BuffStatus status)
    {
        switch (status)
        {
            case BuffStatus.Enable:
                _prefab = ObjectPool.Instance.Spawn("Tent", owner.transform.position, Quaternion.identity, owner.transform);
                //_prefab.transform.localScale = owner.transform.localScale;
                break;
            case BuffStatus.Update:
                break;
            case BuffStatus.Hit:
                break;
            case BuffStatus.Disable:
                ObjectPool.Instance.Return(_prefab);
                break;
            default:
                break;
        }
    }
}
