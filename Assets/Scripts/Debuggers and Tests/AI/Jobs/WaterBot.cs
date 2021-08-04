using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaterBot : MonoBehaviour,IGoap
{
    
    private GoapAction _nextAction;
    private GoapAction _rest;
    private Vector3 _previousDestination;
    public NavMeshAgent _navMeshAgent;
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _previousDestination = transform.position;
        _rest = GetComponent<Rest>();
    }
    void Update()
    {
        Helpers.NegotiateTurn(_navMeshAgent,gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,5f);
    }

    public HashSet<KeyValuePair<string, object>> GetWorldState()
    {
        HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>();
        worldData.Add(new KeyValuePair<string, object>("canRest", TownInventory.Instance.storedWaterAmount >= TownInventory.Instance.waterCapacity));
        return worldData;
    }

    public HashSet<KeyValuePair<string, object>> CreateGoalState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();
        goal.Add(new KeyValuePair<string, object>("doJob", true));
        return goal;
    }

    public void PlanFailed(HashSet<KeyValuePair<string, object>> failedGoal)
    {
       Debug.Log($"Waterbot didn't get it done: {failedGoal}");
    }

    public void PlanFound(HashSet<KeyValuePair<string, object>> goal, Queue<GoapAction> actions)
    {
    }

    public void ActionsFinished()
    {
    }

    public void PlanAborted(GoapAction aborter)
    {
    }

    public bool MoveAgent(GoapAction nextAction)
    {
        float remainingDistance = Helpers.GetPathRemainingDistance(_navMeshAgent);
        _nextAction = nextAction;
        if (_previousDestination == nextAction.target.transform.position)
        {
            nextAction.setInRange(true);
            return true;
        }

        _navMeshAgent.SetDestination(nextAction.target.transform.position);
        if (_navMeshAgent.hasPath && remainingDistance < 2)
        {
            nextAction.setInRange(true);
            _previousDestination = nextAction.target.transform.position;
            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        if (_nextAction != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_nextAction.target.transform.position,.5f);
        }
    }
}
