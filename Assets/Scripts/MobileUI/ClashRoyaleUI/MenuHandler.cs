using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private HorizontalScrollSnap pageScroller = default;
    [SerializeField] private List<PageHandler> pages = default;

    [SerializeField] private List<GameObject> menuButtons = default;

    private int currentPage;

    private float normaltWidth;

    bool isInitialised;

    private void Start()
    {
        pageScroller.OnSelectionPageChangedEvent.AddListener(OnPageChanged);

        StartCoroutine(InitilizeButtons());
    }


    public void ChangePage(int pageNumber)
    {
        if (currentPage == pageNumber) return;

        pageScroller.ChangePage(pageNumber);
    }

    public void OnPageChanged(int pageNumber)
    {
        if (!isInitialised) return;
        Debug.Log($"Page {pageNumber} is now the active page");
        currentPage = pageNumber;
        TweenButtons();

        AlertPage(pageNumber);
    }

    private void AlertPage(int pageNumber)
    {
        pages[pageNumber].OnBecomeActive();
    }

    private void TweenButtons()
    {
        for (int i = 0; i < menuButtons.Count; i++)
        {
            RectTransform rect = menuButtons[i].GetComponent<RectTransform>();

            if (i == currentPage)
            {
                TweenBig(rect);
            }
            else
            {
                TweenSmall(rect);
            }
        }
    }

    private void TweenBig(RectTransform rect)
    {
        Vector2 size = rect.rect.size;

        size.x = normaltWidth + 100;

        LeanTween.size(rect, size, 0.2f).setEaseInOutSine();
    }

    private void TweenSmall(RectTransform rect)
    {
        Vector2 size = rect.rect.size;

        size.x = normaltWidth - 50;
        LeanTween.size(rect, size, 0.2f).setEaseInOutSine();
    }

    private IEnumerator InitilizeButtons()
    {
        yield return new WaitForEndOfFrame();

        normaltWidth = menuButtons[0].GetComponent<RectTransform>().rect.size.x;

        currentPage = pageScroller.CurrentPage;

        isInitialised = true;

        OnPageChanged(currentPage);
    }
}
