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
    public float _rebound; // �� �ݵ���

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

        // �̵��� �������� �Ҹ� �ȳ���
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

    // ��ȣ�ۿ�
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
                // ��ȭ�����϶��� ���� �ڽ����� ��ȣ�ۿ� �ȵǰ�
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
    // Ÿ�� �ı�
    private void TowerDestroy()
    {
        if (_isTowerDestroy == false
             || _isShowUI)
            return;

        MainUIManager.instance.ShowTowerBuilder(true, true);
    }

    // ����Ű �̵�
    private void Move()
    {
        if (isNotMoveable) return;

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
        _playerAudio.mute = true;
    }

    // ��ȭ ������ ����
    public void EnforceApply()
    {
        MoveSpeedGain = StatesEnforce.Instance.PlayerMoveSpeedGain;
    }
}