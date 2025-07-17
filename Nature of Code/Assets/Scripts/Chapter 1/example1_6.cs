using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class example1_6 : MonoBehaviour
{
    //prefab of our line with default sprite shader
    public LineRenderer linePrefab;
    //list to store our lines
    private List<LineRenderer> lines = new List<LineRenderer>();
    private Color[] colors = { Color.grey, Color.black };
    private float[] widths = { 0.1f, 0.5f };
    private Vector2 center = new Vector2(0, 0);
    private Vector2 mouse;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        for (int i = 0; i < 2; i++)
        {
            lines.Add(Instantiate(linePrefab, center, Quaternion.identity));
            lines[i].positionCount = 2;
            lines[i].startColor = colors[i];
            lines[i].endColor = colors[i];
            lines[i].startWidth = widths[i];
            lines[i].endWidth = widths[i];
        }

        lines[1].numCapVertices = 8; // set higher for round ends

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mouse = mouse - center;

        lines[0].SetPosition(0, center);
        lines[0].SetPosition(1, mouse);

        Vector2 normalizedMouse = normalize(mouse);
        normalizedMouse = normalizedMouse * 0.5f;

        lines[1].SetPosition(0, center);
        lines[1].SetPosition(1, normalizedMouse);

    }

    Vector2 normalize(Vector2 v)
    {
        float m = v.magnitude;
        if (m > 0.0f)
        {
            v = v / m;
        }
        return v;
    }
}


