using UnityEngine;

public class BulkUp : EnemyBuffBase
{
    public BulkUp(Enemy owner) : base(owner)
    {
    }

    public override void BuffActive(BuffStatus status)
    {
        switch (status)
        {
            case BuffStatus.Enable:

                owner.gameObject.transform.localScale = Vector3.one * 2f;
                owner.EnemyHealth *= 2;
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
