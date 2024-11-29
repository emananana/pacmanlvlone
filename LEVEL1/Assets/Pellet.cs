using UnityEngine;

public class Pellet : MonoBehaviour
{
    // When Pac-Man collides with a pellet
    void OnTriggerEnter(Collider other)
    {
        // Check if the collider is Pac-Man
        if (other.CompareTag("player"))
        {
            // Destroy the pellet
            Destroy(gameObject);

            // Call a function to update the pellet count in PacMan script 
        }
    }
}

