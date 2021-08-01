using UnityEngine;

/// <summary>
/// We enter this state when the user attempts to jump
/// </summary>
/// <param name="player">The player object</param>
/// <param name="animator">The attached animator</param>
/// <param name="mover">A reference to the player's mover script</param>
public class InitiateJump : IState
{
    private readonly Player _player;
    private readonly Animator _animator;

    public InitiateJump(Player player, Animator animator)
    {
        _player = player;
        _animator = animator;
    }

    public void Tick()
    {
        var info = _animator.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= _player.transitionTiming)
        {
            _player.jumped = true;
        }
        _player.NormalizedAnimationTime = info.normalizedTime;
    }

    public void OnEnter()
    {
        Debug.Log("Entered Initiate Jump");
        _animator.SetTrigger(Utilities.AnimationTriggers.Jump.ToString());
    }

    public void OnExit()
    {
        _animator.SetTrigger(Utilities.AnimationTriggers.ForceTransition.ToString());
    }
}