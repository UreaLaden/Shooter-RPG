using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeliverWater : GoapAction
{
    private bool completed;
    private float startTime;
    private float workDuration;
    public DeliverWater()
    {
        addPrecondition("hasDelivery",true);
        addEffect("doJob",true);
        name = "Deliver Water";

    }
    public override void reset()
    {
        completed = false;
        startTime = 0f;
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
        if (remainingDistance > 2f)
        {
            startTime = Time.time;
        }

        if (startTime == 0f)
        {
            Debug.Log("Delivering water barrels");
            startTime = Time.time;
        }

        if (Time.time - startTime > workDuration)
        {
            Debug.Log("Done delivering water");
            completed = true;
            return true;
        }

        return true;
    }

    public override bool requiresInRange()
    {
        return true;
    }
}
