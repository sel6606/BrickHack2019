using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour {
    public GameObject referenceMoneyBag;
    
    public float itemValue;
    public Text itemValueText;
    public int multipler;
    public Text multiplerText;

    public void IncreaseMoney()
    {
        referenceMoneyBag.GetComponent<BankManager>().IncreaseMoney(itemValue * multipler);
    }

    public float CurrentValue()
    {
        return itemValue * multipler;
    }

    // Use this for initialization
    void Start()
    {
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
