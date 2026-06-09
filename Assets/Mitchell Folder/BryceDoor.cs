using UnityEngine;

public class BryceDoor : MonoBehaviour
{
    private GameObject player;
    private GameObject bryceder;
    private Animator door;

    private bool isClosed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player  = GameObject.FindGameObjectWithTag("Player");
        bryceder = GameObject.FindGameObjectWithTag("Enemy");
        door = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float playerDistance = Vector3.Distance(player.transform.position, transform.position);
        float brycederDistance = Vector3.Distance(bryceder.transform.position, transform.position);

        // If player or bryceder are near door, open. otherwise close
        if (playerDistance < 10 || brycederDistance < 15)
        {
            if (isClosed)
            {
                isClosed = false;
                door.Play("OpenDoor");
            }
        }
        else if (!isClosed)
        {
            door.Play("CloseDoor");
            isClosed = true;
        }
    }
}
