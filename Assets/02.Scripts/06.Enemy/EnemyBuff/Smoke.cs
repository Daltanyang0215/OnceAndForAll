using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : EnemyBuffBase
{
    public Smoke(Enemy owner) : base(owner)
    {
    }

    public override void BuffActive(BuffStatus status)
    {
        switch (status)
        {
            case BuffStatus.Enable:
                break;
            case BuffStatus.Update:
                break;
            case BuffStatus.Hit:
                break;
            case BuffStatus.Disable:
                ObjectPool.Instance.Spawn("Smoke", owner.transform.position);
                break;
            default:
                break;
        }
    }
}
