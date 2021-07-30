using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class WeaponProjectileLauncher : WeaponComponent
{
    [SerializeField] private Rigidbody projectilePrefab;
    [SerializeField] private float velocity = 40f;
    [SerializeField] private float maxDistance = 100f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform shotPoint;
    private RaycastHit hitInfo;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    private Camera camera;
    [SerializeField] private GameObject impact;
    [SerializeField] private GameObject muzzleBlast;
    [SerializeField] private GameObject projectile;

    private void OnEnable()
    {
        CinemachineCore.CameraUpdatedEvent.RemoveListener(OnCameraUpdated);
        CinemachineCore.CameraUpdatedEvent.AddListener(OnCameraUpdated);
    }

    private void OnDisable()
    {
        CinemachineCore.CameraUpdatedEvent.AddListener(OnCameraUpdated);
    }

    void OnCameraUpdated(CinemachineBrain brain)
    {
        camera = brain.OutputCamera;
    }

    protected override void WeaponFired()
    {
        if (Input.GetMouseButton(1))
        {
            Ray targetPoint = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(targetPoint, out hitInfo, maxDistance, layerMask))
            {
                Instantiate(impact, hitInfo.point, Quaternion.identity);
                GameObject muzzleBlastPrefab = Instantiate(muzzleBlast, shotPoint.position, Quaternion.identity);
                GameObject prefab = Instantiate(projectile, hitInfo.point, Quaternion.identity);
                Destroy(muzzleBlastPrefab, 2f);
                Destroy(prefab, .25f);
            }
        }
    }
}