using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    private NavMeshAgent _agent;
    public GameObject target;
    private void Start()
    {
        _agent = this.GetComponent<NavMeshAgent>();
    }

    private void Seek(Vector3 location)
    {
        _agent.SetDestination(location);
    }

    private void Flee(Vector3 location)
    {
        Vector3 fleeVector = location - this.transform.position;
        _agent.SetDestination(this.transform.position - fleeVector);
    }

    private void Pursue()
    {
        Vector3 targetDirection = target.transform.position - this.transform.position;

        float relativeHeading =
            Vector3.Angle(this.transform.forward, this.transform.TransformVector(target.transform.forward));
        float toTarget = Vector3.Angle(this.transform.forward, this.transform.TransformVector(targetDirection));
        
        if ((toTarget > 90 && relativeHeading < 20) )
        {
            Seek(target.transform.position);
            return;
        }
        //float lookAhead = targetDirection.magnitude / (_agent.speed + target.GetComponent<Player>().MoveSpeed);
        Seek(target.transform.position + target.transform.forward  );
    }
    private void Update()
    {
        Pursue();
    }
}
