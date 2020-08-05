using UnityEngine;

public class MovePoolObject : MonoBehaviour
{
    [SerializeField] private float speedMin = default;
    [SerializeField] private float speedMax = default;

    private Vector3 direction;
    private float speed;
       

    private void OnEnable()
    {
        SetRandomDirection();
        SetRandomSpeed();
    }


    private void SetRandomDirection()
    {
        direction = new Vector3
            (
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f)
            );

        // Object must move to eventually leave spawner
        if (direction == Vector3.zero)
        {
            direction.y = 1;
        }
    }


    private void SetRandomSpeed()
    {
        speed = Random.Range(speedMin, speedMax);
    }


    private void Update()
    {
        Move();
    }


    private void Move()
    {
        transform.Translate(direction * speed);
    }
}
