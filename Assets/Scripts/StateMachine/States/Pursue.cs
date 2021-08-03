using UnityEngine;
using UnityEngine.AI;

public class Pursue : IState
{
    private readonly NavMeshAgent _navMeshAgent;
    private readonly Player _player;

    public Pursue(NavMeshAgent agent, Player player)
    {
        _navMeshAgent = agent;
        _player = player;
    }
    public void Tick()
    {
        _navMeshAgent.SetDestination(_player.transform.position);
    }

    public void OnEnter()
    {
        _navMeshAgent.enabled = true;
    }

    public void OnExit()
    {
        _navMeshAgent.enabled = false;
    }
}