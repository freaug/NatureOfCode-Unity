using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;


public class example2_5 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created  public GameObject circlePrefab;
    public GameObject circlePrefab;
    public GameObject liquidPrefab;
    List<Circle2_5> circles = new List<Circle2_5>();
    Liquid2_3 liquid;
    private Vector2 gravity = new Vector2(0.0f, -9.814f * 100);
    private Vector2 wind = new Vector2(1.0f, 0.0f);

    float scalar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector2 bounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        scalar = 100;

        liquid = new Liquid2_3(liquidPrefab, 0.1f * scalar * 0.5f);

        for (int i = 0; i < 8; i++)
        {
            //add 8 circles evenly spaced with random mass and height
            circles.Add(new Circle2_5(Instantiate(circlePrefab), Random.Range(0.75f, 2.25f), new Vector2(i * 4f + (-bounds.x + 1f), (int)Random.Range(3, 8))));
        }

    }

    // Update is called once per frame
    //use fixed update for physics simulation
    void FixedUpdate()
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

            //add friction
            if (circles[i].ContactEdge())
            {
                //calc friction
                float c = 0.01f;
                float normal = 1;
                float frictionMag = c * normal;
                Vector2 friction = circles[i].GetVelocity();
                friction = friction * -1;
                friction = friction.normalized;
                friction = friction * frictionMag;

                circles[i].ApplyForce(friction);
            }

            //apply drag
            if (liquid.Contains(circles[i]))
            {
                Vector2 dragForce = liquid.CalculateDrag(circles[i]);
                circles[i].ApplyForce(dragForce);
            }


        }
    }

    void Update()
    {
        for (int i = 0; i < circles.Count; i++)
        {
            if (Input.GetMouseButtonDown(0))
            {
                circles[i].Reset();
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

public class Circle2_5
{
    float mass;
    float r;
    public Vector2 position;
    Vector2 velocity;
    Vector2 acceleration;
    Vector2 bounds;
    public GameObject ball;
    public Circle2_5(GameObject g, float m, Vector2 pos)
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

    public void Reset()
    {
        position.y = (int)Random.Range(3, 8);
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

public class Liquid2_3
{
    GameObject liquid;
    float coefficient;
    public Liquid2_3(GameObject g, float c)
    {
        liquid = g;
        coefficient = c;
    }

    public bool Contains(Circle2_5 c)
    {
        Vector2 pos = c.position;
        return
            pos.y < liquid.transform.position.y + liquid.transform.localScale.x;
    }

    public Vector2 CalculateDrag(Circle2_5 c)
    {
        float speed = c.GetVelocity().magnitude;
        //Debug.Log("speed = " + speed);
        float dragMagnitude = coefficient * speed * speed;
        //Debug.Log("drag magnitude = " + dragMagnitude);
        Vector2 dragForce = c.GetVelocity();
        //Debug.Log("drag force = " + dragForce);
        dragForce = dragForce * -1;
        //Debug.Log("drag force = " + dragForce);
        Vector2 dragVector = new Vector2(dragForce.x * dragMagnitude, dragForce.y * dragMagnitude);
        //Debug.Log("drag vector = " + dragVector);
        return dragVector;
    }
}
