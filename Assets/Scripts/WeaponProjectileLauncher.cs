using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class WeaponProjectileLauncher : WeaponComponent
{
    [SerializeField] private Rigidbody projectilePrefab;
    [SerializeField] private float velocity = 40f;
    [SerializeField]private float maxDistance = 100f;
    [SerializeField]private LayerMask layerMask;
    [SerializeField] private Transform shotPoint;
    private RaycastHit hitInfo;

    protected override void WeaponFired()
    {
        Vector3 direction = GetDirection();
        var projectile = Instantiate(projectilePrefab, shotPoint.position, Quaternion.Euler(direction));
        projectile.velocity = direction * velocity;
        Destroy(projectile.gameObject,2f);
    }

    private Vector3 GetDirection()
    {
        var ray = Camera.main.ViewportPointToRay(Vector3.one / 2f);
        Vector3 target = ray.GetPoint(300);
        if (Physics.Raycast(ray, out hitInfo, maxDistance, layerMask))
        {
            target = hitInfo.point;
        }

        Vector3 direction = target - transform.position;
        direction.Normalize();

        return direction;
    }
}