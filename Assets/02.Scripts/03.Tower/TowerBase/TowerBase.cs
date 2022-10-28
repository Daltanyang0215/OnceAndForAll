using UnityEngine;

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
    [SerializeField] protected float damage;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float reloadTime;
    protected bool _isLoading;

    protected float timer;

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

    private void Reload()
    {
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
