using System.Collections;
using System.Collections.Generic;
using NSubstitute.ReturnsExtensions;
using UnityEngine;
using UnityEngine.AI;

public class BakeBread : GoapAction
{
    private bool completed = false;
    private float startTime = 0;
    public float workDuration = 2;
    [SerializeField] private int breadCost = 2;

    public BakeBread()
    {
        addPrecondition("hasFlour",true);//if this true action will execute
        addEffect("doJob",true); //matches the goalstate
        name = "BakeBread"; //allows us to debug if needed
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
        var _agent = agent.GetComponent<NavMeshAgent>();
        if (Helpers.GetPathRemainingDistance(_agent) > 2)
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
            TownInventory.Instance.produceAmount -= breadCost;
            TownInventory.Instance.breadAmount += 1;
            completed = true;
        }

        return true;
    }

    /// <summary>
    /// Do i need to be at the location in order to perform my task?
    /// </summary>
    /// <returns></returns>
    public override bool requiresInRange()
    {
        return true;
    }
}
