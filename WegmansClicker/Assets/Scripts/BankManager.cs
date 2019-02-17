using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BankManager : MonoBehaviour {
    public float moneyBag;
    public Text moneyBagText;

    public void IncreaseMoney(float valueOfItem)
    {
        moneyBag+= valueOfItem;
        moneyBag = (float)(Math.Round(moneyBag, 2));
    }

    // Use this for initialization
    void Start()
    {
        moneyBag = 0;
    }

    // Update is called once per frame
    void Update()
    {
        moneyBagText.text = "$" + String.Format("{0:0.00}", moneyBag);
    }
}
