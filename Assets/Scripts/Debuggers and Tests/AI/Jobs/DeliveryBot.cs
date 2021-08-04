using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum BotType { WATER, BREAD,
    PRODUCE
};
public class DeliveryBot : MonoBehaviour,IGoap
{
    [SerializeField] private BotType type;
    public Queue<Stop> _queuedStops = new Queue<Stop>();
    private NavMeshAgent _navMeshAgent;
    private Stop[] _allStopsCopy;
    private GoapAction _nextAction;
    private Vector3 _previousDestination;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _allStopsCopy = (Stop[]) TownInventory.Instance._allStops.Clone();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Queue<Stop> testQueue = GetQueuedStops(_queuedStops, _allStopsCopy,transform.position);
            while (testQueue.Count > 0)
            {
                Stop currentStop = testQueue.Dequeue();
                Debug.Log($"Current Stop: {currentStop._name}");
            }
        }
    }

    private Queue<Stop> GetQueuedStops(Queue<Stop> queuedStops,Stop[] allStops,Vector3 lastStop)
    {
        if (allStops.Length == 0)
        {
            return null;
        }
        if (queuedStops.Count == allStops.Length)
        {
            return queuedStops;
        }

        float nearestDistance = Single.MaxValue;
        Stop closestStop = null;
        int closestStopIdx = -1;
        foreach (Stop _stop in allStops)
        {
            if (_stop)
            {
                var currentDistance = Vector3.Distance(lastStop, _stop.dropOffLocation.transform.position);
                if ( currentDistance < nearestDistance)
                {
                    nearestDistance = currentDistance;
                    closestStop = _stop;
                    closestStopIdx = Array.IndexOf(allStops,_stop);
                }
            }
        }
        queuedStops.Enqueue(closestStop);
        allStops[closestStopIdx] = null;
        return GetQueuedStops(queuedStops, allStops,closestStop.dropOffLocation.transform.position);
    }

    public HashSet<KeyValuePair<string, object>> GetWorldState()
    {
        bool hasOrder = false;
        bool canRest = _queuedStops.Count == 0;
        switch (type)
        {
            case BotType.BREAD:
                hasOrder = TownInventory.Instance.storedBreadAmount >= 5; 
                break;
            case BotType.WATER:
                hasOrder = TownInventory.Instance.storedWaterAmount >= 5;
                break;
            case BotType.PRODUCE:
                hasOrder = TownInventory.Instance.storedProduceAmount >= 5;
                break;
        }
        HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>();
        worldData.Add(new KeyValuePair<string, object>("hasOrder", hasOrder));
        worldData.Add(new KeyValuePair<string, object>("canRest",canRest));
        return worldData;
    }

    public HashSet<KeyValuePair<string, object>> CreateGoalState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();
        goal.Add(new KeyValuePair<string, object>("deliverSupplies", true));
        return goal;
    }

    public void PlanFailed(HashSet<KeyValuePair<string, object>> failedGoal)
    {
        Debug.Log($"Something wrong with {name}");
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
}
