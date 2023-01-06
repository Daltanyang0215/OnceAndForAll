using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public bool isWeaponReady;

    [SerializeField] private List<Weapon> weapons;

    public int currentWeaponsIndex { get; private set; }

    private int[] _hotkeys = new int[3]; // 장비 중인 무기 번호들 
    private bool _isFire, _isReload, _isAction; // 입력용
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
    } // 현재 활성화중인 무기 슬롯 번호 (키1, 키2, 키3)

    private void Start()
    {
        _hotkeys[0] = 0;
        _hotkeys[1] = 1;
        _hotkeys[2] = 2;

        currentWeaponsIndex = 1;
        _activeWeaponKey = 1;
        MainUIManager.instance.ShowUI(activeWeaponKey);
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
            MainUIManager.instance.ShowUI(activeWeaponKey);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            activeWeaponKey = 2;
            MainUIManager.instance.ShowUI(activeWeaponKey);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            activeWeaponKey = 0;
            MainUIManager.instance.ShowUI(activeWeaponKey);
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
        int result = _hotkeys[activeWeaponKey];
        _hotkeys[activeWeaponKey] = newIndex;

        MainUIManager.instance.ShowWeaponIcon(activeWeaponKey-1, weapons[newIndex].GetWeaponIcon);

        WeaponChange(_hotkeys[activeWeaponKey]);
        return result;
    }

    public void OnApply()
    {
        weapons[currentWeaponsIndex].OnApply();
    }
}
