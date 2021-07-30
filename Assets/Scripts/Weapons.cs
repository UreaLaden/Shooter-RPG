using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField] private KeyCode weaponHotKey;
    [SerializeField] private float fireDelay = 0.25f;
    private Inventory _inventory;
    private float fireTimer;
    public event Action OnFire = delegate {  };
    public KeyCode WeaponHotKey
    {
        get { return weaponHotKey; }
    }

    private void Awake()
    {
        _inventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        fireTimer += Time.deltaTime;
        if (Input.GetButtonDown("Fire1"))
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
        return fireTimer >= fireDelay;
    }
}
