using UnityEngine.AI;

public class ChasePlayer : IState
{
    private readonly NavMeshAgent _navMeshAgent;
    public ChasePlayer(NavMeshAgent agent)
    {
        _navMeshAgent = agent;
    }
    public void Tick()
    {
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