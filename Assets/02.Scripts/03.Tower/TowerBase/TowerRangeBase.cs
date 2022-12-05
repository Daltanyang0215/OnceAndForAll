using UnityEngine;

public abstract class TowerRangeBase : TowerBase
{
    protected Collider[] cols;
    protected string _afterEffect;

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

    public override bool OnUpgrad(Element addElement)
    {
        if (upgradLevel >= 3) return false;
        switch (upgradLevel)
        {
            case 0:
                attackType = addElement;
                upgradLevel++;
                return true;
            case 1:
                //_bulletName = "ProjectileBullet_" + addElement.ToString();
                upgradLevel++;

                return true;
            case 2:
                upgradLevel++;
                _afterEffect = addElement.ToString();
                return true;
            default:
                return false;

        }
    }
}
