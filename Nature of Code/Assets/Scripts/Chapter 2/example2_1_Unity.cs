using UnityEngine;

/**
Using unitys built in physics engine.  
In order to get the ball to keep bouncing at the same height 
you need to create and assign a physics material and assign it to the ball and to the enclosure walls
**/
public class example2_1_Unity : MonoBehaviour
{
    public Rigidbody ballRB;

    Vector2 gravity = new Vector2(0.0f, -9.8f);
    Vector2 wind = new Vector2(0.1f, 0.0f);
    private float scalar;

    void Start()
    {

        ballRB = GetComponent<Rigidbody>();

        ballRB.mass = (4f / 3f) * 3.14f * (1 * 1 * 1);

        scalar = 15;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(ballRB.linearVelocity);

        if (Input.GetMouseButtonDown(0))
        {
            ballRB.AddForce(Vector2.left * scalar, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Left"))
        {
            ballRB.AddForce(-wind, ForceMode.Impulse);
        }
        if (collision.gameObject.CompareTag("Right"))
        {
            ballRB.AddForce(wind, ForceMode.Impulse);
        }
    }
}

