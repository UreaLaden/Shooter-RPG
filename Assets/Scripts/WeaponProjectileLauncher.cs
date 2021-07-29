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
        var aimPos = Camera.main.ScreenPointToRay(Input.mousePosition);
        var ray = new Ray(shotPoint.position,shotPoint.forward);
        Vector3 target = ray.GetPoint(300);
        if (Physics.Raycast(aimPos, out hitInfo, maxDistance, layerMask))
        {
            Debug.Log("Hit: " + hitInfo.transform.name);
            target = hitInfo.point;
        }

        Vector3 direction = target - transform.position;
        direction.Normalize();

        return direction;
    }
}