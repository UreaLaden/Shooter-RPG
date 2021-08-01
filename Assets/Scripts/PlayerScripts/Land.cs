using UnityEngine;

public class Land : IState
{
    private readonly Player _player;
    private readonly Animator _animator;

    public Land(Player player, Animator animator)
    {
        _player = player;
        _animator = animator;
    }

    public void Tick()
    {
        if (_player.isGrounded)
        {
            _player.isFalling = false;
            _player.jumped = false;
        }
    }

    public void OnEnter()
    {
        Debug.Log("Entered Land");
        _animator.SetTrigger(Utilities.AnimationTriggers.Landed.ToString());
    }

    public void OnExit()
    {
    }
}