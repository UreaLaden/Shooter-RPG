using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private List<StateTransition> _stateTransitions = new List<StateTransition>();
    private List<StateTransition> _anyStateTransitions = new List<StateTransition>();

    private IState _currentState;
    public IState CurrentState => _currentState;

    /// <summary>
    /// This allows for a transition from any state give specific conditions
    /// </summary>
    /// <param name="to">The state to be transferred to</param>
    /// <param name="condition">The condition that must be met to initiate the transition</param>
    public void AddAnyTransition(IState to, Func<bool> condition)
    {
        var stateTransition = new StateTransition(null, to, condition);
        _anyStateTransitions.Add(stateTransition);
    }

    /// <summary>
    /// This describes the normal transition from one state to another
    /// </summary>
    /// <param name="from">The current state</param>
    /// <param name="to">The state to be transitioned to</param>
    /// <param name="condition">The condition that must be met to initiate the transition</param>
    public void AddTransition(IState from, IState to, Func<bool> condition)
    {
        var stateTransition = new StateTransition(from, to, condition);
        _stateTransitions.Add(stateTransition);
    }

    /// <summary>
    /// Updates the current state
    /// </summary>
    /// <param name="state"></param>
    public void SetState(IState state)
    {
        if (_currentState == state) return;

        _currentState?.OnExit();

        _currentState = state;
        _currentState.OnEnter();
    }

    public void Tick()
    {
        StateTransition transition = CheckForTransition();
        if (transition != null)
        {
            SetState(transition.To);
        }

        _currentState.Tick();
    }

    /// <summary>
    /// Searches the current list of transitions for the condition needed in order to set a new state
    /// </summary>
    /// <returns>The required transition if available otherwise null</returns>
    private StateTransition CheckForTransition()
    {
        foreach (var transition in _anyStateTransitions)
        {
            if (transition.Condition())
            {
                return transition;
            }
        }

        foreach (var transition in _stateTransitions)
        {
            if (transition.From == _currentState && transition.Condition())
            {
                return transition;
            }
        }

        return null;
    }
}