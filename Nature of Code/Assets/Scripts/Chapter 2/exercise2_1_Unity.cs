using UnityEngine;
public class exercise2_1_Unity : MonoBehaviour
{
    private Rigidbody balloonRB;
    private Vector2 downVel;
    private Vector2 helium;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        balloonRB = GetComponent<Rigidbody>();

        downVel = new Vector2(0.0f, -2f);

        helium = new Vector2(0.0f, 1f);

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //add upwards acceleration to balloon
        balloonRB.AddForce(helium, ForceMode.Acceleration);
    }

    //on collision with the ceiling
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ceiling"))
        {
            //bounce down while gradually slowing down
            balloonRB.AddForce(downVel, ForceMode.VelocityChange);
            downVel *= new Vector2(0, 0.75f);
        }
    }
}
