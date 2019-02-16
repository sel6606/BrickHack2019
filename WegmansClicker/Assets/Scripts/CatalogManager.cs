using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatalogManager : MonoBehaviour
{
    public Button[] buttons;
    public GameObject[] panels;
    public GameObject catalog;
    public GameObject farm;

	// Use this for initialization
	void Start ()
    {
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

    public void ChangeVisiblity()
    {
        farm.SetActive(catalog.activeSelf);
        catalog.SetActive(!catalog.activeSelf);
    }
}
