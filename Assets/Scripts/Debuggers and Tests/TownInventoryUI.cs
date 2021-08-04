using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TownInventoryUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _produceText;
    [SerializeField] private TMP_Text _breadText;
    [SerializeField] private TMP_Text _waterText;

    [SerializeField] private TMP_Text _carriedProduce;
    [SerializeField] private TMP_Text _carriedBread;
    [SerializeField] private TMP_Text _carriedWater;
    void Start()
    {
    }

    void Update()
    {
        _produceText.text = "Produce: " + TownInventory.Instance.storedProduceAmount;
        _breadText.text = "Bread: " + TownInventory.Instance.storedBreadAmount;
        _waterText.text = "Water: " + TownInventory.Instance.storedWaterAmount;
        
        _carriedProduce.text = "Carried Produce: " + TownInventory.Instance.onHandFlourAmount;
        _carriedBread.text = "Carried Bread: " + TownInventory.Instance.onHandBreadAmount;
        _carriedWater.text = "Carried Water: " + TownInventory.Instance.onHandWaterAmount;
    }
}