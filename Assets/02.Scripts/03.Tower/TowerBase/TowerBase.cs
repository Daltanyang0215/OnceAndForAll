using UnityEngine;

public abstract class TowerBase : MonoBehaviour
{
    [Header("TowerInfo")]
    [SerializeField] protected TowerInfo _towerInfo;

    [Space]
    [Header("TargetLayer")]
    [SerializeField] protected LayerMask targetLayer;
    [SerializeField] protected LayerMask blockLayer;

    [SerializeField] protected float attackRange;
    [SerializeField] protected float reloadTime;
    protected bool _isLoading;

    protected float timer;

    public abstract void OnApply();

    protected virtual void Update()
    {
        Reload();
    }

    protected virtual void Start()
    {
        if (_isLoading)
        {
            OnApply();
        }
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
