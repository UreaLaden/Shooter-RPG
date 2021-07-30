using System;
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
    [SerializeField] private float speed = .2f;
    [SerializeField]private float xOffsetMin, xOffsetMax;


    private void Awake()
    {

        aim = followCamera.GetCinemachineComponent<CinemachineComposer>();
        freeLookCamera.Priority = 0;
        
    }
    private void LateUpdate()
    {
        isAiming = Input.GetMouseButton(1);
        freeLookCamera.Priority = isAiming ? 0 : 100;
        //freeLookCamera.m_RecenterToTargetHeading.m_enabled = isAiming;

        if (isAiming)
        {
            var vertical = Input.GetAxis("Mouse Y") ;
            var horizontal = Input.GetAxis("Mouse X");
            Debug.Log(horizontal);
            transform.eulerAngles = Vector2.zero * speed;
            aim.m_TrackedObjectOffset.y += vertical * speed;
            aim.m_TrackedObjectOffset.x += horizontal * speed;
            aim.m_TrackedObjectOffset.y = Mathf.Clamp(aim.m_TrackedObjectOffset.y, yOffsetMin, yOffsetMax);
            aim.m_TrackedObjectOffset.x = Mathf.Clamp(aim.m_TrackedObjectOffset.x, xOffsetMin, xOffsetMax);
        }
    }
}
