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

    [Space]
    [Header("Weapon")]
    public Weapon[] weapons;
    private int _currentWeaponsIndex;
    private int[] _hotkeys = new int[2]; // 장비 중인 무기 번호들 
    private bool _isFire, _isReload, _isAction, _isInteraction; // 입력용
    public int _activeWeaponKey ; // 현재 활성화중인 무기 슬롯 번로

    [Space]
    [Header("Move")]
    public float moveSpeed;
    private float MoveSpeedGain; // 강화 정도
    private float _forwad, _right; // 입력용
    private Vector3 _moveVec;
    private Rigidbody _rb;
    public bool isNotMoveable;

    [Space]
    [Header("Camera")]
    [SerializeField] private Transform _cameraAnchor;
    [SerializeField] private float _rotateX_Min, _rotateX_Max; // 상하 회전 최대 값
    [SerializeField] private float _rotateXSpeed, _rotateYSpeed; // 감도
    private float _rotateX, _rotateY;// 입력용
    private float _rebound; // 총 반동용

    [Space]
    [Header("UI")]
    private bool _isShowBuilder;
    private bool _isTowerDestroy;
    private bool _isShowUI;
    public bool isShowUI { set { _isShowUI = value; } }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        // 초기화. 나중에 게임 시작전 선택하게 수정예정
        _hotkeys[0] = 0;
        _hotkeys[1] = 1;
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

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _activeWeaponKey = 0;
            WeaponChange(_hotkeys[_activeWeaponKey]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _activeWeaponKey = 1;
            WeaponChange(_hotkeys[_activeWeaponKey]);
        }
    }

    // 카메라 회전
    private void CameraRotate()
    {
        if (_isShowBuilder) return;

        float tmp_x = _cameraAnchor.eulerAngles.x - _rotateX * _rotateXSpeed - _rebound;
        float tmp_y = transform.eulerAngles.y + _rotateY * _rotateYSpeed;

        tmp_x = tmp_x > 180 ? tmp_x - 360 : tmp_x; // 오일러 0~360 -> -180~180 으로 전환
        tmp_y = tmp_y > 180 ? tmp_y - 360 : tmp_y; // 오일러 0~360 -> -180~180 으로 전환

        tmp_x = Mathf.Clamp(tmp_x, _rotateX_Min, _rotateX_Max);

        _rebound = 0; //총기 반동 초기화

        // 직접적인 카메라 회전 코드
        _cameraAnchor.rotation = Quaternion.Euler(tmp_x, transform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Euler(0, tmp_y, 0);
    }

    // 무기 특수 키 (마우스 우 클릭)
    private void WeaponAction()
    {
        // 공격키가 안눌렸거나, 타워 빌드 중이나, UI가 켜져있는 상태이면 리턴
        if (_isShowUI)
            return;

        // 무기에 특수기 관련 키 입력 전달
        weapons[_currentWeaponsIndex].isWeaponAction = _isAction;
    }

    // 공격(마우스 좌 클릭)
    private void Fire()
    {
        // 공격키가 안눌렸거나, 타워 빌드 중이나, UI가 켜져있는 상태이면 리턴
        if (_isFire == false
            || _isShowUI)
            return;

        _rebound = weapons[_currentWeaponsIndex].Attack();
    }

    // 재장전
    private void Reload()
    {
        // 재장전키가 안눌렸거나, 타워 빌드 중이나, UI가 켜져있는 상태이면 리턴
        if (_isReload == false
             || _isShowUI)
            return;

        weapons[_currentWeaponsIndex].Reload();
    }

    // 상호작용
    private void Interaction()
    {
        if (_isInteraction == false
             || _isShowUI)
            return;

        RaycastHit _hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out _hit, 2f))
        {   
            if(_hit.collider.TryGetComponent(out IInteraction interaction))
            {
                interaction.Interaction();
            }
        }

    }
    // 타워 파괴
    private void TowerDestroy()
    {
        if (_isTowerDestroy == false
             || _isShowUI)
            return;

        MainUIManager.instance.ShowTowerBuilder(true, true);
    }

    // 무기 교체
    private void WeaponChange(int newWeaponIndex)
    {
        if (_isShowUI) return;

        if (newWeaponIndex != _currentWeaponsIndex)
        {
            weapons[_currentWeaponsIndex].gameObject.SetActive(false);
            weapons[newWeaponIndex].gameObject.SetActive(true);
            _currentWeaponsIndex = newWeaponIndex;
        }
    }

    // 방향키 이동
    private void Move()
    {
        if(isNotMoveable) return;

        _moveVec = new Vector3(_right, 0, _forwad).normalized;
        //transform.Translate(_moveVec * moveSpeed * Time.fixedDeltaTime);

        // 바라보는 방향으로 무브벡터 회전
        _moveVec = Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * _moveVec;
        _rb.velocity = _moveVec * moveSpeed * MoveSpeedGain;
    }

    // UI 관련 함수 모음
    private void UIUpdata()
    {
        MainUIManager.instance.isShowBuildCircle = _isShowBuilder;
    }

    public int ActiveWeaponChange(int newIndex)
    {
        int result = _hotkeys[_activeWeaponKey];
        _hotkeys[_activeWeaponKey] = newIndex;
        WeaponChange(_hotkeys[_activeWeaponKey]);
        return result;
    }

    public void PlayerStop()
    {
        _rb.velocity = Vector3.zero;
    }

    // 강화 데이터 적용
    public void EnforceApply()
    {
        MoveSpeedGain = StatesEnforce.Instance.PlayerMoveSpeedGain;
        weapons[_currentWeaponsIndex].OnApply();
    }
}