using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBlock : MonoBehaviour
{
    private GameObject buildBlocksHolder;

    [SerializeField] private GameObject buildingBlockPrefab = default;

    [SerializeField] private List<GameObject> addBuildingCubeButtons = default;
    [SerializeField] private float buttonsAppearTime = default;

    Vector3 buttonStartScale;

    void Start()
    {
        buttonStartScale = addBuildingCubeButtons[0].transform.localScale;
        Appear();

        DisableAllButtons();

        buildBlocksHolder = GameObject.Find("BuildingBlocks");
    }

    private void Appear()
    {
        Vector3 toScale = transform.localScale;

        transform.localScale = Vector3.zero;

        LeanTween.scale(gameObject, toScale, 1f).setEase(LeanTweenType.easeOutBounce);
    }

    public void OnSelected()
    {
        MakeButtonsAppear();
    }

    public void OnDeselect()
    {
        MakeButtonsDisappear();
    }

    private void MakeButtonsAppear()
    {
        EnableAllButtons();
        ResetButtonsScale();

        foreach (GameObject button in addBuildingCubeButtons)
        {
            MoveButtonIn(button);
            ScaleButtonIn(button);
        }
    }

    private void MakeButtonsDisappear()
    {
        Invoke(nameof(DisableAllButtons), buttonsAppearTime);

        foreach (GameObject button in addBuildingCubeButtons)
        {
            //MoveButtonOut(button);
            ScaleButtonOut(button);
        }
    }


    private void MoveButtonIn(GameObject go)
    {
        Vector3 toPostion = go.transform.position;

        go.transform.position = transform.position;

        LeanTween.move(go, toPostion, buttonsAppearTime).setEase(LeanTweenType.easeOutSine); ;
    }


    private void MoveButtonOut(GameObject go)
    {
        LeanTween.move(go, transform.position, buttonsAppearTime).setEase(LeanTweenType.easeOutSine); ;
    }


    private void ScaleButtonIn(GameObject go)
    {
        Vector3 startScale = go.transform.localScale;

        go.transform.localScale = Vector3.zero;

        LeanTween.scale(go, startScale, buttonsAppearTime).setEase(LeanTweenType.easeOutSine);
    }


    private void ScaleButtonOut(GameObject go)
    {
        LeanTween.scale(go, Vector3.zero, buttonsAppearTime).setEase(LeanTweenType.easeOutSine);
    }


    private void EnableAllButtons()
    {
        foreach (GameObject button in addBuildingCubeButtons)
        {
            button.SetActive(true);
        }
    }


    private void DisableAllButtons()
    {
        foreach (GameObject button in addBuildingCubeButtons)
        {
            button.SetActive(false);
        }
    }

    private void ResetButtonsScale()
    {
        foreach (GameObject button in addBuildingCubeButtons)
        {
            button.transform.localScale = buttonStartScale;
        }
    }

    public void AddCubePressed(Vector3 position)
    {
        Instantiate(buildingBlockPrefab, position, Quaternion.identity, buildBlocksHolder.transform);
    }
}
