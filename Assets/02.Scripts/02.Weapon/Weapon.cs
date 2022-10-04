using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponInfo _weaponInfo;

    [SerializeField] private Transform _firePoint;
    [SerializeField] private ParticleSystem _fireParticale;
    [SerializeField] private ParticleSystem _hitParticale;

    private int _currentBullet;
    private float _attackCoolTimer;
    private float _reLoadTimer;

    [SerializeField] private Camera _camera;
    
    private RaycastHit _hit;

    // 공격 쿨타임인지 확인
    protected bool _isAttackCool;
    // 재장전 중인지 확인
    private bool _isReloading;

    private void Start()
    {
        _currentBullet = _weaponInfo.MaxBullet;
        _attackCoolTimer = _weaponInfo.AttackCool;
        _reLoadTimer = _weaponInfo.ReloadTIme;
    }

    private void Update()
    {
        // 공격 타이머
        if (_isAttackCool)
        {
            if (_attackCoolTimer < 0)
            {
                _isAttackCool = false;
            }
            else
            {
                _attackCoolTimer -= Time.deltaTime;
            }
        }

        // 재장전 타이머
        if (_isReloading)
        {
            if (_reLoadTimer < 0)
            {
                ReloadTimeReset();
                _currentBullet = _weaponInfo.MaxBullet;
                Debug.Log("재장전 완료");
            }
            else
            {
                _reLoadTimer -= Time.deltaTime;
            }
        }

    }

    // 공격
    public virtual void Attack()
    {
        if (_isAttackCool) return;

        // 디버그
        if(_isReloading) Debug.Log("재장전 취소");
        
        //

        _isAttackCool = true;
        _attackCoolTimer = _weaponInfo.AttackCool;
        ReloadTimeReset();

        if(_currentBullet > 0)
        {
            Debug.Log("빵");
            
            if (Physics.Raycast(_camera.transform.position,_camera.transform.forward, out _hit,200f))
            {
                Destroy( Instantiate(_hitParticale,_hit.point,Quaternion.identity),0.2f);
            }
            _currentBullet--;
            _fireParticale.Play();

        }
        else
        {
            Debug.Log("탄약부족");
        }

    }

    public void Reload()
    {
        _isReloading=true;
        Debug.Log("재장전 중");
    }

    private void ReloadTimeReset()
    {

        _isReloading = false;
        _reLoadTimer = _weaponInfo.ReloadTIme;
    }
    private void OnEnable()
    {
        // play animation
    }

    private void OnDisable()
    {
        ReloadTimeReset();
    }
}
