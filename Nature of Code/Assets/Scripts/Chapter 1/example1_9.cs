using UnityEngine;

public class example1_9 : MonoBehaviour
{
    public GameObject circlePrefab;
    private GameObject vehicle;
    private Vector2 position, velocity, acceleration;
    float topSpeed = 20f;
    private Vector2 bounds;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        position = new Vector2(0, 0);
        velocity = new Vector2(0, 0);
        acceleration = new Vector2(0, 0);

        vehicle = Instantiate(circlePrefab, position, Quaternion.identity);
        vehicle.transform.localScale = new Vector2(10,10);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        CheckEdges();

        vehicle.transform.position = position;
    }

    void Move()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);

        acceleration = new Vector2(randomX, randomY);
        acceleration.Normalize();
        acceleration = acceleration * Random.Range(10.0f, 40.0f);

        velocity = velocity + acceleration * Time.deltaTime;

        velocity = Vector2.ClampMagnitude(velocity, topSpeed);

        position = position + velocity * Time.deltaTime;

    }

    public void CheckEdges()
    {
        if (position.x > bounds.x)
        {
            position.x = -bounds.x;
        }
        else if (position.x < -bounds.x)
        {
            position.x = bounds.x;
        }

        if (position.y > bounds.y)
        {
            position.y = -bounds.y;
        }
        else if (position.y < -bounds.y)
        {
            position.y = bounds.y;
        }
    }
}
