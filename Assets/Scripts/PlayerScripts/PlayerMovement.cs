using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 30f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float verticalVelocity;
    [SerializeField] private float gravity = 14.0f;
    [SerializeField] private float jumpForce = 10.0f;
    [SerializeField] private Transform _camera;
    [SerializeField] private float turnSmoothTime = 0.1f;
    private Animator animator;
    private CharacterController characterController;
    private float turnSmoothVelocity;
    bool isAiming;
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Jump = Animator.StringToHash("Jump");

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        ProcessJump();
        ProcessMovement();
    }

    private void ProcessJump()
    {
        verticalVelocity = characterController.isGrounded
            ? verticalVelocity = -gravity * Time.deltaTime
            : verticalVelocity -= gravity * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger(Jump);
            verticalVelocity = jumpForce;
        }
    }

    private void ProcessMovement()
    {
        var mousePosX = Input.GetAxis("Mouse X") * rotationSpeed;
        mousePosX = mousePosX < 0 ? -1 : mousePosX > 0 ? 1 : 0;

        var horizontal = isAiming ? mousePosX : Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        isAiming = Input.GetMouseButton(1);

        animator.SetFloat(Horizontal, horizontal);
        animator.SetFloat(Vertical, vertical);
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 motion = new Vector3(0, verticalVelocity, 0) * Time.deltaTime;
        bool isMoving = direction.magnitude >= 0.1;
        if (isMoving)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(
                transform.eulerAngles.y,
                    targetAngle, ref turnSmoothVelocity,
                    turnSmoothTime);
            
            if (!isAiming)
            {
               transform.localRotation = Quaternion.Euler(0f, angle, 0f);
            }

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * new Vector3(0, verticalVelocity, 1);
            motion = moveDir.normalized * (moveSpeed * Time.deltaTime);
        }
        characterController.Move(motion);
    }
}