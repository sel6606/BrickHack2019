using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    public GameObject referenceMoneyBag;
    
    public float itemValue;
    public Text itemValueText;
    public int multipler;
    public Text multiplerText;
    public List<int> boughtSku;

    public void IncreaseMoney()
    {
        referenceMoneyBag.GetComponent<BankManager>().IncreaseMoney((float)(Math.Round(itemValue * multipler, 2)));
    }

    public float CurrentValue()
    {
        return itemValue * multipler;
    }

    // Use this for initialization
    void Start()
    {
        boughtSku = new List<int>();
        multipler = 1;
    }
   
    public void increaseValue(float additionalValue)
    {
        itemValue += additionalValue;
    }

    public void increaseMultipler()
    {
        multipler++;
    }

	// Update is called once per frame
	void Update () {
        itemValueText.text = "Value: $" + itemValue.ToString();
        multiplerText.text = "Multipler: " + multipler.ToString();
    }
}
