using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaterBot : MonoBehaviour,IGoap
{
    private HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();
    private GoapAction _nextAction;
    private GoapAction _rest;
    private Vector3 _previousDestination;
    private NavMeshAgent _navMeshAgent;
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

    public HashSet<KeyValuePair<string, object>> GetWorldState()
    {
        HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>();
        worldData.Add(new KeyValuePair<string, object>("hasWater",TownInventory.Instance.onHandWaterAmount > 1));
        worldData.Add(new KeyValuePair<string, object>("hasDelivery",TownInventory.Instance.storedWaterAmount > 5f ));
        worldData.Add(new KeyValuePair<string, object>("canRest",TownInventory.Instance.deliveredWater >= TownInventory.Instance.waterCapacity));
        return worldData;
    }

    public HashSet<KeyValuePair<string, object>> CreateGoalState()
    {
        goal.Add(new KeyValuePair<string, object>("doJob", true));
        return goal;
    }

    public void PlanFailed(HashSet<KeyValuePair<string, object>> failedGoal)
    {
        Debug.Log(name + " failed his task");
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
        if (_navMeshAgent.hasPath && remainingDistance < 2f)
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
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_nextAction.target.transform.position,2f);
        }
    }
}
