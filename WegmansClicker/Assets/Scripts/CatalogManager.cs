using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatalogManager : MonoBehaviour
{
    public Button[] buttons;
    public GameObject[] panels;
    public GameObject rowPrefab;

    public int debugButtons;

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

    public void InitButtons(int tab)
    {
        foreach(Transform child in panels[tab].transform.GetChild(0))
        {
            Destroy(child.gameObject);
        }
        int numRows = debugButtons / 2;

        for(int i = 0; i < numRows; i++)
        {
            GameObject temp = Instantiate(rowPrefab, panels[tab].transform.GetChild(0));
            temp.transform.localPosition = new Vector3(temp.transform.localPosition.x, temp.transform.localPosition.y - (100 * i), temp.transform.localPosition.z);
        }
    }
}
