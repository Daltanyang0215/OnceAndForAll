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

    [Space]        
    [Header("Camera")]
    [SerializeField] private Transform _cameraAnchor;
    [SerializeField] private float _rotateX_Min, _rotateX_Max;
    [SerializeField] private float _rotateYSpeed;
    [SerializeField] private float _rotateXSpeed;
    private float _rotateX, _rotateY;

    //[Space]
    //[Header("TowerBuild")]
    [SerializeField] private bool _isBuild; 

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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

        _isBuild = Input.GetButton("BuildTower");
    }

    // 카메라 회전
    private void CameraRotate()
    {
        if (_isBuild) return;

        float tmp_x = _cameraAnchor.eulerAngles.x - _rotateX * _rotateXSpeed;
        float tmp_y = transform.eulerAngles.y + _rotateY * _rotateYSpeed;

        tmp_x = tmp_x > 180 ? tmp_x - 360 : tmp_x; // 오일러 0~360 -> -180~180 으로 전환
        tmp_y = tmp_y > 180 ? tmp_y - 360 : tmp_y; // 오일러 0~360 -> -180~180 으로 전환

        tmp_x = Mathf.Clamp(tmp_x, _rotateX_Min, _rotateX_Max);

        _cameraAnchor.rotation = Quaternion.Euler(tmp_x, transform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Euler(0, tmp_y, 0);
    }

    // 방향키 이동
    private void Move()
    {
        _moveVec = new Vector3(_right, 0, _forwad).normalized;
        transform.Translate(_moveVec * moveSpeed * Time.fixedDeltaTime);
    }

    // 공격
    private void Fire()
    {
        if (_isFire == false
            || _isBuild) 
            return;
        
        weapons[_currentWeaponsIndex].Attack();
    }

    // 재장전
    private void Reload()
    {
        if (_isReload == false
             || _isBuild)
            return;

        weapons[_currentWeaponsIndex].Reload();
    }

    // 무기 교체
    private void WeaponChange()
    {
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
        if(weaponIndex != _currentWeaponsIndex)
        {
            weapons[weaponIndex].gameObject.SetActive(false);
            weapons[_currentWeaponsIndex].gameObject.SetActive(true);
        }
    }

    // UI 관련 함수 모음
        private void UIUpdata()
    {
        MainUIManager.instance.isShowBuildCircle = _isBuild;
    }
}