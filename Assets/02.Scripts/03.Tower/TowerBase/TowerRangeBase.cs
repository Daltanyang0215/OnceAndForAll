using UnityEngine;

public abstract class TowerRangeBase : TowerBase
{
    protected Collider[] cols;

    protected override void Update()
    {
        base.Update();
        if (_isLoading == false)
        {
            cols = Physics.OverlapSphere(transform.position, attackRange, targetLayer);
            if (cols.Length > 0)
            {
                _isLoading = true;
                Attack();
            }
        }
    }

    abstract protected void Attack();
}
