using UnityEngine;
using UnityEngine.UI;

public class example1_1 : MonoBehaviour
{

    public GameObject circle;
    private Vector2 bounds;
    private GameObject bounce;

    float maxWidth;
    float minWidth;
    float maxHeight;
    float minHeight;

    float xPos;
    float yPos;

    float r;
    public float xSpeed;
    public float ySpeed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FindCenter();
        maxWidth = bounds.x;
        minWidth = -bounds.x;
        maxHeight = bounds.y;
        minHeight = -bounds.y;


        xPos = 0;
        yPos = 0;

        bounce = Instantiate(circle, new Vector3(xPos, yPos, 0), Quaternion.identity);
        r = circle.transform.localScale.x * 0.5f;

    }

    // Update is called once per frame
    void Update()
    {

        bool flipX = xPos < minWidth + r || xPos > maxWidth - r;
        bool flipY = yPos < minHeight + r || yPos > maxHeight - r;

        if (flipX)
        {
            xSpeed *= -1;
        }

        if (flipY)
        {
            ySpeed *= -1;
        }

        xPos += xSpeed;
        yPos += ySpeed;

        bounce.transform.position = new Vector2(xPos, yPos);
    }

    public void FindCenter()
    {
        Camera.main.orthographic = true;

        Camera.main.transform.position = new Vector3(0, 0, -10);

        bounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }
}


