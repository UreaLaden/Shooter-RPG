using System;
using UnityEngine;
using UnityEngine.AI;

public class EntityStateMachine : MonoBehaviour
{
    public Type CurrentStateType => _stateMachine.CurrentState.GetType();
    private StateMachine _stateMachine;
    private NavMeshAgent _navMeshAgent;
    private Entity _entity;
    private ITrack _tracker;
    private void Awake()
    {
        var player = FindObjectOfType<Player>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _entity = GetComponent<Entity>();
        _stateMachine = new StateMachine();
        
        var idle = new Idle();
        var chasePlayer = new Pursue(_navMeshAgent,player);
        var attack = new Attack();
        var dead = new Dead(_entity);
        
        _tracker = new Tracker(player.transform,_entity);
       
        _stateMachine.AddAnyTransition(
            dead,
            () => _entity.Health <= 0);
        
        _stateMachine.AddTransition(
            chasePlayer,
            attack,
            ()=> DistanceFlat(_navMeshAgent.transform.position,player.transform.position) < 2f);
        _stateMachine.SetState(idle);
        
    }

    private float DistanceFlat(Vector3 source, Vector3 destination)
    {
        return Vector3.Distance(new Vector3(source.x, 0, source.z), new Vector3(destination.x, 0, destination.z));
    }
    private void Update()
    {
        _stateMachine.Tick();
        _tracker.Tick();
    }
    
    
}