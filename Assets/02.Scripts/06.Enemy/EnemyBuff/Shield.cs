using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : EnemyBuffBase
{
    private GameObject _prefab;
    private SphereCollider _col;
    public Shield(Enemy owner) : base(owner)
    {
        _col = owner.GetComponent<SphereCollider>();
    }

    public override void BuffActive(BuffStatus status)
    {
        switch (status)
        {
            case BuffStatus.Enable:
                _prefab = ObjectPool.Instance.Spawn("Shield", owner.transform.position + Vector3.back * _col.radius * 1.5f, owner.transform.rotation, owner.transform);
                _prefab.transform.localScale = Vector3.one * _col.radius * 1.5f;
                break;
            case BuffStatus.Update:
                break;
            case BuffStatus.Hit:
                
                break;
            case BuffStatus.Disable:

                break;
            default:
                break;
        }
    }
}
