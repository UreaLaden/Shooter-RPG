using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Weapons))]
public class WeaponAnimation : MonoBehaviour
{
    private Weapons weapon;
    private Animator animator;

    private void Awake()
    {
        weapon = GetComponent<Weapons>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        weapon.OnFire += Weapon_OnFire;
    }

    private void Weapon_OnFire()
    {
        animator.SetTrigger("Fire");
    }

    private void OnDestroy()
    {
        weapon.OnFire -= Weapon_OnFire;
        
    }
}
