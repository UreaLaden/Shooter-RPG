using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class HarvestProduce : GoapAction
{
    private bool completed = false;
    private float startTime = 0;
    public float workDuration = 2;
    private NavMeshAgent _navMeshAgent;
    public HarvestProduce()
    {
        addPrecondition("canRest",false);
        addEffect("hasProduce", true);
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
        _navMeshAgent = agent.GetComponent<NavMeshAgent>();
        float remainingDistance = Helpers.GetPathRemainingDistance(_navMeshAgent);
        if (remainingDistance > 2f)
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
            completed = true;
        }

        return true;
    }

    public override bool requiresInRange()
    {
        return true;
    }
}