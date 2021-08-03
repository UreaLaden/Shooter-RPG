using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Baker : MonoBehaviour, IGoap
{
    private GoapAction _nextAction;
    private GoapAction _rest;
    private NavMeshAgent _navMeshAgent;
    private Vector3 _previousDestination;
    HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _previousDestination = transform.position;
        _rest = GetComponent<Rest>();
    }

    private void Update()
    {
        Helpers.NegotiateTurn(_navMeshAgent,gameObject);
    }

    public HashSet<KeyValuePair<string, object>> GetWorldState()
    {
        HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>();
        worldData.Add(new KeyValuePair<string, object>("hasStock", (TownInventory.Instance.onHandProduceAmount > 4)));
        worldData.Add(new KeyValuePair<string, object>("hasFlour", (TownInventory.Instance.onHandProduceAmount > 1)));
        worldData.Add(new KeyValuePair<string, object>("hasDelivery", (TownInventory.Instance.onHandBreadAmount > 5)));
        worldData.Add(new KeyValuePair<string, object>("canRest", (TownInventory.Instance.onHandProduceAmount <= 1 && TownInventory.Instance.onHandProduceAmount < 2 )));
        return worldData;
    }

    public HashSet<KeyValuePair<string, object>> CreateGoalState()
    {
        goal.Add(new KeyValuePair<string, object>("doJob", true)); // new goal state
        return goal;
    }

    public bool MoveAgent(GoapAction nextAction)
    {
        float remainingDistance = Helpers.GetPathRemainingDistance(_navMeshAgent);
        _nextAction = nextAction;
        // if we don't need to move anywhere
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
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_nextAction.target.transform.position,2f);
        }
    }

    public void PlanFailed(HashSet<KeyValuePair<string, object>> failedGoal)
    {
        Debug.Log($"Can't bake anymore bread. Gonna rest for a bit at home");
        MoveAgent(_rest);
        _rest.perform(_navMeshAgent.gameObject);
    }

    public void PlanFound(HashSet<KeyValuePair<string, object>> goal, Queue<GoapAction> actions)
    {
        Debug.Log($"{name} Plan Found: {goal}");
    }

    public void ActionsFinished()
    {
        Debug.Log($"{name} Mission Complete");
    }

    public void PlanAborted(GoapAction aborter)
    {
        Debug.Log($"{name} Mission aborted: {aborter}");
    }
}

