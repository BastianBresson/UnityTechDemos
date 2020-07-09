using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSun : MonoBehaviour
{
    public float totalTurnTime;
    public Vector3 startRotationEuler;
    public Vector3 endRotationEuler;
   
    private float elapsedTime = 0;

    private void FixedUpdate()
    {
        if (elapsedTime > totalTurnTime) return;

        elapsedTime += Time.deltaTime;
        float ratio = elapsedTime / totalTurnTime;

        Vector3 newRotationEuler = Vector3.Lerp(startRotationEuler, endRotationEuler, ratio);
        this.transform.rotation = Quaternion.Euler(newRotationEuler);
    }
}
