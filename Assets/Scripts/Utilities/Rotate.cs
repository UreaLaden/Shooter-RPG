using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float harvestDuration = 5f;
    private float startTime;
    public bool extractWater;
    [SerializeField] private GameObject handle;
    [SerializeField] private NavMeshAgent _waterBotNavMeshAgent;

    private void Update()
    {
        float remainingDistance = Helpers.GetPathRemainingDistance(_waterBotNavMeshAgent);
        extractWater = remainingDistance < 2f;
        if (remainingDistance > 2f)
        {
            startTime = Time.time;
        }
        if (startTime == 0)
        {
            startTime = Time.time;
        }
        if (extractWater)
        {
            
            handle.transform.Rotate(new Vector3(0f,0f,100f) * Time.deltaTime);
            if (Time.time - startTime > harvestDuration && extractWater)
            {
                Debug.Log("Done gathering water");
                extractWater = false;
            }
        }

        
       
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Somebody needs water");
    }
}
