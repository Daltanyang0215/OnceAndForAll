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
    // 보상목록 출력용 리스트
    private Dictionary<string, int> _addMonster = new Dictionary<string, int>();
    public void addMonster(string addMonster,int count, string buff)
    {
        string tmpname="";
        if (buff == "None"
            || string.IsNullOrEmpty(buff))
            tmpname = addMonster;
        else
            tmpname = buff + " 효과를 지닌 " + addMonster;

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

    // 무기 공격력 배율
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

    // 플레이어 이동속도 배율
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

    // 타워 공격력 배율
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

    // 타워 사거리 배율
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

    // 적 체력 배율
    [SerializeField] private float _enemyHealthGain = 1;
    public float enemyHealthGain
    {
        get { return _enemyHealthGain; }
        set { _enemyHealthGain = value; }
    }

    // 적 이동속도 배율
    [SerializeField] private float _enemySpeedGain = 1;
    public float enemySpeedGain
    {
        get { return _enemySpeedGain; }
        set { _enemySpeedGain = value; }
    }

    // 적 획득 골드 배율
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

    // 보상 목록 출력 용
    public string GetPositiveList()
    {
        string tmp = "";
        if (_weaponDamageGain != 1) tmp += $"모든 무기 공격력 X {_weaponDamageGain:F3} \n";
        if (_playerMoveSpeedGain != 1) tmp += $"플레이어 이동속도 X {_playerMoveSpeedGain:F3} \n";
        if (_towerDamageGain != 1) tmp += $"모든 타워 공격력 X {_towerDamageGain:F3} \n";
        if (_towerRangeGain != 1) tmp += $"모든 타워 사거리 X {_towerRangeGain:F3} \n";
        if (_enemyMoneyGain != 1) tmp += $"몬스터 획득 골드 X {_enemyMoneyGain:F3} \n";

        return tmp;
    }
    public string GetNegativeList()
    {
        string tmp = "";
        foreach (var monster in _addMonster.Keys)
        {
            tmp += $"{monster} {_addMonster[monster]} 마리 추가 소환 \n";
        }
        if (_enemyHealthGain != 1) tmp += $"모든 몬스터 최대체력 X {_enemyHealthGain:F3} \n";
        if (_enemySpeedGain != 1) tmp += $"몬든 몬스터 이동속도 X {_enemySpeedGain:F3} \n";

        return tmp;
    }
}
