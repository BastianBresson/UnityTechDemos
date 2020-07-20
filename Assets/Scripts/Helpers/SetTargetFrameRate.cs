using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTargetFrameRate : MonoBehaviour
{
    public int targetFrameRate;

    void Start()
    {
        Application.targetFrameRate = targetFrameRate;
    }
}
