using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class example2_1 : MonoBehaviour
{
    public GameObject circlePrefab;
    private GameObject circle;

    private Vector2 gravity = new Vector2(0.0f, -980f);
    private Vector2 wind = new Vector2(50f, 0.0f);
    Circle c;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        circle = Instantiate(circlePrefab);
        c = new Circle(circle);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        c.ApplyForce(gravity);
        c.Update();
        c.CheckEdges();

        if (Input.GetMouseButton(0)) {
            c.ApplyForce(wind);
        }
    }
}

public class Circle
{
    float mass;

    float r;
    Vector2 position;
    Vector2 velocity;
    Vector2 acceleration;
    Vector2 bounds;
    public GameObject ball;
    public Circle(GameObject g)
    {
        bounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        ball = g;
        mass = 1f;
        position = new Vector2(0, bounds.y);
        velocity = Vector2.zero;
        acceleration = Vector2.zero;
        r = ball.transform.localScale.y / 2;
    }

    public void ApplyForce(Vector2 force)
    {
        Vector2 f = force;
        f = f / mass;
        acceleration = acceleration + f * Time.deltaTime;
    }

    public void Update()
    {
        velocity = velocity + acceleration* Time.deltaTime;
        position = position + velocity *  Time.deltaTime;

        acceleration = acceleration * Vector2.zero;

        ball.transform.position = position;
    }

    public void CheckEdges()
    {
        if (position.x + r > bounds.x)
        {
            position.x = bounds.x - r;
            velocity.x *= -1;
        }
        else if (position.x - r < -bounds.x)
        {
            velocity.x *= -1;
            position.x = -bounds.x + r;
        }

        if (position.y  - r < -bounds.y)
        {
            velocity.y *= -1;
            position.y = -bounds.y + r;
        }
    }
}
