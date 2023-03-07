using UnityEngine;

public class MazeGeneratorStartImproved : MonoBehaviour
{
    public GameObject wallPrefab; // reference to the cube wall prefab
    public int mazeSize = 15; // size of the maze
    public float perlinThreshold = 0.5f; // threshold value for Perlin noise
    public float startPointThreshold = 0.1f; // threshold value for start point
    public float endPointThreshold = 0.9f; // threshold value for end point

    public GameObject startPoint; // reference to the start point object
    public GameObject endPoint; // reference to the end point object

    void Start()
    {
        // instantiate start and end points outside of the loop
        startPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        endPoint = GameObject.CreatePrimitive(PrimitiveType.Cylinder);

        startPoint.SetActive(false);
        endPoint.SetActive(false);

        startPoint.transform.parent = transform;
        endPoint.transform.parent = transform;

        // loop through rows and columns to generate walls and set start/end points
        for (int i = 0; i < mazeSize; i++)
        {
            for (int j = 0; j < mazeSize; j++)
            {
                // generate Perlin noise value for current cell
                float perlinValue = Mathf.PerlinNoise(i / (float)mazeSize, j / (float)mazeSize);

                // instantiate a new wall prefab at the current position if Perlin noise value is below threshold
                if (perlinValue < perlinThreshold)
                {
                    GameObject newWall = Instantiate(wallPrefab, new Vector3(i, 0, j), Quaternion.identity);
                    newWall.transform.parent = transform;
                }

                // set start point if Perlin noise value is below start point threshold
                if (perlinValue < startPointThreshold)
                {
                    startPoint.SetActive(true);
                    startPoint.transform.position = new Vector3(i, 0.5f, j);
                }

                // set end point if Perlin noise value is above end point threshold
                if (perlinValue > endPointThreshold)
                {
                    endPoint.SetActive(true);
                    endPoint.transform.position = new Vector3(i, 0.5f, j);
                }
            }
        }
    }
}
/*
In this version of the code, we instantiate the start and end points outside the loop and save references to them in the startPoint and endPoint variables. We also set their parent explicitly to the transform of the MazeGenerator game object.

We also added SetActive(false) to the start and end points at the beginning of the Start() method to hide them until they are set. When the threshold for the start or end point is met, we set the position of the appropriate object and activate it with SetActive(true).
*/ 