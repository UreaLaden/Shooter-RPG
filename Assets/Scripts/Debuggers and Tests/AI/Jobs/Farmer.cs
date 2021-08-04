using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Farmer : MonoBehaviour, IGoap
{
    private NavMeshAgent _navMeshAgent;
    private Vector3 previousDestination;
    private GoapAction _nextAction;
    private GoapAction _rest;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _rest = GetComponent<Rest>();
    }

    private void Update()
    {
        Helpers.NegotiateTurn(_navMeshAgent,gameObject);
    }

    public HashSet<KeyValuePair<string, object>> GetWorldState()
    {
        HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>();
        worldData.Add(new KeyValuePair<string, object>("canRest", TownInventory.Instance.storedProduceAmount >= TownInventory.Instance.produceCapacity)); 
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
        Debug.Log($"Something wrong with {name}");
    }

    public void PlanFound(HashSet<KeyValuePair<string, object>> goal, Queue<GoapAction> actions)
    {
        Debug.Log($"{name} Plan Found");
    }

    public void ActionsFinished()
    {
        
    }

    public void PlanAborted(GoapAction aborter)
    {
        Debug.Log($"{name} Mission aborted");
    }

    public bool MoveAgent(GoapAction nextAction)
    {
        float remainingDistance = Helpers.GetPathRemainingDistance(_navMeshAgent);
        _nextAction = nextAction;

        if (previousDestination == nextAction.target.transform.position)
        {
            nextAction.setInRange(true);
            return true;
        }

        _navMeshAgent.SetDestination(nextAction.target.transform.position);
        if (_navMeshAgent.hasPath && remainingDistance < 2f)
        {
            nextAction.setInRange(true);
            previousDestination = nextAction.target.transform.position;
            return true;
        }

        return false;
    }
    private void OnDrawGizmos()
    {
        if (_nextAction != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(_nextAction.target.transform.position,2f);
        }
    }
}
