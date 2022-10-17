using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AddEffectList
{
    AllDamage,
    AllWeaponDamage,
    AllTowerDamage,

    
}

[CreateAssetMenu(fileName ="AddEffectBase",menuName = "AddEffect/EffectBase")]
public class AddEffect : ScriptableObject
{
    [SerializeField] private AddEffectList _effect;
    [SerializeField] private float _gain;
    [SerializeField] private string _infomation;

    public void Apply()
    {
    }
   
}
