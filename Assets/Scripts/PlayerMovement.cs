using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private float rotationSpeed = 30f;
    [SerializeField]private float moveSpeed = 5f;
    private float turnSmoothVelocity;
    [SerializeField] private Transform _camera;
    [SerializeField] private float turnSmoothTime = 0.1f;
    private Animator animator;
    private CharacterController characterController;
    bool isAiming;
    private GetGrounded grounded;
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
        grounded = GetComponent<GetGrounded>();
    }

    private void Update()
    {
        isAiming = Input.GetMouseButton(1);
        var mousePosX = Input.GetAxis("Mouse X") * rotationSpeed;
        mousePosX = mousePosX < 0 ? -1 : mousePosX > 0 ? 1 : 0; 
        var horizontal = isAiming ? mousePosX: Input.GetAxis("Horizontal")  ;
        var vertical = Input.GetAxis("Vertical");
        animator.SetFloat(Horizontal, horizontal); 
        animator.SetFloat(Vertical, vertical);

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (direction.magnitude >= 0.1)
        {
            
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,ref turnSmoothVelocity,turnSmoothTime);
            if (!isAiming)
            {
                transform.localRotation = Quaternion.Euler(0f,angle,0f) ;
            }
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDir.normalized * (moveSpeed * Time.deltaTime));
        }

    }


}