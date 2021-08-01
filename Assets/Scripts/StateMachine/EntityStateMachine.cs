using UnityEngine;
using UnityEngine.AI;

public class EntityStateMachine : MonoBehaviour
{
    private StateMachine _stateMachine;
    private NavMeshAgent _navMeshAgent;
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        var player = FindObjectOfType<Player>();
        _stateMachine = new StateMachine();
        var idle = new Idle();
        var chasePlayer = new ChasePlayer(_navMeshAgent);
        var attack = new Attack();
        
        _stateMachine.Add(idle);
        _stateMachine.Add(chasePlayer);
        _stateMachine.Add(attack);
        
        _stateMachine.AddTransition(
            idle,
            chasePlayer,
            ()=> Vector3.Distance(_navMeshAgent.transform.position,player.transform.position) < 3f);
        
        _stateMachine.AddTransition(
            chasePlayer,
            attack,
            ()=> Vector3.Distance(_navMeshAgent.transform.position,player.transform.position) < 1f);
        _stateMachine.SetState(idle);
    }

    private void Update()
    {
        _stateMachine.Tick();
    }
}