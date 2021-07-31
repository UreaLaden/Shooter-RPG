using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private KeyCode weaponHotKey;
    [SerializeField] private float fireDelay = 0.25f;
    private Inventory _inventory;
    private float fireTimer;
    private WeaponAmmo ammo;
    public event Action OnFire = delegate {  };
    public KeyCode WeaponHotKey
    {
        get { return weaponHotKey; }
    }
    public bool isInAimMode
    {
        get { return Input.GetMouseButton(1); }
    } 
    private void Awake()
    {
        _inventory = FindObjectOfType<Inventory>();
        ammo = GetComponent<WeaponAmmo>();
    }

    void Update()
    {
        fireTimer += Time.deltaTime;
        if (isInAimMode && Input.GetMouseButtonDown(0))
        {
            if (CanFire())
            {
                Fire();
            }
        }        
    }

    private void Fire()
    {
        fireTimer = 0;
        if (_inventory.activeWeapon == this)
        {
            OnFire();
        }
    }

    private bool CanFire()
    {
        if (ammo != null && ammo.isAmmoReady() == false)
        {
            return false;
        }
        return fireTimer >= fireDelay;
    }
}
