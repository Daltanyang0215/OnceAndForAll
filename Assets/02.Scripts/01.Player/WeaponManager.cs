using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public bool isWeaponReady;

    [SerializeField] private List<Weapon> weapons;

    public int currentWeaponsIndex { get; private set; }

    private int[] _hotkeys = new int[3]; // ��� ���� ���� ��ȣ�� 
    private bool _isFire, _isReload, _isAction; // �Է¿�
    private int _activeWeaponKey;
    public int activeWeaponKey
    {
        get => _activeWeaponKey;
        set
        {
            if (_activeWeaponKey != value)
            {
                _activeWeaponKey = value;
                WeaponChange(_hotkeys[_activeWeaponKey]);
            }
        }
    } // ���� Ȱ��ȭ���� ���� ���� ��ȣ (Ű1, Ű2, Ű3)

    private void Start()
    {
        _hotkeys[0] = 0;
        _hotkeys[1] = 1;
        _hotkeys[2] = 2;

        currentWeaponsIndex = 1;
        _activeWeaponKey = 1;
    }

    private void Update()
    {
        if (isWeaponReady == false) return;

        _isFire = Input.GetButton("Fire1");
        _isAction = Input.GetButton("Fire2");
        _isReload = Input.GetButtonDown("ReLoad");

        weapons[currentWeaponsIndex].isWeaponAction = _isAction;

        if (_isFire)
        {
            weapons[currentWeaponsIndex].Attack();
        }

        if (_isReload)
        {
            weapons[currentWeaponsIndex].Reload();
        }


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            activeWeaponKey = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            activeWeaponKey = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            activeWeaponKey = 0;
        }
    }

    public void WeaponChange(int changedKeyIndex)
    {
        if (changedKeyIndex != currentWeaponsIndex)
        {
            weapons[currentWeaponsIndex].gameObject.SetActive(false);
            weapons[changedKeyIndex].gameObject.SetActive(true);
            currentWeaponsIndex = changedKeyIndex;
        }
    }

    public int ActiveWeaponChange(int newIndex)
    {
        int result = _hotkeys[_activeWeaponKey];
        _hotkeys[_activeWeaponKey] = newIndex;
        WeaponChange(_hotkeys[_activeWeaponKey]);
        return result;
    }

    public void OnApply()
    {
        weapons[currentWeaponsIndex].OnApply();
    }
}