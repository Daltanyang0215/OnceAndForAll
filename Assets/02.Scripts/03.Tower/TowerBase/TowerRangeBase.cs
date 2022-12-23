using UnityEngine;

public abstract class TowerRangeBase : TowerBase
{
    protected Collider[] cols;
    protected EnemyBuffBase giveDebuff = null;
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
        _applyelements.Add(addElement);
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
                switch (addElement)
                {
                    case Element.Normal:
                        break;
                    case Element.Fire:
                        giveDebuff = new Burning(null, 3, 50);
                        break;
                    case Element.Ice:
                        giveDebuff = new Chilling(null, 10, 50);
                        break;
                    case Element.Electricity:
                        giveDebuff = new Sparking(null,10, 2f);
                        break;
                    default:
                        break;
                }
                return true;
            default:
                return false;

        }
    }
}
