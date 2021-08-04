using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeliverWater : GoapAction
{
    private bool completed;
    private float startTime;
    private float workDuration = 2f;
    public DeliverWater()
    {
        addPrecondition("hasWater",true);
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
            startTime = Time.time;
        }

        if (Time.time - startTime > workDuration)
        {
            TownInventory.Instance.deliveredWater++;
            TownInventory.Instance.onHandWaterAmount -= 2;
            TownInventory.Instance.storedWaterAmount++;
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
