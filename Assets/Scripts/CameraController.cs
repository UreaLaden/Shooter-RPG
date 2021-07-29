using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera followCamera;
    [SerializeField] private float mouseLookSensitivity = 1f;
    [SerializeField] private CinemachineFreeLook freeLookCamera;
    [Range(-5, 5)] [SerializeField] private float yOffsetMin, yOffsetMax;
    private CinemachineComposer aim;
    private bool isAiming = false;

    private void Awake()
    {

        aim = followCamera.GetCinemachineComponent<CinemachineComposer>();
        freeLookCamera.Priority = 0;
        
    }
    private void Update()
    {
        isAiming = Input.GetMouseButton(1);
        freeLookCamera.Priority = isAiming ? 0 : 100;
        //freeLookCamera.m_RecenterToTargetHeading.m_enabled = isAiming;

        if (isAiming)
        {
            var vertical = Input.GetAxis("Mouse Y") * mouseLookSensitivity;
            aim.m_TrackedObjectOffset.y += vertical;
            aim.m_TrackedObjectOffset.y = Mathf.Clamp(aim.m_TrackedObjectOffset.y, yOffsetMin, yOffsetMax);
        }
    }
}
