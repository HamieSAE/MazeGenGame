using UnityEngine;
/*
public class PlayerController : MonoBehaviour
{
    public GameObject startPoint; // reference to the start point object
    public float moveSpeed = 5f; // movement speed of the player ball

    void Start()
    {
        // set player position to start point position
        transform.position = startPoint.transform.position;
    }

    void Update()
    {
        // get horizontal and vertical input values
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // calculate movement vector based on input values and move speed
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;

        // apply movement to player ball
        transform.Translate(movement);
    }
}
*/
/* Here's what the script does:

public GameObject startPoint; - this line creates a public variable to store a reference to the start point object.

void Start() - this method is called once when the script is first initialized. We use it to set the player position to the start point position.

float horizontalInput = Input.GetAxis("Horizontal"); and float verticalInput = Input.GetAxis("Vertical"); - these lines get the horizontal and vertical input values from the player's keyboard.

Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime; - this line calculates a movement vector based on the input values and the player's move speed.

transform.Translate(movement); - this line applies the movement vector to the player's position, causing it to move.

To use this script, attach it to the player ball object in your scene. 
Assign the startPoint object to the startPoint variable in the inspector. 
When you run the game, the player ball should spawn on the start point and can be moved using the W, A, S, and D keys. 
The moveSpeed variable can be adjusted to control how fast the player ball moves.
*/

public class PlayerController : MonoBehaviour
{
    public MazeGenerator mazeGenerator; // reference to the MazeGenerator script
    public float moveSpeed = 5f; // movement speed of the player ball

    void Start()
    {
        // set player position to start point position
        transform.position = mazeGenerator.startPoint.transform.position;
    }

    void Update()
    {
        // get horizontal and vertical input values
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // calculate movement vector based on input values and move speed
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;

        // apply movement to player ball
        transform.Translate(movement);
    }
}
