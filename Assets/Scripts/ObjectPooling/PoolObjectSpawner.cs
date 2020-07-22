using ObjectPooling;
using System.Collections;
using UnityEngine;

public class PoolObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnBox = default;
    [SerializeField] private float spawnFrequency = default;
    [SerializeField] private string poolTag = default;


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

            ObjectPooler.Instance.InstantiatePooledItem(poolTag, spawnPosition, Quaternion.identity);
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
        // Gameobject is reused by object pooler
        other.gameObject.SetActive(false);
    }
}
