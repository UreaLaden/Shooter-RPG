using UnityEngine;
using UnityEngine.AI;

public class Rest : GoapAction
{
    private bool completed = false;
    private float startTime = 0;
    public float restDuration = 30f;
    private NavMeshAgent _navMeshAgent;
    
    public Rest()
    {
        addPrecondition("canRest",true);
        addEffect("doJob",true);
        name = "Rest";
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
        if (remainingDistance > 2f) { startTime = Time.time; }
        Debug.Log($"{agent.name} is resting");
        if (startTime == 0)
        {
            startTime = Time.time;
        }

        if (Time.time - startTime >= restDuration)
        {
            Debug.Log("Done resting. Back to work!");
            completed = true;
        }

        return true;
    }

    public override bool requiresInRange()
    {
        return true;
    }
}