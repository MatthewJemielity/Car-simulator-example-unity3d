using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GridItem : MonoBehaviour
{
    [SerializeField]
    private Text txt;
    private CarPart carPart;

    [SerializeField]
    private Image imgCountWear;
    
    public void SetItemOnUI(CarPart carPart)
    {
        this.carPart = carPart;
        txt.text = carPart.part.ToString();

        imgCountWear.fillAmount = carPart.currentWear;
    }

    public void BtnRepairCarPart()
    {
        carPart.Repair();
        imgCountWear.fillAmount = carPart.currentWear;
    }

    public void BtnAssemblyCarPart()
    {
        if (carPart == null)
            return;

        Inventory.instance.HideInventory();

        carPart.Assembly();
        


    }
}
