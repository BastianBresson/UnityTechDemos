using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
    public int rarity;

    public string cardName;

    private void Start()
    {
        GetComponentInChildren<TMP_Text>().text = cardName;
    }
}
