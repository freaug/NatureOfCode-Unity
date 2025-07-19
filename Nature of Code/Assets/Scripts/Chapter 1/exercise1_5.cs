using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


/**
Create a simulation of an object (think about a vehicle) that accelerates 
when you press the up arrow and brakes when you press the down arrow.
**/

public class exercise1_5 : MonoBehaviour
{
    public GameObject circlePrefab;

    private GameObject vehicle;

    private Vector2 position = new Vector2(0, 0), velocity = new Vector2(0, 0), acceleration = new Vector2(0.001f, 0);
    private Vector2 bounds;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        vehicle = Instantiate(circlePrefab, position, Quaternion.identity);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        CheckEdges();

        vehicle.transform.position = position;
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            acceleration.x += 0.01f;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            acceleration.x -= 0.01f;
        }

        velocity = velocity + acceleration;
        velocity = Vector2.ClampMagnitude(velocity, 0.09f);
        if (velocity.x <= 0.0f)
        {
            velocity.x = 0.0f;
            acceleration.x = 0.0f;
        }
        position = position + velocity;
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
