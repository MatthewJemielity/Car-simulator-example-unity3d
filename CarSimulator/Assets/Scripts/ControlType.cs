using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlType : MonoBehaviour
{
    public bool inputAllowed = true;
    public static ControlType instance;
    public Type currentMode = Type.Disassembly;
    public enum Type
    {
        Assembly = 1,
        Disassembly = 2,
        Repair = 3
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {

    }

    public void SetControlTypeOnAssembly()
    {
        currentMode = Type.Assembly;
    }
    public void SetControlTypeOnDisassembly()
    {
        currentMode = Type.Disassembly;
    }

    public void SetControlTypeOnRepair()
    {
        currentMode = Type.Repair;
    }

}
