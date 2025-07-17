using System.Collections.Generic;
using UnityEngine;

public class example1_5 : MonoBehaviour
{
    //created a magnitude function
    //unity provides a .magnitude function as well
    public LineRenderer line;
    private List<LineRenderer> lines = new List<LineRenderer>();

    private Color[] colors = { Color.grey, Color.black };
    private float[] widths = { 0.1f, 0.5f };
    private Vector2 center;
    private Vector2 mouse;
    private Vector2 bounds;

    void Start()
    {
        FindCenter();

        center = new Vector2(0, 0);

        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        for (int i = 0; i < 2; i++)
        {
            lines.Add(Instantiate(line, center, Quaternion.identity));
            lines[i].positionCount = 2;
            lines[i].startColor = colors[i];
            lines[i].endColor = colors[i];
            lines[i].startWidth = widths[i];
            lines[i].endWidth = widths[i];
        }
    }

    void FixedUpdate()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        mouse = mouse - center;

        //float m = magnitude(mouse);

        float m = mouse.magnitude;


        lines[0].SetPosition(0, center);
        lines[0].SetPosition(1, mouse);

        //-bounds.x to draw on the left side of screen
        lines[1].SetPosition(0, new Vector2(-bounds.x, bounds.y));
        //need to subtract bounds.x for correct scale
        lines[1].SetPosition(1, new Vector2(m - bounds.x, bounds.y));

    }

    float magnitude(Vector2 v)
    {
        return Mathf.Sqrt(v.x * v.x + v.y * v.y);
    }

    public void FindCenter()
    {
        Camera.main.orthographic = true;

        Camera.main.transform.position = new Vector3(0, 0, -10);

        bounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }
}
