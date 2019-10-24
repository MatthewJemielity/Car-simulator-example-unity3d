using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    #region enumDeclare

    public static Car instance;

    [HideInInspector]
    public Animator anim;
    private void Awake()
    {
        instance = this;
        anim = GetComponent<Animator>();
    }

    public enum Part
    {
        Hood = 1,
        Exhaust = 2,
        Trunk,
        Spoiler,
        Engine,

        fenderR,
        FenderL,

        WindF,
        WindB,
        WindFR,
        WindFL,
        WindBR,
        WindBL,

        DoorBR,
        DoorBL,
        DoorFL,
        DoorFR,

        HLightFR,
        HLightFL,

        BrakeFR,
        BrakeFL,
        BrakeBR,
        BrakeBL,

        MirrorR,
        MirrorL,

        TireBR,
        TireBL,
        TireFL,
        TireFR,

        BumperB,
        BumperF,

        SeatR,
        SeatL,

        SkirtR,
        SkirtL,
    }


    #endregion

    public List<CarPart> carElements = new List<CarPart>();


    private void Start()
    {
        FindAllCarPart();
    }


    private void FindAllCarPart()
    {
        var foundObjects = FindObjectsOfType<CarPart>();

        foreach(CarPart carPart in foundObjects)
        {
            carElements.Add(carPart);
        }
    }

    

    public void DisplayAllDissamblyParts()
    {
        foreach (CarPart carPart in carElements)
        {
            if (carPart.state == CarPart.PartState.dismounted)
            {
                carPart.DisassemblyMaterialEffect();
            }
            else
            {
                carPart.ShowPart();
            }
        }
    }

    public void DisplayAllAssembly()
    {
        print("DisplayAllAssembly");
        foreach (CarPart carPart in carElements)
        {
            if (carPart.state == CarPart.PartState.installed)
            {
                carPart.AssemblyMaterialEffect();
            }
            else
            {
                carPart.HidePart();
            }
        }
    }





}
