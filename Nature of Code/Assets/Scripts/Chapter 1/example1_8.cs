using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class example1_8 : MonoBehaviour
{
    public GameObject circlePrefab;

    private GameObject circle;
    private Vector2 bounds;
    private Vector2 position = new Vector2(0, 0);
    private Vector2 velocity = new Vector2(0, 0);
    private Vector2 acceleration = new Vector2(-0.001f, 0.01f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        circle = Instantiate(circlePrefab, position, Quaternion.identity);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        CheckEdges();

        circle.transform.position = position;

    }

    private void Move()
    {
        velocity = velocity + acceleration;

        //clamp magnitude is the unity equivilant to limit
        velocity = Vector2.ClampMagnitude(velocity, 0.7f);
     
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
