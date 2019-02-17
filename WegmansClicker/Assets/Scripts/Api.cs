using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading;

// Dairy, Produce, Meat, Grains
// Name, Brand, Price

[Serializable]
public class FoodItem
{
    public int sku;
    public string name;
    public string brand;
    public float price;
}

public class Api : MonoBehaviour
{
    public static Api instance;

    public string currentResponseString; // TODO change how this is handled

    // TODO properly handle API key storage
    private const string API_KEY = "26f415a416f749bbb28fcf6d70c8818b";



    void Awake()
    {
        //If there is not already a GameInfo object, set it to this
        if (instance == null)
        {
            //Object this is attached to will be preserved between scenes
            DontDestroyOnLoad(gameObject);

            instance = this;
        }
        else if (instance != this)
        {
            //Ensures that there are no duplicate objects being made every time the scene is loaded
            Destroy(gameObject);
        }
    }

    public string ProductRequest(string sku)
    {
        string queryString = "https://api.wegmans.io/products/" + sku + "?api-version=2018-10-18&subscription-key=" + API_KEY;
        string responseString = "";
        PerformRequest(queryString, responseString);
        CreateFoodFromJSON(currentResponseString);
        return "";
    }

    public void PriceRequest(string sku, string storeId)
    {
        string queryString = "https://api.wegmans.io/products/" + sku + "?api-version=2018-10-18&subscription-key=" + API_KEY;
        string responseString = "";
        PerformRequest(queryString, responseString);
        CreateFoodFromJSON(responseString);
    }

    public IEnumerator PerformRequest(string requestString, string responseString)
    {
        UnityWebRequest productRequest = UnityWebRequest.Get(requestString);
        var temp = productRequest.SendWebRequest();
        while(!temp.isDone);

        if (productRequest.isNetworkError || productRequest.isHttpError)
        {
            Debug.Log(productRequest.error);
        }
        else
        {
            currentResponseString = productRequest.downloadHandler.text;
            
        }
        return null;
    }

    public FoodItem CreateFoodFromJSON(string jsonString)
    {      
        // Note, this will NOT have a price
        return JsonUtility.FromJson<FoodItem>(jsonString); ;
    }



}
