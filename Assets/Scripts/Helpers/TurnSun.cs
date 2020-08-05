using UnityEngine;

public class TurnSun : MonoBehaviour
{
    [SerializeField] private float totalTurnTime = default;
    [SerializeField] private Vector3 startRotationEuler = default;
    [SerializeField] private Vector3 endRotationEuler = default;
   
    private float elapsedTime = 0;


    private void FixedUpdate()
    {
        if (elapsedTime > totalTurnTime) return;

        elapsedTime += Time.deltaTime;

        float ratio = elapsedTime / totalTurnTime;

        LerpRotation(ratio);
    }


    private void LerpRotation(float ratio)
    {
        Vector3 newRotationEuler = Vector3.Lerp(startRotationEuler, endRotationEuler, ratio);
        this.transform.rotation = Quaternion.Euler(newRotationEuler);
    }
}
