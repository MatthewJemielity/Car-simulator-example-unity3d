using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateModel : MonoBehaviour
{
    public float speed = 2.0F;

    bool rotateMode = false;
    Vector3 oldMousePos;
    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            rotateMode = true;
            oldMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(1))
        {
            rotateMode = false;
        }

        if (rotateMode)
        {
            float h = speed * Input.GetAxis("Mouse X");
            transform.Rotate(0, -h, 0);
        }
    }
}
