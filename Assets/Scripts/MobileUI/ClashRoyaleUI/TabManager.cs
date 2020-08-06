using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    [SerializeField] private List<Button> tabButtons = default;
    [SerializeField] private List<GameObject> tabs = default;

    [SerializeField] Color selectedColor = default;
    [SerializeField] Color deselectedColor = default;

    int activeTab = 0;

    private void Start()
    {
        for (int i = 0; i < tabButtons.Count; i++)
        {
            int copy = i;
            tabButtons[i].onClick.AddListener(() => SwitchTab(copy));
            tabButtons[i].GetComponent<Image>().color = deselectedColor;

            tabs[i].SetActive(false);
        }

        tabButtons[activeTab].Select();
        tabs[activeTab].SetActive(true);
        tabButtons[activeTab].GetComponent<Image>().color = selectedColor;

    }
    
    private void SwitchTab(int selectedTab)
    {
        if (selectedTab == activeTab) return;

        tabButtons[activeTab].GetComponent<Image>().color = deselectedColor;
        tabs[activeTab].SetActive(false);

        tabs[selectedTab].SetActive(true);
        tabButtons[selectedTab].GetComponent<Image>().color = selectedColor;

        activeTab = selectedTab;
    }
}
