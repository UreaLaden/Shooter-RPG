using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TownInventory : MonoBehaviour
{
    public static TownInventory Instance;
    public int produceAmount = 5;
    public int produceCapacity = 5;
    public int breadAmount = 0;
    public int waterAmount = 0;
    public int drawOffset = 0;
    public string name = "";

    private void Awake()
    {
        Instance = this;
    }

    void OnGUI()
    {
        GUI.Box(new Rect(0,0 + drawOffset,100,100),"" + name);
        GUI.Label(new Rect(10,20 + drawOffset,100,20),$"Produce: {produceAmount}");
        GUI.Label(new Rect(10,35 + drawOffset,100,20),$"Bread: {breadAmount}");
        GUI.Label(new Rect(10,50 + drawOffset,100,20),$"Water: {waterAmount}");
    }
   
}
