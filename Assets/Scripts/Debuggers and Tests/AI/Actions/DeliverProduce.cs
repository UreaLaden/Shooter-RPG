using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeliverProduce : GoapAction
{
    [SerializeField]private float workDuration = 2;
    private bool completed = false;
    private float startTime = 0;
    public int deliveryAmount = 5;
    private NavMeshAgent _navMeshAgent;
    
    public DeliverProduce()
    {
        addPrecondition("hasProduce",true);
        addEffect("doJob",true);
        name = "Harvest";
    }
    public override void reset()
    {
        completed = false;
        startTime = 0;
    }

    public override bool isDone()
    {
        return completed;
    }

    public override bool checkProceduralPrecondition(GameObject agent)
    {
        return true;
    }

    public override bool perform(GameObject agent)
    {
        float remainingDistance = Helpers.GetPathRemainingDistance(agent.GetComponent<NavMeshAgent>());
        if (remainingDistance > 2)
        {
            startTime = Time.time;
        }

        if (startTime == 0)
        {
            Debug.Log($"Starting {name}");
            startTime = Time.time;
        }

        if (Time.time - startTime > workDuration)
        {
            Debug.Log($"Finished: {name}");
            
            TownInventory.Instance.storedProduceAmount += deliveryAmount;
            completed = true;
        }

        return true;
    }

    public override bool requiresInRange()
    {
        return true;
    }
}
