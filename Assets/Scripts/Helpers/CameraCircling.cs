using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCircling : MonoBehaviour
{
    public GameObject focalPoint;
    public float circlingSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(focalPoint.transform.position, Vector3.up, circlingSpeed * Time.deltaTime);
    }
}
