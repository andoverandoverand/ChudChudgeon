using UnityEngine;
using UnityEngine.AI;

public class Bryceder : MonoBehaviour
{
    public float wanderRange = 15f;     // How far enemy moves
    public float stoppingDistance = 5f; // How early enemy stops
    public float detectDistance = 5f;   // How far player needs to be to escape range

    public GameObject player;       // Tracks player

    private Transform playerTransform;
    private NavMeshAgent agent;
    protected bool chasing;
    private float playerDistance;
    private LayerMask layerMask;

    void Start()
    {
        // Set variables
        playerTransform = player.GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        layerMask = LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Get how far player is
        playerDistance = Vector3.Distance(player.transform.position, transform.position);

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
            if (playerDistance > detectDistance)
            {
                chasing = false;
            }
        }

        // Enemy sight handler
        RaycastHit hit;
        Vector3 raycastOrigin = transform.position;
        raycastOrigin.y += 0.5f;
        // Shoot raycast out and if it hits player start chase
        if (Physics.Raycast(raycastOrigin, transform.TransformDirection(Vector3.forward), out hit, detectDistance, layerMask))
        {
            chasing = true;
            Debug.Log("Found player");
        }
        Debug.DrawRay(raycastOrigin, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
    }
}
