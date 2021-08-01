using UnityEngine;
using UnityEngine.AI;

public class Mover : IMover
{
    private float _turnSmoothVelocity;
    private readonly Player _player;
    private readonly CharacterController _characterController;
    private readonly Animator _animator;
    private float _verticalVelocity;
    public Mover(Player player)
    {
        _player = player;
        _characterController = _player.GetComponent<CharacterController>();
        _player.GetComponent<NavMeshAgent>().enabled = false;
        _animator = _player.GetComponentInChildren<Animator>();
    }

    public void Tick()
    {
        ProcessJump();
        ProcessMovement();
    }
    private void ProcessJump()
    {
        _verticalVelocity = _player._characterController.isGrounded
            ? _verticalVelocity = -_player.Gravity * Time.deltaTime
            : _verticalVelocity -= _player.Gravity * Time.deltaTime;

        if (_player.PlayerInput.Jumped)
        {
            _player.animator.SetTrigger(Utilities.AnimationTriggers.Jump.ToString());
            _verticalVelocity = _player.JumpForce;
        }
    }

    private void ProcessMovement()
    {
        var mousePosX = _player.PlayerInput.MouseX * _player.RotationSpeed;
        mousePosX = mousePosX < 0 ? -1 : mousePosX > 0 ? 1 : 0;

        var horizontal = _player.PlayerInput.IsAiming ? mousePosX : _player.PlayerInput.Horizontal;
        var vertical = _player.PlayerInput.Vertical;

        if (_animator != null)
        {
            _animator.SetFloat(Utilities.AnimationTriggers.Horizontal.ToString(), horizontal);
            _animator.SetFloat(Utilities.AnimationTriggers.Vertical.ToString(), vertical);
        }
        
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 motion = new Vector3(0, _verticalVelocity, 0) * Time.deltaTime;
        bool isMoving = direction.magnitude >= 0.1;
        if (isMoving)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _player._camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(
                _player.transform.eulerAngles.y,
                targetAngle, ref _turnSmoothVelocity,
                _player.TurnSmoothTime);
            
            if (_player.PlayerInput.IsAiming == false)
            {
                _player.transform.localRotation = Quaternion.Euler(0f, angle, 0f);
            }

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * new Vector3(0, _verticalVelocity, 1);
            motion = moveDir.normalized * (_player.MoveSpeed * Time.deltaTime);
        }
        _player._characterController.Move(motion);
    }
}