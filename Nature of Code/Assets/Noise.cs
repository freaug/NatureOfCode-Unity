using Unity.Collections;
using UnityEngine;

public class Noise : MonoBehaviour
{

    float tx1, tx2;
    float ty1, ty2;

    float step;

    Vector2 bounds;

    public GameObject circle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tx1 = 1.0f;
        ty1 = 1000.0f;
        tx2 = 100.0f;
        ty2 = 2000.0f;
        step = 0.01f;

        FindCenter();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tx1 += step;
        ty1 += step;
        tx2 += step;
        ty2 += step;
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
        float xPos = 1.0f * Mathf.PerlinNoise(tx1, ty1);
        float yPos = 1.0f * Mathf.PerlinNoise(tx1, tx2);
        float mappedX = map(xPos, 0.0f, 1.0f, -bounds.x, bounds.x);
        float mappedY = map(yPos, 0.0f, 1.0f, (-bounds.y + 2), bounds.y);

        GameObject newC = Instantiate(circle, new Vector3(mappedX, mappedY), Quaternion.identity);
        Renderer newR = newC.GetComponent<Renderer>();
        Material newM = new Material(newR.sharedMaterial);
        newR.material = newM;
        newR.material.color = Color.HSVToRGB(0.9f, 0.9f, 0.5f);
        newC.transform.localScale = new Vector3(1, 1, 0.0001f);

    }

    public float map(float input, float cMin, float cMax, float nMin, float nMax)
    {
        //output =  nMim + (input  - cMin) * (( nMax - nMin) / (cMax - cMin))
        return nMin + (input - cMin) * ((nMax - nMin) / (cMax - cMin));
    }
}
