using UnityEngine;
using System.Collections.Generic;
using System.Linq.Expressions;

public class example2_3 : MonoBehaviour
{
  public GameObject circlePrefab;

    List<Circle2_3> circles = new List<Circle2_3>();

    private Vector2 gravity = new Vector2(0.0f, -980f);
    private Vector2 wind = new Vector2(50f, 0.0f);
    Circle2_2 c;
    Circle2_2 lc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            circles.Add(new Circle2_3(Instantiate(circlePrefab), Random.Range(0.5f, 2.0f), new Vector2(Random.Range(-5, 5), 4)));
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < circles.Count; i++)
        {
            //get circle mass
            float currentMass = circles[i].GetMass();
            //multiply it by gravity
            Vector2 scaledGravity = gravity * currentMass;
            circles[i].ApplyForce(scaledGravity);
            circles[i].Update();
            circles[i].CheckEdges();

        }
        if (Input.GetMouseButton(0))
        {
            for (int i = 0; i < circles.Count; i++)
            {
                circles[i].ApplyForce(wind);
            }
        }

    }
}

public class Circle2_3
{
    float mass;
    float r;
    Vector2 position;
    Vector2 velocity;
    Vector2 acceleration;
    Vector2 bounds;
    public GameObject ball;
    public Circle2_3(GameObject g, float m, Vector2 pos)
    {
        bounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        ball = g;
        position = pos;
        mass = m;
        velocity = Vector2.zero;
        acceleration = Vector2.zero;

        ball.transform.localScale = new Vector3(1, 1, 1) * mass;

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
        velocity = velocity + acceleration * Time.deltaTime;
        position = position + velocity * Time.deltaTime;

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

        if (position.y - r < -bounds.y)
        {
            velocity.y *= -1;
            position.y = -bounds.y + r;
        }
    }

    public float GetMass()
    {
        return mass;
    }
}
