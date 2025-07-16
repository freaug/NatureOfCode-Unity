using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class example1_2 : MonoBehaviour
{
    //prefab circle
    public GameObject circle;

    //so we can make an instance of it
    private GameObject bounce;

    //so we can do physics stuff

    //circle pos
    private Vector2 position;
    //circle velocity
    private Vector2 velocity;

    //bounds of world
    private Vector2 bounds;

    private float maxWidth;
    private float minWidth;
    private float maxHeight;
    private float minHeight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FindCenter();
        maxWidth = bounds.x;
        minWidth = -bounds.x;
        maxHeight = bounds.y;
        minHeight = -bounds.y;

        position = new Vector2(0, 0);
        velocity = new Vector2(0.025f, 0.035f);

        bounce = Instantiate(circle, position, Quaternion.identity);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool flipX = position.x > maxWidth || position.x < minWidth;
        bool flipY = position.y > maxHeight || position.y < minHeight;

        if (flipX)
        {
            velocity.x *= -1;
        }


        if (flipY)
        {
            velocity.y *= -1;
        }

        position = position + velocity;

        bounce.transform.position = position;

    }

    public void FindCenter()
    {
        Camera.main.orthographic = true;

        Camera.main.transform.position = new Vector3(0, 0, -10);

        bounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    Vector2 AddVectors(Vector2 pos, Vector2 vel)
    {
        float newX = pos.x + vel.x;
        float newY = pos.y + vel.y;
        return new Vector2(newX, newY);
    }
}
