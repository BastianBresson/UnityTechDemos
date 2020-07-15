﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject spawnBox;
    public GameObject objectToSpawnPrefab;
    public float spawnFrequency;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }


    IEnumerator SpawnRoutine()
    {
        if (spawnFrequency <= 0)
        {
            Debug.LogWarning("Spawn frequency needs to be > 0");
            yield break;
        }

        while (true)
        {
            yield return new WaitForSeconds(spawnFrequency);

            Vector3 spawnPosition = RandomPointInBox(spawnBox.transform.position, spawnBox.transform.localScale);

            GameObject spawnedItem = Instantiate(objectToSpawnPrefab, spawnPosition, Quaternion.identity);

            float x = Random.Range(-1f, 1f);
            float y = Random.Range(-1f, 1f);
            float z = Random.Range(-1f, 1f);
            Vector3 forceDirection = new Vector3(x, y, z);

            float force = Random.Range(50f, 80f);
            spawnedItem.GetComponent<Rigidbody>().AddForce(forceDirection * force);
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
