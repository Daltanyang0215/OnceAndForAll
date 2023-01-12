using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private bool _isGuided;
    protected bool _isHit;
    private float _speed;
    protected float damage;
    protected Element attackElement;
    protected LayerMask touchLayer;
    protected LayerMask targetLayer;
    protected Transform target;
    protected Transform tr;
    protected float timer = 5;

    [SerializeField] private List<Material> _modelMaterials;
    private MeshRenderer _mesh;

    public void SetUp(Transform target,
                      float speed,
                      float damage,
                      bool isGuided,
                      LayerMask touchLayer,
                      LayerMask targetLayer,
                      Element element = Element.Normal)
    {
        this.target = target;
        _speed = speed;
        this.damage = damage;
        _isGuided = isGuided;
        this.touchLayer = touchLayer;
        this.targetLayer = targetLayer;
        attackElement = element;

        tr.GetChild((int)element).gameObject.SetActive(true);
        _mesh.material = _modelMaterials[(int)element];
        tr.LookAt(this.target.position + Vector3.up*0.5f);
        _isHit = false;
        timer = 5;
    }

    private void Awake()
    {
        tr = GetComponent<Transform>();
        _mesh = transform.GetChild(4).GetComponent<MeshRenderer>();
    }

    private void FixedUpdate()
    {
        if (_isHit) return;
        if (_isGuided)
        {
            tr.LookAt(target);
        }
            tr.Translate(Time.fixedDeltaTime * _speed * Vector3.forward,Space.Self);
    }

    private void Update()
    {
        if(timer < 0)
        {
            tr.GetChild((int)attackElement).gameObject.SetActive(false);
            ObjectPool.Instance.Return(gameObject);
        }

        timer -= Time.deltaTime;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {

    }
}
