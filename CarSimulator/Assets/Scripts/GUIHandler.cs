using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GUIHandler : MonoBehaviour
{
    public Button btnAssemblyMode;
    public Button btnDisasemblyMode;
    public Button btnRepairMode;


    public static GUIHandler instance;

    private void Awake()
    {
        instance = this;
    }

}
