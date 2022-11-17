using UnityEngine;

public class BulkUp : EnemyBuffBase
{
    public BulkUp(Enemy owner) : base(owner)
    {
    }
    public override void Actionbuff()
    {
        owner.gameObject.transform.localScale = Vector3.one * 2f;
        owner.EnemyHealth *= 2;
    }
}
