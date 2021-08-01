using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class WeaponAnimation : MonoBehaviour
{
    private Weapon _weapon;
    private Animator animator;
    private static readonly int Fire = Animator.StringToHash("Fire");

    private void Awake()
    {
        _weapon = GetComponent<Weapon>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        _weapon.OnFire += Weapon_OnFire;
    }

    private void Weapon_OnFire()
    {
        animator.SetTrigger(Fire);
    }

    private void OnDestroy()
    {
        _weapon.OnFire -= Weapon_OnFire;
        
    }
}
