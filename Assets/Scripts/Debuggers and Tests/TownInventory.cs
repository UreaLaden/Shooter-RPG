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
    public int onHandProduceAmount = 5;
    public int onHandBreadAmount = 0;
    public int onHandWaterAmount = 0;
    public int storedWaterAmount = 0;
    public int drawOffset = 0;
    public string name = "";

    private void Awake()
    {
        Instance = this;
    }

    void OnGUI()
    {
        GUI.Box(new Rect(0,0 + drawOffset,150,100),"" + name);
        GUI.Label(new Rect(10,20 + drawOffset,150,20),$"Produce: {onHandProduceAmount}");
        GUI.Label(new Rect(10,35 + drawOffset,150,20),$"Bread On Hand: {onHandBreadAmount}");
        GUI.Label(new Rect(10,50 + drawOffset,150,20),$"Water On Hand: {onHandWaterAmount}");
        GUI.Label(new Rect(10,65 + drawOffset,150,20),$"Barrells: {storedWaterAmount}");
        GUI.Label(new Rect(10,65 + drawOffset,150,20),$"Delivered Water: {deliveredWater}");
    }
   
}
