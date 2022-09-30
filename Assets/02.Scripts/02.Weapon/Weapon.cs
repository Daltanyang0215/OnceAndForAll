using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponInfo weaponInfo;

    private int _currentBullet;
    private float _attackCoolTimer;
    private float _reLoadTimer;

    protected bool _isAttackCool;
    private bool _isReloading;

    private void Start()
    {
        _currentBullet = weaponInfo.MaxBullet;
        _attackCoolTimer = weaponInfo.AttackCool;
        _reLoadTimer = weaponInfo.ReloadTIme;
    }

    private void Update()
    {
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

        if (_isReloading)
        {
            if (_reLoadTimer < 0)
            {
                ReloadTimeReset();
                _currentBullet = weaponInfo.MaxBullet;
                Debug.Log("������ �Ϸ�");
            }
            else
            {
                _reLoadTimer -= Time.deltaTime;
            }
        }

    }

    public virtual void Attack()
    {
        if (_isAttackCool) return;

        // �����
        if(_isReloading) Debug.Log("������ ���");
        
        //

        _isAttackCool = true;
        _attackCoolTimer = weaponInfo.AttackCool;
        ReloadTimeReset();

        if(_currentBullet > 0)
        {
            Debug.Log("��");
            _currentBullet--;
        }
        else
        {
            Debug.Log("ź�����");
        }

    }

    public void Reload()
    {
        _isReloading=true;
        Debug.Log("������ ��");
    }

    private void ReloadTimeReset()
    {

        _isReloading = false;
        _reLoadTimer = weaponInfo.ReloadTIme;
    }
}
