using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// Dairy, Produce, Meat, Grains
// Name, Brand, Price

public class Api : MonoBehaviour
{

    public static Api instance;

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

    public IEnumerator PerformRequest()
    {
        Debug.Log("Hi");
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

}
