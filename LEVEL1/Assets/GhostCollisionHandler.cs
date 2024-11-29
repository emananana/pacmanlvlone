using UnityEngine;

public class GhostCollisionHandler : MonoBehaviour
{
    private BasicPacManMovement pacManScript;

    void Start()
    {
        pacManScript = GameObject.FindWithTag("player").GetComponent<BasicPacManMovement>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            if (pacManScript.isPoweredUp)
            {
                Debug.Log("Pac-Man ate a ghost!");
                Destroy(transform.parent.gameObject);  // Destroy the parent (ghost)
            }
            else
            {
                Debug.Log("Pac-Man touched a ghost! Lose a life.");
                pacManScript.LoseLife();  // Pac-Man loses a life
            }
        }
    }
}
