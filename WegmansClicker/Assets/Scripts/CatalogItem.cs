using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatalogItem : MonoBehaviour
{
    public ItemInfo category;
    public float rdValue;

    private FoodItem food;
    public bool bought;

    public float Value
    {
        get { return food.price; }
    }

    public int SKU
    {
        get { return food.sku; }
    }

    public FoodItem Food
    {
        get { return food; }
        set { food = value; }
    }

    


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(gameObject.GetComponent<Button>().interactable && bought)
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
	}

    public void MakePurchase()
    {
        if(category.referenceMoneyBag.GetComponent<BankManager>().moneyBag >= food.price)
        {
            category.increaseValue(food.price);
            category.referenceMoneyBag.GetComponent<BankManager>().moneyBag -= food.price;
            bought = true;
            category.boughtSku.Add(SKU);
        }

    }

    public void IncreaseMultiplier()
    {
        if (category.referenceMoneyBag.GetComponent<BankManager>().moneyBag >= rdValue)
        {
            category.referenceMoneyBag.GetComponent<BankManager>().moneyBag -= rdValue;
            category.increaseMultipler();
            string currentText = gameObject.GetComponentInChildren<Text>().text;

            rdValue += 300.0f;

            currentText = currentText.Substring(0, currentText.IndexOf("\n")) + "\n$" + rdValue;

            gameObject.GetComponentInChildren<Text>().text = currentText;
        }
    }

    public void UnlockCategory(Button tab)
    {
        if(category.referenceMoneyBag.GetComponent<BankManager>().moneyBag >= rdValue)
        {
            tab.interactable = true;
            category.referenceMoneyBag.GetComponent<BankManager>().moneyBag -= rdValue;
            bought = true;
        }
    }

    public void PurchaseExtra()
    {
        if (category.referenceMoneyBag.GetComponent<BankManager>().moneyBag >= rdValue)
        {
            category.referenceMoneyBag.GetComponent<BankManager>().moneyBag -= rdValue;
            bought = true;
        }
    }

    public void BuyCashier()
    {
        if (category.referenceMoneyBag.GetComponent<BankManager>().moneyBag >= rdValue)
        {
            category.referenceMoneyBag.GetComponent<BankManager>().moneyBag -= rdValue;
            string currentText = gameObject.GetComponentInChildren<Text>().text;

            rdValue += 1000.0f;

            currentText = currentText.Substring(0, currentText.IndexOf("\n")) + "\n$" + rdValue;

            gameObject.GetComponentInChildren<Text>().text = currentText;
        }
    }
}
