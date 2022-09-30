using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public Weapon[] weapons;
    private int _currentWeaponsIndex;

    private void Awake()
    {
        Instance = this;
    }

    [Header("Move")]
    private float _forwad, _right;
    private bool _fire,_reload;
    public float moveSpeed;
    private Vector3 _moveVec;


    void Update()
    {
        GetInput();
        Fire();
        Reload();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void GetInput()
    {
        _forwad = Input.GetAxisRaw("Vertical");
        _right = Input.GetAxisRaw("Horizontal");
        _fire = Input.GetButtonDown("Fire1");
        _reload = Input.GetButtonDown("ReLoad");
    }

    private void Move()
    {
        _moveVec = new Vector3(_right, 0, _forwad).normalized;
        transform.Translate(_moveVec * moveSpeed * Time.fixedDeltaTime);
    }

    private void Fire()
    {
        if (_fire == false) return;

        weapons[_currentWeaponsIndex].Attack();
    }
    private void Reload()
    {
        if (_reload == false) return;

        weapons[_currentWeaponsIndex].Reload();
    }
}
