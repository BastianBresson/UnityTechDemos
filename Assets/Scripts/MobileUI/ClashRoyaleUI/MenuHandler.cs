using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
using UnityEngine.UI;

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
            LayoutElement element = menuButtons[i].GetComponent<LayoutElement>();

            if (i == currentPage)
            {
                StartCoroutine(MakeBigger(element));
            }
            else
            {
                StartCoroutine(MakeSmaller(element));
            }
        }
    }


    IEnumerator MakeBigger(LayoutElement element)
    {
        float elapsedTime = 0f;

        while (elapsedTime < 0.2f)
        {
            float width = Mathf.Lerp(element.preferredWidth, normaltWidth + 100, elapsedTime / 0.2f);

            element.preferredWidth = width;

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    IEnumerator MakeSmaller(LayoutElement element)
    {
        float elapsedTime = 0f;

        while (elapsedTime < 0.2f)
        {
            float width = Mathf.Lerp(element.preferredWidth, normaltWidth - 50, elapsedTime / 0.2f);

            element.preferredWidth = width;

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }


    private IEnumerator InitilizeButtons()
    {
        yield return new WaitForEndOfFrame();

        normaltWidth = menuButtons[0].GetComponent<LayoutElement>().preferredWidth;

        currentPage = pageScroller.CurrentPage;

        isInitialised = true;

        OnPageChanged(currentPage);
    }
}
