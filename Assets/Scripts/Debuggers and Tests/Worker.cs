using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Worker : MonoBehaviour, IGoap
{
    public NavMeshAgent _navMeshAgent;
    public Vector3 _previousDestination;
    public BotInventory _inventory;
    [SerializeField]private GoapAction _nextAction;
    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _inventory = this.GetComponent<BotInventory>();
        _previousDestination = transform.position;
    }

    private void Update()
    {
        Helpers.NegotiateTurn(_navMeshAgent,gameObject);
    }

    public HashSet<KeyValuePair<string, object>> GetWorldState()
    {
        HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>();
        worldData.Add(new KeyValuePair<string, object>("hasFlour", (_inventory.flourLevel > 1)));
        return worldData;
    }

    public HashSet<KeyValuePair<string, object>> CreateGoalState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();
        goal.Add(new KeyValuePair<string, object>("doJob", true)); // new goal state
        return goal;
    }

    public bool MoveAgent(GoapAction nextAction)
    {
        // if we don't need to move anywhere
        if (_previousDestination == nextAction.target.transform.position)
        {
            nextAction.setInRange(true);
            return true;
        }

        _nextAction = nextAction;
        float remainingDistance = Helpers.GetPathRemainingDistance(_navMeshAgent);
        //Debug.Log($"Distance to next action ({nextAction.target}): {remainingDistance}");
        Debug.Log($"Next Action in range: {remainingDistance < 2f}");
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
        Debug.Log("Plan Failed");
    }

    public void PlanFound(HashSet<KeyValuePair<string, object>> goal, Queue<GoapAction> actions)
    {
        Debug.Log("Plan Found");
    }

    public void ActionsFinished()
    {
        Debug.Log("Mission Complete");
    }

    public void PlanAborted(GoapAction aborter)
    {
        Debug.Log("Mission aborted");
    }

}
