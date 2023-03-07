using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public GameObject wallPrefab; // reference to the cube wall prefab
    public int mazeSize = 15; // size of the maze
    public float perlinThreshold = 0.5f; // threshold value for Perlin noise
    public float startPointThreshold = 0.1f; // threshold value for start point
    public float endPointThreshold = 0.9f; // threshold value for end point

    public GameObject startPoint; // reference to the start point object

    void Start()
    {
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
                    startPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    startPoint.transform.position = new Vector3(i, 0.5f, j);
                    startPoint.transform.parent = transform;
                }

                // set end point if Perlin noise value is above end point threshold
                if (perlinValue > endPointThreshold)
                {
                    GameObject endPoint = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                    endPoint.transform.position = new Vector3(i, 0.5f, j);
                    endPoint.transform.parent = transform;
                }
            }
        }
    }
}
