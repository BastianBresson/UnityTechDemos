using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePoolObject : MonoBehaviour
{
    public float speedMin;
    public float speedMax;

    Vector3 direction;
    float speed;
       
    private void OnEnable()
    {
        direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        if (direction == Vector3.zero)
        {
            direction.y = 1;
        }
        speed = Random.Range(speedMin, speedMax);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed);
    }
}
