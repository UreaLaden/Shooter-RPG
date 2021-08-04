using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Stop : MonoBehaviour
{
    [SerializeField] private int breadAmount = 0;
    [SerializeField] private int produceAmount = 0;
    [SerializeField] private int waterAmount = 0;
    public Dictionary<string, int> groceries;
    public GameObject dropOffLocation;
    public string _name;
    public int GetBreadAmount () => breadAmount;
    public int GetProduceAmount() => produceAmount;
    public int GetWaterAmount() => waterAmount;
    void Start()
    {
        groceries = new Dictionary<string, int>()
        {
            {"bread", breadAmount},
            {"Produce", produceAmount},
            {"Water", waterAmount}
        };
    }

    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position,1f);
    }
}
