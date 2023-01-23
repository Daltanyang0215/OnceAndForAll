using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public enum AddEffectList
{
    PlayerMoveSpeed,
    AllWeaponDamage,
    AllTowerDamage,
    AllTowerRange,
    EnemyHealth,
    EnemyMoveSpeed,
    EnemyMoney,
    EnemyCount,
    AddElementNormal,
    AddElementFire,
    AddElementIce,
    AddElementEle,
    RepairWall
}
[CreateAssetMenu(fileName = "AddEffectBase", menuName = "AddEffect/EffectBase")]
public class AddEffect : ScriptableObject
{
    [SerializeField] private AddEffectList _effect;
    [SerializeField] private float _gain;
    [SerializeField] private string _infomation;
    [SerializeField] private string _infomation_en;
    [SerializeField] private string _enemyName;
    [SerializeField] private string _enemyName_en;
    [SerializeField] private EnemyBuffs _enemyBuff;
    [SerializeField] private string _enemyBuffInfomation;

    public string GetInfomation()
    {
        if (LocalizationSettings.SelectedLocale != LocalizationSettings.AvailableLocales.Locales[1])
            return (_enemyBuff == EnemyBuffs.None ? "" : $"'{_enemyBuffInfomation}'효과를 지닌 ") + $" {_infomation} X {_gain}";
        else
            return $"{_infomation_en}"+ (_enemyBuff == EnemyBuffs.None ? "" : $"with effect {_enemyBuff}") + $" X {_gain}";
    }


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
                    EnemySpawner.instance.SpawnPoolAdd(_enemyName, (int)_gain, 0.05f, 0.05f, _enemyBuff.ToString());
                    if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0])
                        StatesEnforce.Instance.addMonster(_enemyName, (int)_gain, _enemyBuffInfomation.ToString());
                    else
                        StatesEnforce.Instance.addMonster(_enemyName_en, (int)_gain, _enemyBuff.ToString());
                }
                break;
            case AddEffectList.AddElementNormal:
                {
                    StatesEnforce.Instance.AddElement(Element.Normal, (int)_gain);
                }
                break;
            case AddEffectList.AddElementFire:
                {
                    StatesEnforce.Instance.AddElement(Element.Fire, (int)_gain);
                }
                break;
            case AddEffectList.AddElementIce:
                {
                    StatesEnforce.Instance.AddElement(Element.Ice, (int)_gain);
                }
                break;
            case AddEffectList.AddElementEle:
                {
                    StatesEnforce.Instance.AddElement(Element.Electricity, (int)_gain);
                }
                break;
            case AddEffectList.RepairWall:
                {
                    MainGameManager.Instance.Health += (int)_gain;
                }
                break;
            default:
                break;
        }
    }

}
