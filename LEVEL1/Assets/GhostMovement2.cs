using System.Collections;
using UnityEngine;

public class GhostMovement2 : MonoBehaviour
{
    public float moveSpeed = 3f;               // Movement speed of the ghost
    public float directionChangeInterval = 2f; // Time between changing directions
    public LayerMask wallLayer;                // Layer mask for walls
    public float rayDistance = 1f;             // Distance of raycast to detect walls

    private Vector3 moveDirection;

    void Start()
    {
        // Start changing direction at intervals
        StartCoroutine(ChangeDirectionRoutine());
    }

    void Update()
    {
        MoveGhost();
    }

    void MoveGhost()
    {
        // Move the ghost in the chosen direction
        Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;

        // Check for obstacles in the intended direction
        if (!Physics.Raycast(transform.position, moveDirection, rayDistance, wallLayer))
        {
            transform.position = newPosition; // Move if no wall ahead
        }
        else
        {
            // Change direction if a wall is detected
            ChangeDirection();
        }
    }

    IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            ChangeDirection();
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    void ChangeDirection()
    {
        // Choose a random new direction (left, right, up, or down)
        Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
        moveDirection = directions[Random.Range(0, directions.Length)];

        // Ensure the ghost doesn't get stuck moving into a wall by checking if the direction is clear
        if (Physics.Raycast(transform.position, moveDirection, rayDistance, wallLayer))
        {
            ChangeDirection(); // Recursively call to find a clear direction
        }
    }

    private void OnDrawGizmos()
    {
        // Draw a ray to visualize direction checks
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, moveDirection * rayDistance);
    }
}
