using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BankManager : MonoBehaviour {
    public int moneyBag;
    public Text moneyBagText;

    public void IncreaseMoney(int valueOfItem)
    {
        moneyBag+= valueOfItem;
    }

    // Use this for initialization
    void Start()
    {
        moneyBag = 0;
    }

    // Update is called once per frame
    void Update()
    {
        moneyBagText.text = moneyBag.ToString();
    }
}
