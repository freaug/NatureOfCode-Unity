using UnityEngine;

//Mover Class used with Example1_7 a way to do this but not the best way probably
public class Mover : MonoBehaviour
{
    public Vector2 position;
    public Vector2 velocity;
    public Vector2 bounds;

    //Move Function
    public void Move()
    {
        position = position + velocity * Time.deltaTime;
    }

    //Check and wrap edges function
    public void CheckEdges()
    {
        if (position.x > bounds.x)
        {
            position.x = -bounds.x;
        }
        else if (position.x < -bounds.x)
        {
            position.x = bounds.x;
        }

        if (position.y > bounds.y)
        {
            position.y = -bounds.y;
        }
        else if (position.y < -bounds.y)
        {
            position.y = bounds.y;
        }
    }
}
