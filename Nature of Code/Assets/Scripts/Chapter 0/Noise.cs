using Unity.Collections;
using UnityEngine;

public class Noise : MonoBehaviour
{

    float tx1;
    float ty1;


    Vector2 bounds;

    public GameObject circle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tx1 = 1.0f;
        ty1 = 1000.0f;


        FindCenter();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
  
        DrawCircle();

    }

    public void FindCenter()
    {
        Camera.main.orthographic = true;

        bounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        Debug.Log(bounds);
    }

    public void DrawCircle()
    {
        float xPos = 1.0f * Mathf.PerlinNoise(tx1 * Time.deltaTime, 0.0f);
        float yPos = 1.0f * Mathf.PerlinNoise(0.0f, ty1 * Time.deltaTime);
        float mappedX = Remap(xPos, 0.0f, 1.0f, -bounds.x, bounds.x);
        float mappedY = Remap(yPos, 0.0f, 1.0f, (-bounds.y + 2), bounds.y);

       circle.transform.position =  new Vector3(xPos, yPos);
        Renderer newR = circle.GetComponent<Renderer>();

    }

    public float map(float input, float cMin, float cMax, float nMin, float nMax)
    {
        return nMin + (input - cMin) * ((nMax - nMin) / (cMax - cMin));
    }

    public static float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;

    }
}
