using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CowClick : MonoBehaviour {

    public GameObject referenceMoneyBag;
    public int milkValue;

    public void IncreaseMoney(){
        referenceMoneyBag.GetComponent<BankManager>().moneyBag+=milkValue;
    }

	// Use this for initialization
	void Start () {
        milkValue = 1;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
