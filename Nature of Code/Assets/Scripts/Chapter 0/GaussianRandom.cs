using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class GaussianRandom : MonoBehaviour
{
    public GameObject circle;

    private Vector2 bounds;

    void Start()
    {
        FindBounds();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float xPos = generateNormalRandom(bounds.x, bounds.x * 0.6f);
        float offset = bounds.x * 0.5f;
        Vector3 pos = new Vector3(xPos - offset , 0);
        Instantiate(circle, pos, Quaternion.identity);
    }


    public static float generateNormalRandom(float mu, float sigma)
    {
        float rand1 = Random.Range(0.0f, 1.0f);
        float rand2 = Random.Range(0.0f, 1.0f);

        float n = Mathf.Sqrt(-2.0f * Mathf.Log(rand1)) * Mathf.Cos((2.0f * Mathf.PI) * rand2);

        return (mu + sigma * n);
    }

    public void FindBounds()
    {
        Camera.main.orthographic = true;

        bounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }
}

