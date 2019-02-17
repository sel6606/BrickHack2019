using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatalogItem : MonoBehaviour
{
    public ItemInfo category;

    private FoodItem food;
    private bool bought;

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
		
	}

    public void MakePurchase()
    {
        category.increaseValue((int)food.price);
    }
}
