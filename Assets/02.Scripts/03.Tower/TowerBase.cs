using UnityEngine;

public abstract class TowerBase : MonoBehaviour
{
    [SerializeField] protected TowerInfo _towerInfo;
    [SerializeField] protected float reloadTime;
    protected float timer;

    public abstract void OnApply();

    protected virtual void Start()
    {
        OnApply();
    }
}
