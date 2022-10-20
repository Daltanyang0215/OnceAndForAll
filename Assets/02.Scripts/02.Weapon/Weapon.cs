using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponInfo weaponInfo;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private TMP_Text _bulletCountText;
    [SerializeField] protected ParticleSystem fireParticale;
    private Animator _animator;

    private int _currentBullet;
    private int CurrentBullet
    {
        get
        {
            return _currentBullet;
        }
        set
        {
            _currentBullet = value;
            _bulletCountText.text = _currentBullet.ToString();
        }
    }
    private float _attackCoolTimer;
    private float _reLoadTimer;

    protected Camera maincamera;
    private RaycastHit _hit;

    protected float damage;

    // ���� ��Ÿ������ Ȯ��
    protected bool _isAttackCool;
    // ������ ������ Ȯ��
    private bool _isReloading;

    // ���� (player script call ����)
    public float Attack()
    {
        if (!_isAttackCool)
        {

            // �����. ���� �ִϸ��̼� �߰� �� ���� ����
            if (_isReloading) Debug.Log("������ ���");

            //

            ReloadTimeReset();

                _isAttackCool = true;
            if (CurrentBullet > 0)
            {
                _attackCoolTimer = weaponInfo.AttackCool;
                Shot();
                CurrentBullet--;
                _animator.SetTrigger("Shot");
                fireParticale.Play();
                return weaponInfo.Rebound;
            }
            else
            {
                _attackCoolTimer = weaponInfo.AttackCool * 0.2f;
                Debug.Log("ź�����");
            }
        }
        return 0;
    }

    // ���� ���� ����. ������� �����ε� �� �� �ְ� ����
    protected virtual void Shot()
    {
        if (Physics.Raycast(maincamera.transform.position, maincamera.transform.forward, out _hit, 200f))
        {
            // ��������Ʈ�� ����Ʈ ��ȯ
            GameObject go = ObjectPool.Instance.Spawn("HitEffect", _hit.point);
            ObjectPool.Instance.Return(go, 0.3f);

            if (_hit.collider.TryGetComponent(out Enemy enemy))
            {
                enemy.Hit(damage);
            }
        }
    }

    // ������ (player script call ����)
    public void Reload()
    {
        if (_isAttackCool) return;
        _isReloading = true;
        Debug.Log("������ ��");
    }

    // ��ȭ ����
    public void EnforceApply()
    {
        damage = weaponInfo.Damage * StatesEnforce.Instance.weaponDamageGain;
    }

    // �ʱ�ȭ
    protected virtual void Start()
    {
        maincamera = Camera.main;
        CurrentBullet = weaponInfo.MaxBullet;
        _attackCoolTimer = weaponInfo.AttackCool;
        _reLoadTimer = weaponInfo.ReloadTIme;
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EnforceApply();
        // play animation
    }

    private void OnDisable()
    {
        ReloadTimeReset();
    }

    private void Update()
    {
        // ���� Ÿ�̸�
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

        // ������ Ÿ�̸�
        if (_isReloading)
        {
            if (_reLoadTimer < 0)
            {
                ReloadTimeReset();
                CurrentBullet = weaponInfo.MaxBullet;
                Debug.Log("������ �Ϸ�");
            }
            else
            {
                _reLoadTimer -= Time.deltaTime;
            }
        }

    }

    // ������ ����
    private void ReloadTimeReset()
    {

        _isReloading = false;
        _reLoadTimer = weaponInfo.ReloadTIme;
    }

}
