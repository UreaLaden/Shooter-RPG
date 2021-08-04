using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TownInventory : MonoBehaviour
{
    public static TownInventory Instance;
    public int produceCapacity = 5;
    public int waterCapacity = 10;
    public int deliveredWater = 0;
    public int onHandBreadAmount = 0;
    public int onHandFlourAmount = 0;
    public int onHandWaterAmount = 0;
    public int storedBreadAmount = 0;
    public int storedProduceAmount = 0;
    public int storedWaterAmount = 0;
    public Stop[] _allStops; 
    private void Awake()
    {
        Instance = this;
        _allStops = FindObjectsOfType<Stop>();
    }

}
