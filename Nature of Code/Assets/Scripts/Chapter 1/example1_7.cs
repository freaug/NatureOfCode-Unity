using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class example1_7 : MonoBehaviour
{
    [SerializeField] GameObject mover;
    void Start()
    {
        //there is no constructor in Unity instead you have to do the constructor work
        //in the start of awake function and set the appropriate instance values

        //calcing the bounds of the windos
        Vector2 bounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        //generating a random x,y position
        float randomW = Random.Range(-bounds.x, bounds.x);
        float randomH = Random.Range(bounds.y, -bounds.y);

        //generating a random velocity
        float randomVX = Random.Range(-0.2f, 0.2f);
        float randomVY = Random.Range(-0.3f, 0.3f);


        //create an instance of our mover
        mover = Instantiate(mover);

        //get the mover prefab and assign the values we calculated 
        mover.GetComponent<Mover>().bounds = bounds;
        mover.GetComponent<Mover>().position = new Vector2(randomW, randomH);
        mover.GetComponent<Mover>().velocity = new Vector2(randomVX, randomVY);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //call the move and check edges function
        mover.GetComponent<Mover>().Move();
        mover.GetComponent<Mover>().CheckEdges();

        //set the transform position of our mover to the position value we calculated
        mover.transform.position = mover.GetComponent<Mover>().position;

    }
}


