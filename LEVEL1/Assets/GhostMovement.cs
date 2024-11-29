using UnityEngine;
using UnityEngine.AI;

public class GhostMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private BasicPacManMovement pacManScript;
    public Transform pacMan;
    public float fleeDistance = 10f;
    public float detectionRadius = 0.001f;  // Adjust to match ghost size
    private bool isFleeing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        pacMan = GameObject.FindWithTag("player").transform;
        pacManScript = pacMan.GetComponent<BasicPacManMovement>();

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;
    }

    void Update()
    {
        if (pacManScript.isPoweredUp && !isFleeing)
        {
            isFleeing = true;
            FleeFromPacMan();
        }
        else if (!pacManScript.isPoweredUp && isFleeing)
        {
            isFleeing = false;
            ChasePacMan();
        }
        else if (!pacManScript.isPoweredUp)
        {
            ChasePacMan();
        }

        DetectPacMan();
    }

    void FleeFromPacMan()
    {
        Vector3 directionToPacMan = transform.position - pacMan.position;
        Vector3 fleePosition = transform.position + directionToPacMan.normalized * fleeDistance;
        agent.SetDestination(fleePosition);
    }

    void ChasePacMan()
    {
        agent.SetDestination(pacMan.position);
    }

    void DetectPacMan()
    {
        float distanceToPacMan = Vector3.Distance(transform.position, pacMan.position);

        if (distanceToPacMan <= detectionRadius)
        {
            if (pacManScript.isPoweredUp)
            {
                Debug.Log("Pac-Man ate a ghost!");
                Destroy(gameObject);  // Remove the ghost
            }
            else
            {
                Debug.Log("Pac-Man touched a ghost! Lose a life.");
                pacManScript.LoseLife();  // Pac-Man loses a life
            }
        }
    }

    /*
    void DetectPacMan()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("player"))
            {
                if (pacManScript.isPoweredUp)
                {
                    Debug.Log("Pac-Man ate a ghost!");
                    Destroy(gameObject);  // Remove the ghost
                }
                else
                {
                    Debug.Log("Pac-Man touched a ghost! Lose a life.");
                    pacManScript.LoseLife();  // Pac-Man loses a life
                }
            }
        }
    }
    */
}




