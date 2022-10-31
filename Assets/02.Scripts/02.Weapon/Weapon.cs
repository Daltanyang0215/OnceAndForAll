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
    protected Animator animator;

    private int _currentBullet;
    protected int currentBullet
    {
        get
        {
            return _currentBullet;
        }
        set
        {
            _currentBullet = value < 0 ? 0 : value;
            _bulletCountText.text = _currentBullet.ToString();
        }
    }

    private float _attackCoolTimer;
    private float _reLoadTimer;

    protected Camera maincamera;
    protected RaycastHit _hit;

    protected float damage;

    // ���� ��Ÿ������ Ȯ��
    protected bool _isAttackCool;
    // ������ ������ Ȯ��
    private bool _isReloading;

    // Ư���� ���� ���
    private bool _isWeaponAction;
    public bool isWeaponAction
    {
        get
        {
            return _isWeaponAction;
        }
        set
        {
            if (_isWeaponAction != value)
            {
                _isWeaponAction = value;
                WaeponAction(_isWeaponAction);
            }
        }
    }

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

            if (currentBullet > 0)
            {
                currentBullet--;
                _attackCoolTimer = weaponInfo.AttackCool;
                Shot();
                animator.SetTrigger("Shot");
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
    // ������ (player script call ����)
    public virtual void Reload()
    {
        //if (_isAttackCool) return;
        _isReloading = true;
        Debug.Log("������ ��");
    }

    // ��ȭ ����
    public virtual void OnApply()
    {
        damage = weaponInfo.Damage * StatesEnforce.Instance.weaponDamageGain;
    }
    // Ư����
    protected virtual void WaeponAction(bool isAction)
    {

    }

    // �ʱ�ȭ . ������� �ʱ�ȭ ����� �ø� �� �ְ�
    protected virtual void Start()
    {
        maincamera = Camera.main;
        currentBullet = weaponInfo.MaxBullet;
        _attackCoolTimer = weaponInfo.AttackCool;
        _reLoadTimer = weaponInfo.ReloadTime;
        animator = GetComponent<Animator>();
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

    protected virtual void OnEnable()
    {
        OnApply();
        // play animation
    }

    protected virtual void OnDisable()
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
                currentBullet = weaponInfo.MaxBullet;
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
        _reLoadTimer = weaponInfo.ReloadTime;
    }

}
