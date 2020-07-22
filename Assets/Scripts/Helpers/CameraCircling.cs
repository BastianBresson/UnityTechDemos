using UnityEngine;

public class CameraCircling : MonoBehaviour
{
    [SerializeField] private GameObject focalPoint = default;
    [SerializeField] private float circlingSpeed = default;


    private void Update()
    {
        transform.RotateAround(focalPoint.transform.position, Vector3.up, circlingSpeed * Time.deltaTime);
    }
}
