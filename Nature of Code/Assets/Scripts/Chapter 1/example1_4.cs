using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;
public class example1_4 : MonoBehaviour
{

    //illustrated a multiply and divide function that can be applied to our mouse vector
    //unity allows you to use the * and / operators on vectors with a scalar to achieve the same result
    public LineRenderer line;

    List<LineRenderer> lines = new List<LineRenderer>();

    private Vector2 center;

    private Vector2 mouse;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        center = new Vector2(0, 0);

        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        lines.Add(Instantiate(line, center, Quaternion.identity));
        lines[0].positionCount = 2;
        lines[0].startColor = Color.grey;
        lines[0].endColor = Color.grey;

        lines.Add(Instantiate(line, center, Quaternion.identity));
        lines[1].positionCount = 2;
        lines[1].startColor = Color.black;
        lines[1].endColor = Color.black;
        lines[1].startWidth = 0.2f;
        lines[1].endWidth = 0.2f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mouse = mouse - center;

        lines[0].SetPosition(0, center);
        lines[0].SetPosition(1, mouse);

        // Vector2 scaledMouse = multiply(mouse, 0.5f);

        mouse = mouse / 2;

        //Vector2 scaledMouse = divide(mouse, 2);

        lines[1].SetPosition(0, center);
        lines[1].SetPosition(1, mouse);

    }

    Vector2 multiply(Vector2 v, float scalar)
    {
        float newX = v.x * scalar;
        float newY = v.y * scalar;

        return new Vector2(newX, newY);
    }

    Vector2 divide(Vector2 v, float scalar)
    {
        float newX = v.x / scalar;
        float newY = v.y / scalar;
        return new Vector2(newX, newY);
    }

}
