using UnityEngine;

public class IntroductionFig5 : MonoBehaviour
{
    // Give the script an IntroMover
    private IntroMoverI5 mover;
    

    // Start is called before the first frame update
    void Start()
    {
        // Create the mover instance
        mover = new IntroMoverI5();
    }

    // Update is called once per frame
    void Update()
    {
        mover.timeSinceReset = Time.time - mover.resetTime;
        Debug.Log("Time Since Reset " + mover.timeSinceReset);
        // Have the mover step and check edges
        mover.Step();
        mover.CheckEdges();
    }
}

public class IntroMoverI5
{
    // The location of the mover
    Vector2 location;

    // The window limits
    private Vector2 maximumPos;

    // Range over which height and width varies.
    float heightScale = .7f;
    float widthScale = 1.0f;

    // Distance covered per second along X and Y axis of Perlin plane.
    float xScale = 1.0f;
    float yScale = .5f;

    // Gives the class a GameObject to draw on the screen
    public GameObject moverGO = GameObject.CreatePrimitive(PrimitiveType.Sphere);

    public float timeSinceReset;
    public float resetTime;

    public IntroMoverI5()
    {
        FindWindowLimits();
        location = Vector2.zero;
        // Create a new material for WebGL
        Renderer r = moverGO.GetComponent<Renderer>();
        r.material = new Material(Shader.Find("Diffuse"));
    }

    public void Step()
    {
        float width = widthScale * Mathf.PerlinNoise(Time.time * xScale, 0.0f) * timeSinceReset;
        Debug.Log("X Value " + Mathf.PerlinNoise(Time.time * xScale, 0.0f));
        Debug.Log("X Value times Width Scale " + widthScale * Mathf.PerlinNoise(Time.time * xScale, 0.0f));
        Debug.Log("Width " + width);
        float height = heightScale * Mathf.PerlinNoise(0.0f, Time.time * yScale) * timeSinceReset;
        Debug.Log("Y Value " + Mathf.PerlinNoise(0.0f, Time.time * yScale));
        Debug.Log("Y Value times Height Scale " + heightScale * Mathf.PerlinNoise(0.0f, Time.time * yScale));
        Debug.Log("Height " + height);
        Vector3 pos = moverGO.transform.position;
        pos.y = height;
        pos.x = width;
        moverGO.transform.position = pos;
    }

    public void CheckEdges()
    {
        location = moverGO.transform.position;
            if (location.x > maximumPos.x || location.x < -maximumPos.x)
            {
                Reset();
            }
        
            if (location.y > maximumPos.y || location.y < -maximumPos.y)
            {
                Reset();
            }     
        moverGO.transform.position = location;
    }

    void Reset() 
    {
        location = Vector2.zero;
        resetTime = Time.time;
        heightScale = Random.Range(-1f, 1f);
        widthScale = Random.Range(-1f, 1f);
    }

    private void FindWindowLimits()
    {
        // We want to start by setting the camera's projection to Orthographic mode
        Camera.main.orthographic = true;
        // Next we grab the maximum position for the screen
        maximumPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }
}