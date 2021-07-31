using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Weapon[] weapons;
    public static Inventory Instance;
    public static event Action<Weapon> OnWeaponChange = delegate {  };
    
    public Weapon activeWeapon;

    private void Awake()
    {
        Instance = this;
        SwitchToWeapon(weapons[0]);
        
    }
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
