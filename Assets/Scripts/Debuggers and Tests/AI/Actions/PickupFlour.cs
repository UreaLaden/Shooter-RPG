using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class PickupFlour : GoapAction
{
    [SerializeField]private float workDuration = 2; //seconds
    [SerializeField]private int amountToHarvest = 3;
    private bool completed;
    private float startTime;

    public PickupFlour()
    {
        addPrecondition("hasStock",true);//when this is true we can pickup flour
        addPrecondition("hasFlour",false); //Action Takes place when hasFlour is false
        addEffect("hasFlour",true); //Adding this effect allows bakeBread to execute
        name = "PickupFour"; // name for debugging
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
        var navMeshAgent = agent.GetComponent<NavMeshAgent>();
        var distanceRemaining =Helpers.GetPathRemainingDistance(navMeshAgent)  ;
        if (distanceRemaining > 2)
        {
            startTime = Time.time;
        }
        
        if ( startTime == 0 )
        {
            Debug.Log($"Starting to {name}");
            startTime = Time.time;
        }

        if (distanceRemaining < 1.2f && Time.time - startTime > workDuration)
        {
            Debug.Log($"Finished: {name}");
            TownInventory.Instance.onHandProduceAmount += amountToHarvest;
            TownInventory.Instance.onHandProduceAmount -= amountToHarvest;
            completed = true;
        }

        return true;
    }

    public override bool requiresInRange()
    {
        return true;
    }
}
