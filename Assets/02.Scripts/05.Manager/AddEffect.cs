using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AddEffectList
{
    PlayerMoveSpeed,
    AllWeaponDamage,
    AllTowerDamage,
    AllTowerRange,
    EnemyHealth,
    EnemyMoveSpeed,
    EnemyMoney,
    EnemyCount
}

[CreateAssetMenu(fileName = "AddEffectBase", menuName = "AddEffect/EffectBase")]
public class AddEffect : ScriptableObject
{
    [SerializeField] private AddEffectList _effect;
    [SerializeField] private float _gain;
    [SerializeField] private string _infomation;
    [SerializeField] private string _enemyName;

    public string GetInfomation() => $"{_infomation} * {_gain}";

    public void OnApply()
    {
        switch (_effect)
        {
            case AddEffectList.AllWeaponDamage:
                {
                    StatesEnforce.Instance.weaponDamageGain *= _gain;
                }
                break;
            case AddEffectList.AllTowerDamage:
                {
                    StatesEnforce.Instance.TowerDamageGain *= _gain;
                }
                break;
            case AddEffectList.PlayerMoveSpeed:
                {
                    StatesEnforce.Instance.PlayerMoveSpeedGain *= _gain;
                }
                break;
            case AddEffectList.AllTowerRange:
                {
                    StatesEnforce.Instance.TowerRangeGain *= _gain;
                }
                break;
            case AddEffectList.EnemyHealth:
                {
                    StatesEnforce.Instance.enemyHealthGain *= _gain;
                }
                break;
            case AddEffectList.EnemyMoveSpeed:
                {
                    StatesEnforce.Instance.enemySpeedGain *= _gain;
                }
                break;
            case AddEffectList.EnemyMoney:
                {
                    StatesEnforce.Instance.enemyMoneyGain *= _gain;
                }
                break;
            case AddEffectList.EnemyCount:
                {
                    EnemySpawner.instance.SpawnPoolAdd(_enemyName, (int)_gain, 0.1f, 1f);
                }
                break;
            default:
                break;
        }
    }

}
