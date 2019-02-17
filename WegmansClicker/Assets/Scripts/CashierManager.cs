using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CashierManager : MonoBehaviour {

    public GameObject referenceMoneyBag;
    public GameObject dairyObject;
    public GameObject produceObject;
    public GameObject meatObject;
    public GameObject grainObject;

    public int cashierAmount;
    public float timeAmount;
    public float timer;
    public int pay;

    public Text cashierText;
    // Use this for initialization
    void Start () {
        cashierAmount = 0;
        timeAmount = 900f;
        pay = 0;
    }

    public void IncreaseCashier()
    {
        cashierAmount++;
        DecreaseTime();
    }

    public void DecreaseTime()
    {
        if (timeAmount == 0)
        {
            timeAmount = 270f;
        }
        else
        {
            timeAmount *= (2f/3f);
        }

    }

    void Automate()
    {
        // unsure why starting the game makes the player get paid
        if (pay == 1)
            return;

        float value = dairyObject.GetComponent<ItemInfo>().CurrentValue() +
            produceObject.GetComponent<ItemInfo>().CurrentValue() +
            meatObject.GetComponent<ItemInfo>().CurrentValue() +
            grainObject.GetComponent<ItemInfo>().CurrentValue();

        referenceMoneyBag.GetComponent<BankManager>().IncreaseMoney(value);

    }

    // Update is called once per frame
    void Update () {
        cashierText.text = cashierAmount.ToString() +
            " Cashier(s)\n. Time to earn money: " + timeAmount.ToString() + " second(s)";

        if (timeAmount != 0 && cashierAmount > 0)
        {
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                pay++;
                Automate();
                timer = timeAmount;
            };
        }

    }
}
