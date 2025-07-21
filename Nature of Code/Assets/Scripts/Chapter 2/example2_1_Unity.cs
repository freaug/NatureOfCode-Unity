using UnityEngine;

/**
Using unitys built in physics engine.  
In order to get the ball to keep bouncing at the same height 
you need to create and assign a physics material and assign it to the ball and to the enclosure walls
**/
public class example2_1_Unity : MonoBehaviour
{
    //rigid body of out ball
    private Rigidbody ballRB;
    //vector we add to the  if mouse is pressed
    Vector2 wind = new Vector2(0.1f, 0.0f);
    private float scalar;

    void Start()
    {
        //Get the rigid body
        ballRB = GetComponent<Rigidbody>();

        //set the mass of the ball
        ballRB.mass = (4f / 3f) * 3.14f * (1 * 1 * 1);
    }

    // Update is called once per frame
    void Update()
    {

        //if mouse pressed then we apply the wind force
        if (Input.GetMouseButton(0))
        {
            ballRB.AddForce(wind, ForceMode.Impulse);
        }
    }
}

