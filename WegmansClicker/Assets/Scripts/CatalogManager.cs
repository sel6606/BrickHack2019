using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatalogManager : MonoBehaviour
{
    public Button[] buttons;
    public GameObject[] panels;



	// Use this for initialization
	void Start ()
    {
        OpenCatalog();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SelectTab(int tab)
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            if(i == tab)
            {
                buttons[i].interactable = false;
                panels[i].SetActive(true);
            }
            else
            {
                buttons[i].interactable = true;
                panels[i].SetActive(false);
            }
        }
    }

    public void OpenCatalog()
    {
       StartCoroutine(Api.instance.ProductRequest("125194"));
    }
}