/*
public class GhostMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private BasicPacManMovement pacManScript;
    public Transform pacMan;
    public float fleeDistance = 10f;
    private bool isFleeing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        pacMan = GameObject.FindWithTag("player").transform;
        pacManScript = pacMan.GetComponent<BasicPacManMovement>();

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;
    }

    void Update()
    {
        if (pacManScript.isPoweredUp && !isFleeing)
        {
            isFleeing = true;
            FleeFromPacMan();
        }
        else if (!pacManScript.isPoweredUp && isFleeing)
        {
            isFleeing = false;
            ChasePacMan();
        }
        else if (!pacManScript.isPoweredUp)
        {
            ChasePacMan();
        }
    }

    void FleeFromPacMan()
    {
        Vector3 directionToPacMan = transform.position - pacMan.position;
        Vector3 fleePosition = transform.position + directionToPacMan.normalized * fleeDistance;
        agent.SetDestination(fleePosition);
    }

    void ChasePacMan()
    {
        agent.SetDestination(pacMan.position);
    }
}



/*

public class GhostMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private BasicPacManMovement pacManScript;
    public Transform pacMan;
    public float fleeDistance = 10f;

    private bool isFleeing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        pacMan = GameObject.FindWithTag("player").transform;
        pacManScript = pacMan.GetComponent<BasicPacManMovement>();

        // Ensure Rigidbody is Kinematic to avoid physics interference with the NavMeshAgent
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;  // Disable physics interaction
        }
    }

    void Update()
    {
        // Handle movement logic based on Pac-Man's power-up status
        if (pacManScript.isPoweredUp && !isFleeing)
        {
            isFleeing = true;
            FleeFromPacMan();
        }
        else if (!pacManScript.isPoweredUp && isFleeing)
        {
            isFleeing = false;
            ChasePacMan();
        }
        else if (!pacManScript.isPoweredUp)
        {
            ChasePacMan();
        }
    }

    void FleeFromPacMan()
    {
        // Flee logic: Move the ghost away from Pac-Man
        Vector3 directionToPacMan = transform.position - pacMan.position;
        Vector3 fleePosition = transform.position + directionToPacMan.normalized * fleeDistance;
        agent.SetDestination(fleePosition);
    }

    void ChasePacMan()
    {
        // Chase logic: Move the ghost towards Pac-Man
        agent.SetDestination(pacMan.position);
    }

    // Handle interaction with Pac-Man (e.g., when Pac-Man eats the ghost)
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))  // When Pac-Man collides with a ghost
        {
            if (pacManScript.isPoweredUp)
            {
                Debug.Log("Pac-Man ate a ghost!");
                Destroy(gameObject);  // Destroy the ghost
            }
            else
            {
                Debug.Log("Pac-Man touched a ghost! Lose a life.");
                pacManScript.LoseLife();  // Call the method to lose a life
            }
        }
    }
}

/*


public class GhostMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private BasicPacManMovement pacManScript;
    public Transform pacMan;
    public float fleeDistance = 10f;

    private bool isFleeing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        pacMan = GameObject.FindWithTag("player").transform;
        pacManScript = pacMan.GetComponent<BasicPacManMovement>();
    }

    void Update()
    {
        if (pacManScript.isPoweredUp && !isFleeing)
        {
            isFleeing = true;
            FleeFromPacMan();
        }
        else if (!pacManScript.isPoweredUp && isFleeing)
        {
            isFleeing = false;
            ChasePacMan();
        }
        else if (!pacManScript.isPoweredUp)
        {
            ChasePacMan();
        }
    }

    void FleeFromPacMan()
    {
        Vector3 directionToPacMan = transform.position - pacMan.position;
        Vector3 fleePosition = transform.position + directionToPacMan.normalized * fleeDistance;
        agent.SetDestination(fleePosition);
    }

    void ChasePacMan()
    {
        agent.SetDestination(pacMan.position);
    }
}




public class GhostMovement : MonoBehaviour
{
    public float changeDirectionTime = 2f;
    private NavMeshAgent agent;
    private float timeSinceLastChange;
    private bool isFleeing = false;

    public Transform pacMan;
    public float fleeDistance = 10f;

    private BasicPacManMovement pacManScript;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        pacMan = GameObject.FindWithTag("player").transform;
        pacManScript = pacMan.GetComponent<BasicPacManMovement>();
        timeSinceLastChange = changeDirectionTime;
    }

    void Update()
    {
        timeSinceLastChange += Time.deltaTime;

        if (timeSinceLastChange >= changeDirectionTime && !isFleeing)
        {
            SetRandomDestination();
            timeSinceLastChange = 0f;
        }

        if (pacManScript.isPoweredUp)
        {
            if (!isFleeing)
            {
                isFleeing = true;
                Debug.Log("Ghost is fleeing from PacMan!");
            }
            FleeFromPacMan();
        }
        else
        {
            if (isFleeing)
            {
                isFleeing = false;
                Debug.Log("Ghost is no longer fleeing. Resuming chase.");
            }
            ChasePacMan();
        }
    }

    void FleeFromPacMan()
    {
        Vector3 directionToPacMan = transform.position - pacMan.position;
        Vector3 fleePosition = transform.position + directionToPacMan.normalized * fleeDistance;
        agent.SetDestination(fleePosition);
    }

    void ChasePacMan()
    {
        agent.SetDestination(pacMan.position);
    }

    void SetRandomDestination()
    {
        Vector3 randomPosition = GetRandomPositionInMaze();
        agent.SetDestination(randomPosition);
    }

    Vector3 GetRandomPositionInMaze()
    {
        Vector3 randomPoint = new Vector3(
            Random.Range(-10f, 10f),
            0f,
            Random.Range(-10f, 10f)
        );

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 10f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return transform.position;
    }
}
*/