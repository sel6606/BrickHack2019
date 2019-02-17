using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Globalization;

// Dairy, Produce, Meat, Grains
// Name, Brand, Price

[Serializable]
public class FoodItem
{
    public int sku;
    public string name;
    public string brand;
    public float price;
    public List<TradeIdentifier> tradeIdentifiers;
}

[Serializable]
public class TradeIdentifier
{
    public List<string> images;
}

[Serializable]
public class RandomUsersResponse
{
    public List<Person> results;
}

[Serializable]
public class Person
{
    public PersonName name;
    public PersonRegistered registered;
}

[Serializable]
public class PersonName
{
    public string title;
    public string first;
    public string last;
}

[Serializable]
public class PersonRegistered
{
    public string date;
    public int age;
}

public class User
{
    public string name;
    public int age;

    public User(Person p)
    {
        name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(p.name.first) + ' ' + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(p.name.last);
        age = p.registered.age;
    }
}

public class Api : MonoBehaviour
{
    public static Api instance;
 

    // TODO properly handle API key storage
    private const string API_KEY = "26f415a416f749bbb28fcf6d70c8818b";


    private string[] dairyItems = { "125194", "270092", "465346", "40033", "40032", "196", "10404", "10631", "18610", "23731" };
    private string[] meatItems = { "2226", "4427", "8934", "19564", "15097", "18785", "31186", "10125", "4562", "5111" };
    private string[] produceItems = { "5595", "14524", "14559", "15057", "92576", "23459", "92942", "173303", "50335", "14721" };
    private string[] bakeryItems = { "10193", "24011", "26114", "29391", "29880", "30360", "42044", "42374", "44072", "46082" };

    public List<FoodItem> foodItems;
    public List<Person> cashiers;

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

    public IEnumerator GetCashiers()
    {
        string queryString = "https://randomuser.me/api/?inc=name,registered&results=5";
        string response = PerformRequest(queryString);
        RandomUsersResponse res = JsonUtility.FromJson<RandomUsersResponse>(response);
        cashiers = res.results;
        yield return res.results; // unused
    }
    

    public IEnumerable GetHardFoodItems(string[] hardItems)
    {
        foodItems = new List<FoodItem>();
        foreach (string s in hardItems)
        {
            foodItems.Add(RequestFoodItem(s, "62"));
        }
        return foodItems;
    }

    public IEnumerator GetDairyFoodItems()
    {
        foodItems = new List<FoodItem>();
        foreach (string s in dairyItems)
        {
            foodItems.Add(RequestFoodItem(s, "62"));
        }
        yield return foodItems;
    }

    public IEnumerator GetMeatFoodItems()
    {
        yield return GetHardFoodItems(meatItems);
    }

    public IEnumerator GetProduceFoodItems()
    {
        yield return GetHardFoodItems(produceItems);
    }

    public IEnumerator GetBakeryFoodItems()
    {
        yield return GetHardFoodItems(bakeryItems);
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

    public byte[] getImageAsByteData(FoodItem item)
    {
        if (item.tradeIdentifiers != null && item.tradeIdentifiers.Count > 0)
        {
            foreach(TradeIdentifier ti in item.tradeIdentifiers)
            {
                if (ti.images != null && ti.images.Count > 0)
                {
                    string imageUrl = ti.images[0];
                    using (var webClient = new WebClient())
                    {
                        byte[] imageBytes = webClient.DownloadData(imageUrl);
                        return imageBytes;
                    }                    
                }
                else
                {
                    return null;
                }
            }
            return null;
        }
        else
        {
            return null;
        }
    }
}
