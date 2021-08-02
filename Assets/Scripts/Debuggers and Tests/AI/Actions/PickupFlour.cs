using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PickupFlour : GoapAction
{
    private bool completed = false;
    private float startTime = 0;
    public float workDuration = 2; //seconds
    
    public PickupFlour()
    {
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
            this.GetComponent<BotInventory>().flourLevel += 5;
            completed = true;
        }

        return true;
    }

    public override bool requiresInRange()
    {
        return true;
    }
}
