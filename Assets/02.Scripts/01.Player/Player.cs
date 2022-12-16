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
    public float _rebound; // 총 반동용

    [Space]
    [Header("UI")]
    private bool _isShowBuilder;
    private bool _isTowerDestroy;
    private bool _isShowUI;
    private bool _isInteraction;

    [Space]
    [Header("Sound")]
    [SerializeField] private AudioClip _footstepSound;
    private AudioSource _playerAudio;

    public bool isShowUI
    {
        set
        {
            _isShowUI = value;
            _weaponManager.isWeaponReady = !_isShowUI;
        }
    }
    private RaycastHit _hit;
    private WeaponManager _weaponManager;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerAudio = GetComponent<AudioSource>();
        _playerAudio.clip = _footstepSound;
        _weaponManager = GetComponent<WeaponManager>();
        _weaponManager.isWeaponReady = true;
    }

    void Update()
    {
        GetInput();
        CameraRotate();
        Interaction();
        TowerDestroy();
        UIUpdata();

        // 이동이 없을때는 소리 안나게
        _playerAudio.mute = _moveVec.magnitude == 0;
    }



    private void FixedUpdate()
    {
        Move();
    }

    private void GetInput()
    {
        _forwad = Input.GetAxisRaw("Vertical");
        _right = Input.GetAxisRaw("Horizontal");
        _isInteraction = Input.GetButtonDown("Interaction");

        _rotateY = Input.GetAxis("Mouse X");
        _rotateX = Input.GetAxis("Mouse Y");

        _isShowBuilder = Input.GetButton("BuildTower");
        _isTowerDestroy = Input.GetKeyDown(KeyCode.X);
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

    // 상호작용
    private void Interaction()
    {
        if (_isShowUI)
            return;


        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out _hit, 30f))
        {
            if (_hit.collider.TryGetComponent(out TowerBase tower)
                && _weaponManager.currentWeaponsIndex == 0)
            {

            }

            if (_hit.distance < 2 &&
                _hit.collider.TryGetComponent(out IInteraction interaction))
            {
                // 강화무기일때는 무기 박스에서 상호작용 안되게
                if (interaction is WeaponBox && _weaponManager.currentWeaponsIndex == 0)
                {
                    MainUIManager.instance.ShowInteractionPanel(false);
                    return;
                }
                MainUIManager.instance.ShowInteractionPanel(true);
                if (_isInteraction)
                {
                    interaction.Interaction();
                }
            }
            else
                MainUIManager.instance.ShowInteractionPanel(false);
        }
        else
            MainUIManager.instance.ShowInteractionPanel(false);

    }
    // 타워 파괴
    private void TowerDestroy()
    {
        if (_isTowerDestroy == false
             || _isShowUI)
            return;

        MainUIManager.instance.ShowTowerBuilder(true, true);
    }

    // 방향키 이동
    private void Move()
    {
        if (isNotMoveable) return;

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

    public void PlayerStop()
    {
        _rb.velocity = Vector3.zero;
        _playerAudio.mute = true;
    }

    // 강화 데이터 적용
    public void EnforceApply()
    {
        MoveSpeedGain = StatesEnforce.Instance.PlayerMoveSpeedGain;
    }
}