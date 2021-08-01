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
    [SerializeField] private LayerMask groundLayer;
    [Range(0.01f, 1f)] [SerializeField] public float transitionTiming;
    public CharacterController _characterController;
    public StateMachine _stateMachine;
    public IMover _mover;
    private Rotator _rotator;
    public Animator animator;
    public Transform _camera;
    public float verticalVelocity;
    public bool isGrounded;
    public IPlayerInput PlayerInput { get; set; } = new PlayerInput();
    public float NormalizedAnimationTime { get; set; } = 0f;
    public float Gravity => gravity;
    public float JumpForce => jumpForce;
    public float TurnSmoothTime => turnSmoothTime;
    public float RotationSpeed => rotationSpeed;
    public float MoveSpeed => moveSpeed;
    public CapsuleCollider _collider;
    public bool jumped = false;
    public bool isFalling = false;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _camera = Camera.main.transform;
        _mover = new Mover(this);
        _rotator = new Rotator(this);
        _stateMachine = new StateMachine();
        _characterController = GetComponent<CharacterController>();
        _collider = GetComponent<CapsuleCollider>();
        
        var idle = new Idle();
        var initiateJump = new InitiateJump(this, animator);
        var forceTransition = new ForceTransition(animator,this);
        var land = new Land(this, animator);
        
        SetTransitions(initiateJump, forceTransition, land, idle);
        _stateMachine.SetState(idle);
    }

    private void SetTransitions(InitiateJump initiateJump, ForceTransition forceTransition, Land land, Idle idle)
    {
        _stateMachine.AddAnyTransition(initiateJump,
            () => PlayerInput.Jumped && isGrounded);
        _stateMachine.AddTransition(initiateJump,forceTransition,
            () => NormalizedAnimationTime >= transitionTiming && jumped == false);
        _stateMachine.AddTransition(forceTransition, land,
            () =>isFalling && NormalizedAnimationTime >= transitionTiming);
        _stateMachine.AddTransition(land, idle, () => isFalling == false && jumped == false);
    }

    private void Update()
    {
        _stateMachine.Tick();
        isGrounded = Helpers.IsGrounded(_collider,groundLayer);
    }
    private void FixedUpdate()
    {
        _mover.Tick();
        _rotator.Tick();
    }

   

    private void OnDrawGizmos()
    {
        float radius = _collider.radius * .9f;
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.up * (radius * .9f),radius);
    }
}