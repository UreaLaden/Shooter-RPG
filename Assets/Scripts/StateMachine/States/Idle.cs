using UnityEngine;

public class Idle : IState
{
    public void Tick()
    {
    }

    public void OnEnter()
    {
        Debug.Log("Entered Idle");
    }

    public void OnExit()
    {
    }
}