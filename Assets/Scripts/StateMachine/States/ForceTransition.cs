using System;
using UnityEngine;

public class ForceTransition:IState
{
    private readonly Animator _animator;
    private readonly Player _player;

    public ForceTransition(Animator animator, Player player)
    {
        _animator = animator;
        _player = player;
    }

    public void Tick()
    {
        var info = _animator.GetCurrentAnimatorStateInfo(0);
        _player.NormalizedAnimationTime = info.normalizedTime;
        if (info.normalizedTime >= _player.transitionTiming)
        {
            _player.isFalling = true;
        }
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
        _animator.SetBool(Utilities.AnimationTriggers.Jump.ToString(), false);
    }
}