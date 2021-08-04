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
    public int drawOffset = 0;
    public string name = "";

    private void Awake()
    {
        Instance = this;
    }

   /* void OnGUI()
    {
        GUI.Box(new Rect(0,0 + drawOffset,150,125),"" + name);
        GUI.Label(new Rect(10,20 + drawOffset,150,20),$"Stored Produce:  {storedProduceAmount}");
        GUI.Label(new Rect(10,35 + drawOffset,150,20),$"Stored Bread:    {storedWaterAmount}");
        GUI.Label(new Rect(10,50 + drawOffset,150,20),$"Stored Water:    {storedWaterAmount}");
        GUI.Label(new Rect(10,65 + drawOffset,150,20),$"Bread On Hand:   {onHandBreadAmount}");
        GUI.Label(new Rect(10,80 + drawOffset,150,20),$"Flour On Hand:   {onHandFlourAmount}");
        GUI.Label(new Rect(10,95 + drawOffset,150,20),$"Water On Hand:   {onHandWaterAmount}");
    }
   */
}
