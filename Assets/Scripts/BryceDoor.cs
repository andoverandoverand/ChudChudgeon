using UnityEngine;

public class BryceDoor : MonoBehaviour
{
    private GameObject player;                      // Player game object
    private PlayerController playerController;      // PlayerController script on player
    private GameObject bryceder;                    // Bryceder game object
    private Animator door;                          // This door's animator

    private bool isClosed = true;
    private bool isLocked = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set player related variables
        player  = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        // Set bryceder related variables
        bryceder = GameObject.FindGameObjectWithTag("Enemy");
        // Set door related variables
        door = GetComponentInChildren<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        // Get bryceder and player distance from this door
        float playerDistance = Vector3.Distance(player.transform.position, transform.position);
        float brycederDistance = Vector3.Distance(bryceder.transform.position, transform.position);

        // If door isn't locked and player or bryceder are nearby, be open
        if (playerDistance < 10 || brycederDistance < 15)
        {
            if (isClosed && !isLocked)
            {
                isClosed = false;
                door.Play("OpenDoor");
            }
        }
        // If door is left open and nobody is near, be closed
        else if (!isClosed)
        {
            door.Play("CloseDoor");
            isClosed = true;
        }

        // If door is locked and player is (almost) touching door with energy, unlock the door
        if (isLocked)
        {
            if (playerDistance < 3 && playerController.energies >= 1)
            {
                isLocked = false;
                isClosed = false;
                door.Play("OpenDoor");
            }
            else
            {
                door.Play("LockedIdle");
            }
        }
    }
}
