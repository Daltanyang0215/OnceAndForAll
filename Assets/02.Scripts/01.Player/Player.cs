using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private void Awake()
    {
        Instance = this;
    }

    [Header("Weapon")]
    public Weapon[] weapons;
    private int _currentWeaponsIndex;

    [Header("Move")]
    public float moveSpeed;
    private float _forwad, _right;
    private bool _isFire, _isReload;
    private Vector3 _moveVec;
    private Rigidbody _rb;

    [Space]
    [Header("Camera")]
    [SerializeField] private Transform _cameraAnchor;
    [SerializeField] private float _rotateX_Min, _rotateX_Max;
    [SerializeField] private float _rotateYSpeed;
    [SerializeField] private float _rotateXSpeed;
    private float _rotateX, _rotateY;

    //[Space]
    //[Header("TowerBuild")]
    [SerializeField] private bool _isShowBuilder;

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
        Fire();
        Reload();
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
        _isReload = Input.GetButtonDown("ReLoad");

        _rotateY = Input.GetAxis("Mouse X");
        _rotateX = Input.GetAxis("Mouse Y");

        _isShowBuilder = Input.GetButton("BuildTower");
    }

    // ī�޶� ȸ��
    private void CameraRotate()
    {
        if (_isShowBuilder) return;

        float tmp_x = _cameraAnchor.eulerAngles.x - _rotateX * _rotateXSpeed;
        float tmp_y = transform.eulerAngles.y + _rotateY * _rotateYSpeed;

        tmp_x = tmp_x > 180 ? tmp_x - 360 : tmp_x; // ���Ϸ� 0~360 -> -180~180 ���� ��ȯ
        tmp_y = tmp_y > 180 ? tmp_y - 360 : tmp_y; // ���Ϸ� 0~360 -> -180~180 ���� ��ȯ

        tmp_x = Mathf.Clamp(tmp_x, _rotateX_Min, _rotateX_Max);

        _cameraAnchor.rotation = Quaternion.Euler(tmp_x, transform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Euler(0, tmp_y, 0);
    }

    // ����Ű �̵�
    private void Move()
    {
        _moveVec = new Vector3(_right, 0, _forwad).normalized;
        //transform.Translate(_moveVec * moveSpeed * Time.fixedDeltaTime);

        // �ٶ󺸴� �������� ���꺤�� ȸ��
        _moveVec = Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * _moveVec;
        _rb.velocity = _moveVec * moveSpeed;
    }

    // ����
    private void Fire()
    {
        // ����Ű�� �ȴ��Ȱų�, Ÿ�� ���� ���̳�, UI�� �����ִ� �����̸� ����
        if (_isFire == false
            || _isShowUI)
            return;

        weapons[_currentWeaponsIndex].Attack();
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
        if (weaponIndex != _currentWeaponsIndex)
        {
            weapons[weaponIndex].gameObject.SetActive(false);
            weapons[_currentWeaponsIndex].gameObject.SetActive(true);
        }
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
}