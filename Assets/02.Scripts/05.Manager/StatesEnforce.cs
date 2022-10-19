using System.Collections.Generic;
using UnityEngine;
public class StatesEnforce
{
    private static StatesEnforce _instance;
    public static StatesEnforce Instance
    {
        get { 
            if (_instance == null)
            {
                _instance = MainGameManager.Instance.Enforce;
            }
            return _instance; }
    }

    // ���� ���ݷ� ����
    [SerializeField] private float _weaponDamageGain = 1;
    public float weaponDamageGain
    {
        get
        {
            return _weaponDamageGain;
        }
        set
        {
            _weaponDamageGain = value;
        }
    }

    // �÷��̾� �̵��ӵ� ����
    [SerializeField] private float _playerMoveSpeedGain = 1;
    public float PlayerMoveSpeedGain
    {
        get
        {
            return _playerMoveSpeedGain;
        }
        set
        {
            _playerMoveSpeedGain = value;
        }
    }

    // Ÿ�� ���ݷ� ����
    [SerializeField] private float _towerDamageGain = 1;
    public float TowerDamageGain
    {
        get
        {
            return _towerDamageGain;
        }
        set
        {
            _towerDamageGain = value;
        }
    }

    // Ÿ�� ��Ÿ� ����
    [SerializeField] private float _towerRangeGain = 1;
    public float TowerRangeGain
    {
        get
        {
            return _towerRangeGain;
        }
        set
        {
            _towerRangeGain = value;
        }
    }

    // �� ü�� ����
    [SerializeField] private float _enemyHealthGain = 1;
    public float enemyHealthGain
    {
        get { return _enemyHealthGain; }
        set { _enemyHealthGain = value; }
    }

    // �� �̵��ӵ� ����
    [SerializeField] private float _enemySpeedGain = 1;
    public float enemySpeedGain
    {
        get { return _enemySpeedGain; }
        set { _enemySpeedGain = value; }
    }

    // �� ȹ�� ��� ����
    [SerializeField] private float _enemyMoneyGain = 1;
    public float enemyMoneyGain
    {
        get { return _enemyMoneyGain; }
        set { _enemyMoneyGain = value; }
    }
}
