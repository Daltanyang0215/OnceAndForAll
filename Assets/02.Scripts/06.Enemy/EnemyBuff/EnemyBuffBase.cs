using UnityEngine;
public enum EnemyBuffs
{
    None,
    Shild,
    EnegyShild,
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

    public abstract void BuffActive(BuffStatus status);
}
