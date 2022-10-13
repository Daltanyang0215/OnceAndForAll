using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private bool _isGuided;
    protected bool _isHit;
    private float _speed;
    protected int damage;
    protected LayerMask touchLayer;
    protected LayerMask targetLayer;
    protected Transform target;
    protected Transform tr;


    public void SetUp(Transform target,
                      float speed,
                      int damage,
                      bool isGuided,
                      LayerMask touchLayer,
                      LayerMask targetLayer)
    {
        this.target = target;
        _speed = speed;
        this.damage = damage;
        _isGuided = isGuided;
        this.touchLayer = touchLayer;
        this.targetLayer = targetLayer;

        tr.LookAt(this.target.position + Vector3.up*0.5f);
        _isHit = false;
    }

    private void Awake()
    {
        tr = GetComponent<Transform>();
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

    protected virtual void OnTriggerEnter(Collider other)
    {

    }
}
