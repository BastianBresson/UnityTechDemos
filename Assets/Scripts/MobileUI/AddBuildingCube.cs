using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBuildingCube : MonoBehaviour
{
    [SerializeField] private GameObject buildCubeObject = default;

    BuildingBlock buildBlock;

    private void Start()
    {
        buildBlock = buildCubeObject.GetComponent<BuildingBlock>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);

        transform.forward = transform.forward * -1;
    }

    public void OnAddPressed()
    {
        buildBlock.AddCubePressed(transform.position);
    }
}
