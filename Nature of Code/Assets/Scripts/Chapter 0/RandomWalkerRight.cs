using System;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class RandomWalkerRight : MonoBehaviour
{
    //Create a walker
    WalkerRight walker;

    void Start()
    {
        //instantiate walker
        walker = new WalkerRight();
        //set color
        walker.Show();

    }

    void FixedUpdate()
    {
        walker.Step();
        walker.checkBounds();
    }
}

public class WalkerRight
{

    private Vector3 location;

    private Vector2 bounds;

    private GameObject w = GameObject.CreatePrimitive(PrimitiveType.Sphere);


    public WalkerRight()
    {
        FindCenter();
        Show();
        location = randomPosition();
        Debug.Log(location);


    }

    public void Show()
    {
        float red = UnityEngine.Random.Range(0.0f, 1.0f);
        float green = UnityEngine.Random.Range(0.0f, 1.0f);
        float blue = UnityEngine.Random.Range(0.0f, 1.0f);

        Color randomColor = new Color(red, green, blue);

        var wRenderer = w.GetComponent<Renderer>();
        wRenderer.material.color = randomColor;
    }

    public void Step()
    {
        Vector3 delta = new Vector3();

        float choice = UnityEngine.Random.Range(0, 1.0f);

        //Debug.Log(choice);

        if (choice < 0.4f)
        {
            delta.x += 1.25f;
        }
        else if (choice < 0.6f)
        {
            delta.x -= 1.25f;
        }
        else if (choice < 0.8f)
        {
            delta.y += 1.25f;
        }
        else
        {
            delta.y -= 1.25f;
        }

        w.transform.position += delta * Time.deltaTime;
    }

    public void checkBounds()
    {
        location = w.transform.position;
        if (location.x > bounds.x || location.x < -bounds.x)
        {
                    Show();

            location = randomPosition();

        }
        if (location.y > bounds.y || location.y < -bounds.y)
        {
                    Show();

            location = randomPosition();
        }

        w.transform.position = location;
    }
    public void FindCenter()
    {
        Camera.main.orthographic = true;

        bounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    Vector2 randomPosition()
    {
            float xPos = UnityEngine.Random.Range(-bounds.x, bounds.x);
            float yPos = UnityEngine.Random.Range(-bounds.y, bounds.y);
            return new Vector2(xPos, yPos);
    }
}
