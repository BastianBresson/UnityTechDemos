using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageHandler : MonoBehaviour
{
    [SerializeField] private bool showTopBar = default;

    [SerializeField] private GameObject topBar = default;

    private static bool isTopBarActive = true;

    public void OnBecomeActive()
    {
        if (!showTopBar)
        {
            MakeTopBarDisappear();
        }
        else if (!isTopBarActive)
        {
            MakeTopBarAppear();
        }
    }

    private void MakeTopBarAppear()
    {
        topBar.SetActive(true);
        isTopBarActive = true;
        LeanTween.scaleY(topBar, 1, 0.2f).setEaseOutSine();
    }

    private void MakeTopBarDisappear()
    {
        isTopBarActive = false;
        LeanTween.scaleY(topBar, 0, 0.2f).setEaseOutSine();
        Invoke(nameof(DisableTopBar), 0.2f);
    }

    private void EnableTopBar()
    {
        topBar.SetActive(true);
    }

    private void DisableTopBar()
    {
        topBar.SetActive(false);
    }
}
