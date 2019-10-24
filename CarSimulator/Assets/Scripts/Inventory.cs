using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    public GameObject InventoryPanel;
    public GridItem gridItem;
    public static Inventory instance;

    public List<CarPart> ItemsList = new List<CarPart>();


    public void ShowInventory(CarPart carPart)
    {
        InventoryPanel.SetActive(true);

        gridItem.SetItemOnUI(carPart);
        /*
        print(carPart.part);
        gridItem.GetComponentInChildren<Text>().text = carPart.part.ToString();
        */


    }

    public void HideInventory()
    {
        InventoryPanel.SetActive(false);
    }


    private void Awake()
    {
        instance = this;
    }
}
