using UnityEngine;

public class StartEnd : MonoBehaviour
{
    public GameObject wallPrefab; // reference to the cube wall prefab
    public int mazeSize = 15; // size of the maze
    public float perlinThreshold = 0.5f; // threshold value for Perlin noise
    public float startPointThreshold = 0.1f; // threshold value for start point
    public float endPointThreshold = 0.9f; // threshold value for end point

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
                    GameObject startPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
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
/*Here's what's changed in the code:

public float startPointThreshold = 0.1f; and public float endPointThreshold = 0.9f; - these lines create new public variables to store the threshold values for the start and end points. 
We set them to 0.1 and 0.9 by default, respectively.

if (perlinValue < startPointThreshold) - this line checks if the Perlin noise value for the current cell is below the start point threshold. 
If it is, we instantiate a new sphere primitive at that position to represent the start point.

if (perlinValue > endPointThreshold) - this line checks if the Perlin noise value for the current cell is above the end point threshold. 
If it is, we instantiate a new cylinder primitive at that position to represent the end point.

To use this updated script, you can replace the previous MazeGenerator script with this new version. 
When you run the game, the script will generate a 15x15 maze with walls, a start point, and an end point based on the Perlin noise map. 
You can adjust the startPointThreshold and endPointThreshold values to control where the start and end points are placed.
*/