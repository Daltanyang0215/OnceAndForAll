using UnityEngine;
public enum Element
{
    Normal,
    Fire,
    Ice,
    Electricity
}
public abstract class TowerBase : MonoBehaviour
{
    [Header("TowerInfo")]
    [SerializeField] protected TowerInfo towerInfo;

    [Space]
    [Header("TargetLayer")]
    [SerializeField] protected LayerMask targetLayer;
    [SerializeField] protected LayerMask blockLayer;

    [Space]
    [Header("Attack")]
    protected Element attackType;
    protected float damage;
    protected float attackRange;
    protected float reloadTime;
    protected bool _isLoading;

    protected float timer;

    protected int upgradLevel=0;

    public virtual void OnApply()
    {
        damage = towerInfo.Damage * StatesEnforce.Instance.TowerDamageGain;

        attackRange = towerInfo.AttackRange * StatesEnforce.Instance.TowerRangeGain;
    }

    protected virtual void Awake()
    {
        damage = towerInfo.Damage;
        attackRange = towerInfo.AttackRange;
        reloadTime = towerInfo.AttackCool;

        timer = reloadTime;
    }

    protected virtual void Start()
    {
        OnApply();
    }

    protected virtual void Update()
    {
        if (_isLoading)
            Reload();
    }

    public abstract bool OnUpgrad(Element addElement);

    private void Reload()
    {
        if (reloadTime == -1) return;

        if (timer < 0)
        {
            _isLoading = false;
            timer = reloadTime;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
