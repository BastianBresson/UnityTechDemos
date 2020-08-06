using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SortCardCollection : MonoBehaviour
{
    [SerializeField] private GameObject collection;

    [SerializeField] List<string> buttonSortingTexts = default;

    private Button button;

    private Card[] cards;

    private int currentSort = 0;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SortCollection);

        cards = collection.GetComponentsInChildren<Card>();

        SortByRarity();
    }

    private void SortCollection()
    {
        if (currentSort == 0)
        {
            SortByRarity();
        }
        else
        {
            SortByName();
        }
    }

    private void SortByName()
    {
        Card[] sorted = cards.OrderBy(card => card.name).ToArray();

        for (int i = 0; i < sorted.Length; i++)
        {
            sorted[i].transform.SetSiblingIndex(i);
        }

        currentSort = 0;
        button.GetComponentInChildren<TMP_Text>().text = buttonSortingTexts[0];
    }

    private void SortByRarity()
    {
        Card[] sorted = cards.OrderBy(card => card.rarity).ToArray();

        for (int i = 0; i < sorted.Length; i++)
        {
            sorted[i].transform.SetSiblingIndex(i);
        }

        currentSort = 1;
        button.GetComponentInChildren<TMP_Text>().text = buttonSortingTexts[1];
    }
}
