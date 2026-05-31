using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;             // How fast player moves
    public float runModifier;       // How much sprinting affects speed
    public float gravity;           // How fast the player falls
    public Camera playerCamera;     // Stores camera

    private CharacterController characterController;
    private Transform cameraTransform;
    private float axisH;
    private float axisV;

    private Vector3 velocity;       // Move speed and direction is controlled by this
    private float velY;             // Affects above's Y value

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set static variables on start
        characterController = GetComponent<CharacterController>();
        cameraTransform = playerCamera.transform;
    }

    // Update is called once per frame
    void Update()
    {   
        // Check where player is trying to go
        axisH = Input.GetAxis("Horizontal");
        axisV = Input.GetAxis("Vertical");

        // Update velocity based on where player is trying to go
        velocity = new Vector3(axisH, 0, axisV);
        velocity *= speed;

        // Controls sprinting
        if (Input.GetKey(KeyCode.LeftShift)) velocity *= runModifier;

        // Reset gravity while grounded
        if (characterController.isGrounded) velY = -1f;

        // Gravity
        velY -= gravity * Time.deltaTime;

        // Makes sure player movement is affected by camera direction
        velocity = cameraTransform.TransformDirection(velocity);

        // Overrides Y velocity added from fixing movement direction
        // Also updates velocity with gravity
        velocity.y = velY;

        // Moves character using final velocity value
        characterController.Move(velocity * Time.deltaTime);
    }
}
