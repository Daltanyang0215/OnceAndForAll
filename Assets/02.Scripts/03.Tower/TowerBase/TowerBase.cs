using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    public TowerInfo GetTowerInfo => towerInfo;

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

    protected int upgradLevel = 0;

    private MeshRenderer[] _modelMesh;
    protected List<Element> _applyelements = new List<Element>();
    public Element GetElement(int level) => _applyelements[level];

    public int GetElementLeath() => _applyelements.Count;

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

        _modelMesh = GetComponentsInChildren<MeshRenderer>();
    }

    protected virtual void Start()
    {
        OnApply();
        StartCoroutine(E_showDissolve());
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

    private IEnumerator E_showDissolve()
    {
        float disslove = 1;
        while (disslove > 0)
        {
            foreach (MeshRenderer mesh in _modelMesh)
            {
                mesh.material.SetFloat("_DissolveAmount", disslove);
            }
            disslove -= Time.deltaTime;

            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            Destroy(gameObject);
        }
    }
}
