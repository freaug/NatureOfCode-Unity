using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class RandomNumberDistrobution : MonoBehaviour
{

    //generate a random number between 0 - 20 
    //when a number is generated increment a value at that index
    Vector2 bounds;
    int[] randomCounts = new int[20];
    GameObject[] columns = new GameObject[20];
    void Start()
    {
        FindCenter();
        
        float w = bounds.x * 2 / randomCounts.Length;


        for (int i = 0; i < randomCounts.Length; i++)
        {
            randomCounts[i] = 0;
            columns[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            columns[i].transform.localScale = new Vector3(0.45f, 0, 0.001f);
            columns[i].transform.position = new Vector3(i * w - bounds.x + 0.25f, randomCounts[i] - (bounds.y / 2) - 1 );

            float red = UnityEngine.Random.Range(0.0f, 1.0f);
            float green = UnityEngine.Random.Range(0.0f, 1.0f);
            float blue = UnityEngine.Random.Range(0.0f, 1.0f);

            Color randomColor = new Color(red, green, blue);

            var rendered = columns[i].GetComponent<Renderer>();
            rendered.material.color = randomColor;
        }   
        
    }

    // so position would increment by half of the scale value
    // set the y position to new value and y scale to new value
    void FixedUpdate()
    {
        int index = Random.Range(0, randomCounts.Length);
        randomCounts[index]++;

        float w = bounds.x * 2 / randomCounts.Length;
        float scalar = 0.001f;

        for (int i = 0; i < randomCounts.Length; i++)
        {
            columns[i].transform.localScale += new Vector3(0f, randomCounts[i] * scalar);
            columns[i].transform.position += new Vector3(0f, (randomCounts[i] * 0.5f)) * scalar;
        }

    }

    public void FindCenter()
    {
        Camera.main.orthographic = true;

        bounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }
}


