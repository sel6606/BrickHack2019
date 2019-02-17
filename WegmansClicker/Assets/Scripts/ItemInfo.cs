using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour {
    public GameObject referenceMoneyBag;
    public float itemValue;
    public Text itemValueText;

    public void IncreaseMoney()
    {
        referenceMoneyBag.GetComponent<BankManager>().IncreaseMoney(itemValue);
    }

    // Use this for initialization
    void Start()
    {

    }
   
    public void increaseValue(float additionalValue)
    {
        itemValue += additionalValue;
    }

	// Update is called once per frame
	void Update () {
        itemValueText.text = "Value: $" + itemValue.ToString();
    }
}
