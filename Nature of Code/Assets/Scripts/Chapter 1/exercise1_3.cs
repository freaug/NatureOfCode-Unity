using UnityEngine;

public class exercise1_3 : MonoBehaviour
{
    private GameObject sphere;
  

    Vector3 position;
    Vector3 velocity;

    Vector3 bounds;
    Vector2 borderX;
    Vector2 borderY;
    Vector2 borderZ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FindCenter();

        borderX.x = -bounds.x;
        borderX.y = bounds.x;

        borderY.x = -bounds.x;
        borderY.y = bounds.y;

        borderZ.x = -bounds.z;
        borderZ.y = bounds.z;

        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        position = new Vector3(0, 0, 0);
        velocity = new Vector3(0.15f, 0.25f, 0.5f);

        sphere.transform.position = position;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool flipX = position.x < borderX.x || position.x > borderX.y;
        bool flipY = position.y < borderY.x || position.y > borderY.y;
        bool flipZ = position.z < borderZ.x || position.z > borderZ.y;

        if (flipX)
        {
            velocity.x *= -1;
        }

        if (flipY)
        {
            velocity.y *= -1;
        }
        if (flipZ)
        {
            velocity.z *= -1;
        }

        position = position + velocity;

        sphere.transform.position = position;

    }
    
        public void FindCenter()
    {
        //Camera.main.orthographic = true;

        Camera.main.transform.position = new Vector3(0, 0, -20);

        bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 15));
    }
}
