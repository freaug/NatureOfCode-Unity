using UnityEngine;
using System.Collections.Generic;

public class example2_4 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created  public GameObject circlePrefab;
    public GameObject circlePrefab;

    List<Circle2_4> circles = new List<Circle2_4>();
    private Vector2 gravity = new Vector2(0.0f, -1980f);
    private Vector2 wind = new Vector2(1980f, 0.0f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 1; i++)
        {
            circles.Add(new Circle2_4(Instantiate(circlePrefab), Random.Range(0.5f, 2.0f), new Vector2(Random.Range(-5, 5), 4)));
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
            circles[i].BounceEdge();

            if (circles[i].ContactEdge())
            {
                float c = 0.01f;
                float normal = 1;
                float frictionMag = c * normal;
                Vector2 friction = circles[i].GetVelocity();
                friction = friction * -1;
                friction = friction.normalized;

                //friction = SetMag(friction, c);
                friction = friction * frictionMag;

                circles[i].ApplyForce(friction);
            }

        }


        if (Input.GetMouseButton(0))
        {
            for (int i = 0; i < circles.Count; i++)
            {
                circles[i].ApplyForce(wind);
            }
        }

    }
    public Vector2 SetMag(Vector2 v, float m)
    {
        float newX = v.x * m;
        float newY = v.y * m;
        return new Vector2(newX, newY);
    }

}

public class Circle2_4
{
    float mass;
    float r;
    Vector2 position;
    Vector2 velocity;
    Vector2 acceleration;
    Vector2 bounds;
    public GameObject ball;
    public Circle2_4(GameObject g, float m, Vector2 pos)
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

    public void BounceEdge()
    {
        float bounce = -0.5f;
        if (position.x + r > bounds.x)
        {
            position.x = bounds.x - r;
            velocity.x *= bounce;
        }
        else if (position.x - r < -bounds.x)
        {
            position.x = -bounds.x + r;
            velocity.x *= bounce;

        }

        if (position.y - r < -bounds.y)
        {
            position.y = -bounds.y + r;
            velocity.y *= bounce;

        }
    }

    public bool ContactEdge()
    {
        return position.y > -bounds.y + r + 0.01f;
    }

    public float GetMass()
    {
        return mass;
    }

    public Vector2 GetVelocity()
    {
        return velocity;
    }
}
