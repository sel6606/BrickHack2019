using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour {
    public GameObject referenceMoneyBag;
    public int itemValue;

    public void IncreaseMoney()
    {
        referenceMoneyBag.GetComponent<BankManager>().IncreaseMoney(itemValue);
    }

    // Use this for initialization
    void Start()
    {

    }
   
    public void increaseValue(int additionalValue)
    {
        itemValue += additionalValue;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
