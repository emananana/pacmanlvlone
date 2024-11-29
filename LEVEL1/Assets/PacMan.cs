using UnityEngine;
using System.Collections;

public class BasicPacManMovement : MonoBehaviour
{
    private Vector3 up = Vector3.forward;
    private Vector3 down = Vector3.back;
    private Vector3 left = Vector3.left;
    private Vector3 right = Vector3.right;

    public float moveSpeed = 5f;
    private Vector3 currentDirection;
    public int remainingPellets = 20;
    public bool isPoweredUp = false;
    public float powerUpDuration = 10f;

    public int lives = 3;  // Number of lives Pac-Man has
    private Vector3 startPosition;

    void Start()
    {
        currentDirection = up;
        startPosition = transform.position;
    }

    void Update()
    {
        // Handle user input to change direction
        if (Input.GetKeyDown(KeyCode.UpArrow)) currentDirection = up;
        else if (Input.GetKeyDown(KeyCode.DownArrow)) currentDirection = down;
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) currentDirection = left;
        else if (Input.GetKeyDown(KeyCode.RightArrow)) currentDirection = right;

        // Move Pac-Man
        transform.position += currentDirection * moveSpeed * Time.deltaTime;

        // Rotate Pac-Man to face the current direction
        if (currentDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(currentDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("pellet"))
        {
            remainingPellets--;
            Destroy(other.gameObject);
            if (remainingPellets <= 0) Debug.Log("You collected all the pellets! You win!");
        }
        else if (other.CompareTag("power_pellet"))
        {
            ActivatePowerUp();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("ghost"))
        {
            if (isPoweredUp)
            {
                // Pac-Man eats the ghost
                Debug.Log("Pac-Man ate a ghost!");
                Destroy(other.gameObject);
            }
            else
            {
                // Pac-Man loses a life
                LoseLife();
            }
        }
    }

    void ActivatePowerUp()
    {
        if (!isPoweredUp)
        {
            isPoweredUp = true;
            Debug.Log("Power-up activated!");
            StartCoroutine(PowerUpTimer());
        }
    }

    private IEnumerator PowerUpTimer()
    {
        yield return new WaitForSeconds(powerUpDuration);
        isPoweredUp = false;
        Debug.Log("Power-up ended.");
    }

    public void LoseLife()
    {
        Debug.Log("Pac-Man lost a life.");
        lives--;
        if (lives <= 0)
        {
            Debug.Log("Game Over!");
            Time.timeScale = 0f;  // Pauses the game
        }
        else
        {
            transform.position = startPosition;  // Respawn Pac-Man
        }
    }
}


/*

public class BasicPacManMovement : MonoBehaviour
{
    private Vector3 up = Vector3.forward;
    private Vector3 down = Vector3.back;
    private Vector3 left = Vector3.left;
    private Vector3 right = Vector3.right;

    public float moveSpeed = 5f;
    private Vector3 currentDirection;
    public int remainingPellets = 20;
    public bool isPoweredUp = false;
    public float powerUpDuration = 10f;

    public int lives = 3;  // Number of lives Pac-Man has
    private Vector3 startPosition;  // Store the start position for respawn

    void Start()
    {
        currentDirection = up;
        startPosition = transform.position;  // Store the initial position of Pac-Man
    }

    void Update()
    {
        // Handle user input to change direction
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentDirection = up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentDirection = down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentDirection = left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentDirection = right;
        }

        // Move Pac-Man in the current direction
        transform.position += currentDirection * moveSpeed * Time.deltaTime;

        // Rotate Pac-Man to face the current direction (visual feedback)
        if (currentDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(currentDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("pellet"))
        {
            remainingPellets--;
            Destroy(other.gameObject);

            if (remainingPellets <= 0)
            {
                Debug.Log("You collected all the pellets! You win!");
                // You can add a level reset or something else here
            }
        }
        else if (other.CompareTag("power_pellet"))
        {
            ActivatePowerUp();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("ghost"))
        {
            if (BasicPacManMovement.isPoweredUp)
            {
                // If Pac-Man is powered up, destroy the ghost
                Debug.Log("Pac-Man ate a ghost!");  // Log to indicate Pac-Man ate the ghost
                Destroy(other.gameObject);  // Destroy the ghost

                // Optionally, you can add a sound effect or animation here to give feedback to the player
            }
            else
            {
                // Pac-Man loses a life if not powered up
                LoseLife();  // Call the LoseLife method if Pac-Man is caught by a ghost
            }
        }
    }

    // Function to activate power-up
    void ActivatePowerUp()
    {
        if (!isPoweredUp)
        {
            isPoweredUp = true;
            Debug.Log("Power-up activated! PacMan can eat ghosts now!");
            StartCoroutine(PowerUpTimer());
        }
    }

    private IEnumerator PowerUpTimer()
    {
        yield return new WaitForSeconds(powerUpDuration);
        isPoweredUp = false;
        Debug.Log("Power-up ended. PacMan can no longer eat ghosts.");
    }

    // Method to lose a life when caught by a ghost
    void LoseLife()
    {
        Debug.Log("Pac-Man touched a ghost!");  // Debug log to check if this is being triggered
        lives--;  // Decrease the number of lives
        Debug.Log("Pac-Man was caught! Lives remaining: " + lives);

        if (lives <= 0)
        {
            // Game Over
            Debug.Log("Game Over!");
            // Trigger game over logic (e.g., stop the game, show game over UI)
            Time.timeScale = 0f;  // This will pause the game
        }
        else
        {
            // Respawn Pac-Man to start position
            transform.position = startPosition;
            currentDirection = up;  // Reset the direction to up (or whatever initial direction you prefer)
        }
    }
}
*/