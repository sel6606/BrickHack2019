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
    public GameObject rowPrefab;
    public ItemInfo[] categories;

    public int debugButtons;


    public List<FoodItem> currentFood;

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

    public void InitButtonsDebug(int tab)
    {
        switch(tab)
        {
            case 0:
                StartCoroutine(Api.instance.GetDairyFoodItems());
                break;
            case 1:
                return;
                break;
            case 2:
                return;
                break;
            case 3:
                return;
                break;
            case 4:
                return;
                break;
            default:
                return;
                break;
        }

        currentFood = Api.instance.foodItems;

        foreach (Transform child in panels[tab].transform.GetChild(0))
        {
            Destroy(child.gameObject);
        }
        int numRows = currentFood.Count / 2;

        GameObject currentRow = null;
        for (int i = 0; i < currentFood.Count; i++)
        {
            if (i % 2 == 0)
            {
                currentRow = Instantiate(rowPrefab, panels[tab].transform.GetChild(0));
                currentRow.transform.localPosition = new Vector3(currentRow.transform.localPosition.x, currentRow.transform.localPosition.y - (100 * i), currentRow.transform.localPosition.z);

                currentRow.transform.GetChild(0).GetComponentInChildren<Text>().text = currentFood[i].name + "\n$" + currentFood[i].price;

                currentRow.transform.GetChild(0).gameObject.AddComponent<CatalogItem>();
                currentRow.transform.GetChild(0).GetComponent<CatalogItem>().Food = currentFood[i];
                currentRow.transform.GetChild(0).GetComponent<CatalogItem>().category = categories[tab];

                currentRow.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { currentRow.transform.GetChild(0).GetComponent<CatalogItem>().MakePurchase(); });
            }
            else
            {
                currentRow.transform.GetChild(1).GetComponentInChildren<Text>().text = currentFood[i].name + "\n$" + currentFood[i].price;
                currentRow.transform.GetChild(1).gameObject.AddComponent<CatalogItem>();
                currentRow.transform.GetChild(1).GetComponent<CatalogItem>().Food = currentFood[i];
                currentRow.transform.GetChild(1).GetComponent<CatalogItem>().category = categories[tab];

                currentRow.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { currentRow.transform.GetChild(1).GetComponent<CatalogItem>().MakePurchase(); });
            }
        }
    }

    public void OpenCatalog()
    {
        StartCoroutine(Api.instance.GetDairyFoodItems());
        currentFood = Api.instance.foodItems;
        InitButtonsDebug(0);
    }

    public void ChangeVisiblity()
    {
        farm.SetActive(catalog.activeSelf);
        catalog.SetActive(!catalog.activeSelf);
    }
}
