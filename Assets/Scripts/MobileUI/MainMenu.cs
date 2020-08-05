using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private List<GameObject> roomCubes = default;

    [SerializeField] private GameObject mainMenuUI = default;
    [SerializeField] private List<GameObject> startMenuButtons = default;
    [SerializeField] private GameObject background = default;
    [SerializeField] private GameObject backgroundOutline = default;

    [SerializeField] private GameObject buildingPlatform = default;
    [SerializeField] private GameObject firstBuildingBlock = default;

    [SerializeField] private float mainMenuAppearTime = default;

    Vector3 UIStartScale;

    private void Start()
    {
        StartSequence();

        UIStartScale = mainMenuUI.transform.localScale;
    }

    private void StartSequence()
    {
        MakeRoomAppear();
        MakeBuildingPlatformAppear();

        Invoke(nameof(MakeUIAppear), 6f);
    }

    private void MakeRoomAppear()
    {
        foreach (GameObject cube in roomCubes)
        {
            float delayTime = Random.Range(0f, 2f);

            MakeObjectAppearWithOutBounce(cube, 3f, delayTime);
        }
    }

    private void MakeBuildingPlatformAppear()
    {
        MakeObjectAppearWithOutBounce(buildingPlatform, 1f, 5f);
    }

    private void MakeObjectAppearWithOutBounce(GameObject go, float scaleTime, float delayTime)
    {
        Vector3 startScale = go.transform.localScale;

        go.transform.localScale = Vector3.zero;

        LeanTween.scale(go, startScale, scaleTime).setEase(LeanTweenType.easeOutBounce).setDelay(delayTime);
    }

    private void MakeUIElementAppear(GameObject go, float scaleTime, float delayTime)
    {
        Vector3 startScale = go.transform.localScale;

        go.transform.localScale = new Vector3(1f, 0f, 0f);

        LeanTween.scale(go, startScale, scaleTime).setEase(LeanTweenType.easeOutSine).setDelay(delayTime);
    }

    private void MakeUIElementDisappear(GameObject go, float scaleTime, float delayTime)
    {
        LeanTween.scale(go, new Vector3(1f, 0f, 1f), scaleTime).setEase(LeanTweenType.easeOutSine).setDelay(delayTime);
    }

    private void MakeUIAppear()
    {
        mainMenuUI.SetActive(true);
        mainMenuUI.transform.localScale = UIStartScale;

        MakeBackgroundAppear();
        MakeStartButtonsAppear();
    }

    private void MakeBackgroundAppear()
    {
        backgroundOutline.SetActive(false);
        Invoke(nameof(MakeBackgroundOutlineAppear), mainMenuAppearTime);

        MakeUIElementAppear(background, mainMenuAppearTime, 0.1f);
    }

    private void MakeStartButtonsAppear()
    {
        foreach (GameObject element in startMenuButtons)
        {
            MakeUIElementAppear(element, mainMenuAppearTime, Random.Range(mainMenuAppearTime, mainMenuAppearTime + 0.2f));
        }
    }

    private void MakeBackgroundOutlineAppear()
    {
        backgroundOutline.SetActive(true);
    }

    private void MakeUIDisappear()
    {
        foreach (GameObject element in startMenuButtons)
        {
            MakeUIElementDisappear(element, mainMenuAppearTime, 0);
        }

        backgroundOutline.SetActive(false);

        MakeUIElementDisappear(background, mainMenuAppearTime, mainMenuAppearTime);

        Invoke(nameof(DisableUI), 2*mainMenuAppearTime + 0.5f * mainMenuAppearTime);
    }



    private void DisableUI()
    {
        mainMenuUI.SetActive(false);
    }

    public void OnPlayPressed()
    {
        firstBuildingBlock.SetActive(true);

        MakeUIDisappear();

        MakeObjectAppearWithOutBounce(firstBuildingBlock, 1f, 1f);
    }
}
