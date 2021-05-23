using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click3DCursor : MonoBehaviour
{
    public GameObject placedTarget;
    public GameObject hoverTarget;

    public LayerMask GroundLayer;

    private bool cursorPointingToGround;
    private Camera cam;

    private void Start()
    {
        cam = FindObjectOfType<Camera>();
        placedTarget.transform.parent = null;
        hoverTarget.transform.parent = null;
        placedTarget.SetActive(false);
        hoverTarget.SetActive(false);
    }

    public void InitCursor(Vector3 pos)
    {
        placedTarget.transform.position = pos;
        hoverTarget.transform.position = pos;
        placedTarget.SetActive(false);
        hoverTarget.SetActive(false);
        hoverTarget.transform.localScale *= 0.98f;
    }


    /// <summary>
    /// If computerCursor in game + pointing to ground, 
    ///     1. moves the 3D cursor to the location on the ground where the computerCursor is pointing.
    ///     2. makes the computerCursor invisible, and 3D cursor visible
    /// Else:
    ///     Makes the computerCursor visible, and 3D cursor invisible
    /// </summary>
    void UpdateActiveCursor()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, GroundLayer))
        {
            Cursor.visible = false;
            hoverTarget.SetActive(true);
            Vector3 pos = new Vector3(hit.point.x, hoverTarget.transform.position.y,
                hit.point.z);
            hoverTarget.transform.position = pos;
            placedTarget.transform.position = pos;
        }
        else
        {
            hoverTarget.SetActive(false);
            placedTarget.SetActive(false);
            Cursor.visible = true;
        }
    }

    private void Update()
    {
        UpdateActiveCursor();
        if (Input.GetMouseButton(0))
        {
            if (!Cursor.visible)
            {
                placedTarget.SetActive(true);
            }
        }


        if (Input.GetMouseButtonUp(0))
        {
            placedTarget.SetActive(false);
        }
    }

    public Vector3 GetTargetPosition()
    {
        if (TargetSet())
            return placedTarget.transform.position;
        else
            return Vector3.zero;
    }

    public bool TargetSet()
    {
        return placedTarget.activeSelf;
    }
}