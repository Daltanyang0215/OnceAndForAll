using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBoxManager : MonoBehaviour
{
    [SerializeField] private List<WeaponBox> weaponBoxs = new List<WeaponBox>();

    private void Start()
    {
        
    }

    public void ChildBoxAdd(WeaponBox box)
    {
        weaponBoxs.Add(box);
    }

    public void BoxIntaraction(int index)
    {
        weaponBoxs[Player.Instance.gameObject.GetComponent<WeaponManager>().ActiveWeaponChange(index)].RendererReset();
    }
}
