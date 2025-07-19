using Unity.Mathematics;
using UnityEngine;

/**
Using forces, simulate a helium-filled balloon floating upward and bouncing off the top of a window. 
Can you add a wind force that changes over time, perhaps according to Perlin noise?
**/

public class exercise2_1 : MonoBehaviour
{

    public GameObject circlePrefab;
    public LineRenderer linePrefab;
    private GameObject balloon;
    float r;
    private LineRenderer balloonString;
    Vector2 position, velocity, acceleration, helium;
    Vector2 wind;
    float xScale, xPos, xWidth, xTime;
    float timeSinceReset;
    Vector2 bounds;
    void Start()
    {
        //get the world bounds
        bounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        //set default values for balloon
        position = Vector2.zero;
        velocity = Vector2.zero;
        acceleration = Vector2.zero;
        helium = new Vector2(0, 200f);
        wind = Vector2.zero;

        //create the string instance
        balloonString = Instantiate(linePrefab, position, Quaternion.identity);

        //how many points will the string have
        balloonString.positionCount = 2;

        //draw the string coming from the ballon
        balloonString.SetPosition(0, new Vector2(0, -0.5f));
        balloonString.SetPosition(1, new Vector2(0, -2.25f));

        //create the ballon instance
        balloon = Instantiate(circlePrefab, position, Quaternion.identity);
        //get the renderer of the ballon
        Renderer balloonRenderer = balloon.GetComponent<Renderer>();
        //set the ballon color to red
        balloonRenderer.material.color = Color.red;

        //get the radius of the ballon 
        r = balloon.transform.localScale.y / 2;

        //set the parent of the ballonString to the Balloon 
        balloonString.transform.parent = balloon.transform;

        xScale = 1.0f;
        xWidth = 0.7f;
        xTime = 0f;

    }

    void FixedUpdate()
    {
        timeSinceReset = Time.time - xTime;
        Move();
    }

    void ApplyForce(Vector2 force)
    {
        acceleration = acceleration + force * Time.deltaTime;
    }

    bool ShouldBounce()
    {
        return balloon.transform.position.y + r > bounds.y;
    }

    void Move()
    {
        if (ShouldBounce())
        {
            position.y = bounds.y - r;

            velocity *= new Vector2(0, -0.75f);
        }

        ApplyForce(helium);

        xPos = xWidth * Mathf.PerlinNoise(Time.time * xScale, 0.0f) * timeSinceReset;
        xPos = math.remap(0, 1, -5, 5, xPos);
        wind = new Vector2(xPos, 0.0f);
 
        ApplyForce(wind);
  
        velocity = velocity + acceleration * Time.deltaTime;
        velocity = Vector2.ClampMagnitude(velocity, 10f);
        position = position + velocity * Time.deltaTime;
        acceleration = acceleration * Vector2.zero;

        balloon.transform.position = position;
    }

}