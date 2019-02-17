using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading.Tasks;

// Dairy, Produce, Meat, Grains
// Name, Brand, Price

[Serializable]
public class FoodItem
{
    public int sku;
    public string name;
    public string brand;
    public float price;
    public List<TradeIdentifiers> tradeIdentifiers;
}

[Serializable]
public class TradeIdentifiers
{
    public List<string> images;
}

public class Api : MonoBehaviour
{
    public static Api instance;
 

    // TODO properly handle API key storage
    private const string API_KEY = "26f415a416f749bbb28fcf6d70c8818b";

    private string[] dairyItems = { "125194", "270092", "465346", "40033", "40032" };

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

    public IEnumerator GetDairyFoodItems()
    {
        // for each food call requestFoodItem ()
        List<FoodItem> foodItems = new List<FoodItem>();
        foreach (string s in dairyItems)
        {
            foodItems.Add(RequestFoodItem(s, "62"));
        }        
        var test = foodItems;
        return null;
    }
    
    public FoodItem RequestFoodItem(string sku, string storeId)
    {
        FoodItem food = CreateFoodFromJSON(ProductRequest(sku));
        string priceResponseString = PriceRequest(sku, storeId);
        AddPriceToFood(ref food, priceResponseString);
        return food;
    }

    public string ProductRequest(string sku)
    {
        string queryString = "https://api.wegmans.io/products/" + sku + "?api-version=2018-10-18&subscription-key=" + API_KEY;
        return PerformRequest(queryString);
    }

    public string PriceRequest(string sku, string storeId)
    {
        string queryString = "https://api.wegmans.io/products/" + sku + "/prices/" + storeId + "?api-version=2018-10-18&subscription-key=" + API_KEY;
        return PerformRequest(queryString);
    }

    public string PerformRequest(string requestString)
    {
        // Create a New HttpClient object and dispose it when done, so the app doesn't leak resources
        using (HttpClient client = new HttpClient())
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions
            try
            {
                HttpResponseMessage response = client.GetAsync(requestString).Result;
                response.EnsureSuccessStatusCode();
                string responseBody = response.Content.ReadAsStringAsync().Result;
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                return responseBody;
                //Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
        return "";
    }

    public FoodItem CreateFoodFromJSON(string jsonString)
    {
        // Note, this will NOT have a price
        return JsonUtility.FromJson<FoodItem>(jsonString);
    }

    public FoodItem AddPriceToFood(ref FoodItem food, string priceResponseString)
    {
        FoodItem has_price = JsonUtility.FromJson<FoodItem>(priceResponseString);
        food.price = has_price.price;
        return has_price; // unused rn
    }
}
