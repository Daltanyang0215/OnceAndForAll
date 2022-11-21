using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rush : EnemyBuffBase
{
    public Rush(Enemy owner) : base(owner)
    {
    }

    public override void BuffActive(BuffStatus status)
    {
        switch (status)
        {
            case BuffStatus.Enable:
                owner.navi.enabled = false;
                owner.rb.isKinematic = false;
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
