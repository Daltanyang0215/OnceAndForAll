using System.Collections.Generic;
using UnityEngine;
public class StatesEnforce
{
    private static StatesEnforce _instance;
    public static StatesEnforce Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = MainGameManager.Instance.Enforce;
            }
            return _instance;
        }
    }
    // ������ ��¿� ����Ʈ
    private Dictionary<string, int> _addMonster = new Dictionary<string, int>();
    public void addMonster(string addMonster,int count, string buff)
    {
        string tmpname="";
        if (buff == "None"
            || string.IsNullOrEmpty(buff))
            tmpname = addMonster;
        else
            tmpname = buff + " ȿ���� ���� " + addMonster;

        if (_addMonster.ContainsKey(tmpname))
        {
            _addMonster[tmpname] += count;
        }
        else
        {
            _addMonster.Add(tmpname, count);
        }
    }

    private int[] _elementCount = new int[4];
    public int getElementCount(Element element) => _elementCount[(int)element];
    public void AddElement(Element element, int count) => _elementCount[(int)element]+=count;

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

    [SerializeField] private float _elementNomralDamageGain = 1.5f;
    public float elementNomralDamageGain
    {
        get { return _elementNomralDamageGain; }
        set { _elementNomralDamageGain = value; }
    }

    // ���� ��� ��� ��
    public string GetPositiveList()
    {
        string tmp = "";
        if (_weaponDamageGain != 1) tmp += $"��� ���� ���ݷ� X {_weaponDamageGain:F3} \n";
        if (_playerMoveSpeedGain != 1) tmp += $"�÷��̾� �̵��ӵ� X {_playerMoveSpeedGain:F3} \n";
        if (_towerDamageGain != 1) tmp += $"��� Ÿ�� ���ݷ� X {_towerDamageGain:F3} \n";
        if (_towerRangeGain != 1) tmp += $"��� Ÿ�� ��Ÿ� X {_towerRangeGain:F3} \n";
        if (_enemyMoneyGain != 1) tmp += $"���� ȹ�� ��� X {_enemyMoneyGain:F3} \n";

        return tmp;
    }
    public string GetNegativeList()
    {
        string tmp = "";
        foreach (var monster in _addMonster.Keys)
        {
            tmp += $"{monster} {_addMonster[monster]} ���� �߰� ��ȯ \n";
        }
        if (_enemyHealthGain != 1) tmp += $"��� ���� �ִ�ü�� X {_enemyHealthGain:F3} \n";
        if (_enemySpeedGain != 1) tmp += $"��� ���� �̵��ӵ� X {_enemySpeedGain:F3} \n";

        return tmp;
    }
}
