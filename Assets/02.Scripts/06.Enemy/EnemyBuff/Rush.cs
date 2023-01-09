using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rush : EnemyBuffBase
{
    private SphereCollider _sphere;

    public Rush(Enemy owner) : base(owner)
    {
        _sphere = owner.GetComponent<SphereCollider>();
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
                if (Physics.CheckSphere(owner.transform.position+ _sphere.center, _sphere.radius*1.2f, 1 << LayerMask.NameToLayer("Tower")))
                {

                    owner.navi.enabled = true;
                    owner.rb.isKinematic = true;

                    owner.RemoveBuff(this);
                }
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
