using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class example1_4 : MonoBehaviour
{
    //prefab for our circle
    public GameObject circle;

    // List<LineRenderer> lines = new List<LineRenderer>(); //refactor
    //have to create line renderer per line
    LineRenderer lineRenderer;
    LineRenderer newLine;
    LineRenderer anotherLine;

    //center circle and mouse circle
    GameObject centerCirc;
    GameObject mouseCirc;

    //vectors for center, mouse, top left
    Vector2 center;
    Vector2 mouse;
    Vector2 topLeft;

    //bounds of our screen
    Vector2 bounds;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //used this func to get the bounds of the screen
        FindCenter();

        //init center vector
        center = new Vector2(0, 0);

        //init mouse vector
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //init top left vector
        topLeft = new Vector2(-bounds.x, bounds.y);

        //create a circle 
        centerCirc = Instantiate(circle, center, Quaternion.identity);
        //get the renderer
        Renderer centerCircRenderer = centerCirc.GetComponent<Renderer>();
        //set the material color to grey
        centerCircRenderer.material.color = new Color(0.5f, 0.5f, 0.5f);

        //create a circle
        mouseCirc = Instantiate(circle, mouse, Quaternion.identity);
        //get the renderer
        Renderer mouseCircRenderer = mouseCirc.GetComponent<Renderer>();
        //set the material color to grey
        mouseCircRenderer.material.color = new Color(0.5f, 0.5f, 0.5f);


        //create a line renderer and assign a material
        lineRenderer = new GameObject().AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        //assign number of positions, for start and end position
        lineRenderer.positionCount = 2;
        //thickness of line
        lineRenderer.startWidth = 0.1f;
        //start and end color
        lineRenderer.startColor = new Color(0, 0, 0);
        lineRenderer.endColor = new Color(0, 0, 0);

        newLine = new GameObject().AddComponent<LineRenderer>();
        newLine.material = new Material(Shader.Find("Sprites/Default"));
        newLine.positionCount = 2;
        newLine.startWidth = 0.1f;
        newLine.startColor = new Color(0, 0, 0, 0.5f);
        newLine.endColor = new Color(0, 0, 0, 0.5f);

        anotherLine = new GameObject().AddComponent<LineRenderer>();
        anotherLine.material = new Material(Shader.Find("Sprites/Default"));
        anotherLine.positionCount = 2;
        anotherLine.startWidth = 0.1f;
        anotherLine.startColor = new Color(0, 0, 0, 0.5f);
        anotherLine.endColor = new Color(0, 0, 0, 0.5f);


    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //get the mouse position from screen space 
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //set the mouse position
        mouseCirc.transform.position = mouse;

        //draw line from top left to center
        newLine.SetPosition(0, topLeft);
        newLine.SetPosition(1, center);

        //draw line from top left to mouse
        anotherLine.SetPosition(0, topLeft);
        anotherLine.SetPosition(1, mouse);

        //calculate the difference from the mouse position to the center
        Vector2 newVec = SubtractVector(mouse, center);

        //draw line from the center to our new vector
        lineRenderer.SetPosition(0, center);
        lineRenderer.SetPosition(1, newVec);

    }

    //function to get bounds and to set up camera
    public void FindCenter()
    {
        Camera.main.orthographic = true;

        Camera.main.transform.position = new Vector3(0, 0, -10);

        bounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    //subtract function could also just use - since unity supports that
    Vector2 SubtractVector(Vector2 v1, Vector2 v2)
    {
        float newX = v1.x - v2.x;
        float newY = v1.y - v2.y;
        return new Vector2(newX, newY);
    }
}
