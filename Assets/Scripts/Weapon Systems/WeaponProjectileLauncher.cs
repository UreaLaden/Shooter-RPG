using Cinemachine;
using UnityEngine;

public class WeaponProjectileLauncher : WeaponComponent
{
    [SerializeField] private float maxDistance = 100f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private PooledMonoBehaviour impact;
    [SerializeField] private PooledMonoBehaviour muzzleBlast;
    private Camera _camera;

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
        _camera = brain.OutputCamera;
    }

    protected override void WeaponFired()
    {
        if (_weapon.isInAimMode)
        {
            Ray targetPoint =  _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(targetPoint, out RaycastHit hitInfo, maxDistance, layerMask))
            {
                var projectile = impact.Get<Projectile>(hitInfo.point, Quaternion.identity);
                var muzzleFlash = muzzleBlast.Get<Projectile>(shotPoint.position, Quaternion.identity);
            }
        }
    }
}