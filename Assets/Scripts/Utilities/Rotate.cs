using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Rotate : MonoBehaviour
{
    [SerializeField] private GameObject handle;
    public bool extractWater = false;
    private void Update()
    {
        if (extractWater == true)
        {
            handle.transform.Rotate(new Vector3(0f,0f,100f) * Time.deltaTime);
        }
    }

 
}
