using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnBox = default;
    [SerializeField] private GameObject objectToSpawnPrefab = default;
    [SerializeField] private float spawnFrequency = default;


    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }


    private IEnumerator SpawnRoutine()
    {
        if (spawnFrequency <= 0)
        {
            Debug.LogWarning("Spawn frequency cannot be less than 0");

            // Exit coroutine
            yield break;
        }

        while (true)
        {
            yield return new WaitForSeconds(spawnFrequency);

            Vector3 spawnPosition = RandomPointInBox(spawnBox.transform.position, spawnBox.transform.localScale);

            Instantiate(objectToSpawnPrefab, spawnPosition, Quaternion.identity);
        }
    }


    private Vector3 RandomPointInBox(Vector3 center, Vector3 size)
    {
        return center + new Vector3
        (
           (Random.value - 0.5f) * size.x,
           (Random.value - 0.5f) * size.y,
           (Random.value - 0.5f) * size.z
        );
    }


    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
