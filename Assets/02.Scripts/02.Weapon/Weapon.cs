using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponInfo weaponInfo;
    [SerializeField] private Transform _firePoint;
    [SerializeField] protected ParticleSystem fireParticale;
    [SerializeField] protected LayerMask targetLayer;
    [SerializeField] protected AudioClip _fireSound;
    [SerializeField] protected AudioClip _reloadSound;
    protected Animator animator;
    protected AudioSource weaponaudio;
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
            MainUIManager.instance.ShowBullet(currentBullet, weaponInfo.MaxBullet);
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
    public virtual float Attack()
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
                // sound
                AudioSource sound = ObjectPool.Instance.Spawn("Sound", transform.position).GetComponent<AudioSource>();
                sound.clip = _fireSound;
                sound.Play();

                Player.Instance._rebound = weaponInfo.Rebound;
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
        weaponaudio.clip = _reloadSound;
        weaponaudio.Play();
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
        weaponaudio = GetComponent<AudioSource>();
    }

    // ���� ���� ����. ������� �����ε� �� �� �ְ� ����
    protected virtual void Shot()
    {
        fireParticale.Play();
        Vector3 point = Vector3.zero;
        if (Physics.Raycast(maincamera.transform.position, maincamera.transform.forward, out _hit, 500f, targetLayer)
            && _hit.distance < 0.5f)
        {
            point = _hit.point;
        }
        else
        {
            point = maincamera.transform.position + maincamera.transform.forward.normalized * 500f;
        }

        // ��������Ʈ�� ����Ʈ ��ȯ
        PlayerBullet bullet = ObjectPool.Instance.Spawn("PlayerBullet", _firePoint.position).GetComponent<PlayerBullet>();
        bullet.transform.LookAt(point);
        bullet.Setup(damage, 400f, targetLayer);
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
        weaponaudio.Stop();
    }

    public Sprite GetWeaponIcon => weaponInfo.Icon;
}
