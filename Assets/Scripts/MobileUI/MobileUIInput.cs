using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileUIInput : MonoBehaviour
{
    private BuildingBlock selectedBlock = null;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseClick();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (selectedBlock != null)
            {
                selectedBlock.OnDeselect();
                selectedBlock = null;
            }
        }
    }

    private void MouseClick()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("BuildingCube"))
            {
                HandleBuildingBlockClick(hit.collider.gameObject);
            }
        }
    }

    private void HandleBuildingBlockClick(GameObject buildingBlock)
    {
        if (selectedBlock != null)
        {
            if (buildingBlock.GetInstanceID() == selectedBlock.gameObject.GetInstanceID())
            {
                return;
            }
            selectedBlock.OnDeselect();
        }

        selectedBlock = buildingBlock.GetComponent<BuildingBlock>();
        selectedBlock.OnSelected();
    }
}
