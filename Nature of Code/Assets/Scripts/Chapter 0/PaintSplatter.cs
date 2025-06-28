using UnityEngine;

public class PaintSplatter : MonoBehaviour
{
    public GameObject circle;
    private Vector2 bounds;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FindBounds();
        Splatter(0, 0, 0.25f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static float generateNormalRandom(float mu, float sigma)
    {
        float rand1 = Random.Range(0.0f, 1.0f);
        float rand2 = Random.Range(0.0f, 1.0f);

        float n = Mathf.Sqrt(-2.0f * Mathf.Log(rand1)) * Mathf.Cos((2.0f * Mathf.PI) * rand2);

        return (mu + sigma * n);
    }


    public void FindBounds()
    {
        Camera.main.orthographic = true;

        bounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    public void Splatter(float x, float y, float r)
    {

        GameObject instantiatedCircle = Instantiate(circle, new Vector3(x, y), Quaternion.identity);
        Renderer instantiatedRenderer = instantiatedCircle.GetComponent<Renderer>();
        Material instanceMaterial = new Material(instantiatedRenderer.sharedMaterial);
        instantiatedRenderer.material = instanceMaterial;
        instantiatedRenderer.material.color = Color.HSVToRGB(0.5f, 0.9f, 0.5f);
        instantiatedCircle.transform.localScale = new Vector3(r, r, 0.0001f);

        for (int i = 0; i < 15; i++)
        {
            float xPos = generateNormalRandom(x, 2.0f);
            float yPos = generateNormalRandom(y, 2.0f);
            float size = generateNormalRandom(r, 1.0f);

            GameObject newC = Instantiate(circle, new Vector3(xPos, yPos), Quaternion.identity);
            Renderer newR = newC.GetComponent<Renderer>();
            Material newM = new Material(newR.sharedMaterial);
            newR.material = newM;
            newR.material.color =  Color.HSVToRGB(generateNormalRandom(0.5f, 0.01f), 0.9f, 0.5f);
            newC.transform.localScale = new Vector3(size, size, 0.0001f);


        }
    }
}
