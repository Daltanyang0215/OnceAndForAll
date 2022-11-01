using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private void Awake()
    {
        Instance = this;
    }

    [Space]
    [Header("Weapon")]
    public Weapon[] weapons;
    private int _currentWeaponsIndex;
    private bool _isFire, _isReload, _isAction, _isInteraction; // �Է¿�

    [Space]
    [Header("Move")]
    public float moveSpeed;
    private float MoveSpeedGain; // ��ȭ ����
    private float _forwad, _right; // �Է¿�
    private Vector3 _moveVec;
    private Rigidbody _rb;
    public bool isNotMoveable;

    [Space]
    [Header("Camera")]
    [SerializeField] private Transform _cameraAnchor;
    [SerializeField] private float _rotateX_Min, _rotateX_Max; // ���� ȸ�� �ִ� ��
    [SerializeField] private float _rotateXSpeed, _rotateYSpeed; // ����
    private float _rotateX, _rotateY;// �Է¿�
    private float _rebound; // �� �ݵ���

    [Space]
    [Header("UI")]
    private bool _isShowBuilder;
    private bool _isTowerDestroy;
    private bool _isShowUI;
    public bool isShowUI { set { _isShowUI = value; } }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInput();
        CameraRotate();
        WeaponAction();
        Fire();
        Reload();
        Interaction();
        TowerDestroy();
        WeaponChange();
        UIUpdata();
    }



    private void FixedUpdate()
    {
        Move();
    }

    private void GetInput()
    {
        _forwad = Input.GetAxisRaw("Vertical");
        _right = Input.GetAxisRaw("Horizontal");
        _isFire = Input.GetButton("Fire1");
        _isAction = Input.GetButton("Fire2");
        _isReload = Input.GetButtonDown("ReLoad");
        _isInteraction = Input.GetButtonDown("Interaction");

        _rotateY = Input.GetAxis("Mouse X");
        _rotateX = Input.GetAxis("Mouse Y");

        _isShowBuilder = Input.GetButton("BuildTower");
        _isTowerDestroy = Input.GetKeyDown(KeyCode.X);
    }

    // ī�޶� ȸ��
    private void CameraRotate()
    {
        if (_isShowBuilder) return;

        float tmp_x = _cameraAnchor.eulerAngles.x - _rotateX * _rotateXSpeed - _rebound;
        float tmp_y = transform.eulerAngles.y + _rotateY * _rotateYSpeed;

        tmp_x = tmp_x > 180 ? tmp_x - 360 : tmp_x; // ���Ϸ� 0~360 -> -180~180 ���� ��ȯ
        tmp_y = tmp_y > 180 ? tmp_y - 360 : tmp_y; // ���Ϸ� 0~360 -> -180~180 ���� ��ȯ

        tmp_x = Mathf.Clamp(tmp_x, _rotateX_Min, _rotateX_Max);

        _rebound = 0; //�ѱ� �ݵ� �ʱ�ȭ

        // �������� ī�޶� ȸ�� �ڵ�
        _cameraAnchor.rotation = Quaternion.Euler(tmp_x, transform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Euler(0, tmp_y, 0);
    }

    // ���� Ư�� Ű (���콺 �� Ŭ��)
    private void WeaponAction()
    {
        // ����Ű�� �ȴ��Ȱų�, Ÿ�� ���� ���̳�, UI�� �����ִ� �����̸� ����
        if (_isShowUI)
            return;

        // ���⿡ Ư���� ���� Ű �Է� ����
        weapons[_currentWeaponsIndex].isWeaponAction = _isAction;
    }

    // ����(���콺 �� Ŭ��)
    private void Fire()
    {
        // ����Ű�� �ȴ��Ȱų�, Ÿ�� ���� ���̳�, UI�� �����ִ� �����̸� ����
        if (_isFire == false
            || _isShowUI)
            return;

        _rebound = weapons[_currentWeaponsIndex].Attack();
    }

    // ������
    private void Reload()
    {
        // ������Ű�� �ȴ��Ȱų�, Ÿ�� ���� ���̳�, UI�� �����ִ� �����̸� ����
        if (_isReload == false
             || _isShowUI)
            return;

        weapons[_currentWeaponsIndex].Reload();
    }

    // ��ȣ�ۿ�
    private void Interaction()
    {
        if (_isInteraction == false
             || _isShowUI)
            return;

        RaycastHit _hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out _hit, 200f))
        {   
            if(_hit.collider.TryGetComponent(out IInteraction interaction))
            {
                interaction.Interaction();
            }
        }

    }
    // Ÿ�� �ı�
    private void TowerDestroy()
    {
        if (_isTowerDestroy == false
             || _isShowUI)
            return;

        MainUIManager.instance.ShowTowerBuilder(true, true);
    }

    // ���� ��ü
    private void WeaponChange()
    {
        if (_isShowUI) return;

        int weaponIndex = _currentWeaponsIndex;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _currentWeaponsIndex = 0;
            Debug.Log($"Changed weapons 1");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _currentWeaponsIndex = 1;
            Debug.Log($"Changed weapons 2");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _currentWeaponsIndex = 2;
            Debug.Log($"Changed weapons 3");
        }

        if (weaponIndex != _currentWeaponsIndex)
        {
            weapons[weaponIndex].gameObject.SetActive(false);
            weapons[_currentWeaponsIndex].gameObject.SetActive(true);
        }
    }

    // ����Ű �̵�
    private void Move()
    {
        if(isNotMoveable) return;

        _moveVec = new Vector3(_right, 0, _forwad).normalized;
        //transform.Translate(_moveVec * moveSpeed * Time.fixedDeltaTime);

        // �ٶ󺸴� �������� ���꺤�� ȸ��
        _moveVec = Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * _moveVec;
        _rb.velocity = _moveVec * moveSpeed * MoveSpeedGain;
    }

    // UI ���� �Լ� ����
    private void UIUpdata()
    {
        MainUIManager.instance.isShowBuildCircle = _isShowBuilder;
    }

    public void PlayerStop()
    {
        _rb.velocity = Vector3.zero;
    }

    // ��ȭ ������ ����
    public void EnforceApply()
    {
        MoveSpeedGain = StatesEnforce.Instance.PlayerMoveSpeedGain;
        weapons[_currentWeaponsIndex].OnApply();
    }
}