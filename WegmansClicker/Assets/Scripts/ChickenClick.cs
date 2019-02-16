using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenClick : MonoBehaviour {

    public GameObject referenceMoneyBag;
    public int eggsValue;

    public void IncreaseMoney()
    {
        referenceMoneyBag.GetComponent<BankManager>().moneyBag+=eggsValue;
    }

    // Use this for initialization
    void Start()
    {
        eggsValue = 12;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
