using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class DeliverBread : GoapAction
{
    private bool completed = false;
    private float startTime = 0;
    public float workDuration = 2;

    public DeliverBread()
    {
        addPrecondition("hasDelivery",true);
        addEffect("doJob",true);
        name = "DeliverBread";
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

    public override bool requiresInRange()
    {
        return true;
    }
    public override bool checkProceduralPrecondition(GameObject agent)
    {
        return true;
    }

    public override bool perform(GameObject agent)
    {
        var remainingDistance = Helpers.GetPathRemainingDistance(agent.GetComponent<NavMeshAgent>());
        if (remainingDistance > 2)
        {
            startTime = Time.time;
        }
        if (startTime == 0)
        {
            Debug.Log($"Starting: {name}");
            startTime = Time.time;
        }

        if (Time.time - startTime > workDuration)
        {
            Debug.Log($"Finished: {name}");
            this.GetComponent<TownInventory>().onHandBreadAmount -= 5;
            completed = true;
        }
        return true;
    }

}
