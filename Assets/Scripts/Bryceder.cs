using UnityEngine;
using UnityEngine.AI;

public class Bryceder : MonoBehaviour
{
    public float wanderRange = 15f;     // How far enemy moves
    public float stoppingDistance = 5f; // How early enemy stops
    public float detectDistance = 5f;   // How far player needs to be to escape range
    public LayerMask layerMask;         // Layer bryceder will look for to chase player

    public GameObject player;       // Tracks player

    private Transform playerTransform;
    private NavMeshAgent agent;
    protected bool chasing;
    private float playerDistance;

    void Start()
    {
        // Set variables
        playerTransform = player.GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get how far player is
        playerDistance = Vector3.Distance(player.transform.position, transform.position);

        // Enemy sight handler
        Vector3 raycastOrigin = transform.position;
        raycastOrigin.y += 0.5f;
        // Shoot raycast out and if it hits player start chase
        if (Physics.Raycast(raycastOrigin, transform.forward, out RaycastHit hit, detectDistance, layerMask, QueryTriggerInteraction.Ignore))
        {
            chasing = true;
            Debug.Log("Found player");
        }

        // Wander while not chasing
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && !chasing)
        {
            Vector3 offset;
            offset.x = Random.Range(-wanderRange, wanderRange);
            offset.y = 0f;
            offset.z = Random.Range(-wanderRange, wanderRange);

            agent.stoppingDistance = stoppingDistance;

            agent.SetDestination(transform.position + offset);
        }
        // Stop chasing if player is too far
        else if (chasing)
        {
            if (playerDistance > detectDistance * 1.5)
            {
                chasing = false;
            }
            agent.SetDestination(playerTransform.position);
            agent.stoppingDistance = 0f;
        }
    }
}
