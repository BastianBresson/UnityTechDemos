using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnTextBoxOnClick : MonoBehaviour
{
    [SerializeField] private string text = default;

    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(Clicked);
    }

    private void Clicked()
    {
        Debug.Log(text);
    }
}
