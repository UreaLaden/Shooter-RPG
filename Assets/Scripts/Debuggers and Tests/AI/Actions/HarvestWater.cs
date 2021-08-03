using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HarvestWater : GoapAction
{
    [SerializeField] private float workDuration = 5f;
    private bool completed;
    private float startTime = 0f;
    private NavMeshAgent _navMeshAgent;

    public HarvestWater()
    {
        addPrecondition("canRest",false);
        addEffect("hasWater",true);
        name = "Harvest Water";
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

        if (startTime == 0)
        {
            Debug.Log("Collecting Water");
            startTime = Time.time;
        }

        if (Time.time - startTime > workDuration)
        {
            Debug.Log("Done gathering water");
            completed = true;
        }

        return true;
    }

    public override bool requiresInRange()
    {
        return true;
    }
}