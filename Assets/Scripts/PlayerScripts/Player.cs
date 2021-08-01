using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 30f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravity = 14.0f;
    [SerializeField] private float jumpForce = 10.0f;
    [SerializeField] private float turnSmoothTime = 0.1f;
    public CharacterController _characterController;
    private IMover _mover;
    private Rotator _rotator;
    public Animator animator;
    public Transform _camera;
    public IPlayerInput PlayerInput { get; set; } = new PlayerInput();
    public float Gravity => gravity;
    public float JumpForce => jumpForce;
    public float TurnSmoothTime => turnSmoothTime;
    public float RotationSpeed => rotationSpeed;
    public float MoveSpeed => moveSpeed;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _camera = Camera.main.transform;
        _mover = new Mover(this);
        _rotator = new Rotator(this);
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _mover.Tick();
       _rotator.Tick();
    }
}