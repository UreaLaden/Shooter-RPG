using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIAmmoText : MonoBehaviour
{
    private TMP_Text tmproText;
    private WeaponAmmo currentWeaponAmmo;

    private void Awake()
    {
        tmproText = GetComponent<TMP_Text>();
        Inventory.OnWeaponChange += Inventory_OnWeaponChanged;
    }

    private void Inventory_OnWeaponChanged(Weapon weapon)
    {
        if (currentWeaponAmmo != null)
        {
            currentWeaponAmmo.OnAmmoChanged -= CurrentWeaponAmmo_OnAmmoChanged;
        }
        currentWeaponAmmo = weapon.GetComponent<WeaponAmmo>();
        if (currentWeaponAmmo != null)
        {
            currentWeaponAmmo.OnAmmoChanged += CurrentWeaponAmmo_OnAmmoChanged;
            tmproText.text = currentWeaponAmmo.GetAmmoText();
        }
        else
        {
            tmproText.text = "Unlimited";
        }
    }

    private void CurrentWeaponAmmo_OnAmmoChanged()
    {
        tmproText.text = currentWeaponAmmo.GetAmmoText();
    }
}