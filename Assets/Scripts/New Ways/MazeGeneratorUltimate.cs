using UnityEngine;
using System.Collections.Generic;

public class MazeGeneratorUltimate : MonoBehaviour
{
    public GameObject wallPrefab; // reference to the cube wall prefab
    public int mazeSize = 15; // size of the maze
    public float wallLength = 1.0f; // length of each wall
    public float wallHeight = 2.0f; // height of each wall
    public float pathWidth = 1.0f; // width of the paths
    public float pathHeight = 0.0f; // height of the paths (0 to remove the walls)
    public float spawnDelay = 0.01f; // delay between spawning walls

    private bool[,] visited; // 2D array to keep track of visited cells
    private GameObject[,] walls; // 2D array to store references to wall gameobjects
    private Vector3 mazeOffset; // offset to center the maze
    private Vector3 wallOffset; // offset to center each wall
    private Coroutine spawnCoroutine; // reference to the spawn coroutine

    void Start()
    {
        // initialize private variables
        visited = new bool[mazeSize, mazeSize];
        walls = new GameObject[mazeSize + 1, mazeSize + 1];
        mazeOffset = new Vector3(-(mazeSize - 1) * wallLength / 2, 0, -(mazeSize - 1) * wallLength / 2);
        wallOffset = new Vector3(wallLength / 2, wallHeight / 2, wallLength / 2);

        // start spawn coroutine
        spawnCoroutine = StartCoroutine(SpawnWalls());

        GenerateMaze();
    }

    private IEnumerator<object> SpawnWalls()
    {
        // loop through rows and columns to spawn walls
        for (int i = 0; i <= mazeSize; i++)
        {
            for (int j = 0; j <= mazeSize; j++)
            {
                // instantiate a new wall prefab at the current position
                Vector3 wallPosition = new Vector3(i * wallLength, 0, j * wallLength) + mazeOffset;
                GameObject newWall = Instantiate(wallPrefab, wallPosition + wallOffset, Quaternion.identity);

                // set the parent of the new wall to the MazeGenerator game object
                newWall.transform.parent = transform;

                // store reference to the new wall gameobject
                walls[i, j] = newWall;

                // wait for a delay between spawns
                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }

    private void GenerateMaze()
    {
        // initialize visited cells to false
        for (int i = 0; i < mazeSize; i++)
        {
            for (int j = 0; j < mazeSize; j++)
            {
                visited[i, j] = false;
            }
        }

        // choose a random starting cell
        int startX = Random.Range(0, mazeSize);
        int startY = Random.Range(0, mazeSize);

        // mark starting cell as visited
        visited[startX, startY] = true;

        // create a stack to keep track of visited cells
        Stack<Vector2Int> cellStack = new Stack<Vector2Int>();
        cellStack.Push(new Vector2Int(startX, startY));

        // loop while there are still unvisited cells
        while (cellStack.Count > 0)
        {
            // get current cell from top of stack
            Vector2Int currentCell = cellStack.Peek();

            // find unvisited neighbors of current cell
            List<Vector2Int> unvisitedNeighbors = GetUnvisitedNeighbors(currentCell);
            
            if (unvisitedNeighbors.Count > 0)
            {
                // choose a random unvisited neighbor
                Vector2Int nextCell = unvisitedNeighbors[Random.Range(0, unvisitedNeighbors.Count)];

                // mark neighbor cell as visited
                visited[nextCell.x, nextCell.y] = true;

                // remove wall between current cell and neighbor cell
                RemoveWall(currentCell, nextCell);

                // push neighbor cell onto stack
                cellStack.Push(nextCell);
            }
            else
            {
                // backtrack to previous cell
                cellStack.Pop();
            }
        }
        // remove wall between current cell and neighbor cell
        //RemoveWall(currentCell, nextCell);
    }
    private List<Vector2Int> GetUnvisitedNeighbors(Vector2Int cell)
    {
        List<Vector2Int> unvisitedNeighbors = new List<Vector2Int>();

        // check neighbor to the right
        if (cell.x < mazeSize - 1 && !visited[cell.x + 1, cell.y])
        {
            unvisitedNeighbors.Add(new Vector2Int(cell.x + 1, cell.y));
        }

        // check neighbor to the left
        if (cell.x > 0 && !visited[cell.x - 1, cell.y])
        {
            unvisitedNeighbors.Add(new Vector2Int(cell.x - 1, cell.y));
        }

        // check neighbor above
        if (cell.y < mazeSize - 1 && !visited[cell.x, cell.y + 1])
        {
        unvisitedNeighbors.Add(new Vector2Int(cell.x, cell.y + 1));
        }

        // check neighbor below
        if (cell.y > 0 && !visited[cell.x, cell.y - 1])
        {
            unvisitedNeighbors.Add(new Vector2Int(cell.x, cell.y - 1));
        }

        return unvisitedNeighbors;
    }

    private void RemoveWall(Vector2Int currentCell, Vector2Int nextCell)
    {
        // find the position of the wall to remove
        int wallX = currentCell.x + nextCell.x + 1;
        int wallY = currentCell.y + nextCell.y + 1;

        // remove the wall
        GameObject wallToRemove = walls[wallX, wallY];
        Destroy(wallToRemove);

       // set the reference to null
        walls[wallX, wallY] = null;
    }
}