using UnityEngine;
public enum EnemyBuffs
{
    None,
    Shield,
    Barrier,
    Rush,
    BulkUp,
    Smoke,
    Immortality,
    Tent,
    Band
}
public enum BuffStatus
{
    Enable,
    Update,
    Hit,
    Disable
}
public abstract class EnemyBuffBase
{
    protected Enemy owner;

    public EnemyBuffBase(Enemy owner)
    {
        this.owner = owner;
    }

    public void SetOwner(Enemy enemy) => owner = enemy;

    public abstract void BuffActive(BuffStatus status);

    public virtual float HitActive(float damage) => damage;
}
