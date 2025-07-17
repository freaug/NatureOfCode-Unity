using System;
using Unity.IntegerTime;
using Unity.Mathematics;
using UnityEditor.Profiling;
using UnityEngine;

public class exercise1_6 : MonoBehaviour
{

    public GameObject circlePrefab;
    private GameObject vehicle;
    private Vector2 position, velocity, acceleration;
    float topSpeed = 1f;
    float heightScale, widthScale;
    float xScale, yScale;
    float xPos, yPos;
    private Vector2 bounds;

    float timeSinceReset, resetTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        position = new Vector2(0, 0);
        velocity = new Vector2(0, 0);
        acceleration = new Vector2(0, 0);

        vehicle = Instantiate(circlePrefab, position, Quaternion.identity);

        xScale = 1.0f;
        yScale = .5f;
        heightScale = 0.7f;
        widthScale = 1.0f;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeSinceReset = Time.time - resetTime;
        Move();
        CheckEdges();

    }

    void Move()
    {

        xPos = widthScale * Mathf.PerlinNoise(Time.time * xScale, 0.0f) * timeSinceReset;

        yPos = heightScale * Mathf.PerlinNoise(0.0f, Time.time * yScale) * timeSinceReset;

        acceleration = new Vector2(xPos, yPos);

        velocity = velocity + acceleration * Time.deltaTime;

        velocity = Vector2.ClampMagnitude(velocity, topSpeed);

        position = position + velocity * Time.deltaTime;

        vehicle.transform.position = position;

    }

    public void CheckEdges()
    {
        if (position.x > bounds.x)
        {
            Reset();
            position.x = -bounds.x;

        }
        else if (position.x < -bounds.x)
        {
            Reset();
            position.x = bounds.x;
        }

        if (position.y > bounds.y)
        {
            Reset();
            position.y = -bounds.y;
        }
        else if (position.y < -bounds.y)
        {
            Reset();
            position.y = bounds.y;
        }
    }

    void Reset()
    {
        resetTime = Time.time;
        heightScale = UnityEngine.Random.Range(-1f, 1f);
        widthScale = UnityEngine.Random.Range(-1f, 1f);
    }
}
