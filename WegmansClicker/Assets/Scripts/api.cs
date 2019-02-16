using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// Dairy, Produce, Meat, Grains
// Name, Brand, Price

public class Api {

    // TODO properly handle API key storage
    private const string API_KEY = "26f415a416f749bbb28fcf6d70c8818b";

    public IEnumerable PerformRequest()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://api.wegmans.io/products/125194?api-version=2018-10-18&subscription-key=" + API_KEY);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
        }
    }

    static void Main(string[] args)
    {
        new Api().PerformRequest();
    }

 }
