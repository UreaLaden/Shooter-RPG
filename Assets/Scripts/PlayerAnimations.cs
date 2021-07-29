using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float drawWeaponSpeed = .35f;
    [SerializeField] private int shootingLayerIndex = 1;
    private float _currentWeight;
    private float _currentWeightVelocity;

    private void Start()
    {
        _currentWeight = animator.GetLayerWeight(1);
    }

    private void Update()
    {
        ToggleAim();
    }

    private void ToggleAim()
    {
        _currentWeight = Mathf.SmoothDamp(_currentWeight, Input.GetMouseButton(1) ? 1 : 0, ref _currentWeightVelocity,
            drawWeaponSpeed);
        animator.SetLayerWeight(shootingLayerIndex, _currentWeight);
    }
}