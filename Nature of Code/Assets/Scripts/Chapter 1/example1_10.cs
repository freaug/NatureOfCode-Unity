using UnityEngine;

public class example1_10 : MonoBehaviour
{
    public GameObject circlePrefab;
    private GameObject vehicle;
    private GameObject mouse;

    private Vector2 position, velocity, acceleration;

    private Vector2 mousePosition;
    private Vector2 bounds;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        vehicle = Instantiate(circlePrefab, position, Quaternion.identity);

        //make a small red circle so we can see where the mouse is
        mouse = Instantiate(circlePrefab, mousePosition, Quaternion.identity);
        mouse.transform.localScale = new Vector2(0.2f, 0.2f);
        Renderer mouseRenderer = mouse.GetComponent<Renderer>();
        mouseRenderer.material.color = Color.red;

        position = Vector2.zero;
        velocity = Vector2.zero;
        acceleration = Vector2.zero;

    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.transform.position = mousePosition;

        Move();
    }

    void Move()
    {
        //Vector2 direction = SubtractVector(mousePosition, position);

        Vector2 direction = mousePosition - position;

        direction.Normalize();

        direction = direction * 20f;

        acceleration = direction;

        velocity = velocity + acceleration * Time.deltaTime;
        velocity = Vector2.ClampMagnitude(velocity, 20.0f);
        position = position + velocity * Time.deltaTime;

        vehicle.transform.position = position;
    }

    Vector2 SubtractVector(Vector2 v1, Vector2 v2)
    {
        float newX = v1.x - v2.x;
        float newY = v1.y - v2.y;
        return new Vector2(newX, newY);
    }
}
