using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CarPart : MonoBehaviour
{
    private float currentCountWear, maxCountWear = 100;

    public float currentWear
    {
        get { return currentCountWear / maxCountWear;}
    }

    public MeshCollider collider;
    public Car.Part part;
    public DisassemblyDirection disassemblyDirection;
    private Vector3 direction;


    public PartState state;


    public List<CarPart> disassemblyRequirements;
    public List<CarPart> assemblyRequirements;

    private bool isTransparent = false;
    private MeshRenderer meshRendered;
    public Material[] mat;
    private Shader originalShader;
    private Color[] originalColors;
    public Vector3 startPos;


    void Awake()
    {
        currentCountWear = Random.Range(0, maxCountWear);

        collider = GetComponent<MeshCollider>();

        startPos = transform.localPosition;
        InitializeDirection();
        meshRendered = this.gameObject.GetComponent<MeshRenderer>();

        mat = new Material[meshRendered.materials.Length];
        originalColors = new Color[meshRendered.materials.Length];

        for (int i = 0; i < meshRendered.materials.Length; i++)
        {
            mat[i] = meshRendered.materials[i];
            originalColors[i] = mat[i].color;
        }
    }


    public enum DisassemblyDirection
    {
        Left,
        Right,
        Forward,
        Back,
        Up,
        Down
    }
    public enum PartState
    {
        installed,
        dismounted
    }


    public void Assembly()
    {
        StartCoroutine(AssemblyPart());
    }

    private IEnumerator AssemblyPart()
    {
        ShowDefaultPart(false);
        Cursor.visible = false;
        ControlType.instance.inputAllowed = false;

        transform.position += direction / 60 * 20;

        for (int i = 0; i < 20; i++)
        {
            transform.position -= direction / 60;
            yield return new WaitForSeconds(0.01f);
        }

        state = PartState.installed;
        transform.localPosition = startPos;


        Cursor.visible = true;
        ControlType.instance.inputAllowed = true;

        Car.instance.DisplayAllDissamblyParts();

    }
    private IEnumerator DisassemblyPart()
    {

        Inventory.instance.ItemsList.Add(this);

        ShowDefaultPart(false);
        Cursor.visible = false;
        ControlType.instance.inputAllowed = false;

        for (int i = 0; i < 20; i++)
        {
            transform.position += direction / 60;
            yield return new WaitForSeconds(0.01f);
        }

        state = PartState.dismounted;
        transform.localPosition = startPos;
        meshRendered.enabled = false;

        collider.enabled = false;

        Cursor.visible = true;
        ControlType.instance.inputAllowed = true;
    }

    private void InitializeDirection()
    {
        switch (disassemblyDirection)
        {
            case DisassemblyDirection.Left:
                direction = Vector3.left;
                break;

            case DisassemblyDirection.Right:
                direction = Vector3.right;
                break;


            case DisassemblyDirection.Forward:
                direction = Vector3.forward;
                break;


            case DisassemblyDirection.Back:
                direction = Vector3.back;
                break;

            case DisassemblyDirection.Up:
                direction = Vector3.up;
                break;

            case DisassemblyDirection.Down:
                direction = Vector3.down;
                break;
        }
    }



    private bool CanAssembly()
    {
        if (state == PartState.dismounted)
        {
            foreach (CarPart part in assemblyRequirements)
            {
                if (part.state == PartState.dismounted)
                {
                    return false;
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool CanDisassembly()
    {
        if (state == PartState.installed)
        {
            foreach (CarPart part in disassemblyRequirements)
            {
                if (part.state == PartState.installed)
                {
                    return false;
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool CanRepair()
    {
        if (state == PartState.installed)
        {
            return true;
        }
        else
        {
            return false;
        }
    }



    public void ShowPart()
    {
        for (int i = 0; i < mat.Length; i++)
        {
            Shaders.setTransparent(mat[i], originalColors[i], false);
        }
        meshRendered.enabled = true;
    }

    public void HidePart()
    {
        meshRendered.enabled = false;
    }

    private void ShowDefaultPart(bool transparent)
    {
        meshRendered.enabled = true;
        for (int i = 0; i < mat.Length; i++)
        {
            Shaders.setTransparent(mat[i], Color.gray, true);
        }
    }




    private void RequireToRemove()
    {
        if (state == PartState.installed)
        {
            for (int i = 0; i < mat.Length; i++)
            {
                Shaders.setTransparent(mat[i], Color.red, true);
            }
        }
    }

    private void RequireToAdd()
    {
        if (state == PartState.dismounted)
        {
            for (int i = 0; i < mat.Length; i++)
            {
                Shaders.setTransparent(mat[i], Color.black, true);
            }
        }
    }


    public void AssemblyMaterialEffect()
    {
        if (state == PartState.installed)
        {
            for (int i = 0; i < mat.Length; i++)
            {
                Shaders.setTransparent(mat[i], Color.gray, true);

            }
            meshRendered.enabled = true;
        }
    }



    public void DisassemblyMaterialEffect()
    {
        if (state == PartState.dismounted)
        {
            for (int i = 0; i < mat.Length; i++)
            {
                Shaders.setTransparent(mat[i], Color.gray, true);

            }
        }
        meshRendered.enabled = true;
        collider.enabled = true;

        for (int i = 0; i < assemblyRequirements.Count; i++)
        {
            if (assemblyRequirements[i].state == PartState.dismounted)
            {
                collider.enabled = false;
                break;
            }
        }
    }

    public void Repair()
    {
        currentCountWear = maxCountWear;
    }







    #region CLICKEVENTS
    public void OnMouseDown()
    {
        if (!ControlType.instance.inputAllowed)
            return;

        switch (ControlType.instance.currentMode)
        {
            case ControlType.Type.Assembly:

                if (CanAssembly() && !Inventory.instance.InventoryPanel.activeSelf)
                {
                    Inventory.instance.ShowInventory(this);
                }
                break;


            case ControlType.Type.Disassembly:
                if (CanDisassembly())
                    StartCoroutine(DisassemblyPart());
                break;


            case ControlType.Type.Repair:
                if (CanRepair())
                {

                }
                break;
        }
    }




    public void OnMouseEnter()
    {

        switch (ControlType.instance.currentMode)
        {
            case ControlType.Type.Assembly:

                if (state == PartState.dismounted)
                {

                    for (int i=0; i<mat.Length; i++)
                    {
                        Shaders.setTransparent(mat[i], Color.yellow, true);
                    }


                    foreach (CarPart carPart in assemblyRequirements)
                    {
                        carPart.RequireToAdd();
                    }
                }

                break;


            case ControlType.Type.Disassembly:
                if (state == PartState.installed)
                {
                    for (int i = 0; i < mat.Length; i++)
                    {
                        Shaders.setTransparent(mat[i], Color.green, true);
                    }

                    foreach (CarPart carPart in disassemblyRequirements)
                    {
                        carPart.RequireToRemove();
                    }
                }



                break;


            case ControlType.Type.Repair:
                if (CanRepair())
                {

                }
                    //Shaders.setShader(Shaders.ShaderMode.Normal, mat, Color.blue);
                break;
        }


    }
    public void OnMouseExit()
    {

        switch (ControlType.instance.currentMode)
        {
            case ControlType.Type.Assembly:

                if (state == PartState.dismounted)
                {
                    ShowDefaultPart(true);

                    foreach (CarPart carPart in assemblyRequirements)
                    {
                        if (carPart.state == PartState.dismounted)
                            carPart.ShowDefaultPart(true);
                    }
                }
                break;


            case ControlType.Type.Disassembly:

                if (state == PartState.installed)
                {
                    ShowDefaultPart(true);

                    foreach (CarPart carPart in disassemblyRequirements)
                    {
                        if (carPart.state == PartState.installed)
                        carPart.ShowDefaultPart(true);
                    }
                }

                break;


            case ControlType.Type.Repair:
                if (CanRepair())
                {
                    meshRendered.enabled = true;
                    //Shaders.setShader(Shaders.ShaderMode.Normal, mat, originalColor);
                }
                break;
        }
        
    }

    #endregion

}




