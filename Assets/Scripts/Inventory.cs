using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Inventory : MonoBehaviour
{
    public static event Action<Weapon> OnWeaponChange = delegate {  };
    [SerializeField] private Weapon[] weapons;
    
    public Weapon activeWeapon;
    void Update()
    {
        foreach (var weapon in weapons)
        {
            if (Input.GetKeyDown(weapon.WeaponHotKey))
            {
                SwitchToWeapon(weapon);
                break;
            }
        }
    }

    private void SwitchToWeapon(Weapon weaponToSwitchTo)
    {
        foreach (var weapon in weapons)
        {
            weapon.gameObject.SetActive(weapon == weaponToSwitchTo);
        }
        activeWeapon = weaponToSwitchTo;

        OnWeaponChange(weaponToSwitchTo);
    }
}
