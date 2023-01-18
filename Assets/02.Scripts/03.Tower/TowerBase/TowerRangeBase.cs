using UnityEngine;

public abstract class TowerRangeBase : TowerBase
{
    protected Collider[] cols;
    protected EnemyBuffBase giveDebuff = null;
    [SerializeField] protected Transform _effectTr;
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
                if (addElement == Element.Normal) damage *= StatesEnforce.Instance.elementNomralDamageGain;
                attackType = addElement;
                upgradLevel++;

                _effectTr.GetChild(0).gameObject.SetActive(false);
                _effectTr.GetChild((int)addElement).gameObject.SetActive(true);
                return true;
            case 1:
                //_bulletName = "ProjectileBullet_" + addElement.ToString();

                switch (addElement)
                {
                    case Element.Normal:
                        damage *= StatesEnforce.Instance.elementNomralDamageGain;
                        break;
                    case Element.Fire:
                        attackRange *= 1.5f;
                        break;
                    case Element.Ice:
                        reloadTime *= 0.667f;
                        break;
                    case Element.Electricity:
                        damage *= StatesEnforce.Instance.elementNomralDamageGain;
                        break;
                    default:
                        break;
                }

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
