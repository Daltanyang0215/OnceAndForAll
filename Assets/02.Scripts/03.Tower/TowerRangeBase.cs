using UnityEngine;

public abstract class TowerRangeBase : TowerBase
{
    [SerializeField] protected float attackRange;




    abstract protected void Attack();

}
