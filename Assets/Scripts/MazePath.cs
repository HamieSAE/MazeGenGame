using UnityEngine;

public class MazePath : MonoBehaviour
{
    public GameObject wallPrefab; // reference to the cube wall prefab
    public int mazeSize = 15; // size of the maze
    public float perlinThreshold = 0.5f; // threshold value for Perlin noise

    void Start()
    {
        // loop through rows and columns to generate walls
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
            }
        }
    }
}
/*Here's what's changed in the code:
public float perlinThreshold = 0.5f; - this line creates a new public variable to store the threshold value for the Perlin noise map. 
We set it to 0.5 by default, but we can change it in the Unity editor as well.

float perlinValue = Mathf.PerlinNoise(i / (float)mazeSize, j / (float)mazeSize); - this line generates a Perlin noise value for the current cell using the Mathf.PerlinNoise() function. 
We divide i and j by mazeSize to map them to the range [0, 1].

if (perlinValue < perlinThreshold) - this line checks if the Perlin noise value for the current cell is below the threshold. 
If it is, we instantiate a new wall prefab at that position.

To use this updated script, you can replace the previous MazeGenerator script with this new version. 
When you run the game, the script will generate a 15x15 maze with walls and passages based on the Perlin noise map. 
You can adjust the perlinThreshold value to control how many walls are generated.
*/